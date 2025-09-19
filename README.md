<img width="1472" height="704" alt="My Awesome Game" src="https://github.com/user-attachments/assets/7f11d4cc-5b7c-4490-afa0-5e9e9a83a734" />

<h1 align="center">🎮 My Awesome Game — Unity 2D Platformer</h1>

<p align="center">
  Collect diamonds, dodge hazards, advance through levels — a classic Unity 2D platformer built for fast iteration.
</p>

<p align="center">
  <a href="https://unity.com/releases/lts"><img alt="Unity 2022.3 LTS+" src="https://img.shields.io/badge/Unity-2022.3+_LTS-black?logo=unity"></a>
  <img alt="2D Renderer" src="https://img.shields.io/badge/Render-2D_Renderer-6aa84f">
  <img alt="Input: Keyboard" src="https://img.shields.io/badge/Input-Keyboard-blue">
  <img alt="TextMeshPro" src="https://img.shields.io/badge/TextMeshPro-✔-informational">
  <a href="./LICENSE"><img alt="License: MIT" src="https://img.shields.io/badge/license-MIT-yellow"></a>
</p>

<p align="center">
  <a href="#-demo--downloads">Demo</a> •
  <a href="#-about-the-game">About</a> •
  <a href="#-key-features">Features</a> •
  <a href="#-game-mechanics">Mechanics</a> •
  <a href="#-controls">Controls</a> •
  <a href="#-technical-details">Tech</a> •
  <a href="#-project-structure">Structure</a> •
  <a href="#-testing">Tests</a> •
  <a href="#-performance-checklist-2d">Perf</a> •
  <a href="#-git-lfs--repo-hygiene">LFS</a> •
  <a href="#-architecture">Architecture</a> •
  <a href="#-screenshots--media">Media</a> •
  <a href="#-troubleshooting">Troubleshooting</a> •
  <a href="#-roadmap">Roadmap</a> •
  <a href="#-asset-credits">Credits</a> •
  <a href="#-contributing">Contributing</a> •
  <a href="#-license">License</a>
</p>

---

## 🚀 Demo & Downloads

* **Latest build (Releases):** *Add a link after your first release*
* **Itch.io page (optional):** *Add if hosted*
* **Short gameplay video:** *Embed a YouTube link*

---

## 📖 About the Game

A classic 2D platformer: move, jump, **collect diamonds**, avoid **red hazards**, progress through **multi-scene** levels. Designed to practice **movement & physics**, **collection loops**, **TMP UI**, and **audio**.

---

## ✨ Key Features

* **Platformer Core:** keyboard movement + jump
* **Score & Lives:** collect to score; 3 lives baseline
* **Level Flow:** multi-scene progression & game over loop
* **Audio Cues:** pickup/jump/game state feedback
* **Animations:** run / jump / idle blends
* **Parallax:** layered backgrounds for depth

---

## 🎯 Game Mechanics

* **Diamond Collection:** increases score, can gate level completion
* **Hazards:** colliding reduces lives; 0 lives → **Game Over**
* **Level Progression:** collect/finish → next scene
* **Game Over:** show result, allow restart

---

## 🎮 Controls

| Input                 | Action            |
| --------------------- | ----------------- |
| `A / D` or Arrow keys | Move left / right |
| `Space`               | Jump              |
| `Esc` (optional)      | Pause / Menu      |

---

## 🛠 Technical Details

**Development Environment**

* **Engine:** Unity 2D (recommended **2022.3 LTS** or newer)
* **Language:** C#
* **Platform:** PC/Mac (expandable to WebGL/Mobile)

**Main Scripts**

* `characterController.cs` — horizontal move + jump
* `PlayerPoints.cs` — scoring & diamond pickup
* `Engel.cs` — hazard collisions / life handling
* `LivesControl.cs` — lives system UI/state
* `score.cs` — global score/life tracking (consider naming `GameState/ScoreManager`)
* `NextLevel.cs` — scene transitions

> *Tip:* Decouple UI from logic using `ScoreManager` + events; consider a simple `GameEvents` static class.

