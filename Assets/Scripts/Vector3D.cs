﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Vector3D {
    public float x, y, z, w;
    public static Vector3D front = new Vector3D(0,0,1);
    public static Vector3D up = new Vector3D(0,1,0) ;
    public static Vector3D right = new Vector3D(1,0,0);

    // overload operator +
    public static Vector3D operator +(Vector3D rhs, Vector3D lhs) {
        return new Vector3D(rhs.x + lhs.x, rhs.y + lhs.y, rhs.z + lhs.z);
    }
    // overload operator -
    public static Vector3D operator -(Vector3D rhs, Vector3D lhs) {
        return rhs + new Vector3D(-lhs.x,-lhs.y,-lhs.z);
    }

    public static Vector3D operator /(Vector3D rhs, float lhs) {
        return new Vector3D(-rhs.x / lhs, -rhs.y / lhs, -rhs.z / lhs);
    }

    public static Vector3D operator *(Vector3D rhs, float lhs) {
        return new Vector3D(-rhs.x * lhs, -rhs.y * lhs, -rhs.z * lhs);
    }

    public static Vector3D operator *(float lhs, Vector3D rhs) {
        return new Vector3D(-rhs.x * lhs, -rhs.y * lhs, -rhs.z * lhs);
    }

    public static Vector3D operator -(Vector3D rhs) {
        return new Vector3D(-rhs.x, -rhs.y, -rhs.z);
    }

    public Vector3D() {
        x = y = z = w = 0;
    }
    public Vector3D(Vector3 a) {
        x = a.x;
        y = a.y;
        z = a.z;
        w = 0;

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

    static public float Angle(Vector3D v1, Vector3D v2) {
        
        float sin = (Cross(v1, v2)).Magnitude() / (v2.Magnitude() * v1.Magnitude());
        float cos = (Dot(v1, v2)) / (v2.Magnitude() * v1.Magnitude());
        return Mathf.Rad2Deg * Mathf.Atan2(sin, cos);
    }

    public float Magnitude() {
        return Mathf.Sqrt(x * x + y * y + z * z);
    }

    public Vector3D Normalized() {
        Vector3D vec = new Vector3D();
        float mag = Magnitude();
        vec.x = x / mag;
        vec.y = y / mag;
        vec.z = z / mag;
        return vec;
    }

    static public float Distance(Vector3D a, Vector3D b) {
        Vector3D vec = b-a;

        return Mathf.Sqrt(((vec.x * vec.x) + (vec.y * vec.y) + (vec.z * vec.z)));
    }

    public static float Dot(Vector3D v1, Vector3D v2) {
        float dotProduct = v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
        return dotProduct;
    }

    public static Vector3D Cross(Vector3D v1, Vector3D v2) {
        Vector3D cross = new Vector3D {
            x = v1.y * v2.z - v2.y * v1.z,
            y = -(v1.x * v2.z - v2.x * v1.z),
            z = v1.x * v2.y - v2.x * v1.y
        };
        return cross;
    }

    public void Set(float x, float y, float z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public void Print() {
        Debug.Log("(" + x + "," + y + "," + z + ")");
    }

    public static Vector3D ToVector3D(Vector3 a) {
        Vector3D temp = new Vector3D();
        temp.x = a.x;
        temp.y = a.y;
        temp.z = a.z;

        return temp;
    }
    public Vector3 ToVector3() {
        Vector3 temp = new Vector3();
        temp.x = x;
        temp.y = y;
        temp.z = z;

        return temp;
    }


    /*
     * Sobrecarga de operadores 
     */

    public static Vector3D operator *(Quat q, Vector3D vec) {
        Quat quat;
        quat = q;

        float num = quat.x * 2f;
        float num2 = quat.y * 2f;
        float num3 = quat.z * 2f;
        float num4 = quat.x * num;
        float num5 = quat.y * num2;
        float num6 = quat.z * num3;
        float num7 = quat.x * num2;
        float num8 = quat.x * num3;
        float num9 = quat.y * num3;
        float num10 = quat.w * num;
        float num11 = quat.w * num2;
        float num12 = quat.w * num3;
        Vector3D result = new Vector3D();
        result.x = (1f - (num5 + num6)) * vec.x + (num7 - num12) * vec.y + (num8 + num11) * vec.z;
        result.y = (num7 + num12) * vec.x + (1f - (num4 + num6)) * vec.y + (num9 - num10) * vec.z;
        result.z = (num8 - num11) * vec.x + (num9 + num10) * vec.y + (1f - (num4 + num5)) * vec.z;
        return result;
    }
}
