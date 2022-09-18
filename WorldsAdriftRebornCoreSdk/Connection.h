#pragma once
#include "OpList.h"

class Connection
{
private:
    bool didSendAddEntityRequest = false;
    bool didLoadPlayer = false;
    bool didCreatePlayer = false;
public:
    bool IsConnected();
    OpList* GetOpList();
    void SendAssetLoaded(AssetLoaded* asset_loaded);
};

