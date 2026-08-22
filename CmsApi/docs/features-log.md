# Features Log

Central status tracker for every feature built through the
PRD → Spec → Implementation → Verification workflow. The `/implement-feature`
command updates the `Status` column here automatically once a feature
passes the verification checklist.

| ID  | Feature  | PRD                       | Spec                        | Status      | Branch                |
|-----|----------|----------------------------|-------------------------------|-------------|------------------------|
| 001 | Languages | docs/prd/001-languages.md   | docs/specs/001-languages.md    | Not Started | feature/001-languages   |

## Status values
- `Not Started` — PRD/Spec exist, implementation hasn't begun.
- `In Progress` — implementation started but not yet verified.
- `Implemented` — passed every item in `docs/harness/verification-checklist.md`.
- `Verified` — reviewed and merged into `main` (set manually after PR merge). 
