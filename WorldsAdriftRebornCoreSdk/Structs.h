#pragma once

#include "VTable.h"
#include <cstdint>

struct EntityId {
    long Id;
} typedef EntityId;

struct LogMessage {
    unsigned char LogLevel;
    char* LoggerName;
    char* Message;
};

struct GaugeMetric {
    char* Key;
    double Value;
};

struct HistogramMetricBucket {
    double UpperBound;
    unsigned int Samples;
};

struct HistogramMetric {
    char* Key;
    double Sum;
    unsigned int BucketCount;
    HistogramMetricBucket* Bucket;
};

struct Metrics {
    double* Load;
    unsigned int GaugeMetricCount;
    GaugeMetric* GaugeMetric;
    unsigned int HistogramMetricCount;
    HistogramMetric* HistogramMetric;
};

struct ClientObject
{
    unsigned long Reference;
};

struct ComponentObject {
    unsigned int ComponentId;
    ClientObject* Object;
};

struct Entity {
    long EntityId;
    unsigned int ComponentCount;
    ComponentObject* Component;
};

enum ConstraintType {
    ConstraintType_EntityId = 1,
    ConstraintType_Component,
    ConstraintType_Sphere,
    ConstraintType_And,
    ConstraintType_Or
};

struct Constraint;

struct EntityIdConstraint {
    long EntityId;
};

struct ComponentConstraint {
    unsigned int ComponentId;
};

struct SphereConstraint {
    double PositionX;
    double PositionY;
    double PositionZ;
    double Radius;
};

struct AndConstraint {
    unsigned int ConstraintCount;
    Constraint* Constraint;
};

struct OrConstraint {
    unsigned int ConstraintCount;
    Constraint* Constraint;
};

union ConstraintUnion {
    EntityIdConstraint EntityIdConstraint;
    ComponentConstraint ComponentConstraint;
    SphereConstraint SphereConstraint;
    AndConstraint AndConstraint;
    OrConstraint OrConstraint;
};


struct Constraint {
    unsigned char ConstraintType;
    ConstraintUnion ConstraintTypeUnion;
};


enum ResultType {
    ResultType_Count = 1,
    ResultType_Snapshot
};

struct EntityQuery {
    Constraint Constraint;
    unsigned char ResultType;
    unsigned int SnapshotResultTypeComponentIdCount;
    unsigned int* SnapshotResultTypeComponentId;
};

struct InterestedComponents {
    unsigned int ComponentIdCount;
    unsigned int* ComponentId;
};

struct InterestOverride {
    unsigned int ComponentId;
    unsigned char IsInterested;
};

struct AssetLoaded {
    char* AssetType;
    char* Name;
    char* Context;
};

struct DisconnectOp {
    char* Reason;
};

struct LogMessageOp {
    unsigned char LogLevel;
    char* Message;
};

struct MetricsOp {
    Metrics Metrics;
};

struct CriticalSectionOp {
    unsigned char InCriticalSection;
};

struct AssetLoadRequestOp {
    char* AssetType;
    char* Name;
    char* Context;
    char* Url;
};

struct AddEntityOp {
    long EntityId;
    char* PrefabName;
    char* PrefabContext;
};
struct stripped_AddEntityOp {
    char* PrefabName;
    char* PrefabContext;
};

struct RemoveEntityOp {
    long EntityId;
};

struct ReserveEntityIdResponseOp {
    unsigned int RequestId;
    unsigned char StatusCode;
    char* Message;
    long EntityId;
};

struct CreateEntityResponseOp {
    unsigned int RequestId;
    unsigned char StatusCode;
    char* Message;
    long EntityId;
};

struct DeleteEntityResponseOp {
    unsigned int RequestId;
    long EntityId;
    unsigned char StatusCode;
    char* Message;
};

struct EntityQueryResponseOp {
    unsigned int RequestId;
    unsigned char StatusCode;
    char* Message;
    unsigned int ResultCount;
    Entity* Result;
};

