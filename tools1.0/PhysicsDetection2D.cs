using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2D物理检测
namespace PhysicsDetection2D
{
    public enum Align
    {
        top,
        left,
        down,
        right,
        leftTop,
        leftDown,
        rightTop,
        rightDown,
        topLeft,
        topRight,
        downLeft,
        downRight
    };
    public struct DrawRaycastAlign
    {
        public Vector2 from;
        public Vector2 to;
        public float distance;
        public bool tigger;
        public Color? color;
        public Color? tiggerColor;
    };
    // 2D盒子对齐射线
    public class BoxRaycast
    {
        //绘制线条对象
        private List<DrawRaycastAlign> _drawRayList;
        //默认颜色
        private Color? _Color = Color.blue;
        private Color? _tiggerColor = Color.red;
        public BoxRaycast(Color? _color = null, Color? _tiggercolor = null)
        {
            if (_color != null)
                _Color = _color;
            if (_tiggercolor != null)
                _tiggerColor = _tiggercolor;
            _drawRayList = new List<DrawRaycastAlign>();
        }
        //提供自定义选项
        public bool RaycastToBool(Vector2 origin, Vector2 direction, float distance, LayerMask layer, Color? _color = null, Color? _triggercolor = null, bool _debug = true)
        {
            bool result;
            result = (Physics2D.Raycast(origin, direction, distance, layer).collider != null) ? true : false;
            if (_debug)
            {
                DrawRaycastAlign drawRayParam;
                drawRayParam.from = origin;
                drawRayParam.to = direction;
                drawRayParam.distance = distance;
                drawRayParam.color = (_color != null) ? _color : _Color;
                drawRayParam.tiggerColor = (_triggercolor != null) ? _triggercolor : _tiggerColor;
                drawRayParam.tigger = result;
                _drawRayList.Add(drawRayParam);
            }
            return result;
        }
        public bool RaycastToBool(Vector2[] posXY, float distance, LayerMask layer, Color? _color = null, Color? _triggercolor = null, bool _debug = true)
        {
            bool result;
            result = (Physics2D.Raycast(posXY[0], posXY[1], distance, layer).collider != null) ? true : false;
            if (_debug)
            {
                DrawRaycastAlign drawRayParam;
                drawRayParam.from = posXY[0];
                drawRayParam.to = posXY[1];
                drawRayParam.distance = distance;
                drawRayParam.color = (_color != null) ? _color : _Color;
                drawRayParam.tiggerColor = (_triggercolor != null) ? _triggercolor : _tiggerColor;
                drawRayParam.tigger = result;
                _drawRayList.Add(drawRayParam);
            }
            return result;
        }

