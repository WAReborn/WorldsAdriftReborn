#pragma once

#include "enet/enet.h"
#include "Logger.h"

#include "Structs.h"
#include "AssetLoadRequestOp.pb.h"
#include "AddEntityOp.pb.h"
#include "SendComponentInterest.pb.h"
#include "AddComponentOp.pb.h"

#define DLL_EXPORT extern "C" __declspec(dllexport)

#define CH_AssetLoadRequestOp 0
#define CH_AddEntityOp 1
#define CH_SendComponentInterest 2

typedef void OnNewClientConnected(ENetPeer* peer);
typedef void OnClientDisconnected(ENetPeer* peer);

struct ENetPacket_Wrapper {
    void* data;
    long dataLength;
    const char* identifier;
    int channel;
    ENetPacket* packet;
};

DLL_EXPORT int __cdecl ENet_EXP_Initialize();
DLL_EXPORT ENetHost* __cdecl ENet_EXP_Create_Host(int port, int maxConnections, int maxChannels, int inBandwidth, int outBandwidth);
DLL_EXPORT ENetPeer* __cdecl ENet_EXP_Connect(char* hostname, int port, ENetHost* client, int maxChannels);
DLL_EXPORT void __cdecl ENet_EXP_Disconnect(ENetPeer* peer, ENetHost* client);
DLL_EXPORT void __cdecl ENet_EXP_Deinitialize(ENetHost* client);
DLL_EXPORT ENetPacket_Wrapper* __cdecl ENet_EXP_Poll(ENetHost* client, int waitTime, OnNewClientConnected* callbackC, OnClientDisconnected* callbackD);
DLL_EXPORT void __cdecl ENet_EXP_Destroy_Packet(ENetPacket_Wrapper* packet);
DLL_EXPORT void __cdecl ENet_EXP_Send(ENetPeer* peer, int channel, const void* data, long len, int flag);
DLL_EXPORT void __cdecl ENet_EXP_Flush(ENetHost* client);

DLL_EXPORT void* __cdecl PB_EXP_AssetLoadRequestOp_Serialize(AssetLoadRequestOp* op, int* len);
DLL_EXPORT void* __cdecl PB_EXP_AddEntityOp_Serialize(stripped_AddEntityOp* op, int* len, long entityId);
DLL_EXPORT bool __cdecl PB_EXP_SendComponentInterest_Deserialize(const void* data, int len, long* entityId, InterestOverride** interest_override, unsigned int* interest_override_count);
DLL_EXPORT void* __cdecl PB_EXP_AddComponentOp_Serialize(long entityId, PB_AddComponentOp* addComponentOp, unsigned int addComponentOp_count, int* len);

int ENet_Initialize();
// set port to 0 if you are a client
ENetHost* ENet_Create_Host(int port, int maxConnections, int maxChannels, int inBandwidth, int outBandwidth);
ENetPeer* ENet_Connect(char* hostname, int port, ENetHost* client, int maxChannels);
void ENet_Disconnect(ENetPeer* peer, ENetHost* client);
void ENet_Deinitialize(ENetHost* client);
ENetPacket_Wrapper* ENet_Poll(ENetHost* client, int waitTime, OnNewClientConnected* callbackC, OnClientDisconnected* callbackD);
void ENet_Destroy_Packet(ENetPacket_Wrapper* packet);
void ENet_Send(ENetPeer* peer, int channel, const void* data, long len, int flag);
void ENet_Flush(ENetHost* client);

void* PB_AssetLoadRequestOp_Serialize(AssetLoadRequestOp* op, int* len);
bool PB_AssetLoadRequestOp_Deserialize(const void* data, int len, AssetLoadRequestOp* op);
void* PB_AddEntityOp_Serialize(stripped_AddEntityOp* op, int* len, long entityId);
bool PB_AddEntityOp_Deserialize(const void* data, int len, AddEntityOp* op);
void* PB_SendComponentInterest_Serialize(long entityId, InterestOverride* interest_override, unsigned int interest_override_count, int* len);
bool PB_SendComponentInterest_Deserialize(const void* data, int len, long* entityId, InterestOverride** interest_override, unsigned int* interest_override_count);
void* PB_AddComponentOp_Serialize(long entityId, PB_AddComponentOp* addComponentOp, unsigned int addComponentOp_count, int* len);
bool PB_AddComponentOp_Deserialze(const void* data, int len, long* entityId, PB_AddComponentOp** addComponentOp, unsigned int* addComponentOp_count);