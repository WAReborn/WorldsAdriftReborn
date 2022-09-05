#pragma once

#include "Structs.h"
#include "Callbacks.h"

#include "Dispatcher.h"
#include "Locator.h"
#include "Dispatcher.h"
#include "DeploymentListFuture.h"
#include "ConnectionFuture.h"
#include "Connection.h"
#include "OpList.h"

#define DLL_EXPORT extern "C" __declspec(dllexport)

DLL_EXPORT Dispatcher* __cdecl WorkerProtocol_Dispatcher_Create();
DLL_EXPORT void __cdecl WorkerProtocol_Dispatcher_Destroy(Dispatcher* dispatcher);
DLL_EXPORT void __cdecl WorkerProtocol_Dispatcher_RegisterDisconnectCallback(Dispatcher* dispatcher, void* data, DisconnectCallback callback);
DLL_EXPORT void __cdecl WorkerProtocol_Dispatcher_RegisterLogMessageCallback(Dispatcher* dispatcher, void* data, LogMessageCallback callback);
DLL_EXPORT void __cdecl WorkerProtocol_Dispatcher_RegisterMetricsCallback(Dispatcher* dispatcher, void* data, MetricsCallback callback);
DLL_EXPORT void __cdecl WorkerProtocol_Dispatcher_RegisterCriticalSectionCallback(Dispatcher* dispatcher, void* data, CriticalSectionCallback callback);
DLL_EXPORT void __cdecl WorkerProtocol_Dispatcher_RegisterAssetLoadRequestCallback(Dispatcher* dispatcher, void* data, AssetLoadRequestCallback callback);
DLL_EXPORT void __cdecl WorkerProtocol_Dispatcher_RegisterAddEntityCallback(Dispatcher* dispatcher, void* data, AddEntityCallback callback);
DLL_EXPORT void __cdecl WorkerProtocol_Dispatcher_RegisterRemoveEntityCallback(Dispatcher* dispatcher, void* data, RemoveEntityCallback callback);
DLL_EXPORT void __cdecl WorkerProtocol_Dispatcher_RegisterReserveEntityIdResponseCallback(Dispatcher* dispatcher, void* data, ReserveEntityIdResponseCallback callback);
DLL_EXPORT void __cdecl WorkerProtocol_Dispatcher_RegisterCreateEntityResponseCallback(Dispatcher* dispatcher, void* data, CreateEntityResponseCallback callback);
DLL_EXPORT void __cdecl WorkerProtocol_Dispatcher_RegisterDeleteEntityResponseCallback(Dispatcher* dispatcher, void* data, DeleteEntityResponseCallback callback);
DLL_EXPORT void __cdecl WorkerProtocol_Dispatcher_RegisterEntityQueryResponseCallback(Dispatcher* dispatcher, void* data, EntityQueryResponseCallback callback);
DLL_EXPORT void __cdecl WorkerProtocol_Dispatcher_RegisterAddComponentCallback(Dispatcher* dispatcher, void* data, AddComponentCallback callback);
DLL_EXPORT void __cdecl WorkerProtocol_Dispatcher_RegisterRemoveComponentCallback(Dispatcher* dispatcher, void* data, RemoveComponentCallback callback);
DLL_EXPORT void __cdecl WorkerProtocol_Dispatcher_RegisterAuthorityChangeCallback(Dispatcher* dispatcher, void* data, AuthorityChangeCallback callback);
DLL_EXPORT void __cdecl WorkerProtocol_Dispatcher_RegisterComponentUpdateCallback(Dispatcher* dispatcher, void* data, ComponentUpdateCallback callback);
DLL_EXPORT void __cdecl WorkerProtocol_Dispatcher_RegisterCommandRequestCallback(Dispatcher* dispatcher, void* data, CommandRequestCallback callback);
DLL_EXPORT void __cdecl WorkerProtocol_Dispatcher_RegisterCommandResponseCallback(Dispatcher* dispatcher, void* data, CommandResponseCallback callback);
DLL_EXPORT void __cdecl WorkerProtocol_Dispatcher_Process(Dispatcher* dispatcher, OpList* op_list);
DLL_EXPORT Locator* __cdecl WorkerProtocol_Locator_Create(char* hostname, LocatorParameters* parameters);
DLL_EXPORT void __cdecl WorkerProtocol_Locator_Destroy(Locator* locator);
DLL_EXPORT DeploymentListFuture* __cdecl WorkerProtocol_Locator_GetDeploymentListAsync(Locator* locator);
DLL_EXPORT ConnectionFuture* __cdecl WorkerProtocol_Locator_ConnectAsync(Locator* locator, char* deployment_name, ConnectionParameters* parameters, void* data, QueueStatusCallback callback);
DLL_EXPORT ConnectionFuture* __cdecl WorkerProtocol_ConnectAsync(char* hostname, unsigned short port, ConnectionParameters* parameters);
DLL_EXPORT void __cdecl WorkerProtocol_DeploymentListFuture_Destroy(DeploymentListFuture* future);
DLL_EXPORT void __cdecl WorkerProtocol_DeploymentListFuture_Get(DeploymentListFuture* future, unsigned int* timeout_millis, void* data, DeploymentListCallback callback);
DLL_EXPORT void __cdecl WorkerProtocol_ConnectionFuture_Destroy(ConnectionFuture* future);
DLL_EXPORT Connection* __cdecl WorkerProtocol_ConnectionFuture_Get(ConnectionFuture* future, unsigned int* timeout_millis);
DLL_EXPORT void __cdecl WorkerProtocol_Connection_Destroy(Connection* connection);
DLL_EXPORT bool __cdecl WorkerProtocol_Connection_IsConnected(Connection* connection);
DLL_EXPORT void __cdecl WorkerProtocol_Connection_SendLogMessage(Connection* connection, LogMessage* log_message);
DLL_EXPORT void __cdecl WorkerProtocol_Connection_SendMetrics(Connection* connection, Metrics* metrics);
DLL_EXPORT unsigned int __cdecl WorkerProtocol_Connection_SendReserveEntityIdRequest(Connection* connection, unsigned int* timeout_millis);
DLL_EXPORT unsigned int __cdecl WorkerProtocol_Connection_SendCreateEntityRequest(Connection* connection, char* prefab_name, unsigned int component_count, ComponentObject* component, EntityId* entity_id, unsigned int* timeout_millis);
DLL_EXPORT unsigned int __cdecl WorkerProtocol_Connection_SendDeleteEntityRequest(Connection* connection, EntityId entity_id, unsigned int* timeout_millis);
DLL_EXPORT unsigned int __cdecl WorkerProtocol_Connection_SendEntityQueryRequest(Connection* connection, EntityQuery* entity_query, unsigned int* timeout_millis);
DLL_EXPORT void __cdecl WorkerProtocol_Connection_SendComponentUpdate(Connection* connection, long entity_id, ComponentObject* component_update, unsigned char legacy_callback_semantics);
DLL_EXPORT unsigned int __cdecl WorkerProtocol_Connection_SendCommandRequest(Connection* connection, long entity_id, ComponentObject* request, unsigned int command_id, unsigned int* timeout_millis, CommandParameters* parameters);
DLL_EXPORT void __cdecl WorkerProtocol_Connection_SendCommandResponse(Connection* connection, unsigned int request_id, ComponentObject* response);
DLL_EXPORT void __cdecl WorkerProtocol_Connection_SendCommandFailure(Connection* connection, unsigned int request_id, char* message);
DLL_EXPORT void __cdecl WorkerProtocol_Connection_SendInterestedComponents(Connection* connection, long entity_id, InterestedComponents* component_update);
DLL_EXPORT void __cdecl WorkerProtocol_Connection_SendComponentInterest(Connection* connection, long entity_id, InterestOverride* interest_override, unsigned int interest_override_count);
DLL_EXPORT void __cdecl WorkerProtocol_Connection_SendAssetLoaded(Connection* connection, AssetLoaded* asset_loaded);
DLL_EXPORT void __cdecl WorkerProtocol_Connection_SetProtocolLoggingEnabled(Connection* connection, unsigned char enabled);
DLL_EXPORT OpList* __cdecl WorkerProtocol_Connection_GetOpList(Connection* connection, unsigned int timeout_millis);
DLL_EXPORT void __cdecl WorkerProtocol_OpList_Destroy(OpList* op_list);
DLL_EXPORT void __cdecl WorkerProtocol_Connection_GetFlag(Connection* connection, char* name, void* user_data, GetFlagCallback callback);
DLL_EXPORT void __cdecl WorkerProtocol_LoadSnapshot(char* filename, SnapshotParameters* parameters, void* user_data, LoadSnapshotCallback callback);
DLL_EXPORT void __cdecl WorkerProtocol_SaveSnapshot(char* filename, SnapshotParameters* parameters, Snapshot* snapshot, void* user_data, SaveSnapshotCallback callback);