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

At the moment we can instruct the game to load and spawn entities this way, the next thing will be to add and update their components to get a similar result as the one you see in the last video found [here](https://www.youtube.com/watch?v=9qpqRZ9W9GE)

## Build Instructions
First you will need the correct version of the game. Get a copy of [DepotDownloader](https://github.com/SteamRE/DepotDownloader) and run `DepotDownloader.exe -app 322780 -depot 322783 -manifest 4624240741051053915 -username <yourusername> -password <yourpassword>`
Which will download the correct game files. Copy the files over to the gameroot folder.

Next download the latest [BepInEx Release](https://github.com/BepInEx/BepInEx/releases) and unzip those files into gameroot as well.

Now open up the project sln with Visual Studio and add the [BepInEx](https://github.com/BepInEx/BepInEx source to the nuget package manager.
To do this goto Tools -> NuGet Package Manager -> Package Manager Settings -> Package Sources and add this url `https://nuget.bepinex.dev/v3/index.json`

Next you will need to point Visual Studio at the game Dlls. The Dlls that need configuring will have a yellow warning sign next to them and are found under WorldsAdriftReborn -> Refrences
To add a refrence right click on Refrences and then Add refrence. Find the Browse button and navigate to `gameroot\UnityClient@Windows_Data\Managed` here you will find the Dlls that Visual Studio is complaining about.

After building  [WorldsAdriftRebornCoreSdk](https://github.com/sp00ktober/WorldsAdriftReborn/tree/main/WorldsAdriftRebornCoreSdk) and [WorldsAdriftReborn mod](https://github.com/sp00ktober/WorldsAdriftReborn/tree/main/WorldsAdriftReborn), copy over `CoreSdkDll.dll` to `gameroot\UnityClient@Windows_Data\Plugins\CoreSdkDll.dll`. Next make a new directory inside of `gameroot\BeplnEx\plugins` you can name this whatever you want.
Copy the compiled mod files into this directory.

# Contact us
Any support is welcome! You can find us on [Discord](https://discord.gg/pSrfna7NDx)
