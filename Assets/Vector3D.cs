using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3D {
    public float x, y, z, w;
    public Vector3D front = new Vector3D(0, 0, 1);
    public Vector3D up = new Vector3D(0, 1, 0);
    public Vector3D right = new Vector3D(1, 0, 0);
    public Vector3D() {
        x = y = z = w = 0;
    }
    public Vector3D(float n) {
        x = y = z = n;
        w = 0;
    }
    public Vector3D(float x, float y, float z) {
        this.x = x;
        this.y = y;
        this.z = z;
        w = 0;
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
        cross.x = v1.y * v2.z - v2.y * v1.z;
        cross.y = -(v1.x * v2.z - v2.x * v1.z);
        cross.z = v1.x * v2.y - v2.x * v1.y;
        
        return cross;
    }

    public static Vector3D operator + (Vector3D c1, Vector3D c2) {
        return new Vector3D(c1.x + c2.x, c1.y + c2.y, c1.z + c2.z);
    }

    public static Vector3D operator -(Vector3D c1, Vector3D c2) {
        return new Vector3D(c1.x - c2.x, c1.y - c2.y, c1.z - c2.z);
    }

    public static Vector3D operator *(Vector3D c1, Quaternion c2) {
        return new Vector3D();
    }
    public static Vector3D operator *(Quaternion q, Vector3D v) {

        return new Vector3D();
    }
}
