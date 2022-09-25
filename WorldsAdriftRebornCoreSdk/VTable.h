#pragma once

enum class ClientObjectType : unsigned char
{
    Update = 1,
    Snapshot,
    Request,
    Response
};

struct ClientObject;

typedef void ClientBufferFree(unsigned int component_id, char* buffer);
typedef void ClientFree(unsigned int component_id, ClientObjectType objType, ClientObject* obj);
typedef ClientObject* ClientCopy(unsigned int component_id, ClientObjectType objType, ClientObject* obj);
typedef bool ClientDeserialize(unsigned int component_id, ClientObjectType objType, char* buffer, unsigned int length, ClientObject** obj);
typedef void ClientSerialize(unsigned int component_id, ClientObjectType objType, ClientObject* obj, char** buffer, unsigned int* length);