---

## 📁 Project Structure

```
Assets/
├── Scripts/             # Gameplay & UI logic
├── Scenes/              # start, 2dScene, 2sScene3, Finish, gameover
├── Prefabs/             # Player, diamonds, hazards, UI
├── Textures/            # Sprites & tiles
├── Animation/           # Animator controllers, clips
├── CasualGameSounds/    # Audio
└── TextMesh Pro/        # TMP essentials
```

---

## 🧪 Testing

Use **Unity Test Runner** (EditMode/PlayMode) with **NUnit**.

```
Assets/Tests/
├─ EditMode/
│  └─ ScoreTests.cs
└─ PlayMode/
   └─ MovementSmokeTests.cs
```

*Sample NUnit (EditMode):*

```csharp
using NUnit.Framework;

public class MathSmokeTest {
  [Test] public void Addition_Works() => Assert.AreEqual(4, 2 + 2);
}
```

---

## ⚡ Performance Checklist (2D)

* **Sprite Atlas** to reduce draw calls
* **Import Settings:** correct PPU, compression, filter mode
* **Parallax:** limit full-screen alpha layers
* **Physics2D:** sane collision matrix; avoid per-frame allocations
* **Pooling:** for repeated pickups/effects
* **Camera:** stable orthographic size; enable pixel-perfect if needed
* **Audio:** compress & stream long clips; avoid `PlayOneShot` spam

---

## 📦 Git LFS & Repo Hygiene

* Install once: `git lfs install`
* Track heavy assets:

  ```
  git lfs track "*.png" "*.psd" "*.jpg" "*.wav" "*.mp3" "*.mp4" "*.ttf" "*.prefab" "*.anim" "*.unity"
  ```
* Commit `.gitattributes`
* Essential `.gitignore`:

  ```
  [Ll]ibrary/
  [Tt]emp/
  [Oo]bj/
  [Bb]uild*/ 
  [Ll]ogs/
  *.csproj
  *.sln
  *.user
  ```

---

## 🧭 Architecture

```mermaid
flowchart LR
  PC[characterController] --> COL[Diamond Collectibles]
  COL --> SM[ScoreManager / score.cs]
  HZ[Engel (Hazards)] --> LM[LivesControl]
  SM --> HUD[TMP HUD]
  LM --> HUD
  SF[SceneFlow] --> NL[NextLevel]
  SF --> GO[GameOver]
```

---

## 🖼 Screenshots & Media

* Add GIFs: pickup loop, hazard hit, level finish
* Place under `/Docs/media/` and embed here

---

## 🧰 Troubleshooting

1. **Pink sprites?** Reimport textures; ensure 2D Renderer is configured
2. **Floaty jump?** Tune gravity scale & jump impulse
3. **Parallax jitter?** Move layers in `LateUpdate` tied to camera
4. **Build too big?** Compress textures/audio; remove unused assets
5. **Input dead?** Verify Project Settings → Input / New Input System not required for this

---

## 🗺 Roadmap

* [ ] Checkpoints & hazards variety
* [ ] Power-ups (double jump, dash)
* [ ] Endless mode + score persistence
* [ ] Mobile controls (touch joystick / swipe)
* [ ] Localization (en/tr) with simple string table

---

## 🎨 Asset Credits

* **2D Adventure Beach Pack** — sprites/backgrounds
* **Casual Game Sounds** — SFX
* **TextMesh Pro** — UI text

> Ensure licenses allow redistribution in this repo/build.

---

## 🤝 Contributing

PRs welcome!

1. Fork → feature branch
2. Keep changes small, add a short demo GIF
3. Use clear commit messages (Conventional Commits preferred)
4. Open a PR with a brief description

---

## 📝 License

This project is licensed under the **MIT License**. See the [`LICENSE`](./LICENSE) file.

---

### Metadata

**Developer:** Yağmur Cem Gül
**Project Date:** 2025
**Unity Version:** 2022.3 LTS+ (works on 2021.3+)

