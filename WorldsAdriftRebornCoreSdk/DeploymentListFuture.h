#pragma once

#include "Callbacks.h"

class DeploymentListFuture
{
public:
    void Get(unsigned int* timeout_millis, void* data, DeploymentListCallback callback);
};

