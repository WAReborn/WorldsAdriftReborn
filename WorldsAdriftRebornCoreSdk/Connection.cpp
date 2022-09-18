#include "Connection.h"
#include <iostream>

bool Connection::IsConnected()
{
    return false;
}

OpList* Connection::GetOpList() {
    OpList* op_list = new OpList();

    if (!this->didCreatePlayer) {
        op_list->assetLoadRequestOp = new AssetLoadRequestOp();

        op_list->assetLoadRequestOp->AssetType = (char*)"";
    }

    // spawns the specified island at 0,0,0
    if (!this->didSendAddEntityRequest) {
        op_list->addEntityOp = new AddEntityOp();

        op_list->addEntityOp->EntityId = 1;
        op_list->addEntityOp->PrefabName = (char*)"1044497584@Island";
        op_list->addEntityOp->PrefabContext = (char*)"defg";

        this->didSendAddEntityRequest = true;
    }

    return op_list;
}
