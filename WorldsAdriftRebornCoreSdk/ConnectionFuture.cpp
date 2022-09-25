#include "ConnectionFuture.h"

ConnectionFuture::ConnectionFuture(char* hostname, unsigned short port, ConnectionParameters* parameters)
{
    this->connexion = new Connection(hostname, port, parameters);
}

Connection* ConnectionFuture::Get(unsigned int* timeout_millis)
{
    return this->connexion;
}
