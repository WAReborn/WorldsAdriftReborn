#pragma once
#include "OpList.h"

class Connection
{
private:
    bool didSendAddEntityRequest = false;
    bool didCreatePlayer = false;
public:
    bool IsConnected();
    OpList* GetOpList();
};

