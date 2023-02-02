#include "Connection.h"

Connection::Connection(char* hostname, unsigned short port, ConnectionParameters* parameters, ENetHost* client) {
    this->hostname = hostname;
    this->port = port;
    this->client = client;

    if (this->client != NULL && this->hostname != NULL && this->port != 0) {
        Logger::Debug("Trying to connect to game server at " + std::string(this->hostname));
        this->peer = ENet_Connect(this->hostname, this->port, this->client, 4);
        if (this->peer != NULL) {
            Logger::Debug("SUCCESS!");
        }
        else {
            Logger::Debug("FAILED!");
        }
    }

    for (unsigned int i = 0; i < parameters->ClientComponentVtableCount; ++i) {
        this->vtable[parameters->ClientComponentVtable[i].ComponentId] = parameters->ClientComponentVtable[i];
        Logger::Debug("Component registered: " + std::to_string(parameters->ClientComponentVtable[i].ComponentId));
    }
}

Connection::~Connection() {
    // assume when the connection is closed the game does not need ENet anymore
    // if its needed again i hope it will create a new Locator so we re-init ENet again
    ENet_Disconnect(this->peer, this->client);
    ENet_Deinitialize(this->client);

    this->peer = NULL;
    this->client = NULL;
}

bool Connection::IsConnected()
{
    return this->peer != NULL;
}

OpList* Connection::GetOpList() {
    OpList* op_list = new OpList();

    if (this->client != NULL) {
        ENetPacket_Wrapper* packet = ENet_Poll(this->client, 0, NULL, NULL);

        if (packet != NULL) {
            if (packet->channel == CH_AssetLoadRequestOp) {
                op_list->assetLoadRequestOp = new AssetLoadRequestOp();
                if (!PB_AssetLoadRequestOp_Deserialize(packet->data, packet->dataLength, op_list->assetLoadRequestOp)) {
                    delete op_list->assetLoadRequestOp;
                    op_list->assetLoadRequestOp = NULL;
                    Logger::Debug("FAILED TO DESERIALIZE DATA!");
                }
                else {
                    Logger::Debug("[info] got an asset load request for: " + std::string(op_list->assetLoadRequestOp->Context) + " " + std::string(op_list->assetLoadRequestOp->Name));
                }
            }
            else if (packet->channel == CH_AddEntityOp) {
                op_list->addEntityOp = new AddEntityOp();
                if (!PB_AddEntityOp_Deserialize(packet->data, packet->dataLength, op_list->addEntityOp)) {
                    delete op_list->addEntityOp;
                    op_list->addEntityOp = NULL;
                    Logger::Debug("FAILED TO DESERIALIZE DATA!");
                }
                else {
                    ENet_Send(this->peer, CH_AddEntityOp, "a", 1, ENET_PACKET_FLAG_RELIABLE); // just used as ack
                }
            }
            else if (packet->channel == CH_SendComponentInterest) {
                PB_AddComponentOp* addComponentOp = NULL;
                unsigned int addComponentCount = 0;
                long entityId = 0;
                if (!PB_AddComponentOp_Deserialze(packet->data, packet->dataLength, &entityId, &addComponentOp, &addComponentCount)) {
                    Logger::Debug("FAILED TO DESERIALIZE COMPONENTS!");
                }
                else {
                    op_list->addComponentOp = new AddComponentOp[addComponentCount];
                    op_list->addComponentLen = addComponentCount;

                    for (int i = 0; i < addComponentCount; i++) {
                        ClientObject* object;
                        bool success = DeserializeComponent(addComponentOp[i].ComponentId, ClientObjectType::Snapshot, addComponentOp[i].ComponentData, addComponentOp[i].DataLength, &object);
                        if (success) {
                            Logger::Debug("SUCCESSFULLY received component " + std::to_string(addComponentOp[i].ComponentId) + " from server, adding to entity " + std::to_string(entityId));
                            op_list->addComponentOp[i].EntityId = entityId;
                            op_list->addComponentOp[i].InitialComponent.ComponentId = addComponentOp[i].ComponentId;
                            op_list->addComponentOp[i].InitialComponent.Object = object;
                        }
                    }
                }
            }
            else if (packet->channel == CH_AuthorityChangeOp) {
                Stripped_AuthorityChangeOp* authorityChangeOp = NULL;
                unsigned int authorityChangeOpCount = 0;
                long entityId = 0;
                if (!PB_AuthorityChangeOp_Deserialize(packet->data, packet->dataLength, &entityId, &authorityChangeOp, &authorityChangeOpCount)) {
                    Logger::Debug("FAILED TO DESERIALIZE AUTH CHANGE!");
                }
                else {
                    op_list->authorityChangeOp = new AuthorityChangeOp[authorityChangeOpCount];
                    op_list->authorityChangeOpLen = authorityChangeOpCount;

                    for (int i = 0; i < authorityChangeOpCount; i++) {
                        op_list->authorityChangeOp[i].EntityId = entityId;
                        op_list->authorityChangeOp[i].ComponentId = authorityChangeOp[i].ComponentId;
                        op_list->authorityChangeOp[i].HasAuthority = authorityChangeOp[i].HasAuthority;
                    }
                }
            }
            ENet_Destroy_Packet(packet);
        }
    }

    return op_list;
}

void Connection::SendAssetLoaded(AssetLoaded* asset_loaded) {
    Logger::Debug("ASSET LOADED: " + std::string(asset_loaded->Name) + " " + asset_loaded->Context);
    // send ack
    ENet_Send(this->peer, CH_AssetLoadRequestOp, asset_loaded, sizeof(asset_loaded), ENET_PACKET_FLAG_RELIABLE);
}

bool Connection::DeserializeComponent(unsigned int component_id, ClientObjectType objType, char* buffer, unsigned int length, ClientObject** obj) {
    return this->vtable[component_id].Deserialize(component_id, objType, buffer, length, obj);
}

bool Connection::SerializeComponent(unsigned int component_id, ClientObjectType objType, ClientObject* obj, char** buffer, unsigned int* length) {
    return this->vtable[component_id].Serialize(component_id, objType, obj, buffer, length);
}

void Connection::SendComponentInterest(long entity_id, InterestOverride* interest_override, unsigned int interest_override_count)
{
    int len = 0;
    void* ptr = PB_SendComponentInterest_Serialize(entity_id, interest_override, interest_override_count, &len);

    if (ptr != NULL && len > 0 && this->peer != NULL) {
        ENet_Send(this->peer, CH_SendComponentInterest, ptr, len, ENET_PACKET_FLAG_RELIABLE);
    }
}
