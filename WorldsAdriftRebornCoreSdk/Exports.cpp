#include "Exports.h"
#include <string>
#include <iostream>
#include <fstream>


void hook(const std::string& method) {
    // TODO: Find a way to log this as STDOUT/STDERR don't seems to work when called from pinvoke (or whatever is causing this behavior)
    std::cerr << "Invoked " << method << std::endl;
    std::ofstream output;
    output.open("CoreSdk_OutputLog.txt", std::ios::app);
    output << "Invoked " << method << std::endl;
    output.close();
}

Dispatcher* __cdecl WorkerProtocol_Dispatcher_Create() {
    hook("WorkerProtocol_Dispatcher_Create");
    return new Dispatcher();
}
void __cdecl WorkerProtocol_Dispatcher_Destroy(Dispatcher* dispatcher) {
    hook("WorkerProtocol_Dispatcher_Destroy");
    delete dispatcher;
}
void __cdecl WorkerProtocol_Dispatcher_RegisterDisconnectCallback(Dispatcher* dispatcher, void* data, DisconnectCallback callback) {
    hook("WorkerProtocol_Dispatcher_RegisterDisconnectCallback");
    // TODO: Add method RegisterDisconnectCallback to dispatcher and call it here
}
void __cdecl WorkerProtocol_Dispatcher_RegisterLogMessageCallback(Dispatcher* dispatcher, void* data, LogMessageCallback callback) {
    hook("WorkerProtocol_Dispatcher_RegisterLogMessageCallback");
    // TODO: Add method RegisterLogMessageCallback to dispatcher and call it here
}
void __cdecl WorkerProtocol_Dispatcher_RegisterMetricsCallback(Dispatcher* dispatcher, void* data, MetricsCallback callback) {
    hook("WorkerProtocol_Dispatcher_RegisterMetricsCallback");
    // TODO: Add method RegisterMetricsCallback to dispatcher and call it here
}
void __cdecl WorkerProtocol_Dispatcher_RegisterCriticalSectionCallback(Dispatcher* dispatcher, void* data, CriticalSectionCallback callback) {
    hook("WorkerProtocol_Dispatcher_RegisterCriticalSectionCallback");
    // TODO: Add method RegisterCriticalSectionCallback to dispatcher and call it here
}
void __cdecl WorkerProtocol_Dispatcher_RegisterAssetLoadRequestCallback(Dispatcher* dispatcher, void* data, AssetLoadRequestCallback callback) {
    hook("WorkerProtocol_Dispatcher_RegisterAssetLoadRequestCallback");
    dispatcher->RegisterAssetLoadRequestCallback(callback, data);
}
void __cdecl WorkerProtocol_Dispatcher_RegisterAddEntityCallback(Dispatcher* dispatcher, void* data, AddEntityCallback callback) {
    hook("WorkerProtocol_Dispatcher_RegisterAddEntityCallback");
    // data is the GCHandle passed by c# land which needs to be passed back to callbacks
    dispatcher->RegisterAddEntityCallback(callback, data);
}
void __cdecl WorkerProtocol_Dispatcher_RegisterRemoveEntityCallback(Dispatcher* dispatcher, void* data, RemoveEntityCallback callback) {
    hook("WorkerProtocol_Dispatcher_RegisterRemoveEntityCallback");
    // TODO: Add method RegisterRemoveEntityCallback to dispatcher and call it here
}
void __cdecl WorkerProtocol_Dispatcher_RegisterReserveEntityIdResponseCallback(Dispatcher* dispatcher, void* data, ReserveEntityIdResponseCallback callback) {
    hook("WorkerProtocol_Dispatcher_RegisterReserveEntityIdResponseCallback");
    // TODO: Add method RegisterReserveEntityIdResponseCallback to dispatcher and call it here
}
void __cdecl WorkerProtocol_Dispatcher_RegisterCreateEntityResponseCallback(Dispatcher* dispatcher, void* data, CreateEntityResponseCallback callback) {
    hook("WorkerProtocol_Dispatcher_RegisterCreateEntityResponseCallback");
    // TODO: Add method RegisterCreateEntityResponseCallback to dispatcher and call it here
}
void __cdecl WorkerProtocol_Dispatcher_RegisterDeleteEntityResponseCallback(Dispatcher* dispatcher, void* data, DeleteEntityResponseCallback callback) {
    hook("WorkerProtocol_Dispatcher_RegisterDeleteEntityResponseCallback");
    // TODO: Add method RegisterDeleteEntityResponseCallback to dispatcher and call it here
}
void __cdecl WorkerProtocol_Dispatcher_RegisterEntityQueryResponseCallback(Dispatcher* dispatcher, void* data, EntityQueryResponseCallback callback) {
    hook("WorkerProtocol_Dispatcher_RegisterEntityQueryResponseCallback");
    // TODO: Add method RegisterEntityQueryResponseCallback to dispatcher and call it here
}
void __cdecl WorkerProtocol_Dispatcher_RegisterAddComponentCallback(Dispatcher* dispatcher, void* data, AddComponentCallback callback) {
    hook("WorkerProtocol_Dispatcher_RegisterAddComponentCallback");
    // TODO: Add method RegisterAddComponentCallback to dispatcher and call it here
}
void __cdecl WorkerProtocol_Dispatcher_RegisterRemoveComponentCallback(Dispatcher* dispatcher, void* data, RemoveComponentCallback callback) {
    hook("WorkerProtocol_Dispatcher_RegisterRemoveComponentCallback");
    // TODO: Add method RegisterRemoveComponentCallback to dispatcher and call it here
}
void __cdecl WorkerProtocol_Dispatcher_RegisterAuthorityChangeCallback(Dispatcher* dispatcher, void* data, AuthorityChangeCallback callback) {
    hook("WorkerProtocol_Dispatcher_RegisterAuthorityChangeCallback");
    // TODO: Add method RegisterAuthorityChangeCallback to dispatcher and call it here
}
void __cdecl WorkerProtocol_Dispatcher_RegisterComponentUpdateCallback(Dispatcher* dispatcher, void* data, ComponentUpdateCallback callback) {
    hook("WorkerProtocol_Dispatcher_RegisterComponentUpdateCallback");
    // TODO: Add method RegisterComponentUpdateCallback to dispatcher and call it here
}
void __cdecl WorkerProtocol_Dispatcher_RegisterCommandRequestCallback(Dispatcher* dispatcher, void* data, CommandRequestCallback callback) {
    hook("WorkerProtocol_Dispatcher_RegisterCommandRequestCallback");
    // TODO: Add method RegisterCommandRequestCallback to dispatcher and call it here
}
void __cdecl WorkerProtocol_Dispatcher_RegisterCommandResponseCallback(Dispatcher* dispatcher, void* data, CommandResponseCallback callback) {
    hook("WorkerProtocol_Dispatcher_RegisterCommandResponseCallback");
    // TODO: Add method RegisterCommandResponseCallback to dispatcher and call it here
}
void __cdecl WorkerProtocol_Dispatcher_Process(Dispatcher* dispatcher, OpList* op_list) {
    hook("WorkerProtocol_Dispatcher_Process");
    dispatcher->Process(op_list);
}

