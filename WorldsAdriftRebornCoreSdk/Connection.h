#pragma once
#include "OpList.h"
#include <unordered_map>

class Connection
{
private:
    char* hostname;
    unsigned short port;

    std::unordered_map<unsigned int, ClientComponentVtable> vtable;

    bool didSendAddEntityRequest = false;
    bool didLoadPlayer = false;
    bool gameLoadedPlayer = false;
    bool didCreatePlayer = false;
    bool didSendAddComponentRequest = false;

public:
    Connection(char* hostname, unsigned short port, ConnectionParameters* parameters);

    bool IsConnected();
    OpList* GetOpList();
    void SendAssetLoaded(AssetLoaded* asset_loaded);

    // Internal API
    bool DeserializeComponent(unsigned int component_id, ClientObjectType objType, char* buffer, unsigned int length, ClientObject** obj);
    void SendComponentInterest(long entity_id, InterestOverride* interest_override, unsigned int interest_override_count);
};

