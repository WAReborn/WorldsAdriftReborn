#include "Connection.h"
#include "Logger.h"
#include <iostream>
#include <fstream>
#include <string>

Connection::Connection(char* hostname, unsigned short port, ConnectionParameters* parameters) {
    this->hostname = hostname;
    this->port = port;

    for (unsigned int i = 0; i < parameters->ClientComponentVtableCount; ++i) {
        this->vtable[parameters->ClientComponentVtable[i].ComponentId] = parameters->ClientComponentVtable[i];
        Logger::Debug("Component registered: " + std::to_string(parameters->ClientComponentVtable[i].ComponentId));
    }
}

bool Connection::IsConnected()
{
    return false;
}

OpList* Connection::GetOpList() {
    OpList* op_list = new OpList();

    if (!this->didLoadPlayer) {
        op_list->assetLoadRequestOp = new AssetLoadRequestOp();

        // prefab name gets assembled by game to "Traveller@Player_unityclient"
        op_list->assetLoadRequestOp->AssetType = (char*)"OnlyForResponse?";
        op_list->assetLoadRequestOp->Name = (char*)"Traveller";
        op_list->assetLoadRequestOp->Context = (char*)"Player";

        this->didLoadPlayer = true;
    }
    else if (!this->didCreatePlayer && this->gameLoadedPlayer) {
        op_list->addEntityOp = new AddEntityOp();

        op_list->addEntityOp->EntityId = 1;
        op_list->addEntityOp->PrefabName = (char*)"Traveller";
        op_list->addEntityOp->PrefabContext = (char*)"Player";

        this->didCreatePlayer = true;
    }
    else if (!this->didSendAddEntityRequest) {
        op_list->addEntityOp = new AddEntityOp();

        op_list->addEntityOp->EntityId = 2;
        op_list->addEntityOp->PrefabName = (char*)"1044497584@Island";
        op_list->addEntityOp->PrefabContext = (char*)"defg";

        this->didSendAddEntityRequest = true;
    }
    else if (!this->didSendAddComponentRequest) {
        char* buffer = new char[12] {
            '\x8a', '\xf8', '\x03', '\x08', '\x0a', '\x06', '\x50', '\x6c', '\x61', '\x79', '\x65', '\x72' // identifier == Player
        };
        ClientObject* object;
        bool success = DeserializeComponent(8065, ClientObjectType::Snapshot, buffer, 12, &object);
        Logger::Debug("refid = " + std::to_string(object->Reference) + " success = " + (success ? "true" : "false"));
        op_list->addComponentOp = new AddComponentOp();

        op_list->addComponentOp->EntityId = 2;
        op_list->addComponentOp->InitialComponent.ComponentId = 8065;
        op_list->addComponentOp->InitialComponent.Object = object;
        
        /*
        char* buffer = new char[1000];
        unsigned int length = 0;
        ClientObject* object = new ClientObject();
        object->Reference = 1; // injected by our mod, see ChangelogLoader_Patch.cs. Object is broken somehow, set breakpoint at ClientObjects.Dereference and set reference to 1 in debugger.

        bool success = SerializeComponent(8065, ClientObjectType::Snapshot, object, &buffer, &length);

        if (success) {
            Logger::Debug("IT WORKED!");
            Logger::Debug("length: " + length);
            Logger::Hexify(buffer, length);
        }
        else {
            Logger::Debug("FML!");
        }
        */

        this->didSendAddComponentRequest = true;
    }

    return op_list;
}

void Connection::SendAssetLoaded(AssetLoaded* asset_loaded) {
    Logger::Debug("ASSET LOADED: " + std::string(asset_loaded->Name) + " " + asset_loaded->Context);

    if (strcmp(asset_loaded->Name, "Traveller") == 0 && strcmp(asset_loaded->Context, "Player") == 0) {
        this->gameLoadedPlayer = true;
    }
}

bool Connection::DeserializeComponent(unsigned int component_id, ClientObjectType objType, char* buffer, unsigned int length, ClientObject** obj) {
    return this->vtable[component_id].Deserialize(component_id, objType, buffer, length, obj);
}

bool Connection::SerializeComponent(unsigned int component_id, ClientObjectType objType, ClientObject* obj, char** buffer, unsigned int* length) {
    return this->vtable[component_id].Serialize(component_id, objType, obj, buffer, length);
}

void Connection::SendComponentInterest(long entity_id, InterestOverride* interest_override, unsigned int interest_override_count)
{
    for (unsigned int i = 0; i < interest_override_count; i++) {
        
    }
    //
}
