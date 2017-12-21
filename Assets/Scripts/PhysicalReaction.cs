using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalReaction : MonoBehaviour {
    public Vector3D velocidadRecibida;
    public bool active;
    public bool initialHit;
    public float mass;
    public float sphereMass;
    public Vector3D linearVelocity1;
    public float angularVelocity1;
    public float contactTime;

    float[] angles;
    Vector3D[] distances;
	// Use this for initialization
	void Start () {
        angles = new float[3];
        distances = new Vector3D[3];
	}
	
	// Update is called once per frame
	void Update () {
        if (active) {
            if (!initialHit) {
                initialHit = true;

                Vector3D F1 = sphereMass * (-velocidadRecibida) / contactTime;
                angles[0] = (Vector3D.Angle(F1, (Vector3D.ToVector3D(GetComponent<InverseKinematics>().Joints[GetComponent<InverseKinematics>().Joints.Length - 1].gameObject.transform.position)- Vector3D.ToVector3D(GetComponent<InverseKinematics>().Joints[GetComponent<InverseKinematics>().Joints.Length - 2].gameObject.transform.position))));
                distances[0] = (Vector3D.ToVector3D(GetComponent<InverseKinematics>().Joints[GetComponent<InverseKinematics>().Joints.Length - 1].gameObject.transform.position) - Vector3D.ToVector3D(GetComponent<InverseKinematics>().Joints[GetComponent<InverseKinematics>().Joints.Length - 2].gameObject.transform.position));

                Vector3D F1X = F1 * Mathf.Cos(Mathf.Deg2Rad * angles[0]);
                Vector3D F1Y = F1 * Mathf.Sin(Mathf.Deg2Rad * angles[0]);
                Vector3D acceleration = F1Y / mass;
                linearVelocity1 = acceleration * contactTime;
                //angularVelocity1 = linearVelocity1.Magnitude() / distances[0];


                //angles[1] = (Vector3D.Angle(F1X, (Vector3D.ToVector3D(GetComponent<ENTICourse.IK.InverseKinematics>().Joints[GetComponent<ENTICourse.IK.InverseKinematics>().Joints.Length - 1].gameObject.transform.position) - Vector3D.ToVector3D(GetComponent<ENTICourse.IK.InverseKinematics>().Joints[GetComponent<ENTICourse.IK.InverseKinematics>().Joints.Length - 2].gameObject.transform.position))));

            }




        }
	}
}
