#include "Connection.h"
#include <iostream>

bool Connection::IsConnected()
{
    return false;
}

OpList* Connection::GetOpList() {
    OpList* op_list = new OpList();

    // return an empty OpList if you dont want the game to try to load the asset and instead "stay" in the loop of waiting for the LocalPlayer to get ready
    if (this->didSendAddEntityRequest) {
        op_list->addEntityOp = new AddEntityOp();

        op_list->addEntityOp->EntityId = 1;
        op_list->addEntityOp->PrefabName = (char*)"650186469@island";
        op_list->addEntityOp->PrefabContext = (char*)"defg";

        this->didSendAddEntityRequest = true;
    }

    return op_list;
}
