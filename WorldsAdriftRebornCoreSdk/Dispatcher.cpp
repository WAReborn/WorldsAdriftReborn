#include "Dispatcher.h"
#include <corecrt_malloc.h>

void Dispatcher::RegisterAddEntityCallback(AddEntityCallback callback, void* GCHandle) { this->addEntityCallback = callback; this->GCHandle = GCHandle; }
void Dispatcher::RegisterAssetLoadRequestCallback(AssetLoadRequestCallback callback, void* GCHandle) { this->assetLoadRequestCallback = callback; this->GCHandle = GCHandle; }
void Dispatcher::RegisterAddComponentCallback(AddComponentCallback callback, void* GCHandle) { this->addComponentCallback = callback; this->GCHandle = GCHandle; }

void Dispatcher::Process(OpList* op_list) {
    if (op_list != nullptr && op_list->addEntityOp != nullptr) {
        // userptr needs to be set to this dispatcher or you get a nullref in c# land
        this->addEntityCallback(this->GCHandle, op_list->addEntityOp);
    }
    if (op_list != nullptr && op_list->assetLoadRequestOp != nullptr) {
        this->assetLoadRequestCallback(this->GCHandle, op_list->assetLoadRequestOp);

        //free(op_list->assetLoadRequestOp->AssetType);
        //free(op_list->assetLoadRequestOp->Name);
        //free(op_list->assetLoadRequestOp->Context);

        //delete op_list->assetLoadRequestOp;
    }
    if (op_list != nullptr && op_list->addComponentOp != nullptr) {
        for (int i = 0; i < op_list->addComponentLen; i++) {
            // need to copy over here because the EntityId is garbage in c# land if not done.
            AddComponentOp* op = new AddComponentOp();
            op->EntityId = op_list->addComponentOp[i].EntityId;
            op->InitialComponent = op_list->addComponentOp[i].InitialComponent;

            this->addComponentCallback(this->GCHandle, op);

            delete op;
        }
    }
}