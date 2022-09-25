#pragma once

#include "Connection.h"

class ConnectionFuture
{
private:
    Connection* connexion;

public:
    ConnectionFuture(char* hostname, unsigned short port, ConnectionParameters* parameters);

    Connection* Get(unsigned int* timeout_millis);
};

