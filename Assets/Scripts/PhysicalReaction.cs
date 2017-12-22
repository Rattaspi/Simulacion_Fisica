using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalReaction : MonoBehaviour {
    public Vector3D velocidadRecibida;
    public bool active;
    public bool initialHit;
    public float armMass = 10;
    public float sphereMass = 1f;
    public Vector3D linearVelocity1, linearVelocity2;
    public float angularVelocity1, angularVelocity2;
    public float contactTime;

    float[] angles;
    Vector3D[] distances, axis;
    private RobotJoint[] joints;

	// Use this for initialization
	void Start () {
        angles = new float[3];
        distances = new Vector3D[3];
        axis = new Vector3D[3];
        joints = GetComponent<InverseKinematics>().Joints;
        contactTime = 0.1f;
    }
	
	// Update is called once per frame
	void Update () {
        if (active) {
            if (!initialHit) {
                initialHit = true;

                Vector3D F1 = sphereMass * ((-velocidadRecibida) / contactTime);
                angles[0] = Vector3D.Angle(F1, (Vector3D.ToVector3D(joints[joints.Length - 1].gameObject.transform.position)- Vector3D.ToVector3D(joints[joints.Length - 2].gameObject.transform.position)));
                distances[0] = Vector3D.ToVector3D(joints[joints.Length - 1].gameObject.transform.position) - Vector3D.ToVector3D(joints[joints.Length - 2].gameObject.transform.position);
                axis[0] = Vector3D.Cross(F1, distances[0]).Normalized();

                Vector3D F1X = F1 * Mathf.Cos(Mathf.Deg2Rad * angles[0]);
                Vector3D F1Y = F1 * Mathf.Sin(Mathf.Deg2Rad * angles[0]);
                Vector3D acceleration1 = F1Y / armMass;
                linearVelocity1 = acceleration1 * contactTime;
                angularVelocity1 = linearVelocity1.Magnitude() / distances[0].Magnitude();

                //SEGUNDO SEGMENTO DEL BRAZO
                Vector3D F2 = F1X;
                distances[1] = Vector3D.ToVector3D(joints[joints.Length - 2].gameObject.transform.position) - Vector3D.ToVector3D(joints[joints.Length - 3].gameObject.transform.position);
                angles[1] = (Vector3D.Angle(F2, distances[1]));
                axis[1] = Vector3D.Cross(distances[1], F2).Normalized();
                Vector3D F2X = F2 * Mathf.Cos(Mathf.Deg2Rad * angles[1]);
                Vector3D F2Y = F2 * Mathf.Sin(Mathf.Deg2Rad * angles[1]);
                Vector3D acceleration2 = F2Y / armMass;//no se si aqui hay que dividirlo por la masa realmente
                linearVelocity2 = acceleration2 * contactTime;
                angularVelocity2 = linearVelocity1.Magnitude() / distances[0].Magnitude();

            }
            if(angularVelocity1 > 0.001) {
                //joints[joints.Length - 2].gameObject.transform.rotation = Quaternion.AngleAxis(angularVelocity1 * Time.deltaTime, joints[joints.Length-2].Axis.ToVector3()) * joints[joints.Length - 2].gameObject.transform.rotation;
                joints[joints.Length - 2].gameObject.transform.rotation = Quaternion.AngleAxis((angularVelocity1*Mathf.Rad2Deg) * Time.deltaTime, axis[0].ToVector3()) * joints[joints.Length - 2].gameObject.transform.rotation;
                angularVelocity1 -= angularVelocity1 / 30;
                

                //SEGUNDO SEGMENTO DEL BRAZO
                joints[joints.Length - 3].gameObject.transform.rotation = Quaternion.AngleAxis((angularVelocity1 * Mathf.Rad2Deg) * Time.deltaTime, axis[1].ToVector3()) * joints[joints.Length - 3].gameObject.transform.rotation;
                angularVelocity2 -= angularVelocity2 / 30;
            }
        }
	}
}
