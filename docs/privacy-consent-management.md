# Privacy and consent management follow-up

Status: proposed follow-up, not implemented.

## Interim scope

The privacy page is an informational document. Its former consent status and
opt-in/opt-out button were static and had no working handler. They have been
removed, along with unverified statements that this page stores consent and that
opting out disables third-party cookies.

This change does not block advertising or analytics, implement consent, or
establish legal compliance. Do not remove a working provider-managed consent or
withdrawal control merely because the custom control was removed. Where consent
or withdrawal mechanisms are required, hiding a custom button is not a substitute
for them; verify the existing provider setup before deferring further work.

External Google Analytics and advertising preference links remain in the policy.
They are provider controls, not a site-wide opt-out implemented by this app.

## Existing surfaces to investigate

- [Privacy page](../CommonComponents/Pages/Privacy.razor): disclosures and future
  entry point for changing preferences.
- [Web HTML entry point](../Web/wwwroot/index.html): Google Funding Choices
  scripts, AdSense script, third-party preconnections, and Microsoft Clarity.
  Funding Choices is already present; verify its enabled message types,
  geographic targeting, consent/withdrawal UI, and account configuration rather
  than assuming there is no CMP or adding a second one.
- [Web startup](../Web/Program.cs) and
  [MAUI startup](../MAUI/MauiProgram.cs): analytics and advertising registrations.
  Registration alone does not prove when a browser request is made.
- [Ad component](../SharedComponents/GoogleAdSense.razor) and
  [ad rendering logic](../SharedComponents/GoogleAdSense.razor.cs): production
  slot rendering and ad requests. Also check provider-managed Auto ads.
- [Article layout](../BaseComponents/Content.razor) and
  [recommendations](../BaseComponents/ContentRecommendation.razor): shared slots.
- [MAUI HTML entry point](../MAUI/wwwroot/index.html): assess native WebView
  requirements separately from web requirements.

Inventory other scripts, embeds, cookies, storage, recipients, purposes, and
retention before deciding what must be gated. Browser network/storage evidence
and provider account settings are needed; repository inspection alone cannot
verify deployed behavior.

## Proposed behavior

Choose the geographic/legal scope with the policy owner and appropriate advice.
Use one consent source of truth, integrating the existing Google setup where
suitable. Do not build a competing local-storage boolean and assume it overrides
Google's consent state.

| Choice | Proposed site behavior |
|---|---|
| No decision yet | Do not start non-essential processing that requires consent. |
| Accept all | Enable disclosed optional purposes supported by the CMP and provider settings. |
| Reject non-essential | Proposed conservative product choice: no optional ads, analytics, or tracking embeds; core content remains usable. |
| Manage preferences | Apply the choices per purpose, not as one ambiguous opt-out flag. |
| Withdraw or change choice | Reopen preferences, update provider signals, and stop future disallowed requests and events. |

"Reject personalized ads" is not synonymous with "reject all ads."
Non-personalized ads can still use cookies or local storage and may need consent.
If a limited/non-personalized advertising path is considered, verify the specific
Google product, CMP configuration, and applicable requirements before enabling it.
The proposed no-ads-on-rejection behavior above is a product choice, not a claim
that every legal regime universally requires all ads to disappear.

## Implementation work

1. Audit the existing CMP and live network behavior, including initial page load
   before Blazor starts and client-side navigation.
2. Confirm the CMP and its published configuration meet the requirements for
   the regions and products served. Google requires a certified TCF-integrated
   CMP for personalized ads in the EEA, UK, and Switzerland.
3. Provide accessible Accept all, Reject non-essential, and Manage preferences
   actions, plus a persistent way to reopen preferences from the privacy page
   and site navigation. Use provider-supported controls where possible.
4. Apply consent before optional script initialization, ad requests, analytics
   events, and tracking embeds. Hiding an iframe or ad with CSS does not prevent
   requests. Cover static HTML, Auto ads, shared slots, and delayed SPA renders.
5. Persist and restore choices through the CMP, with the required records,
   purpose/vendor scope, policy version, expiry, and re-consent behavior.
   Do not treat merely visiting the site as affirmative consent.
6. Handle withdrawal with documented provider APIs; already transmitted data
   cannot be recalled by hiding content. Identify any necessary controlled
   reload and eligible first-party storage cleanup without promising to erase
   inaccessible third-party cookies.
7. Keep prerendered and interactive layouts stable. Decide how rejected ad
   placements are laid out without avoidable CLS or permanent empty gaps.
8. Review MAUI/WebView support and applicable SDK/provider requirements
   separately; do not assume the web CMP automatically covers native clients.
9. Update policy wording, dates, and provider links to match verified behavior.
   Review existing claims about data collection, implied consent, transfers,
   analytics, advertising, retention, and children's privacy with the owner.

## Verification and acceptance criteria

- Test fresh visitors, accept, reject, granular choices, reloads, SPA navigation,
  direct article entry, expiry, withdrawal, and unavailable CMP scripts.
- Verify actual network requests, storage, and event delivery, not only visible
  banners or ad placeholders. Use controlled test environments; do not click
  live ads.
- Cover both manual ad slots and Auto ads, analytics, Clarity, and embeds found
  during the inventory.
- Verify that rejection does not block core reading/navigation or silently
  become acceptance if the CMP fails. Define any legally permitted limited
  behavior explicitly rather than using a success-shaped fallback.
- Test region-specific configuration, keyboard/screen-reader access,
  320px mobile layout, light/dark mode, and layout shifts.
- Assess production behavior separately: development ad placeholders do not
  prove consent enforcement.

## Official references

Recheck these policies when implementing; this document is not legal advice or
a compliance certification.

- [Google publisher CMP requirements](https://support.google.com/adsense/answer/13554020?hl=en)
- [Google EU User Consent Policy](https://www.google.com/about/company/user-consent-policy.html)
- [Google European regulations and consent guidance](https://support.google.com/adsense/answer/11546682?hl=en)
