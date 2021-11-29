# UnityTools
个人开发的Unity工具，用于给BoxCollider增加位置为锚点的射线，提供偏移以及自定义射线和射线检测图形显示
## 用法
 ```CSharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhysicsDetection2D;
public class isOnGround : MonoBehaviour
{
  
  private BoxRaycast ray;
  private BoxCollider coll;
  public LayerMask groundLayer;
  
  void Awake(){
    ray = new BoxRaycast();
    coll = GetComponent<BoxCollider>();
  }
  void Update(){
    isOnGround = (
            ray.RaycastAlignToBool(transform.position, coll, 0.2f, Align.downLeft, groundLayer)|
            ray.RaycastAlignToBool(transform.position, coll, 0.2f, Align.downRight, groundLayer)
        );
    // ray.DebugShowRay();
  }
  private void OnDrawGizmos()
  {
        ray?.DebugShowRay();
  }
}
```
