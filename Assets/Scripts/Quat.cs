using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Quat {
    public float w, x, y, z;
    // Use this for initialization

    public Quat(float w,float x, float y, float z) {
        this.w = w;
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Quat(Quaternion q) {
        w = q.w;
        x = q.x;
        y = q.y;
        z = q.z;
    }

    public Quat() {
        w = 0;
        x = 0;
        y = 0;
        z = 0;
    }

    public static Quat Normalize(Quat quat) {
        Quat temp = new Quat();
        float modulo = Mathf.Sqrt(quat.w * quat.w + quat.x * quat.x + quat.y * quat.y + quat.z * quat.z);
        temp = quat;
        temp.w /= modulo;
        temp.x /= modulo;
        temp.y /= modulo;
        temp.z /= modulo;
        return temp;
    }

    public static Quat Inverse(Quat quat) {
        return new Quat(quat.w, -quat.x, -quat.y, -quat.z);
    }

    public static Quat Multiply(Quat quat1, Quat quat2) {
        Quat temp = new Quat();

        temp.w = quat1.w * quat2.w - quat1.x * quat2.x - quat1.y * quat2.y - quat1.z * quat2.z;
        temp.x = quat1.x * quat2.w + quat1.w * quat2.x + quat1.y * quat2.z - quat1.z * quat2.y;
        temp.y = quat1.w * quat2.y - quat1.x * quat2.z + quat1.y * quat2.w + quat1.z * quat2.x;
        temp.z = quat1.w * quat2.z + quat1.x * quat2.y - quat1.y * quat2.x + quat1.z * quat2.w;
        return temp;
    }

    public static void SetRotation(GameObject a, Quat myQuat) {
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

    public static Quat EulerToMyQuat(float yaw, float pitch, float roll){
            float rollOver2 = roll * 0.5f;
            float sinRollOver2 = Mathf.Sin(rollOver2);
            float cosRollOver2 = Mathf.Cos(rollOver2);
            float pitchOver2 = pitch * 0.5f;
            float sinPitchOver2 = Mathf.Sin(pitchOver2);
            float cosPitchOver2 = Mathf.Cos(pitchOver2);
            float yawOver2 = yaw * 0.5f;
            float sinYawOver2 = Mathf.Sin(yawOver2);
            float cosYawOver2 = Mathf.Cos(yawOver2);
            Quat result = new Quat();
            result.x = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2;
            result.y = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2;
            result.z = cosYawOver2 * sinPitchOver2 * cosRollOver2 + sinYawOver2 * cosPitchOver2 * sinRollOver2;
            result.w = sinYawOver2 * cosPitchOver2 * cosRollOver2 - cosYawOver2 * sinPitchOver2 * sinRollOver2;
            return result;
        }


    public static Quat AxisAngleToMyQuat(Vector3D axis, float angle) {
        float localAngle = angle * Mathf.Deg2Rad;
        Vector3D temp = axis.Normalized();
        Quat result = new Quat();
        result.w = Mathf.Cos(localAngle / 2);
        result.x = temp.x * Mathf.Sin(localAngle / 2);
        result.y = temp.y * Mathf.Sin(localAngle / 2);
        result.z = temp.z * Mathf.Sin(localAngle / 2);

        result = Normalize(result);
        return result;
    }

    public static void MyQuatToAxisAngle(Quat myQuat,Vector3 axis, float angle) {
        Quat temp = temp = Normalize(myQuat);
        angle = Mathf.Rad2Deg * 2 * Mathf.Acos(myQuat.w);
        axis.x = temp.x / Mathf.Sqrt(1 - temp.w * temp.w);
        axis.y = temp.y / Mathf.Sqrt(1 - temp.w * temp.w);
        axis.z = temp.z / Mathf.Sqrt(1 - temp.w * temp.w);
    }


    public static void toEulerAngle(Quat q, float yaw, float pitch, float roll){
        roll = Mathf.Atan2(q.w * q.y + q.x * q.z, q.w * q.z - q.x * q.y);
        pitch = Mathf.Acos(-q.w * q.w - q.x * q.x + q.y * q.y + q.z * q.z);
        yaw = Mathf.Atan2(q.w * q.y - q.x * q.z, q.x * q.y + q.w * q.z);
    }

}
