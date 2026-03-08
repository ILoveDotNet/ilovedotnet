#!/usr/bin/env python3
"""Fix CodeSnippet CssClass values that were set to language-plaintext.

Uses gist.github.com/{user}/{gist_id}.json to get the actual filename,
then derives the correct PrismJS CssClass from the extension.

Reads gist-map.json (from migrate-gists.py) to know which files and gists
to fix, and in what order snippets appear in each file.
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
GIST_JSON_URL = "https://gist.github.com/{user}/{gist_id}.json"
HEADERS = {"User-Agent": "Mozilla/5.0 (fix-css-classes/1.0)"}
RATE_LIMIT_DELAY = 0.3
DEFAULT_USER = "fingers10"

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

# Match opening CodeSnippet tag with any CssClass value
CODE_SNIPPET_OPEN_RE = re.compile(r'<CodeSnippet\s+CssClass="([^"]+)">')


def get_css_class_for_gist(gist_id, user_id=DEFAULT_USER):
    """Fetch gist .json metadata to find filename, return PrismJS CssClass."""
    url = GIST_JSON_URL.format(user=user_id, gist_id=gist_id)
    req = urllib.request.Request(url, headers=HEADERS)
    with urllib.request.urlopen(req, timeout=30, context=_SSL_CTX) as resp:
        data = json.loads(resp.read())
    files = data.get("files", [])
    if not files:
        return "language-plaintext"
    # Use the first file's extension
    ext = Path(files[0]).suffix.lower()
    return EXT_MAP.get(ext, "language-plaintext")


def fix_file(file_path, gist_ids, gist_class_cache):
    """Update CodeSnippet CssClass values in file_path using gist_ids order."""
    rel_path = str(file_path.relative_to(REPO_ROOT))
    content = file_path.read_text(encoding="utf-8")

    # Find all CodeSnippet opening tags in order
    matches = list(CODE_SNIPPET_OPEN_RE.finditer(content))

    if len(matches) < len(gist_ids):
        print(f"  ⚠  {rel_path}: {len(matches)} CodeSnippet(s) but {len(gist_ids)} gist(s) — skipping")
        return False

    if len(matches) != len(gist_ids):
        print(f"  ⚠  {rel_path}: {len(matches)} CodeSnippet(s) vs {len(gist_ids)} gist(s) — will fix first {len(gist_ids)}")

    result = content
    offset = 0  # track position shift as we make replacements
    changed = False

    for i, gist_id in enumerate(gist_ids):
        css_class = gist_class_cache.get(gist_id, "language-plaintext")
        if css_class == "language-plaintext":
            continue  # nothing to fix

        match = matches[i]
        old_tag = match.group(0)  # e.g. <CodeSnippet CssClass="language-plaintext">
        new_tag = f'<CodeSnippet CssClass="{css_class}">'

        if old_tag == new_tag:
            continue  # already correct

        start = match.start() + offset
        end = match.end() + offset
        result = result[:start] + new_tag + result[end:]
        offset += len(new_tag) - len(old_tag)
        changed = True

    if changed:
        file_path.write_text(result, encoding="utf-8")
        return True
    return False


def main():
    if not GIST_MAP_FILE.exists():
        print(f"❌ gist-map.json not found. Run migrate-gists.py first.")
        return

    gist_map = json.loads(GIST_MAP_FILE.read_text(encoding="utf-8"))

    # Collect all unique gist IDs
    all_gist_ids = set()
    for ids in gist_map.values():
        all_gist_ids.update(ids)

    print(f"Fetching CSS class for {len(all_gist_ids)} unique gist(s)...\n")

    gist_class_cache = {}
    errors = 0
    for gist_id in sorted(all_gist_ids):
        try:
            css_class = get_css_class_for_gist(gist_id)
            gist_class_cache[gist_id] = css_class
            if css_class != "language-plaintext":
                print(f"  {gist_id[:8]}... → {css_class}")
            time.sleep(RATE_LIMIT_DELAY)
        except Exception as exc:
            print(f"  ❌ {gist_id[:8]}...: {exc}")
            gist_class_cache[gist_id] = "language-plaintext"
            errors += 1

    # Summary of class distribution
    from collections import Counter
    dist = Counter(gist_class_cache.values())
    print(f"\nCSS class distribution: {dict(dist)}")

    non_plaintext = sum(v for k, v in dist.items() if k != "language-plaintext")
    print(f"Gists needing class fix: {non_plaintext}\n")

    # Fix files
    files_changed = 0
    for rel_path, gist_ids in gist_map.items():
        file_path = REPO_ROOT / rel_path
        if not file_path.exists():
            print(f"⚠  File not found: {rel_path}")
            continue
        changed = fix_file(file_path, gist_ids, gist_class_cache)
        if changed:
            files_changed += 1
            print(f"  ✅ Fixed: {rel_path}")

    print(f"\n{'=' * 60}")
    print(f"Files updated: {files_changed}")
    print(f"Fetch errors:  {errors}")

    # Also update the gist-map.json with the resolved CSS classes
    enhanced_map = {k: [{"gist_id": g, "css_class": gist_class_cache.get(g, "language-plaintext")} for g in v]
                    for k, v in gist_map.items()}
    enhanced_file = GIST_MAP_FILE.parent / "gist-class-map.json"
    enhanced_file.write_text(json.dumps(enhanced_map, indent=2), encoding="utf-8")
    print(f"Class map saved: {enhanced_file}")


if __name__ == "__main__":
    main()