struct PB_AddComponentOp {
    unsigned int ComponentId;
    char* ComponentData;
    int DataLength;
};

struct AddComponentOp{
    long EntityId;
    ComponentObject InitialComponent;
};

struct RemoveComponentOp {
    long EntityId;
    unsigned int ComponentId;
};

struct Stripped_AuthorityChangeOp {
    unsigned int ComponentId;
    unsigned char HasAuthority;
};

#pragma pack(push, 1)
struct AuthorityChangeOp {
    int64_t EntityId;
    int32_t ComponentId;
    unsigned char HasAuthority;
};
#pragma pack(pop)

struct ComponentUpdateOp {
    long EntityId;
    ComponentObject Update;
};

struct PB_ComponentUpdateOp {
    unsigned int ComponentId;
    char* ComponentData;
    int DataLength;
};

struct CommandRequestOp {
    unsigned int RequestId;
    long EntityId;
    unsigned int TimeoutMillis;
    unsigned char* CallerWorkerId;
    unsigned int CallerAttributeCount;
    unsigned char** CallerAttribute;
    ComponentObject Request;
};

struct CommandResponseOp {
    unsigned int RequestId;
    long EntityId;
    unsigned char StatusCode;
    char* Message;
    ComponentObject Response;
    unsigned int CommandId;
};

struct RakNetParameters {
    unsigned int HeartbeatTimeoutMillis;
};

struct TcpParameters {
    unsigned char MultiplexLevel;
    unsigned char NoDelay;
    unsigned int SendBufferSize;
    unsigned int ReceiveBufferSize;
};

struct NetworkParameters {
    const unsigned char FlagTcp = 1;
    const unsigned char FlagRakNet = 2;
    const unsigned char FlagUseExternalIp = 4;
    unsigned char Flags;
    RakNetParameters RakNet;
    TcpParameters Tcp;
};

struct SdkParameters {
    unsigned int SendQueueCapacity;
    unsigned int ReceiveQueueCapacity;
    unsigned int LogMessageQueueCapacity;
    unsigned int BuiltInMetricsReportPeriodMillis;
    unsigned char EnableAssetLoadRequestCallbacks;
    unsigned char EnableProtocolLoggingAtStartup;
    char* LogPrefix;
    unsigned int MaxLogFiles;
    unsigned int MaxLogFileSizeBytes;
};

struct ClientComponentVtable
{
    unsigned int ComponentId;
    ClientBufferFree* BufferFree;
    ClientFree* Free;
    ClientCopy* Copy;
    ClientDeserialize* Deserialize;
    ClientSerialize* Serialize;
};

struct ConnectionParameters {
    const char* WorkerType;
    const char* WorkerId;
    const char* Metadata;
    NetworkParameters Network;
    SdkParameters Sdk;
    unsigned int ClientComponentVtableCount;
    ClientComponentVtable* ClientComponentVtable;
};

struct LoginTokenCredentials {
    const char* Token;
};

struct SteamCredentials {
    const char* Ticket;
    const char* DeploymentTag;
};

struct LocatorParameters {
    unsigned char FlagLoginTokenCredentials;
    unsigned char FlagSteamCredentials;
    const char* ProjectName;
    unsigned char Flags;
    LoginTokenCredentials LoginToken;
    SteamCredentials Steam;
};

struct Deployment {
    const char* DeploymentName;
    const char* AssemblyName;
    const char* Description;
};

struct DeploymentList {
    unsigned int DeploymentCount;
    Deployment* Deployment;
    const char* Error;
};

struct QueueStatus {
    unsigned int PositionInQueue;
    const char* Error;
};

struct CommandParameters {
    unsigned char AllowShortCircuiting;
};

struct SnapshotEntity {
    Entity Entity;
    const char* PrefabName;
};

struct Snapshot {
    unsigned int EntityCount;
    SnapshotEntity* Entity;
};

struct SnapshotParameters {
    unsigned int ClientComponentVtableCount;
    ClientComponentVtable* ClientComponentVtable;
};
