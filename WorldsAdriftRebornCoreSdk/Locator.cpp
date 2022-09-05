#include "Locator.h"

Locator::Locator(std::string hostname, LocatorParameters* parameters)
{
	m_hostname = hostname;
}

DeploymentListFuture* Locator::GetDeploymentListAsync()
{
    return new DeploymentListFuture();
}

ConnectionFuture* Locator::ConnectAsync(char* deployment_name, ConnectionParameters* parameters, void* data, QueueStatusCallback callback)
{
    return new ConnectionFuture();
}
