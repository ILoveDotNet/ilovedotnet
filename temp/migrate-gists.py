#!/usr/bin/env python3
"""Migrate GithubGistSnippet tags to inline CodeSnippet blocks.

Fetches raw content from gist CDN (no GitHub API).
Encodes content for Razor: & -> &amp;, < -> &lt;, > -> &gt;, @ -> @@
Language detected from redirect URL filename extension.
"""

import json
import re
import ssl
import time
import urllib.request
from pathlib import Path

# ── SSL context ───────────────────────────────────────────────────────────────
try:
    import certifi
    _SSL_CTX = ssl.create_default_context(cafile=certifi.where())
except ImportError:
    _SSL_CTX = ssl.create_default_context()

# ── Paths ─────────────────────────────────────────────────────────────────────
REPO_ROOT = Path(__file__).resolve().parent.parent
GIST_MAP_FILE = Path(__file__).resolve().parent / "gist-map.json"

# ── Constants ─────────────────────────────────────────────────────────────────
RAW_URL_TEMPLATE = "https://gist.githubusercontent.com/{user}/{gist_id}/raw/"
HEADERS = {"User-Agent": "Mozilla/5.0 (migration-script/1.0)"}
RATE_LIMIT_DELAY = 0.3  # seconds between fetches

# ── Extension → PrismJS CssClass ─────────────────────────────────────────────
EXT_MAP = {
    ".cs": "language-csharp",
    ".razor": "language-razor",
    ".js": "language-javascript",
    ".ts": "language-javascript",
    ".json": "language-json",
    ".html": "language-markup",
    ".htm": "language-markup",
    ".xml": "language-xml",
    ".csproj": "language-xml",
    ".props": "language-xml",
    ".targets": "language-xml",
    ".sh": "language-bash",
    ".bash": "language-bash",
    ".zsh": "language-bash",
    ".bat": "language-bash",
    ".cmd": "language-bash",
    ".ps1": "language-powershell",
    ".yml": "language-yaml",
    ".yaml": "language-yaml",
    ".sql": "language-sql",
    ".css": "language-css",
    ".py": "language-python",
    ".md": "language-markdown",
    ".markdown": "language-markdown",
    ".diff": "language-diff",
    ".patch": "language-diff",
    ".c": "language-c",
    ".cpp": "language-c",
    ".h": "language-c",
}

# Match <GithubGistSnippet ...></GithubGistSnippet> (single- or multi-line attributes)
# [^>]* matches any char except >, including newlines in character classes
GIST_TAG_RE = re.compile(
    r"<GithubGistSnippet\s[^>]*>\s*</GithubGistSnippet>",
)


# ── Core functions ────────────────────────────────────────────────────────────

def extract_gist_attrs(tag_text):
    """Extract (user_id, gist_id) from a GithubGistSnippet tag string."""
    user_match = re.search(r'UserId="([^"]+)"', tag_text)
    file_match = re.search(r'FileName="([a-f0-9]{32})"', tag_text)
    user_id = user_match.group(1) if user_match else "fingers10"
    gist_id = file_match.group(1) if file_match else None
    return user_id, gist_id


def fetch_raw(user_id, gist_id):
    """Fetch raw gist content. Returns (content_str, final_redirect_url)."""
    url = RAW_URL_TEMPLATE.format(user=user_id, gist_id=gist_id)
    req = urllib.request.Request(url, headers=HEADERS)
    with urllib.request.urlopen(req, timeout=30, context=_SSL_CTX) as resp:
        content = resp.read().decode("utf-8", errors="replace")
        final_url = resp.url
    return content, final_url


def css_class_from_url(final_url):
    """Determine PrismJS CssClass from redirect URL filename extension."""
    filename = final_url.rstrip("/").rsplit("/", 1)[-1].split("?")[0]
    ext = Path(filename).suffix.lower()
    return EXT_MAP.get(ext, "language-plaintext")


