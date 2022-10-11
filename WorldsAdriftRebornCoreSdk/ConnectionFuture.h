#pragma once

#include "Connection.h"
#include "enetLayer.h"

class ConnectionFuture
{
private:
    Connection* connection;

public:
    ConnectionFuture(char* hostname, unsigned short port, ConnectionParameters* parameters, ENetHost* client);

    Connection* Get(unsigned int* timeout_millis);
};

