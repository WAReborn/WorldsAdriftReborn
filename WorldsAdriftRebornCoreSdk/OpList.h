#pragma once
#include "Structs.h"

class OpList
{
public:
    AddEntityOp* addEntityOp;
    AssetLoadRequestOp* assetLoadRequestOp;
    AddComponentOp* addComponentOp;
    int addComponentLen;
};