using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyQuat {
    public float w, x, y, z;
    // Use this for initialization

    public MyQuat(float w,float x, float y, float z) {
        this.w = w;
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public MyQuat() {
        w = 0;
        x = 0;
        y = 0;
        z = 0;
    }

    public static MyQuat Normalize(MyQuat quat) {
        MyQuat temp = new MyQuat();
        float modulo = Mathf.Sqrt(quat.w * quat.w + quat.x * quat.x + quat.y * quat.y + quat.z * quat.z);
        temp = quat;
        temp.w /= modulo;
        temp.x /= modulo;
        temp.y /= modulo;
        temp.z /= modulo;
        return temp;
    }

    public static MyQuat Inverse(MyQuat quat) {

        return new MyQuat(quat.w, -quat.x, -quat.y, -quat.z);
    }

    public static MyQuat Multiply(MyQuat quat1, MyQuat quat2) {

        MyQuat temp = new MyQuat();

        temp.w = quat1.w * quat2.w - quat1.x * quat2.x - quat1.y * quat2.y - quat1.z * quat2.z;
        temp.x = quat1.x * quat2.w + quat1.w * quat2.x + quat1.y * quat2.z - quat1.z * quat2.y;
        temp.y = quat1.w * quat2.y - quat1.x * quat2.z + quat1.y * quat2.w + quat1.z * quat2.x;
        temp.z = quat1.w * quat2.z + quat1.x * quat2.y - quat1.y * quat2.x + quat1.z * quat2.w;
        return temp;
    }

    public static void SetRotation(GameObject a, MyQuat myQuat) {
        Quaternion result;
        result.w = myQuat.w;
        result.x = myQuat.x;
        result.y = myQuat.y;
        result.z = myQuat.z;

        a.transform.rotation = result;
    }

    public void PrintQuad() {
        Debug.Log("w = " + w);
        Debug.Log("x = " + x);
        Debug.Log("y = " + y);
        Debug.Log("z = " + z);
    }

    public static MyQuat EulerToMyQuat(float yaw, float pitch, float roll) 
{
            float rollOver2 = roll * 0.5f;
            float sinRollOver2 = Mathf.Sin(rollOver2);
            float cosRollOver2 = Mathf.Cos(rollOver2);
            float pitchOver2 = pitch * 0.5f;
            float sinPitchOver2 = Mathf.Sin(pitchOver2);
            float cosPitchOver2 = Mathf.Cos(pitchOver2);
            float yawOver2 = yaw * 0.5f;
            float sinYawOver2 = Mathf.Sin(yawOver2);
            float cosYawOver2 = Mathf.Cos(yawOver2);
            MyQuat result = new MyQuat();
            result.x = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2;
            result.y = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2;
            result.z = cosYawOver2 * sinPitchOver2 * cosRollOver2 + sinYawOver2 * cosPitchOver2 * sinRollOver2;
            result.w = sinYawOver2 * cosPitchOver2 * cosRollOver2 - cosYawOver2 * sinPitchOver2 * sinRollOver2;
            return result;
        }


    public static MyQuat AxisAngleToMyQuat(Vector3D axis, float angle) {

        float localAngle = angle * Mathf.Deg2Rad;

        Vector3D temp = axis.Normalized();
        MyQuat result = new MyQuat();
        result.w = Mathf.Cos(localAngle / 2);
        result.x = temp.x * Mathf.Sin(localAngle / 2);
        result.y = temp.y * Mathf.Sin(localAngle / 2);
        result.z = temp.z * Mathf.Sin(localAngle / 2);

        result = Normalize(result);
        return result;
    }

    public static void MyQuatToAxisAngle(MyQuat myQuat,Vector3 axis, float angle) {

        MyQuat temp = temp = Normalize(myQuat);



        angle = Mathf.Rad2Deg * 2 * Mathf.Acos(myQuat.w);
        axis.x = temp.x / Mathf.Sqrt(1 - temp.w * temp.w);
        axis.y = temp.y / Mathf.Sqrt(1 - temp.w * temp.w);
        axis.z = temp.z / Mathf.Sqrt(1 - temp.w * temp.w);
    }


    public static void toEulerAngle(MyQuat q, float yaw, float pitch, float roll)
{
        roll = Mathf.Atan2(q.w * q.y + q.x * q.z, q.w * q.z - q.x * q.y);
        pitch = Mathf.Acos(-q.w * q.w - q.x * q.x + q.y * q.y + q.z * q.z);
        yaw = Mathf.Atan2(q.w * q.y - q.x * q.z, q.x * q.y + q.w * q.z);
    }

}
