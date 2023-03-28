#pragma once

#include "OpList.h"
#include "Logger.h"
#include "enetLayer.h"
#include <iostream>
#include <fstream>
#include <string>
#include <unordered_map>

class Connection
{
private:
    char* hostname;
    unsigned short port;

    ENetHost* client = NULL;
    ENetPeer* peer = NULL;

    std::unordered_map<unsigned int, ClientComponentVtable> vtable;

public:
    Connection(char* hostname, unsigned short port, ConnectionParameters* parameters, ENetHost* client);
    ~Connection();

    bool IsConnected();
    OpList* GetOpList();
    void SendAssetLoaded(AssetLoaded* asset_loaded);

    // Internal API
    bool DeserializeComponent(unsigned int component_id, ClientObjectType objType, char* buffer, unsigned int length, ClientObject** obj);
    bool SerializeComponent(unsigned int component_id, ClientObjectType objType, ClientObject* obj, char** buffer, unsigned int* length);
    void SendComponentInterest(long entity_id, InterestOverride* interest_override, unsigned int interest_override_count);
    void SendComponentUpdate(long entityId, ComponentObject* component_update);
};

