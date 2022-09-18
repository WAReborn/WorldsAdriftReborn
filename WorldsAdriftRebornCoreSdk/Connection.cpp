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

        op_list->assetLoadRequestOp->AssetType = (char*)"OnlyForResponse?";
        op_list->assetLoadRequestOp->Name = (char*)"Traveller";
        op_list->assetLoadRequestOp->Context = (char*)"Player";

        this->didLoadPlayer = true;
    }
    else if (!this->didCreatePlayer) {
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
    std::cout << "ASSET LOADED" << std::endl; // will most likely not be visible
    std::ofstream output;
    output.open("CoreSdk_OutputLog.txt", std::ios::app);
    output << "ASSET LOADED" << std::endl;
    output.close();
}
