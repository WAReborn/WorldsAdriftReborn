#include "enetLayer.h"

int __cdecl ENet_EXP_Initialize() {
    return ENet_Initialize();
}
ENetHost* __cdecl ENet_EXP_Create_Host(int port, int maxConnections, int maxChannels, int inBandwidth, int outBandwidth) {
    return ENet_Create_Host(port, maxConnections, maxChannels, inBandwidth, outBandwidth);
}
ENetPeer* __cdecl ENet_EXP_Connect(char* hostname, int port, ENetHost* client, int maxChannels) {
    return ENet_Connect(hostname, port, client, maxChannels);
}
void __cdecl ENet_EXP_Disconnect(ENetPeer* peer, ENetHost* client) {
    ENet_Disconnect(peer, client);
}
void __cdecl ENet_EXP_Deinitialize(ENetHost* client) {
    ENet_Deinitialize(client);
}

int ENet_Initialize() {
    return enet_initialize();
}

ENetHost* ENet_Create_Host(int port, int maxConnections, int maxChannels, int inBandwidth, int outBandwidth) {
    if (port != 0) {
        ENetAddress address;

        address.host = ENET_HOST_ANY;
        address.port = port;

        return enet_host_create(&address, maxConnections, maxChannels, inBandwidth, outBandwidth);
    }

    return enet_host_create(NULL, maxConnections, maxChannels, inBandwidth, outBandwidth);
}

ENetPeer* ENet_Connect(char* hostname, int port, ENetHost* client, int maxChannels) {
    if (client == NULL || hostname == NULL) {
        Logger::Debug("[ERROR] Either no valid client or host was given, cannot connect.");
        return NULL;
    }

    ENetAddress address;
    ENetEvent event;
    ENetPeer* peer;

    enet_address_set_host(&address, hostname);
    address.port = port;

    peer = enet_host_connect(client, &address, maxChannels, 0);

    if (peer == NULL) {
        Logger::Debug("[ERROR] No available peers for initiating an ENet connection.");
        return NULL;
    }

    // timeout after 5 secs
    if (enet_host_service(client, &event, 5000) > 0 && event.type == ENET_EVENT_TYPE_CONNECT) {
        return peer;
    }

    enet_peer_reset(peer);
    return NULL;
}

void ENet_Disconnect(ENetPeer* peer, ENetHost* client) {
    if (peer == NULL || client == NULL) {
        return;
    }

    ENetEvent event;

    enet_peer_disconnect(peer, 0);

    // wait for 3 secs to disconnect gracefully
    while (enet_host_service(client, &event, 3000) > 0) {
        switch (event.type) {
        case ENET_EVENT_TYPE_RECEIVE:
            enet_packet_destroy(event.packet);
            break;
        case ENET_EVENT_TYPE_DISCONNECT:
            return;
        }
    }

    // failed to gracefully disconnect, force it now
    enet_peer_reset(peer);
}

void ENet_Deinitialize(ENetHost* client) {
    if (client != NULL) {
        enet_host_destroy(client);
    }

    enet_deinitialize();
}