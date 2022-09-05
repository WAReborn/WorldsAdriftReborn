#pragma once

#include "Connection.h"

class ConnectionFuture
{
public:
    Connection* Get(unsigned int* timeout_millis);
};

