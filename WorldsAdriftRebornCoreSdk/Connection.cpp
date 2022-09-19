#include "Connection.h"
#include <iostream>
#include <fstream>

bool Connection::IsConnected()
{
    return false;
}

OpList* Connection::GetOpList() {
    OpList* op_list = new OpList();

    if (!this->didLoadPlayer) {
        op_list->assetLoadRequestOp = new AssetLoadRequestOp();

        // prefab name gets assembled by game to "Traveller@Player_unityclient"
        op_list->assetLoadRequestOp->AssetType = (char*)"OnlyForResponse?";
        op_list->assetLoadRequestOp->Name = (char*)"Traveller";
        op_list->assetLoadRequestOp->Context = (char*)"Player";

        this->didLoadPlayer = true;
    }
    else if (!this->didCreatePlayer && this->gameLoadedPlayer) {
        op_list->addEntityOp = new AddEntityOp();

        op_list->addEntityOp->EntityId = 1;
        op_list->addEntityOp->PrefabName = (char*)"Traveller";
        op_list->addEntityOp->PrefabContext = (char*)"Player";

        this->didCreatePlayer = true;
    }
    else if (!this->didSendAddEntityRequest) {
        op_list->addEntityOp = new AddEntityOp();

        op_list->addEntityOp->EntityId = 2;
        op_list->addEntityOp->PrefabName = (char*)"1044497584@Island";
        op_list->addEntityOp->PrefabContext = (char*)"defg";

        this->didSendAddEntityRequest = true;
    }

    return op_list;
}

void Connection::SendAssetLoaded(AssetLoaded* asset_loaded) {
    std::cout << "ASSET LOADED: " << asset_loaded->Name << " " << asset_loaded->Context << std::endl; // will most likely not be visible
    std::ofstream output;
    output.open("CoreSdk_OutputLog.txt", std::ios::app);
    output << "ASSET LOADED: " << asset_loaded->Name << " " << asset_loaded->Context << std::endl;
    output.close();

    if (strcmp(asset_loaded->Name, "Traveller") == 0 && strcmp(asset_loaded->Context, "Player") == 0) {
        this->gameLoadedPlayer = true;
    }
}
