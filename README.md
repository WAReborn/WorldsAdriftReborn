# WorldsAdriftReborn

# About
Worlds Adrift Reborn is a community made mod in an attempt to revive the Worlds Adrift game with a Dedicated server option.
This means anyone would be able to host his/her own server and let other people join in.

# Current state
As you might guessed this is a very ambitious project. The game heavily relies on proprietary code for its networking (SpatialOS) and we need to replace it with our own implementation.
We can't say for sure if this project will succeed but we will do our best for it.



## Boot the game
The Game cannot be purchased anymore so we patched out the need to have steam running (for now) as well as a few other checks made when the game starts.
This way we can reach the main menu.

We use [BepInEx](https://github.com/BepInEx/BepInEx) and [Harmony](https://github.com/pardeike/Harmony) to patch the game at runtime, you can find the mod project [here](https://github.com/sp00ktober/WorldsAdriftReborn/tree/main/WorldsAdriftReborn)

## Main Menu
The game communicates with a HTTP REST server when you perform actions in the main menu. This is the "WorldsAdriftServer" project that you can find [here](https://github.com/sp00ktober/WorldsAdriftReborn/tree/main/WorldsAdriftServer)
So far you can get to the character creation screen and choose one of the hardcoded characters to enter the game.

## In Game
After the intro video the game usually bootstraps its SpatialOs networking. To replace it with our own implementation we made a C++ project that you can find [here](https://github.com/sp00ktober/WorldsAdriftReborn/tree/main/WorldsAdriftRebornCoreSdk).
This will compile into a .dll which you use to replace the original one.

Our implementation offers the same methods as SpatialOs does. This means the game still thinks its talking to the SpatialOs dll while it is in fact calling our own methods. This will allow us to implement our own networking.

At the moment we can instruct the game to load and spawn entities this way, the next thing will be to add and update their components to get a similar result as the one you see in the last video found [here](https://www.youtube.com/watch?v=IWKu2Olw0rc)

## Installation and launching the game using the precompiled binaries
First you will need the correct version of the game. Get a copy of [DepotDownloader](https://github.com/SteamRE/DepotDownloader) and run `DepotDownloader.exe -app 322780 -depot 322783 -manifest 4624240741051053915 -username <yourusername> -password <yourpassword>`
Which will download the correct game files. Copy the files over to the gameroot folder.  
⚠ Note that the most up to date steam version of the game is **Not** supported! 
This is due to the game having been stripped of most of its contents in and update just before the game's shutdown.

Next download the latest 5.x [BepInEx Release](https://github.com/BepInEx/BepInEx/releases) and unzip those files into gameroot (detailed installation instructions can be found [here](https://docs.bepinex.dev/articles/user_guide/installation/index.html)).

Also create a `steam_appid.txt` file in the gameroot which contains a single line `322780` (this is the appid and is required to start the game, else you get a steam required error).

Next download the latest bleeding-edge release, you can find this on the [releases](https://github.com/sp00ktober/WorldsAdriftReborn/releases) page of the repo.
Download the WorldsAdriftReborn-Release.zip and extract its content to a folder of your choosing.

Inside the folder you extracted the zip into you will find 3 folders:
- WorldsAdriftReborn
- WorldsAdriftRebornGameServer
- WorldsAdriftRebornServer

Copy or move the WorldsAdriftReborn directory into you `<game root>\BepInEx\plugins` folder.

To launch the game follow the following steps:
Go into the WorldsAdriftRebornGameServer folder and launch the `WorldsAdriftRebornGameServer.exe`
Go into the WorldsAdriftRebornServer folder and launch the `WorldsAdriftRebornServer.exe`
⚠ Temporarily you will also need to replace the `Improbable.WorkerSdkCsharp.dll`, `Improbable.WorkerSdkCsharp.Framework.dll`, `Generated.Code.dll` and `protobuf-net.dll` in the WorldsAdriftRebornGameServer folder with the ones found in the `<game root>\UnityClient@Windows_Data\Managed` folder!
This will be fixed in future releases.
Launch the game from the gameroot

## Build Instructions
First you will need the correct version of the game. Get a copy of [DepotDownloader](https://github.com/SteamRE/DepotDownloader) and run `DepotDownloader.exe -app 322780 -depot 322783 -manifest 4624240741051053915 -username <yourusername> -password <yourpassword>`
Which will download the correct game files. Copy the files over to the gameroot folder.  
⚠ Note that the most up to date steam version of the game is **Not** supported! 
This is due to the game having been stripped of most of its contents in and update just before the game's shutdown.

Clone the repository including submodules using `git clone --recurse-submodules <repository>`
or (if you already cloned the repository normally) cd to your repository and run `git submodule update --init --recursive`

Next download the latest 5.x [BepInEx Release](https://github.com/BepInEx/BepInEx/releases) and unzip those files into gameroot (detailed installation instructions can be found [here](https://docs.bepinex.dev/articles/user_guide/installation/index.html)).

Also create a `steam_appid.txt` file in the gameroot which contains a single line `322780` (this is the appid and is required to start the game, else you get a steam required error).

Now open up the project sln with Visual Studio 2022 (⚠ Lower versions of Visual Studio are not supported due to this project requiring dotnet 6.0).  
⚠ Also note that at this moment ony the `Any CPU` (default) and `x64` solution platforms are supported.

Rider (JetBrains C# IDE) can open and build the solution as well. You just need to create an empty `LocalPackages` subdirectory inside the solution folder.

If your game installation is not at the default location (`C:\Program Files (x86)\Steam\steamapps\common\WorldsAdrift`) visual studio will report an error and a DevEnv.targets file should have been generated at the root of your copy of the WorldsAdriftReborn repo. 
You can change the path to your game installation location, save and reopen the project sln with visual studio.

Building the [WorldsAdriftReborn](https://github.com/sp00ktober/WorldsAdriftReborn/tree/main/WorldsAdriftReborn) mod will automatically build the required [WorldsAdriftRebornCoreSdk](https://github.com/sp00ktober/WorldsAdriftReborn/tree/main/WorldsAdriftRebornCoreSdk) CoreSdkDll.dll and copies this and the built BepInEx WorldsAdriftReborn plugin to the BepInEx plugins directory of your game. 
It will also give an error if you try to build WorldsAdriftReborn for an an incompatible version of the game.

Running the game locally requires you to build all projects in the solution, and subsequently starting the required servers and game:
- Start the [WorldsAdriftGameServer](https://github.com/sp00ktober/WorldsAdriftReborn/tree/main/WorldsAdriftGameServer) 
- Start the [WorldsAdriftServer](https://github.com/sp00ktober/WorldsAdriftReborn/tree/main/WorldsAdriftServer).
- And then start the game.

The projects also includes launch configurations for the WorldsAdriftReborn, WorldsAdriftGameServer and WorldsAdriftServer the projects. 
The launch configuration for WorldsAdriftReborn will launch the game itself (⚠ when launching worlds adrift through visual studio you have to make sure you launch the game without debugging).
You can launch everything at once by configuring the solution for Multiple Startup projects.

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

# Contact us
Any support is welcome! You can find us on [Discord](https://discord.gg/pSrfna7NDx)
