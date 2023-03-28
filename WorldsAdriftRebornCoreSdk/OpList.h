#pragma once
#include "Structs.h"

class OpList
{
public:
    AddEntityOp* addEntityOp;
    AssetLoadRequestOp* assetLoadRequestOp;
    AddComponentOp* addComponentOp;
    AuthorityChangeOp* authorityChangeOp;
    ComponentUpdateOp* componentUpdateOp;
    int addComponentLen;
    int authorityChangeOpLen;
    int componentUpdateOpLen;
};