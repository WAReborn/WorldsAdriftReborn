## Installation & Launch

> ⚠ **Important:** The **current Steam version of the game is NOT supported.**  
> ⚠ **Important:** This guide is only for people who are **joining a server** and not for those hosting one. 
> You must download the **specific older build** using DepotDownloader, or the game will be missing content and will not work.

---

### 1. Prepare a Game Root Folder

Create a folder where the game will live, for example:

- `C:\Games\WAReborn\`

In the rest of this guide, this folder will be called **`gameroot`**.

---

### 2. Download DepotDownloader

1. Go to the [**DepotDownloader** releases page (GitHub)](https://github.com/SteamRE/DepotDownloader/releases).  
2. Download the latest release for your system.
3. Extract it somewhere convenient, for example:  
   `C:\Tools\DepotDownloader\`

You should now have `DepotDownloader.exe` inside that folder.

---

### 3. Download the Correct Game Version via DepotDownloader

> ⚠ **Do not** use the normal Steam install. You *must* use this specific depot/manifest.

1. Open a **Command Prompt** or **PowerShell** window.
2. Navigate to your DepotDownloader folder, for example:
  ```bat
     cd C:\Tools\DepotDownloader
  ````

3. Run DepotDownloader with the following command (replace with your Steam login):
   ```bat
   DepotDownloader.exe -app 322780 -depot 322783 -manifest 4624240741051053915 -username <yourusername> -password <yourpassword>
   ```

   * `-app 322780` – the game’s appid
   * `-depot 322783` – the depot that contains the correct game files
   * `-manifest 4624240741051053915` – the **specific build** required
   * `-username <yourusername>` – your Steam login USERNAME. **Do not** include the `<>`.
   * `-password <yourpassword>` – your Steam login PASSWORD. **Do not** include the `<>`.

4. After it finishes, copy all downloaded game files into your **`gameroot`** folder.

---

### 4. Install BepInEx 5.x

1. Download the **latest BepInEx 5.x Release** (Windows, x64) from the official releases page:

   * [https://github.com/BepInEx/BepInEx/releases](https://github.com/BepInEx/BepInEx/releases)
2. Extract the contents of the `.zip` **directly into your `gameroot` folder**.
   Your `gameroot` should now contain things like:

   * `BepInEx/`
   * `doorstop_config.ini`
   * `winhttp.dll`
   * (plus the original game files)

> For more details you can also refer to the BepInEx installation docs, but the above is the basic setup.

---

### 5. Create `steam_appid.txt`

1. In your **`gameroot`** folder, create a new text file named:

   `steam_appid.txt`

2. Open it and add this single line:

   ```text
   322780
   ```

3. Save the file.

> This prevents the “Steam required” error and allows the game to start correctly.

---

### 6. Install the Mod

1. Take the mod `.zip` or folder you downloaded.
2. Extract its contents **directly into the `BepInEx` folder** inside your `gameroot`.
3. Allow the extraction to **merge**:
   - The mod’s `config/` files with `BepInEx/config/`
   - The mod’s `plugins/` files with `BepInEx/plugins/`

---

### 7. Launching the Game

1. Go to your **`gameroot`** folder.
2. Run the game’s main executable (e.g. `UnityClient@Windows.exe`).
3. If everything is set up correctly:

   * The game should launch **without** a Steam error.
   * BepInEx should initialize on first run (you’ll see a `BepInEx\LogOutput.log` created).
