# WorldsAdriftReborn

# About
Worlds Adrift Reborn is a community made mod in an attempt to revive the Worlds Adrift game with a Dedicated server option.
This means anyone would be able to host his/her own server and let other people join in.

# Current state
As you might guessed this is a very eager project. The game heavily relies on proprietary code for its networking (SpatialOS) and we need to replace it with our own implementation.
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

Our implementation offers the same methods as SpatialOs does. This means the game still thinks its talking to the SpatialOs dll while it is infact calling our own methods. This will allow us to implement our own networking.

At the moment we can instruct the game to load and spawn entities this way, the next thing will be to add and update their components to get a similar result as the one you see in the last video found [here](https://www.youtube.com/watch?v=IWKu2Olw0rc)

## Build Instructions
First you will need the correct version of the game. Get a copy of [DepotDownloader](https://github.com/SteamRE/DepotDownloader) and run `DepotDownloader.exe -app 322780 -depot 322783 -manifest 4624240741051053915 -username <yourusername> -password <yourpassword>`
Which will download the correct game files. Copy the files over to the gameroot folder.

Clone the repository including submodules using `git clone --recurse-submodules <repository>`
Or (if you already cloned the repositiory normally) cd to your repository and run `git submodule update --init --recursive`

Next download the latest [BepInEx Release](https://github.com/BepInEx/BepInEx/releases) and unzip those files into gameroot as well.

Now open up the project sln with Visual Studio.
If your game installation is not at the default location (`C:\Program Files (x86)\Steam\steamapps\common\WorldsAdrift`) visual studio will report an error and a DevEnv.targets file should have been generated at the root of your copy of the WorldsAdriftReborn repo. 
You can change the path to your game installation location, save and reopen the project sln with visual studio.

After building [WorldsAdriftRebornCoreSdk](https://github.com/sp00ktober/WorldsAdriftReborn/tree/main/WorldsAdriftRebornCoreSdk) and [WorldsAdriftReborn mod](https://github.com/sp00ktober/WorldsAdriftReborn/tree/main/WorldsAdriftReborn), copy over `CoreSdkDll.dll` to `gameroot\UnityClient@Windows_Data\Plugins\CoreSdkDll.dll`. Finally make a new directory inside of `gameroot\BeplnEx\plugins` you can name this whatever you want.
Copy the compiled mod files into this directory.

## Updating protobuf
At the moment the WorldsAdriftRebornCoreSdk is dependant on protobuf, in order to keep the project portable and not require and external package managers (vcpkg) we opted to include a build and publish nuget package.
This nuget package was exported by vcpkg using the `vcpkg export protobuf:x64-windows-static-md --nuget --nuget-id=WorldsAdriftReborn-protobuf-x64-windows-static-md` option of vcpkg ( see https://devblogs.microsoft.com/cppblog/vcpkg-introducing-export-command/ for more info).
And released on nuget as https://www.nuget.org/packages/WorldsAdriftReborn-protobuf-x64-windows-static-md/ .
The package can be updated by going to your locally installed vcpkg installation folder, removing any installed version of protobuf using the `vcpkg remove protobuf:x64-windows protobuf:x64-windows-static protobuf:x64-windows-static-md` command,
reinstall them using the `vcpkg install protobuf:x64-windows protobuf:x64-windows-static protobuf:x64-windows-static-md` and subsequently running the aforementioned the export command again.
This will generate a new package for you, which you can then upload to nuget, and update through the nuget package manager.

For testing purpouses, you can also (instead of uploading the package to nuget) localy load an exported nuget package by placing the exported .nupkg in the LocalPackages folder of the repo, 
this will make it appear in the LocalPackages package source in the nuget package manager.

Asside from https://www.nuget.org/packages/WorldsAdriftReborn-protobuf-x64-windows-static-md/ we also provide the https://www.nuget.org/packages/WorldsAdriftReborn-protobuf-x64-windows-static/ and https://www.nuget.org/packages/WorldsAdriftReborn-protobuf-x64-windows/ variants.
Do not that if you choose to switch a variant (or to a local package) that has a different package name you will need update the proto.targets with the changed package path in order for autocompiling of the .proto files to work and be mindfull of the required compilation settings changes below.

You can switch linking modes by going to the WorldsAdriftRebornCoreSdk project properties and switching various settings:
- vcpkg > Use static libraries > No / C/C++ > Code Generation > Runtime Library: MDd (default): This will dynamic link everything, wich will also result in seperate protobuf DLLS in the output (works with all versions of the package, however you might want to switch to https://www.nuget.org/packages/WorldsAdriftReborn-protobuf-x64-windows/ for a leaner package)
- vcpkg > Use static libraries > Yes / vcpkg > Use Use Dynamic CRT > No / C/C++ > Code Generation > Runtime Library: MTd: This will static link everything, resulting in a single output DLL. (requires https://www.nuget.org/packages/WorldsAdriftReborn-protobuf-x64-windows-static/ )
- (Current default) vcpkg > Use static libraries > Yes / vcpkg > Use Use Dynamic CRT > Yes / C/C++ > Code Generation > Runtime Library: MDd (default): This will static link everything, resulting in a single output DLL. (requires https://www.nuget.org/packages/WorldsAdriftReborn-protobuf-x64-windows-static-md/ )

# Contact us
Any support is welcome! You can find us on [Discord](https://discord.gg/pSrfna7NDx)
