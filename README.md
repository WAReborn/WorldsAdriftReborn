# Worlds Adrift Reborn

## About
This is a mod for Worlds Adrift that attempts to recreate the original MMO experience by replacing the official infrastructure with custom, self-hosted substitutes.

## Technical Overview
The project consists of four main components:
- A client mod that patches the game at runtime using BepInEx and Harmony
- A custom replacement for the SpatialOS SDK written in C++
- A custom HTTP server written in C# that handles player login and character creation
- A game server responsible for sending and receiving ENet packets

## Current State
This project currently demonstrates:
- An outline for an API handling login and character creation
- A functional replacement layer for SpatialOS
- Spawning entities and loading a client into the game world
- Basic handling of player state, such as clothing items and the glider

## Install Instructions

1. Obtain a supported version of the game using [DepotDownloader](https://github.com/SteamRE/DepotDownloader):

    ```bash
    DepotDownloader.exe -app 322780 -depot 322783 -manifest 4624240741051053915 -username <yourusername> -password <yourpassword>
    ```

   Copy the downloaded files into the game root directory.

   ⚠ The latest Steam version of the game is **not supported**, as a final update before shutdown removed most of the game content.****

2. Download the latest **BepInEx 5.x** release from
   [https://github.com/BepInEx/BepInEx/releases](https://github.com/BepInEx/BepInEx/releases)
   and extract it into the game root directory
   (installation details: [https://docs.bepinex.dev/articles/user_guide/installation/index.html](https://docs.bepinex.dev/articles/user_guide/installation/index.html)).

3. Create a `steam_appid.txt` file in the game root directory and fill the contents with:

   ```
   322780
   ```

4. Download the latest bleeding-edge release from the repository’s
   [Releases](https://github.com/sp00ktober/WorldsAdriftReborn/releases) page and extract it.

5. Copy the `WorldsAdriftReborn` folder into:

   ```
   <game root>\BepInEx\plugins
   ```

6. Start `WorldsAdriftRebornGameServer.exe`, then `WorldsAdriftRebornServer.exe`.

   ⚠ Temporarily replace the following DLLs in the `WorldsAdriftRebornGameServer` folder with the versions from
   `<game root>\UnityClient@Windows_Data\Managed`:

    * `Improbable.WorkerSdkCsharp.dll`
    * `Improbable.WorkerSdkCsharp.Framework.dll`
    * `Generated.Code.dll`
    * `protobuf-net.dll`

7. Launch the game from the game root directory.

## Build Instructions

### 1. Obtain the correct game version
You must use a supported version of the game.

- Download **DepotDownloader** from  
  <https://github.com/SteamRE/DepotDownloader>
- Run the following command (replace the placeholders with your Steam credentials):

```bash
DepotDownloader.exe -app 322780 -depot 322783 -manifest 4624240741051053915 -username <yourusername> -password <yourpassword>
```

* Once the download completes, copy the downloaded files into the **game root directory**.

> ⚠ **Important**
> The latest Steam version of the game is **not supported**.
> A final update before shutdown removed most of the game content, making it incompatible.

---

### 2. Clone the repository with submodules

Clone the repository including all submodules:

```bash
git clone --recurse-submodules <repository>
```

If you already cloned the repository without submodules, run:

```bash
git submodule update --init --recursive
```

---

### 3. Install BepInEx

* Download the latest **BepInEx 5.x** release from
  [https://github.com/BepInEx/BepInEx/releases](https://github.com/BepInEx/BepInEx/releases)
* Extract all files into the **game root directory**.

Detailed installation instructions are [available at docs.bepinex.dev](https://docs.bepinex.dev/articles/user_guide/installation/index.html).

---

### 4. Create `steam_appid.txt`

* In the game root directory, create a file named `steam_appid.txt`
* Add the following single line to the file:

```text
322780
```

This App ID is required to launch the game; without it, a Steam-related error will occur.

---

### 5. Open the solution

* Open the project `.sln` file using **Visual Studio 2022**

> ⚠ **Notes**
>
> * Visual Studio versions older than 2022 are **not supported** (the project requires .NET 6.0).
> * Only the `Any CPU` (default) and `x64` solution platforms are currently supported.

---

### 6. Using Rider (optional)

JetBrains Rider can also open and build the solution.

* Create an empty directory named `LocalPackages` inside the solution root before opening the project.

---

### 7. Configure non-default game paths

If your game is **not installed at**:

```
C:\Program Files (x86)\Steam\steamapps\common\WorldsAdrift
```

Your IDE should show an error and generate a `DevEnv.targets` file at the root of the repository.

* Edit this file to point to your actual game installation path
* Save the file
* Reopen the solution in Visual Studio

---

### 8. Building the mod

Building the **WorldsAdriftReborn** project will automatically:

* Build the required **WorldsAdriftRebornCoreSdk** (`CoreSdkDll.dll`)
* Copy both the Core SDK DLL and the compiled BepInEx plugin into the game's `BepInEx/plugins` directory

If the game version is incompatible, the build process will fail with an error.

Relevant projects:

* [https://github.com/sp00ktober/WorldsAdriftReborn/tree/main/WorldsAdriftReborn](https://github.com/sp00ktober/WorldsAdriftReborn/tree/main/WorldsAdriftReborn)
* [https://github.com/sp00ktober/WorldsAdriftReborn/tree/main/WorldsAdriftRebornCoreSdk](https://github.com/sp00ktober/WorldsAdriftReborn/tree/main/WorldsAdriftRebornCoreSdk)

---

### 9. Running the game locally

To run the game locally, you must first build **all projects** in the solution, then start the following components in order:

1. Start **WorldsAdriftGameServer**
   [https://github.com/sp00ktober/WorldsAdriftReborn/tree/main/WorldsAdriftGameServer](https://github.com/sp00ktober/WorldsAdriftReborn/tree/main/WorldsAdriftGameServer)
2. Start **WorldsAdriftServer**
   [https://github.com/sp00ktober/WorldsAdriftReborn/tree/main/WorldsAdriftServer](https://github.com/sp00ktober/WorldsAdriftReborn/tree/main/WorldsAdriftServer)
3. Launch the game

---

### 10. Launch configurations

The solution includes launch profiles for:

* WorldsAdriftReborn
* WorldsAdriftGameServer
* WorldsAdriftServer

> ⚠ When launching the game from Visual Studio, ensure it is started **without debugging**.

You can start all components simultaneously by configuring the solution to use **Multiple Startup Projects**.

## Protobuf Information
WorldsAdriftRebornCoreSdk is dependent on Protobuf. To avoid external package managers we opted to include a build and publish nuget package.

To export the package via vcpkg: `vcpkg export protobuf:x64-windows-static-md --nuget --nuget-id=WorldsAdriftReborn-protobuf-x64-windows-static-md` [(More Info)](https://devblogs.microsoft.com/cppblog/vcpkg-introducing-export-command/)

Nuget URL: https://www.nuget.org/packages/WorldsAdriftReborn-protobuf-x64-windows-static-md/

To update the package, reinstall protobuf via vcpkg and re-run the export command. The generated `.nupkg` can be uploaded to NuGet or placed in the repo’s `LocalPackages` folder to use it locally via the NuGet Package Manager.

The following package variants are available:

* [https://www.nuget.org/packages/WorldsAdriftReborn-protobuf-x64-windows/](https://www.nuget.org/packages/WorldsAdriftReborn-protobuf-x64-windows/)
* [https://www.nuget.org/packages/WorldsAdriftReborn-protobuf-x64-windows-static/](https://www.nuget.org/packages/WorldsAdriftReborn-protobuf-x64-windows-static/)
* [https://www.nuget.org/packages/WorldsAdriftReborn-protobuf-x64-windows-static-md/](https://www.nuget.org/packages/WorldsAdriftReborn-protobuf-x64-windows-static-md/)

⚠ Switching to a differently named variant (or a local package) requires updating `proto.targets` to reflect the new package path and adjusting the build configuration.

Linking mode is configured in the **WorldsAdriftRebornCoreSdk** project properties:

* **Dynamic linking (DLLs output)**
  `Use static libraries: No`, `Runtime Library: MDd`
  (works with all variants; prefer `https://www.nuget.org/packages/WorldsAdriftReborn-protobuf-x64-windows/`)
* **Fully static (single DLL output)**
  `Use static libraries: Yes`, `Use Dynamic CRT: No`, `Runtime Library: MTd`
  (requires `https://www.nuget.org/packages/WorldsAdriftReborn-protobuf-x64-windows-static/`)
* **Static libs + dynamic CRT (default, single DLL output)**
  `Use static libraries: Yes`, `Use Dynamic CRT: Yes`, `Runtime Library: MDd`
  (requires `https://www.nuget.org/packages/WorldsAdriftReborn-protobuf-x64-windows-static-md/`)

---

#### Contributing
See [CONTRIBUTING.md](CONTRIBUTING.md) for further details.

#### Contact us
Any support is welcome! You can find us on [Discord](https://discord.gg/pSrfna7NDx)
