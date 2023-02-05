#pragma once
#include "Structs.h"

class OpList
{
public:
    AddEntityOp* addEntityOp;
    AssetLoadRequestOp* assetLoadRequestOp;
    AddComponentOp* addComponentOp;
    AuthorityChangeOp* authorityChangeOp;
    int addComponentLen;
    int authorityChangeOpLen;
};