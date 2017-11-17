using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3D {
    public float x, y, z;

    public Vector3D() {
        x = y = z = 0;
    }
    public Vector3D(float n) {
        x = y = z = n;
    }
    public Vector3D(float x, float y, float z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3D Normalized() {
        Vector3D vec = new Vector3D();
        float mag = Mathf.Sqrt(x * x + y * y + z * z);
        vec.x = x / mag;
        vec.y = y / mag;
        vec.z = z / mag;
        return vec;
    }

    public static float Dot(Vector3D v1, Vector3D v2) {
        float dotProduct = v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
        return dotProduct;
    }

    public static Vector3D Cross(Vector3D v1, Vector3D v2) {
        Vector3D cross = new Vector3D();
        cross.x = v1.y * v2.z - v1.z * v2.y;
        cross.y = v1.z * v2.x - v1.x * v2.z;
        cross.z = v1.x * v2.y - v1.y * v2.x;
        
        return cross;
    }
}
