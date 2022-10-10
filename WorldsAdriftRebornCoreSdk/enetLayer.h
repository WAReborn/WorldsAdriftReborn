#pragma once

#include "enet/enet.h"
#include "Logger.h"

#define DLL_EXPORT extern "C" __declspec(dllexport)

DLL_EXPORT int __cdecl ENet_EXP_Initialize();
DLL_EXPORT ENetHost* __cdecl ENet_EXP_Create_Host(int port, int maxConnections, int maxChannels, int inBandwidth, int outBandwidth);
DLL_EXPORT ENetPeer* __cdecl ENet_EXP_Connect(char* hostname, int port, ENetHost* client, int maxChannels);
DLL_EXPORT void __cdecl ENet_EXP_Disconnect(ENetPeer* peer, ENetHost* client);
DLL_EXPORT void __cdecl ENet_EXP_Deinitialize(ENetHost* client);

int ENet_Initialize();
// set port to 0 if you are a client
ENetHost* ENet_Create_Host(int port, int maxConnections, int maxChannels, int inBandwidth, int outBandwidth);
ENetPeer* ENet_Connect(char* hostname, int port, ENetHost* client, int maxChannels);
void ENet_Disconnect(ENetPeer* peer, ENetHost* client);
void ENet_Deinitialize(ENetHost* client);