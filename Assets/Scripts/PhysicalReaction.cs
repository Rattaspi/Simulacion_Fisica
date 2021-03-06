﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalReaction : MonoBehaviour {
    public float elasticityC;
    public float contactTime;
    public float mechanichRes;
    public GameObject Emisor; //the sphere
    public Vector3D velocidadRecibida;
    public bool active;
    public bool initialHit;
    public float armMass = 10;
    public float sphereMass = 1f;
    public Vector3D linearVelocity1, linearVelocity2, linearVelocity3;
    public float angularVelocity1, angularVelocity2, angularVelocity3;
    public Vector3D localVelocityInspector;
    public SimpleLineRendering torque1Render;
    public SimpleLineRendering torque2Render;
    public SimpleLineRendering torque3Render;
    Vector3D []cross1;
    Vector3D []cross2;
    public SimpleLineRendering decomposedForces1;
    public SimpleLineRendering decomposedForces2;

    public Transform endeffector;


    float[] angles;
    Vector3D[] distances, axis;
    private RobotJoint[] joints; //length 4
    Vector3D[] forces;
    public static bool drawForces;
    float timeToDrawForces = 0.5f;
    float drawForcesTimer;
    // Use this for initialization
    LineRenderer HitForceRender;

    void Start () {
        drawForces = true;
        cross1 = new Vector3D[3];
        cross2 = new Vector3D[3];
        angles = new float[3];
        distances = new Vector3D[3];
        axis = new Vector3D[3];
        joints = GetComponent<InverseKinematics>().Joints;
        contactTime = 0.1f;
        mechanichRes = 0.25f;
        elasticityC = 0;
        forces = new Vector3D[3];
        drawForcesTimer = 0;

    }

    public void SetDraw(bool b) {
        drawForces = b;
    }

    public void SetArmMass(float m) {
        armMass = m;
    }

    public void SetSphereMass(float m) {
        sphereMass = m;
        Emisor.GetComponent<GamePhysics>().sphereMass = m;
    }

	// Update is called once per frame
	void Update () {
        if(Emisor!=null)
        localVelocityInspector = Vector3D.ToVector3D(Emisor.GetComponent<GamePhysics>().velocityInspector);

        if (drawForces && forces[0] != null) {
            //Debug.Log(Emisor.gameObject.transform.position);
            //Debug.Log(forces[0]);
            //Debug.DrawLine(Emisor.gameObject.transform.position, ((new Vector3D(Emisor.gameObject.transform.position) + forces[0] / forces[0].Magnitude() * 25).ToVector3()), Color.red);


            if (HitForceRender == null) {
                HitForceRender = gameObject.AddComponent<LineRenderer>();
            }
            //HitForceRender.material = new Material(Shader.Find("Particles/Additive"));
            HitForceRender.material = Resources.Load<Material>("redMaterial");
            HitForceRender.widthMultiplier = 0.2f;
            HitForceRender.positionCount = 2;
            Vector3D[] posiciones = new Vector3D[2] {Vector3D.ToVector3D(endeffector.position), new Vector3D(endeffector.position) + forces[0] / forces[0].Magnitude() * 25 };

            HitForceRender.SetPositions(new Vector3[2] {posiciones[0].ToVector3(),posiciones[1].ToVector3()});
            HitForceRender.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            HitForceRender.startColor = new Color(255, 0, 0);
            HitForceRender.endColor = new Color(255, 0, 0);

            torque1Render.position1 = Vector3D.ToVector3D(torque1Render.gameObject.transform.position);
            torque1Render.position2 = (torque1Render.position1 - axis[0])*0.5f;
            torque1Render.elColor = SimpleLineRendering.colore.verde;

            torque2Render.position1 = Vector3D.ToVector3D(torque2Render.gameObject.transform.position);
            torque2Render.position2 = torque2Render.position1 - axis[1] * 5;
            torque2Render.elColor = SimpleLineRendering.colore.verde;

            torque3Render.position1 = Vector3D.ToVector3D(torque3Render.gameObject.transform.position);
            torque3Render.position2 = torque3Render.position1 - axis[2] * 1000;
            torque3Render.elColor = SimpleLineRendering.colore.verde;



            torque1Render.Go();
            torque2Render.Go();
            torque3Render.Go();

            //torque1Render.position1 = new Vector3D(1,0,0);

            drawForcesTimer += Time.deltaTime;
            if (drawForcesTimer > timeToDrawForces) {
                drawForces = false;
            }
        } else {
            if (HitForceRender != null) {
                Destroy(HitForceRender);
            }

        }


        //Debug.Log(mechanichRes);
        if (active) {
            if (!initialHit) {
                initialHit = true;
                Vector3D F1 = sphereMass * ((-velocidadRecibida * (1 - elasticityC)) / contactTime) * (1 - armMass / (armMass + sphereMass));

                forces[0] = F1;
                forces[0].x *= -1;
                angles[0] = Vector3D.Angle(F1, (Vector3D.ToVector3D(joints[joints.Length - 1].gameObject.transform.position)- Vector3D.ToVector3D(joints[joints.Length - 2].gameObject.transform.position)));
                distances[0] = Vector3D.ToVector3D(joints[joints.Length - 1].gameObject.transform.position) - Vector3D.ToVector3D(joints[joints.Length - 2].gameObject.transform.position);
                axis[0] = Vector3D.Cross(distances[0].Normalized(),F1);

                Vector3D F1X = F1 * Mathf.Cos(Mathf.Deg2Rad * angles[0]);
                Vector3D F1Y = F1 * Mathf.Sin(Mathf.Deg2Rad * angles[0]);
                Vector3D acceleration1 = F1Y / armMass;
                linearVelocity1 = acceleration1 * contactTime;
                angularVelocity1 = linearVelocity1.Magnitude() / distances[0].Magnitude();

                //SEGUNDO SEGMENTO DEL BRAZO
                Vector3D F2 = F1X;
                forces[1] = F1X;
                distances[1] = Vector3D.ToVector3D(joints[joints.Length - 2].gameObject.transform.position) - Vector3D.ToVector3D(joints[joints.Length - 3].gameObject.transform.position);
                angles[1] = (Vector3D.Angle(F2, distances[1]));
                axis[1] = Vector3D.Cross(distances[1], F2).Normalized();
                Vector3D F2X = F2 * Mathf.Cos(Mathf.Deg2Rad * angles[1]);
                Vector3D F2Y = F2 * Mathf.Sin(Mathf.Deg2Rad * angles[1]);
                Vector3D acceleration2 = F2Y / armMass;//no se si aqui hay que dividirlo por la masa realmente
                linearVelocity2 = acceleration2 * contactTime;
                angularVelocity2 = linearVelocity2.Magnitude() / distances[1].Magnitude();

                //TERCER SEGMENTO DEL BRAZO
                Vector3D F3 = new Vector3D(0, F1.y, 0);
                forces[2] = F3;
                distances[2] = Vector3D.ToVector3D(joints[0].transform.position) - Vector3D.ToVector3D(Emisor.transform.position);
                distances[2].x = 0;
                angles[2] = Vector3D.Angle(F3, distances[2]);
                axis[2] = Vector3D.Cross(distances[2], F3);
                F3.y = F3.y * Mathf.Sin(angles[2]);
                Vector3D acceleration3 = F3 / armMass;
                linearVelocity3 = acceleration3 * contactTime;
                angularVelocity3 = linearVelocity3.Magnitude() / distances[2].Magnitude(); 

            }

            if (angularVelocity1 > 0.001) {
                Vector3D vector1,vector2;
                vector1 = Vector3D.ToVector3D(endeffector.transform.position)-Vector3D.ToVector3D(joints[2].transform.position) ;
                vector2 = Vector3D.ToVector3D(joints[1].transform.position) - Vector3D.ToVector3D(joints[2].transform.position);
                //Debug.Log(Vector3D.Angle(vector1, vector2));
                if (Vector3D.Angle(vector1, vector2) > 90.0f){
                    // joints[joints.Length - 2].gameObject.transform.rotation = Quaternion.AngleAxis((angularVelocity1*Mathf.Rad2Deg) * Time.deltaTime, axis[0].ToVector3()) * joints[joints.Length - 2].gameObject.transform.rotation;
                    joints[joints.Length - 2].gameObject.transform.rotation = (Quat.AxisAngleToMyQuat(axis[0], (angularVelocity1 * Mathf.Rad2Deg) * Time.deltaTime) * Quat.toQuat(joints[joints.Length - 2].gameObject.transform.rotation)).ToQuaternion();
                    angularVelocity1 -= angularVelocity1 * Time.deltaTime * 4 * mechanichRes;
                }
            }

            if(angularVelocity2 > 0.001f) {
                Vector3D vector1, vector2;
                vector1 = Vector3D.ToVector3D(joints[2].transform.position) - Vector3D.ToVector3D(joints[1].transform.position);
                vector2 = Vector3D.ToVector3D(joints[0].transform.position) - Vector3D.ToVector3D(joints[1].transform.position);
                //SEGUNDO SEGMENTO DEL BRAZO
                //joints[joints.Length - 3].gameObject.transform.rotation = Quaternion.AngleAxis((angularVelocity2 * Mathf.Rad2Deg) * Time.deltaTime, axis[1].ToVector3()) * joints[joints.Length - 3].gameObject.transform.rotation;
                if (Vector3D.Angle(vector1, vector2) > 90.0f) {
                    joints[joints.Length - 3].gameObject.transform.rotation = (Quat.AxisAngleToMyQuat(axis[1], (angularVelocity2 * Mathf.Rad2Deg) * Time.deltaTime) * Quat.toQuat(joints[joints.Length - 3].gameObject.transform.rotation)).ToQuaternion();
                    angularVelocity2 -= (angularVelocity2 * Time.deltaTime) * 4 * mechanichRes;
                }
            }

            if(angularVelocity3 > 0.001f) {
                //TERCER SEGMENTO DEL BRAZO
                joints[0].transform.rotation = (Quat.AxisAngleToMyQuat(axis[2], (angularVelocity3 * Mathf.Rad2Deg) * Time.deltaTime) * Quat.toQuat(joints[0].transform.rotation)).ToQuaternion();
                angularVelocity3 -= (angularVelocity3 * Time.deltaTime) * 4 * mechanichRes;
            }

            if (drawForces && forces[0] != null) {

                cross1[0] = Vector3D.Cross(Vector3D.ToVector3D(joints[3].transform.position) - Vector3D.ToVector3D(joints[2].transform.position), forces[0]);
                cross2[0] = Vector3D.Cross(cross1[0], Vector3D.ToVector3D(joints[3].transform.position) - Vector3D.ToVector3D(joints[2].transform.position));
                decomposedForces1.position1 = Vector3D.ToVector3D(endeffector.gameObject.transform.position);
                decomposedForces1.position2 = cross2[0];
                //decomposedForces1.SetUp(new Color(255, 0, 0));
                decomposedForces1.elColor = SimpleLineRendering.colore.rojo;
                decomposedForces1.Go();

                cross1[1] = Vector3D.Cross(Vector3D.ToVector3D(joints[2].transform.position) - Vector3D.ToVector3D(joints[1].transform.position), forces[1]);
                cross2[1] = Vector3D.Cross(cross1[1], Vector3D.ToVector3D(joints[2].transform.position) - Vector3D.ToVector3D(joints[1].transform.position));

                decomposedForces2.position1 = Vector3D.ToVector3D(joints[2].gameObject.transform.position);
                decomposedForces2.position2 = cross2[1];
                decomposedForces2.elColor = SimpleLineRendering.colore.rojo;
                //decomposedForces2.SetUp(new Color(255, 0, 0));
                decomposedForces2.Go();

            }


        }

	}

    public void SetContactTime(float a) {
        contactTime = a;
    }

    public void SetElasticityC(float a) {
        elasticityC = a;
    }

    public void SetMechRes(float a) {
        mechanichRes = a;
    }

    public void OnDestroy() {
        PermanentData.instance.SaveValues(localVelocityInspector.x,elasticityC, contactTime,mechanichRes,armMass,sphereMass);
    }
}
