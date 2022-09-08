#pragma once
#include "OpList.h"

class Connection
{
private:
    bool didSendAddEntityRequest = false;
public:
    bool IsConnected();
    OpList* GetOpList();
};

