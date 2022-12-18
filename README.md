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
At the moment the WorldsAdriftRebornCoreSdk is dependant on protobuf, in order to keep the project portable and not require and external package managers (vcpkg) we opted to include a local nuget package.
This local nuget package was exported by vcpkg using the `vcpkg export protobuf:x64-windows --nuget` option of vcpkg ( see https://devblogs.microsoft.com/cppblog/vcpkg-introducing-export-command/ for more info).
The package can be updated by going to your locally installed vcpkg installation folder, removing any installed version of protobuf `vcpkg remove protobuf:x64-windows` and running the export command again.
This will generate a new package for you, which you then have to place in the LocalPackages folder of this repository.
Subsequently you have to go to the WorldsAdriftRebornCoreSdk Project and ManageNugetPackages to install the new version (you might want to remove the old version as well).

# Contact us
Any support is welcome! You can find us on [Discord](https://discord.gg/pSrfna7NDx)
