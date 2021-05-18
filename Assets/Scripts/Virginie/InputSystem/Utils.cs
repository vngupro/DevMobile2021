using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector3 ScreenToWorld(Camera cam, Vector2 position)
    {
        Vector3 newPos = cam.ScreenToWorldPoint(position);
        newPos.z = cam.nearClipPlane;
        return newPos;
    }
}
