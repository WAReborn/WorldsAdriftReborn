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
    ENetHost* client = NULL;
    
    if (ENet_Initialize() < 0) {
        Logger::Debug("[ERROR] Could not initialize ENet, no networking possible.");
    }
    else {
        // port set to 0 means its a client and not a server
        client = ENet_Create_Host(0, 1, 3, 0, 0);

        if (client == NULL) {
            Logger::Debug("[ERROR] Could not create an ENet client, no networking possible.");
        }
    }

    return new ConnectionFuture((char*)m_hostname.c_str(), 7777, parameters, client);
}