        // 对齐射线，并将结果转换成布尔
        public bool RaycastAlignToBool(Vector2 position, BoxCollider2D playerCollider, float distance, Align state, LayerMask layer, Color? _color = null, Color? _triggercolor = null, bool _debug = true)
        {



            bool result;
            Vector2[] raycastPos = alignStateToPos(position, playerCollider.bounds.size, playerCollider.offset * playerCollider.transform.localScale, state);
            result = (Physics2D.Raycast(raycastPos[0], raycastPos[1], distance, layer).collider != null) ? true : false;
            if (_debug)
            {
                DrawRaycastAlign drawRayParam;
                drawRayParam.from = raycastPos[0];
                drawRayParam.to = raycastPos[1];
                drawRayParam.distance = distance;
                drawRayParam.color = (_color != null) ? _color : _Color;
                drawRayParam.tiggerColor = (_triggercolor != null) ? _triggercolor : _tiggerColor;
                drawRayParam.tigger = result;
                _drawRayList.Add(drawRayParam);
            }
            return result;
        }
        //对齐射线，提供偏移参数
        public bool RaycastAlignToBool(Vector2 position, BoxCollider2D playerCollider, float distance, float offsetX, float offsetY, Align state, LayerMask layer, Color? _color = null, Color? _triggercolor = null, bool _debug = true)
        {
            bool result;
            Vector2[] raycastPos = alignStateToPos(position, playerCollider.bounds.size, playerCollider.offset * playerCollider.transform.localScale, state);
            raycastPos[0].x += offsetX;
            raycastPos[0].y += offsetY;
            result = (Physics2D.Raycast(raycastPos[0], raycastPos[1], distance, layer).collider != null) ? true : false;
            if (_debug)
            {
                DrawRaycastAlign drawRayParam;
                drawRayParam.from = raycastPos[0];
                drawRayParam.to = raycastPos[1];
                drawRayParam.distance = distance;
                drawRayParam.color = (_color != null) ? _color : _Color;
                drawRayParam.tiggerColor = (_triggercolor != null) ? _triggercolor : _tiggerColor;
                drawRayParam.tigger = result;
                _drawRayList.Add(drawRayParam);
            }
            return result;
        }
        public RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, LayerMask layer, Color? _color = null, Color? _triggercolor = null, bool _debug = true)
        {
            RaycastHit2D result;
            result = Physics2D.Raycast(origin, direction, distance, layer);
            if (_debug)
            {
                DrawRaycastAlign drawRayParam;
                drawRayParam.from = origin;
                drawRayParam.to = direction;
                drawRayParam.distance = distance;
                drawRayParam.color = (_color != null) ? _color : _Color;
                drawRayParam.tiggerColor = (_triggercolor != null) ? _triggercolor : _tiggerColor;
                drawRayParam.tigger = (result.collider != null) ? true : false;
                _drawRayList.Add(drawRayParam);
            }
            return result;
        }
        public RaycastHit2D Raycast(Vector2[] posXY, float distance, LayerMask layer, Color? _color = null, Color? _triggercolor = null, bool _debug = true)
        {
            RaycastHit2D result;
            result = Physics2D.Raycast(posXY[0], posXY[1], distance, layer);
            if (_debug)
            {
                DrawRaycastAlign drawRayParam;
                drawRayParam.from = posXY[0];
                drawRayParam.to = posXY[1];
                drawRayParam.distance = distance;
                drawRayParam.color = (_color != null) ? _color : _Color;
                drawRayParam.tiggerColor = (_triggercolor != null) ? _triggercolor : _tiggerColor;
                drawRayParam.tigger = (result.collider != null) ? true : false;
                _drawRayList.Add(drawRayParam);
            }
            return result;
        }

