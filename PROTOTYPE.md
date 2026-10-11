# My Awesome Game — DeepJam prototype

This is a playable packaging of the existing Unity 2D platformer, based on source commit `bff6e4b`. It is not the proposed Shiftshore redesign. There is no player-controlled day/sunset/night switching mechanic.

## Existing game

- Five scenes, in order: start, 2dScene, 2sScene3, Finish, gameover.
- Two playable levels, movement, jumping, diamonds, score, hazards and three lives.
- A/D or Left/Right: move. W, Space or Up: jump.
- R: reset score/lives and restart the first level.
- Enter: start or play again from a menu/end screen.
- Movement direction is selected while grounded, preserving the original controller's behaviour.

## Packaging changes

- Browser loading page with controls and asset credits.
- Alternative keys, corrected title spelling, restart and end-screen button.
- Falling below the lowest ground loses a life instead of leaving the player stuck indefinitely.
- Game over works on either level; score and lives reset for a new run.
- WAV files stay in ordinary Git, matching the actual committed audio data and avoiding the repository's inconsistent LFS tracking rule.
- Timeline is pinned to 1.8.6: the repository's 1.8.9 package calls an Editor API unavailable in Unity 2023.2.3f1.

## Validation

Three Unity Play Mode tests pass: diamond pickup increases score and removes the diamond; both level exits reach the finish scene; three falls reach game over and a new run resets lives and score. `PrototypeBuild` also checks all five scenes for missing scripts and checks the player, score UI and exits in both levels before building.

## Build

Use Unity **2023.2.3f1**, revision **21747dafc6ee**, matching `ProjectSettings/ProjectVersion.txt`. Activate your own legitimate Unity license in Unity Hub. Add the WebGL Build Support module (or Windows Build Support for Windows).

Open the project, allow package restoration/import to finish, then use the five enabled scenes in Build Settings. For an automated build, run the Unity executable with:

```sh
Unity -batchmode -quit -projectPath /absolute/path/to/my-awesome-game -buildTarget WebGL -executeMethod PrototypeBuild.WebGL -logFile /absolute/path/to/build.log
```

Other entry points: `PrototypeBuild.Mac`, `PrototypeBuild.Windows`.

Web output: `Builds/WebGL/`. Upload the entire folder, including `index.html`, `Build/` and any `StreamingAssets/`. Compression is disabled so ordinary static hosting can serve it without custom Content-Encoding headers.

## itch.io upload

1. Zip the **contents** of `Builds/WebGL`, with `index.html` at the root of the zip.
2. Create a project named My Awesome Game, choose HTML as Kind of project, and choose free/no payments.
3. Upload the zip and enable “This file will be played in the browser”. Use an embedded 1280 × 720 viewport and enable fullscreen.
4. Add the description and asset/AI disclosures below. Set Visibility to Public and save.
5. Open the published page in a signed-out/private browser. Load the game, start a level, move, jump, collect a diamond, lose a life and restart. Copy the real published URL only after testing.

## GitHub Pages upload

Publish only the contents of `Builds/WebGL` on a `gh-pages` branch, including an empty `.nojekyll` file. Set repository Settings → Pages → Deploy from a branch → `gh-pages` → `/ (root)`. Wait for the Pages deployment and verify the URL returned by GitHub. Do not publish source files as if they were a playable build.

## Credits

- Free 2D Adventure Beach Pack — Super Brutal Assets: https://superbrutalassets.itch.io/free-2d-adventure-beach-background
- Toon Characters — Kenney, CC0: https://kenney.nl/assets/toon-characters
- Simplified Platformer Pack — Kenney, CC0: https://kenney.nl/assets/simplified-platformer-pack
- Casual Game Sounds — Dustyroom, CC0: https://dustyroom.com/free-casual-game-sounds/ ; original `Assets/CasualGameSounds/license.pdf` is preserved.
- TextMesh Pro — Unity; Liberation Sans — SIL Open Font License, included under `Assets/TextMesh Pro/Fonts/`.
- Third-party assets retain their original licenses; the repository MIT license does not replace them.

## AI disclosure

According to the developer, the original gameplay was written without generative AI during Google Oyun ve Uygulama Akademisi. Codex assisted with this prototype's build and publishing preparation, browser page, alternate keys, restart handling and falling/life handling. The proposed Shiftshore concept is not implemented in this build.
