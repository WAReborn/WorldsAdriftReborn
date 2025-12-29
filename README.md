# WorldsAdriftReborn

# About
Worlds Adrift Reborn is a community made mod in an attempt to revive the Worlds Adrift game with a Dedicated server option.
This means anyone would be able to host his/her own server and let other people join in.

# Current state
As you might guessed this is a very ambitious project. The game heavily relies on proprietary code for its networking (SpatialOS) and we need to replace it with our own implementation.
We can't say for sure if this project will succeed but we will do our best for it.

## Technical Details
We use [BepInEx](https://github.com/BepInEx/BepInEx) and [Harmony](https://github.com/pardeike/Harmony) to patch the game at runtime, you can find the mod project [here](https://github.com/sp00ktober/WorldsAdriftReborn/tree/main/WorldsAdriftReborn)

## Main Menu
The game communicates with a HTTP REST server when you perform actions in the main menu. This is the "WorldsAdriftServer" project that you can find [here](https://github.com/sp00ktober/WorldsAdriftReborn/tree/main/WorldsAdriftServer)
So far you can get to the character creation screen and choose one of the hardcoded characters to enter the game.

## In Game
After the intro video the game usually bootstraps its SpatialOs networking. To replace it with our own implementation we made a C++ project that you can find [here](https://github.com/sp00ktober/WorldsAdriftReborn/tree/main/WorldsAdriftRebornCoreSdk).
This will compile into a .dll which you use to replace the original one.

Our implementation offers the same methods as SpatialOs does. This means the game still thinks its talking to the SpatialOs dll while it is in fact calling our own methods. This will allow us to implement our own networking.

At the moment we can instruct the game to load and spawn entities this way, the next thing will be to add and update their components to get a similar result as the one you see in the last video found [here](https://www.youtube.com/watch?v=IWKu2Olw0rc)
Got it. Here’s the **tight, numbered install list**, no extra headings, no fluff:

# Install Steps

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

Visual Studio will show an error and generate a `DevEnv.targets` file at the root of the repository.

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

## Updating protobuf
At the moment the WorldsAdriftRebornCoreSdk is dependant on protobuf, in order to keep the project portable and not require and external package managers (vcpkg) we opted to include a build and publish nuget package.

This nuget package was exported by vcpkg using the `vcpkg export protobuf:x64-windows-static-md --nuget --nuget-id=WorldsAdriftReborn-protobuf-x64-windows-static-md` option of vcpkg ( see https://devblogs.microsoft.com/cppblog/vcpkg-introducing-export-command/ for more info).
And released on nuget as https://www.nuget.org/packages/WorldsAdriftReborn-protobuf-x64-windows-static-md/ .

The package can be updated by going to your locally installed vcpkg installation folder, removing any installed version of protobuf using the `vcpkg remove protobuf:x64-windows protobuf:x64-windows-static protobuf:x64-windows-static-md` command,
reinstall them using the `vcpkg install protobuf:x64-windows protobuf:x64-windows-static protobuf:x64-windows-static-md` and subsequently running the aforementioned the export command again.
This will generate a new package for you, which you can then upload to nuget, and update through the nuget package manager.

For testing purposes, you can also (instead of uploading the package to nuget) locally load an exported nuget package by placing the exported .nupkg in the LocalPackages folder of the repo, 
this will make it appear in the LocalPackages package source in the nuget package manager.

Aside from https://www.nuget.org/packages/WorldsAdriftReborn-protobuf-x64-windows-static-md/ we also provide the https://www.nuget.org/packages/WorldsAdriftReborn-protobuf-x64-windows-static/ and https://www.nuget.org/packages/WorldsAdriftReborn-protobuf-x64-windows/ variants.  
⚠ Do note that if you choose to switch a variant (or to a local package) that has a different package name you will need update the proto.targets with the changed package path in order for auto compiling of the .proto files to work and be mindful of the required compilation settings changes below.

You can switch linking modes by going to the WorldsAdriftRebornCoreSdk project properties and switching various settings:
- vcpkg > Use static libraries > No / C/C++ > Code Generation > Runtime Library: MDd (default): This will dynamic link everything, which will also result in separate protobuf DLLS in the output (works with all versions of the package, however you might want to switch to https://www.nuget.org/packages/WorldsAdriftReborn-protobuf-x64-windows/ for a leaner package)
- vcpkg > Use static libraries > Yes / vcpkg > Use Use Dynamic CRT > No / C/C++ > Code Generation > Runtime Library: MTd: This will static link everything, resulting in a single output DLL. (requires https://www.nuget.org/packages/WorldsAdriftReborn-protobuf-x64-windows-static/ )
- (Current default) vcpkg > Use static libraries > Yes / vcpkg > Use Use Dynamic CRT > Yes / C/C++ > Code Generation > Runtime Library: MDd (default): This will static link everything, resulting in a single output DLL. (requires https://www.nuget.org/packages/WorldsAdriftReborn-protobuf-x64-windows-static-md/ )

#### Contributing
See [CONTRIBUTING.md](CONTRIBUTING.md) for further details.

#### Contact us
Any support is welcome! You can find us on [Discord](https://discord.gg/pSrfna7NDx)
