#include "DeploymentListFuture.h"

void DeploymentListFuture::Get(unsigned int* timeout_millis, void* data, DeploymentListCallback callback)
{
    callback(
        data,
        new DeploymentList{
            1,
            new Deployment[]{
                {
                    "d_name",
                    "a_name",
                    "desc"
                }
            },
            nullptr
        }
    );
}
