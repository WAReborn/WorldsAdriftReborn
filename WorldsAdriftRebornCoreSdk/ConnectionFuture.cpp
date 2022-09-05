#include "ConnectionFuture.h"

Connection* ConnectionFuture::Get(unsigned int* timeout_millis)
{
    return new Connection();
}
