#pragma once

#include <string>
#include "Structs.h"
#include "DeploymentListFuture.h"
#include "ConnectionFuture.h"

class Locator
{
    std::string m_hostname;
public:
    Locator(std::string hostname, LocatorParameters* parameters);
    DeploymentListFuture* GetDeploymentListAsync();
    ConnectionFuture* ConnectAsync(char* deployment_name, ConnectionParameters* parameters, void* data, QueueStatusCallback callback);
};