Locator* __cdecl WorkerProtocol_Locator_Create(char* hostname, LocatorParameters* parameters) {
    hook("WorkerProtocol_Locator_Create");
    return new Locator(hostname, parameters);
}
void __cdecl WorkerProtocol_Locator_Destroy(Locator* locator) {
    hook("WorkerProtocol_Locator_Destroy");
    delete locator;
}
DeploymentListFuture* __cdecl WorkerProtocol_Locator_GetDeploymentListAsync(Locator* locator) {
    hook("WorkerProtocol_Locator_GetDeploymentListAsync");
    return locator->GetDeploymentListAsync();
}
ConnectionFuture* __cdecl WorkerProtocol_Locator_ConnectAsync(Locator* locator, char* deployment_name, ConnectionParameters* parameters, void* data, QueueStatusCallback callback) {
    hook("WorkerProtocol_Locator_ConnectAsync");
    return locator->ConnectAsync(deployment_name, parameters, data, callback);
}

ConnectionFuture* __cdecl WorkerProtocol_ConnectAsync(char* hostname, unsigned short port, ConnectionParameters* parameters) {
    hook("WorkerProtocol_ConnectAsync");
    return new ConnectionFuture();
}

void __cdecl WorkerProtocol_DeploymentListFuture_Destroy(DeploymentListFuture* future) {
    hook("WorkerProtocol_DeploymentListFuture_Destroy");
    delete future;
}
void __cdecl WorkerProtocol_DeploymentListFuture_Get(DeploymentListFuture* future, unsigned int* timeout_millis, void* data, DeploymentListCallback callback) {
    hook("WorkerProtocol_DeploymentListFuture_Get");
    future->Get(timeout_millis, data, callback);
}

