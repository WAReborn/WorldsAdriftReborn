#pragma once
#include "Callbacks.h"
#include "OpList.h"
class Dispatcher
{
private:
    void* GCHandle;
    AddEntityCallback* addEntityCallback;
    AssetLoadRequestCallback* assetLoadRequestCallback;
    AddComponentCallback* addComponentCallback;
public:
    void RegisterAddEntityCallback(AddEntityCallback callback, void* GCHandle);
    void RegisterAssetLoadRequestCallback(AssetLoadRequestCallback callback, void* GCHandle);
    void RegisterAddComponentCallback(AddComponentCallback callback, void* GCHandle);

    void Process(OpList* op_list);
};

