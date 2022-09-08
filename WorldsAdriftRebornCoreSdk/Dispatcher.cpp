#include "Dispatcher.h"

void Dispatcher::RegisterAddEntityCallback(AddEntityCallback callback, void* GCHandle) { this->addEntityCallback = callback; this->GCHandle = GCHandle; }

void Dispatcher::Process(OpList* op_list) {
    if (op_list != nullptr && op_list->addEntityOp != nullptr) {
        // userptr needs to be set to this dispatcher or you get a nullref in c# land
        this->addEntityCallback(this->GCHandle, op_list->addEntityOp);
    }
}