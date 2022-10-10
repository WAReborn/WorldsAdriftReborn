#include "ConnectionFuture.h"

ConnectionFuture::ConnectionFuture(char* hostname, unsigned short port, ConnectionParameters* parameters, ENetHost* client)
{
    this->connection = new Connection(hostname, port, parameters, client);
}

Connection* ConnectionFuture::Get(unsigned int* timeout_millis)
{
    return this->connection;
}
