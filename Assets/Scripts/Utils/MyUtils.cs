using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Finark.Utils
{
    public static class MyUtils
    {

        public static Vector2 GetDirectionVector2(Vector2 target, Vector2 origin)
        {
            return (target - origin).normalized;
        }

        public static Vector3 GetDirectionVector3(Vector3 target, Vector3 origin)
        {
            return (target - origin).normalized;
        }

        // Is Mouse over a UI Element? Used for ignoring World clicks through UI
        public static bool IsPointerOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }

        // Get Mouse Position in World with Z = 0f
        public static Vector3 GetMouseWorldPosition()
        {
            Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
            vec.z = 0f;
            return vec;
        }

        public static Vector3 GetMouseWorldPositionWithZ()
        {
            return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        }

        public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
        {
            return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
        }

        public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
        {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }

        public static float DotProduct(Vector3 firstVector, Vector3 secondVector)
        {
            return Vector3.Dot(firstVector, secondVector);
        }
    }
}
