#pragma once

#include "Structs.h"

typedef void DisconnectCallback(void* user_data, DisconnectOp* op);
typedef void LogMessageCallback(void* user_data, LogMessageOp* op);
typedef void MetricsCallback(void* user_data, MetricsOp* op);
typedef void CriticalSectionCallback(void* user_data, CriticalSectionOp* op);
typedef void AssetLoadRequestCallback(void* user_data, AssetLoadRequestOp* op);
typedef void AddEntityCallback(void* user_data, AddEntityOp* op);
typedef void RemoveEntityCallback(void* user_data, RemoveEntityOp* op);
typedef void ReserveEntityIdResponseCallback(void* user_data, ReserveEntityIdResponseOp* op);
typedef void CreateEntityResponseCallback(void* user_data, CreateEntityResponseOp* op);
typedef void DeleteEntityResponseCallback(void* user_data, DeleteEntityResponseOp* op);
typedef void EntityQueryResponseCallback(void* user_data, EntityQueryResponseOp* op);
typedef void AddComponentCallback(void* user_data, AddComponentOp* op);
typedef void RemoveComponentCallback(void* user_data, RemoveComponentOp* op);
typedef void AuthorityChangeCallback(void* user_data, AuthorityChangeOp* op);
typedef void ComponentUpdateCallback(void* user_data, ComponentUpdateOp* op);
typedef void CommandRequestCallback(void* user_data, CommandRequestOp* op);
typedef void CommandResponseCallback(void* user_data, CommandResponseOp* op);
typedef void DeploymentListCallback(void* user_data, DeploymentList* deployment_list);
typedef unsigned char QueueStatusCallback(void* user_data, QueueStatus* queue_status);
typedef void GetFlagCallback(void* user_data, char* value);
typedef void LoadSnapshotCallback(void* user_data, Snapshot* snapshot, char* error);
typedef void SaveSnapshotCallback(void* user_data, char* error);