        public RaycastHit2D RaycastAlign(Vector2 position, BoxCollider2D playerCollider, float distance, Align state, LayerMask layer, Color? _color = null, Color? _triggercolor = null, bool _debug = true)
        {
            RaycastHit2D result;
            Vector2[] raycastPos = alignStateToPos(position, playerCollider.bounds.size, playerCollider.offset * playerCollider.transform.localScale, state);
            result = Physics2D.Raycast(raycastPos[0], raycastPos[1], distance, layer);
            if (_debug)
            {
                DrawRaycastAlign drawRayParam;
                drawRayParam.from = raycastPos[0];
                drawRayParam.to = raycastPos[1];
                drawRayParam.distance = distance;
                drawRayParam.color = (_color != null) ? _color : _Color;
                drawRayParam.tiggerColor = (_triggercolor != null) ? _triggercolor : _tiggerColor;
                drawRayParam.tigger = (result.collider != null) ? true : false;
                _drawRayList.Add(drawRayParam);
            }
            return result;
        }
        public RaycastHit2D RaycastAlign(Vector2 position, BoxCollider2D playerCollider, float distance, float offsetX, float offsetY, Align state, LayerMask layer, Color? _color = null, Color? _triggercolor = null, bool _debug = true)
        {
            RaycastHit2D result;
            Vector2[] raycastPos = alignStateToPos(position, playerCollider.bounds.size, playerCollider.offset * playerCollider.transform.localScale, state);
            raycastPos[0].x += offsetX;
            raycastPos[0].y += offsetY;
            result = Physics2D.Raycast(raycastPos[0], raycastPos[1], distance, layer);
            if (_debug)
            {
                DrawRaycastAlign drawRayParam;
                drawRayParam.from = raycastPos[0];
                drawRayParam.to = raycastPos[1];
                drawRayParam.distance = distance;
                drawRayParam.color = (_color != null) ? _color : _Color;
                drawRayParam.tiggerColor = (_triggercolor != null) ? _triggercolor : _tiggerColor;
                drawRayParam.tigger = (result.collider != null) ? true : false;
                _drawRayList.Add(drawRayParam);
            }
            return result;
        }
        public RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance, LayerMask layer, Color? _color = null, Color? _triggercolor = null, bool _debug = true)
        {
            RaycastHit2D[] result;

            result = Physics2D.RaycastAll(origin, direction, distance, layer);
            if (_debug)
            {
                DrawRaycastAlign drawRayParam;
                drawRayParam.from = origin;
                drawRayParam.to = direction;
                drawRayParam.distance = distance;
                drawRayParam.color = (_color != null) ? _color : _Color;
                drawRayParam.tiggerColor = (_triggercolor != null) ? _triggercolor : _tiggerColor;
                drawRayParam.tigger = (result.Length > 0) ? true : false;
                _drawRayList.Add(drawRayParam);
            }
            return result;
        }
        public RaycastHit2D[] RaycastAll(Vector2[] posXY, float distance, LayerMask layer, Color? _color = null, Color? _triggercolor = null, bool _debug = true)
        {
            RaycastHit2D[] result;
            result = Physics2D.RaycastAll(posXY[0], posXY[1], distance, layer);
            if (_debug)
            {
                DrawRaycastAlign drawRayParam;
                drawRayParam.from = posXY[0];
                drawRayParam.to = posXY[1];
                drawRayParam.distance = distance;
                drawRayParam.color = (_color != null) ? _color : _Color;
                drawRayParam.tiggerColor = (_triggercolor != null) ? _triggercolor : _tiggerColor;
                drawRayParam.tigger = (result.Length > 0) ? true : false;
                _drawRayList.Add(drawRayParam);
            }
            return result;
        }

        public RaycastHit2D[] RaycastAllAlign(Vector2 position, BoxCollider2D playerCollider, float distance, Align state, LayerMask layer, Color? _color = null, Color? _triggercolor = null, bool _debug = true)
        {
            RaycastHit2D[] result;
            Vector2[] raycastPos = alignStateToPos(position, playerCollider.bounds.size, playerCollider.offset * playerCollider.transform.localScale, state);
            result = Physics2D.RaycastAll(raycastPos[0], raycastPos[1], distance, layer);
            if (_debug)
            {
                DrawRaycastAlign drawRayParam;
                drawRayParam.from = raycastPos[0];
                drawRayParam.to = raycastPos[1];
                drawRayParam.distance = distance;
                drawRayParam.color = (_color != null) ? _color : _Color;
                drawRayParam.tiggerColor = (_triggercolor != null) ? _triggercolor : _tiggerColor;
                drawRayParam.tigger = (result.Length > 0) ? true : false;
                _drawRayList.Add(drawRayParam);
            }
            return result;
        }
        public RaycastHit2D[] RaycastAllAlign(Vector2 position, BoxCollider2D playerCollider, float distance, float offsetX, float offsetY, Align state, LayerMask layer, Color? _color = null, Color? _triggercolor = null, bool _debug = true)
        {
            RaycastHit2D[] result;
            Vector2[] raycastPos = alignStateToPos(position, playerCollider.bounds.size, playerCollider.offset * playerCollider.transform.localScale, state);
            raycastPos[0].x += offsetX;
            raycastPos[0].y += offsetY;
            result = Physics2D.RaycastAll(raycastPos[0], raycastPos[1], distance, layer);
            if (_debug)
            {
                DrawRaycastAlign drawRayParam;
                drawRayParam.from = raycastPos[0];
                drawRayParam.to = raycastPos[1];
                drawRayParam.distance = distance;
                drawRayParam.color = (_color != null) ? _color : _Color;
                drawRayParam.tiggerColor = (_triggercolor != null) ? _triggercolor : _tiggerColor;
                drawRayParam.tigger = (result.Length > 0) ? true : false;
                _drawRayList.Add(drawRayParam);
            }
            return result;
        }
        public RaycastHit2D[] RaycastNonAlloc(Vector2 origin, Vector2 direction, float distance, int maxResult, Align state, LayerMask layer, Color? _color = null, Color? _triggercolor = null, bool _debug = true)
        {
            RaycastHit2D[] result = new RaycastHit2D[maxResult];
            Physics2D.RaycastNonAlloc(origin, direction, result, distance, layer);
            if (_debug)
            {
                DrawRaycastAlign drawRayParam;
                drawRayParam.from = origin;
                drawRayParam.to = direction;
                drawRayParam.distance = distance;
                drawRayParam.color = (_color != null) ? _color : _Color;
                drawRayParam.tiggerColor = (_triggercolor != null) ? _triggercolor : _tiggerColor;
                drawRayParam.tigger = (result[0].collider != null) ? true : false;
                _drawRayList.Add(drawRayParam);
            }
            return result;
        }
        public RaycastHit2D[] RaycastNonAlloc(Vector2[] posXY, float distance, int maxResult, Align state, LayerMask layer, Color? _color = null, Color? _triggercolor = null, bool _debug = true)
        {
            RaycastHit2D[] result = new RaycastHit2D[maxResult];
            Physics2D.RaycastNonAlloc(posXY[0], posXY[1], result, distance, layer);
            if (_debug)
            {
                DrawRaycastAlign drawRayParam;
                drawRayParam.from = posXY[0];
                drawRayParam.to = posXY[1];
                drawRayParam.distance = distance;
                drawRayParam.color = (_color != null) ? _color : _Color;
                drawRayParam.tiggerColor = (_triggercolor != null) ? _triggercolor : _tiggerColor;
                drawRayParam.tigger = (result[0].collider != null) ? true : false;
                _drawRayList.Add(drawRayParam);
            }
            return result;
        }

        public RaycastHit2D[] RaycastNonAllocAlign(Vector2 position, BoxCollider2D playerCollider, float distance, int maxResult, Align state, LayerMask layer, Color? _color = null, Color? _triggercolor = null, bool _debug = true)
        {
            RaycastHit2D[] result = new RaycastHit2D[maxResult];
            Vector2[] raycastPos = alignStateToPos(position, playerCollider.bounds.size, playerCollider.offset * playerCollider.transform.localScale, state);
            Physics2D.RaycastNonAlloc(raycastPos[0], raycastPos[1], result, distance, layer);
            if (_debug)
            {
                DrawRaycastAlign drawRayParam;
                drawRayParam.from = raycastPos[0];
                drawRayParam.to = raycastPos[1];
                drawRayParam.distance = distance;
                drawRayParam.color = (_color != null) ? _color : _Color;
                drawRayParam.tiggerColor = (_triggercolor != null) ? _triggercolor : _tiggerColor;
                drawRayParam.tigger = (result[0].collider != null) ? true : false;
                _drawRayList.Add(drawRayParam);
            }
            return result;
        }
        public RaycastHit2D[] RaycastNonAllocAlign(Vector2 position, BoxCollider2D playerCollider, float distance, int maxResult, float offsetX, float offsetY, Align state, LayerMask layer, Color? _color = null, Color? _triggercolor = null, bool _debug = true)
        {
            RaycastHit2D[] result = new RaycastHit2D[maxResult];
            Vector2[] raycastPos = alignStateToPos(position, playerCollider.bounds.size, playerCollider.offset * playerCollider.transform.localScale, state);
            raycastPos[0].x += offsetX;
            raycastPos[0].y += offsetY;
            Physics2D.RaycastNonAlloc(raycastPos[0], raycastPos[1], result, distance, layer);
            if (_debug)
            {
                DrawRaycastAlign drawRayParam;
                drawRayParam.from = raycastPos[0];
                drawRayParam.to = raycastPos[1];
                drawRayParam.distance = distance;
                drawRayParam.color = (_color != null) ? _color : _Color;
                drawRayParam.tiggerColor = (_triggercolor != null) ? _triggercolor : _tiggerColor;
                drawRayParam.tigger = (result[0].collider != null) ? true : false;
                _drawRayList.Add(drawRayParam);
            }
            return result;
        }
        public static Vector2 getAlignToPos(Transform transform, BoxCollider2D collider, Align align)
        {
            return alignStateToPos(transform.position, collider.size, collider.offset, align)[0];
        }
        public static Vector2[] getAlignToPosXY(Transform transform, BoxCollider2D collider, Align align)
        {
            return alignStateToPos(transform.position, collider.size, collider.offset, align);
        }
        public static Vector2[] getAlignToPosXY(Transform transform, BoxCollider2D collider, float offsetX, float offsetY, Align align)
        {
            Vector2[] result = alignStateToPos(transform.position, collider.size, collider.offset, align);
            result[0].x += offsetX;
            result[0].y += offsetY;
            return result;
        }

        public static float getAlignToPosX(Transform transform, BoxCollider2D collider, Align align)
        {
            return alignStateToPos(transform.position, collider.size, collider.offset, align)[0].x;
        }
        public static float getAlignToPosY(Transform transform, BoxCollider2D collider, Align align)
        {
            return alignStateToPos(transform.position, collider.size, collider.offset, align)[0].y;
        }
        public static Vector2 getAlignToPosVX(Transform transform, BoxCollider2D collider, Align align)
        {
            return new Vector2(alignStateToPos(transform.position, collider.size, collider.offset, align)[0].x, 0);
        }
        public static Vector2 getAlignToPosVY(Transform transform, BoxCollider2D collider, Align align)
        {
            return new Vector2(0, alignStateToPos(transform.position, collider.size, collider.offset, align)[0].y);
        }
        // 居中状态转位置
        private static Vector2[] alignStateToPos(Vector2 position, Vector2 colliderSize, Vector2 colliderOffset, Align state)
        {
            Vector2[] result = new Vector2[2];
            switch (state)
            {
                case Align.top:
                    result[0] = new Vector2(position.x + (colliderOffset.x), position.y + (colliderSize.y / 2) + (colliderOffset.y));
                    result[1] = Vector2.up;
                    break;
                case Align.left:
                    result[0] = new Vector2(position.x - (colliderSize.x / 2) + (colliderOffset.x), position.y + (colliderOffset.y));
                    result[1] = Vector2.left;
                    break;
                case Align.down:
                    result[0] = new Vector2(position.x + (colliderOffset.x), position.y - (colliderSize.y / 2) + (colliderOffset.y));
                    result[1] = Vector2.down;
                    break;
                case Align.right:
                    result[0] = new Vector2(position.x + (colliderSize.x / 2) + (colliderOffset.x), position.y + (colliderOffset.y));
                    result[1] = Vector2.right;
                    break;
                case Align.leftTop:
                    result[0] = new Vector2(position.x - (colliderSize.x / 2) + (colliderOffset.x), position.y + (colliderSize.y / 2) + (colliderOffset.y));
                    result[1] = Vector2.left;
                    break;
                case Align.leftDown:
                    result[0] = new Vector2(position.x - (colliderSize.x / 2) + (colliderOffset.x), position.y - (colliderSize.y / 2) + (colliderOffset.y));
                    result[1] = Vector2.left;
                    break;
                case Align.rightTop:
                    result[0] = new Vector2(position.x + (colliderSize.x / 2) + (colliderOffset.x), position.y + (colliderSize.y / 2) + (colliderOffset.y));
                    result[1] = Vector2.right;
                    break;
                case Align.rightDown:
                    result[0] = new Vector2(position.x + (colliderSize.x / 2) + (colliderOffset.x), position.y - (colliderSize.y / 2) + (colliderOffset.y));
                    result[1] = Vector2.right;
                    break;
                case Align.topLeft:
                    result[0] = new Vector2(position.x - (colliderSize.x / 2) + (colliderOffset.x), position.y + (colliderSize.y / 2) + (colliderOffset.y));
                    result[1] = Vector2.up;
                    break;
                case Align.topRight:
                    result[0] = new Vector2(position.x + (colliderSize.x / 2) + (colliderOffset.x), position.y + (colliderSize.y / 2) + (colliderOffset.y));
                    result[1] = Vector2.up;
                    break;
                case Align.downLeft:
                    result[0] = new Vector2(position.x - (colliderSize.x / 2) + (colliderOffset.x), position.y - (colliderSize.y / 2) + (colliderOffset.y));
                    result[1] = Vector2.down;
                    break;
                case Align.downRight:
                    result[0] = new Vector2(position.x + (colliderSize.x / 2) + (colliderOffset.x), position.y - (colliderSize.y / 2) + (colliderOffset.y));
                    result[1] = Vector2.down;
                    break;
            }
            return result;
        }
        public void GizmosShowRay([System.Runtime.CompilerServices.CallerMemberName] string member = "")
        {
            //如果调用方法不是应该调用的地方
            if (!member.Equals("OnDrawGizmos") && !member.Equals("OnDrawGizmosSelected"))
            {
                Debug.LogError("该函数只能在OnDrawGizmosSelected和OnDrawGizmos中使用，如要在Update类函数中使用请用DebugShowRay()");
                return;
            }
            if (_drawRayList == null)
                return;
            foreach (DrawRaycastAlign drawRayParam in _drawRayList)
            {
                Gizmos.color = (Color)((drawRayParam.tigger) ? drawRayParam.tiggerColor : drawRayParam.color);
                Gizmos.DrawRay(drawRayParam.from, drawRayParam.to * drawRayParam.distance);

            }
            _drawRayList = new List<DrawRaycastAlign>();
        }
        public void DebugShowRay()
        {
            if (_drawRayList == null)
                return;
            foreach (DrawRaycastAlign drawRayParam in _drawRayList)
            {
                Debug.DrawRay(drawRayParam.from, drawRayParam.to * drawRayParam.distance,
                    (Color)((drawRayParam.tigger) ? drawRayParam.tiggerColor : drawRayParam.color), drawRayParam.distance);
            }
            _drawRayList = new List<DrawRaycastAlign>();
        }
    }
}