void __cdecl WorkerProtocol_ConnectionFuture_Destroy(ConnectionFuture* future) {
    hook("WorkerProtocol_ConnectionFuture_Destroy");
    delete future;
}
Connection* __cdecl WorkerProtocol_ConnectionFuture_Get(ConnectionFuture* future, unsigned int* timeout_millis) {
    hook("WorkerProtocol_ConnectionFuture_Get");
    return future->Get(timeout_millis);
}

void __cdecl WorkerProtocol_Connection_Destroy(Connection* connection) {
    hook("WorkerProtocol_Connection_Destroy");
    delete connection;
}
bool __cdecl WorkerProtocol_Connection_IsConnected(Connection* connection) {
    hook("WorkerProtocol_Connection_IsConnected");
    return connection->IsConnected();
}
void __cdecl WorkerProtocol_Connection_SendLogMessage(Connection* connection, LogMessage* log_message) {
    hook("WorkerProtocol_Connection_SendLogMessage");
    // TODO: Add method SendLogMessage to connection and call it here
}
void __cdecl WorkerProtocol_Connection_SendMetrics(Connection* connection, Metrics* metrics) {
    hook("WorkerProtocol_Connection_SendMetrics");
    // TODO: Add method SendMetrics to connection and call it here
}
unsigned int __cdecl WorkerProtocol_Connection_SendReserveEntityIdRequest(Connection* connection, unsigned int* timeout_millis) {
    hook("WorkerProtocol_Connection_SendReserveEntityIdRequest");
    // TODO: Add method SendReserveEntityIdRequest to connection, call it here and return it's return value
    return 0;
}
unsigned int __cdecl WorkerProtocol_Connection_SendCreateEntityRequest(Connection* connection, char* prefab_name, unsigned int component_count, ComponentObject* component, EntityId* entity_id, unsigned int* timeout_millis) {
    hook("WorkerProtocol_Connection_SendCreateEntityRequest");
    // TODO: Add method SendCreateEntityRequest to connection, call it here and return it's return value
    return 0;
}
unsigned int __cdecl WorkerProtocol_Connection_SendDeleteEntityRequest(Connection* connection, EntityId entity_id, unsigned int* timeout_millis) {
    hook("WorkerProtocol_Connection_SendDeleteEntityRequest");
    // TODO: Add method SendDeleteEntityRequest to connection, call it here and return it's return value
    return 0;
}
unsigned int __cdecl WorkerProtocol_Connection_SendEntityQueryRequest(Connection* connection, EntityQuery* entity_query, unsigned int* timeout_millis) {
    hook("WorkerProtocol_Connection_SendEntityQueryRequest");
    // TODO: Add method SendEntityQueryRequest to connection, call it here and return it's return value
    return 0;
}
void __cdecl WorkerProtocol_Connection_SendComponentUpdate(Connection* connection, long entity_id, ComponentObject* component_update, unsigned char legacy_callback_semantics) {
    hook("WorkerProtocol_Connection_SendComponentUpdate");
    // TODO: Add method SendComponentUpdate to connection and call it here
}
unsigned int __cdecl WorkerProtocol_Connection_SendCommandRequest(Connection* connection, long entity_id, ComponentObject* request, unsigned int command_id, unsigned int* timeout_millis, CommandParameters* parameters) {
    hook("WorkerProtocol_Connection_SendCommandRequest");
    // TODO: Add method SendCommandRequest to connection, call it here and return it's return value
    return 0;
}
void __cdecl WorkerProtocol_Connection_SendCommandResponse(Connection* connection, unsigned int request_id, ComponentObject* response) {
    hook("WorkerProtocol_Connection_SendCommandResponse");
    // TODO: Add method SendCommandResponse to connection and call it here
}
void __cdecl WorkerProtocol_Connection_SendCommandFailure(Connection* connection, unsigned int request_id, char* message) {
    hook("WorkerProtocol_Connection_SendCommandFailure");
    // TODO: Add method SendCommandFailure to connection and call it here
}
void __cdecl WorkerProtocol_Connection_SendInterestedComponents(Connection* connection, long entity_id, InterestedComponents* component_update) {
    hook("WorkerProtocol_Connection_SendInterestedComponents");
    // TODO: Add method SendInterestedComponents to connection and call it here
}
void __cdecl WorkerProtocol_Connection_SendComponentInterest(Connection* connection, long entity_id, InterestOverride* interest_override, unsigned int interest_override_count) {
    hook("WorkerProtocol_Connection_SendComponentInterest");
    // temp code
    std::ofstream output;
    output.open("CoreSdk_OutputLog.txt", std::ios::app);
    output << "entity_id: " << entity_id << std::endl;
    std::cout << "entity_id: " << entity_id << std::endl;
    for (int i = 0; i < interest_override_count; i++) {
        output << "----" << std::endl;
        output << interest_override[i].ComponentId << " " << (interest_override[i].IsInterested ? "true" : "false") << std::endl;
        std::cout << "----" << std::endl;
        std::cout << interest_override[i].ComponentId << " " << (interest_override[i].IsInterested ? "true" : "false") << std::endl;
    }
    output.close();
    // TODO: Add method SendComponentInterest to connection and call it here
}
void __cdecl WorkerProtocol_Connection_SendAssetLoaded(Connection* connection, AssetLoaded* asset_loaded) {
    hook("WorkerProtocol_Connection_SendAssetLoaded");
    connection->SendAssetLoaded(asset_loaded);
}
void __cdecl WorkerProtocol_Connection_SetProtocolLoggingEnabled(Connection* connection, unsigned char enabled) {
    hook("WorkerProtocol_Connection_SetProtocolLoggingEnabled");
    // TODO: Add method SetProtocolLoggingEnabled to connection and call it here
}
OpList* __cdecl WorkerProtocol_Connection_GetOpList(Connection* connection, unsigned int timeout_millis) {
    hook("WorkerProtocol_Connection_GetOpList");
    return connection->GetOpList();
}
void __cdecl WorkerProtocol_OpList_Destroy(OpList* op_list) {
    hook("WorkerProtocol_OpList_Destroy");
    delete op_list;
}
void __cdecl WorkerProtocol_Connection_GetFlag(Connection* connection, char* name, void* user_data, GetFlagCallback callback) {
    hook("WorkerProtocol_Connection_GetFlag");
    // TODO: Add method GetFlag to connection and call it here
}

void __cdecl WorkerProtocol_LoadSnapshot(char* filename, SnapshotParameters* parameters, void* user_data, LoadSnapshotCallback callback) {
    hook("WorkerProtocol_LoadSnapshot");
    // TODO: Add implementation (in another file please T.T)
}
void __cdecl WorkerProtocol_SaveSnapshot(char* filename, SnapshotParameters* parameters, Snapshot* snapshot, void* user_data, SaveSnapshotCallback callback) {
    hook("WorkerProtocol_SaveSnapshot");
    // TODO: Add implementation (in another file please T.T)
}