## Unity Team Workflow Rules

---

# Branch Structure

- `main` → final submission only (do not work directly here)
- `dev` → main integration branch for active development
- `feature/*` → individual short-lived branches for tasks

---

# General Workflow

- Always branch from `dev`
- Always pull latest `dev` before starting work
- Work in feature branches, not directly in `dev`
- Commit small changes often (avoid huge commits)
- Merge feature branches into `dev` when the feature is functional
- Delete feature branch after successful merge (for cleanliness, optional but recommended)
- Do not modify other people's files without prior communication/approval

---

# Integration Rules (IMPORTANT)

- `dev` must ALWAYS remain in a playable state
- Merge feature branches into `dev` frequently 
- If `dev` breaks, it must be fixed immediately before continuing new work

---

# Scene Rules (Unity Critical)

- Only one person may edit `Main_Scene` at a time
- Announce "Main_Scene LOCKED" in Discord before editing
- Pull latest `dev` before making any scene changes
- Push and commit immediately after scene edits
- Announce "Main_Scene FREE" when done
- All experimentation must be done in personal test scenes (NOT Main_Scene)

---

# Testing Rules

- Each developer should use a personal test scene for system development
- Prefabs should be used for all major systems (UI, enemies, VFX, etc.)
- Integration into `Main_Scene` happens only after features are functional

---

# Communication Rules

- Announce when:
  - you lock/unlock Main_Scene
  - you are merging into `dev`
  - you are working on shared systems
- Communicate before touching shared or cross-system files

---

# Goal

Maintain a stable, always-playable `dev` branch and avoid Unity scene conflicts while enabling fast parallel development.