def encode_for_razor(text):
    """HTML-encode content for use inside a Razor CodeSnippet.

    Order is CRITICAL: & must come first to avoid double-encoding.
    """
    text = text.replace("&", "&amp;")
    text = text.replace("<", "&lt;")
    text = text.replace(">", "&gt;")
    text = text.replace("@", "@@")
    return text


def get_line_indent(match_start, file_content):
    """Return the leading whitespace of the line containing match_start."""
    line_start = file_content.rfind("\n", 0, match_start) + 1
    indent = []
    for ch in file_content[line_start:]:
        if ch in (" ", "\t"):
            indent.append(ch)
        else:
            break
    return "".join(indent)


def migrate_file(file_path, gist_map):
    """Replace all GithubGistSnippet tags in file_path with CodeSnippet blocks.

    Returns True if all tags were migrated successfully.
    Updates gist_map[rel_path] = [gist_id, ...] (in document order).
    """
    rel_path = str(file_path.relative_to(REPO_ROOT))
    content = file_path.read_text(encoding="utf-8")
    matches = list(GIST_TAG_RE.finditer(content))

    if not matches:
        print(f"  ⚠  No GithubGistSnippet tags found in {file_path.name}")
        return True

    print(f"\n📄 {rel_path} ({len(matches)} gist(s))")

    # Process matches in REVERSE order to keep earlier offsets valid
    result = content
    ordered_gist_ids = []
    had_error = False

    for match in reversed(matches):
        original = match.group(0)
        user_id, gist_id = extract_gist_attrs(original)

        if not gist_id:
            print(f"  ❌ Could not extract gist ID from: {original[:80]!r}")
            had_error = True
            continue

        indent = get_line_indent(match.start(), result)

        try:
            raw_content, final_url = fetch_raw(user_id, gist_id)
            time.sleep(RATE_LIMIT_DELAY)

            css_class = css_class_from_url(final_url)
            encoded = encode_for_razor(raw_content)

            # Strip exactly one trailing newline so closing tag is on its own line
            if encoded.endswith("\n"):
                encoded = encoded[:-1]

            replacement = (
                f'<CodeSnippet CssClass="{css_class}">\n'
                f"{encoded}\n"
                f"{indent}</CodeSnippet>"
            )

            result = result[: match.start()] + replacement + result[match.end():]
            ordered_gist_ids.insert(0, gist_id)  # reversed loop → prepend
            print(f"  ✅ {gist_id[:8]}... → {css_class} ({len(raw_content):,} bytes)")

        except Exception as exc:
            print(f"  ❌ FETCH ERROR {gist_id}: {exc}")
            had_error = True

    if not had_error:
        file_path.write_text(result, encoding="utf-8")
        gist_map[rel_path] = ordered_gist_ids
        print(f"  💾 Saved.")
    else:
        print(f"  ⚠  Skipping write due to fetch error(s) in {file_path.name}")

    return not had_error


def main():
    gist_map = {}
    total_ok = 0
    total_fail = 0

    # Discover all razor files containing GithubGistSnippet (skip build dirs)
    skip_dirs = {"bin", "obj", "node_modules", ".git"}
    target_files = []
    for f in sorted(REPO_ROOT.rglob("*.razor")):
        if any(part in skip_dirs for part in f.parts):
            continue
        try:
            if "GithubGistSnippet" in f.read_text(encoding="utf-8", errors="replace"):
                target_files.append(f)
        except OSError:
            pass

    print(f"Found {len(target_files)} file(s) with GithubGistSnippet tags.\n")

    for file_path in target_files:
        ok = migrate_file(file_path, gist_map)
        if ok:
            total_ok += 1
        else:
            total_fail += 1

    GIST_MAP_FILE.write_text(json.dumps(gist_map, indent=2), encoding="utf-8")

    print(f"\n{'=' * 60}")
    print(f"Migration complete: {total_ok} ✅ | {total_fail} ❌")
    print(f"Gist map saved to:  {GIST_MAP_FILE}")
    total_files = total_ok + total_fail
    total_gists = sum(len(v) for v in gist_map.values())
    print(f"Files processed:    {total_files}")
    print(f"Gists migrated:     {total_gists}")


if __name__ == "__main__":
    main()
