using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // A typical error function to minimise
    public delegate float ErrorFunction(Vector3D target, float[] solution);
    public struct PositionRotation
    {
        Vector3D position;
        Quat rotation;

        public PositionRotation(Vector3D position, Quat rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }

        // PositionRotation to Vector3
        public static implicit operator Vector3D(PositionRotation pr)
        {
            return pr.position;
        }
        // PositionRotation to Quaternion
        public static implicit operator Quat(PositionRotation pr)
        {
            return pr.rotation;
        }
    }

//[ExecuteInEditMode]
public class InverseKinematics : MonoBehaviour {
    public GameObject caja;
    bool unPaused = false;
    public bool useCalculatedPos;

    public bool move = true;
    public int iterationsPerFrame;

    [Header("Joints")]
    public Transform BaseJoint;


    [SerializeField]
    public RobotJoint[] Joints = null;
    // The current angles
    [SerializeField]
    public float[] Solution = null;

    [Header("Destination")]
    public Transform Effector;
    [Space]
    public Transform Destination;
    public float DistanceFromDestination;
    private Vector3D target;

    [Header("Inverse Kinematics")]
    [Range(0, 1f)]
    public float DeltaGradient = 0.1f; // Used to simulate gradient (degrees)
    [Range(0, 100f)]
    public float LearningRate = 0.1f; // How much we move depending on the gradient

    [Space()]
    [Range(0, 10f)]
    public float StopThreshold = 0.1f; // If closer than this, it stops
    [Range(0, 10f)]
    public float SlowdownThreshold = 0.25f; // If closer than this, it linearly slows down

    GamePhysics gamePhysics;

    public ErrorFunction ErrorFunction;



    [Header("Debug")]
    public bool DebugDraw = true;



    // Use this for initialization
    void Start() {
        if (Joints == null)
            GetJoints();

        gamePhysics = Destination.gameObject.GetComponent<GamePhysics>();

        ErrorFunction = DistanceFromTarget;
        float t = (30 - (Destination.position).x) / gamePhysics.velocity.x;

        float y = Destination.position.y + gamePhysics.velocity.y * t + ((gamePhysics.gLuna * t * t) / 2);
        //Debug.Log(y);
        target = new Vector3D(30, y, Destination.position.z);
        caja.transform.position = Joints[Joints.Length - 1].gameObject.transform.position;

    }

    public void GetJoints() {
        Joints = BaseJoint.GetComponentsInChildren<RobotJoint>();
        Solution = new float[Joints.Length];
    }



    // Update is called once per frame
    void Update() {
        if (!FindObjectOfType<GameLogic>().paused) {
            if (!unPaused && Destination.GetComponent<GamePhysics>().done) {
                unPaused = true;
                float t = (30 - Vector3D.ToVector3D(Destination.position).x) / gamePhysics.velocity.x;

                float y = Destination.position.y + gamePhysics.velocity.y * t + ((gamePhysics.gLuna * t * t) / 2);
                //Debug.Log(y);
                target = new Vector3D(30, y, Destination.position.z);
                    caja.transform.position = Joints[Joints.Length - 1].gameObject.transform.position;
                
            }
            if (move) {
                // Do we have to approach the target?
                //TODO
                int contador = 0;
                while (contador < iterationsPerFrame) {
                    if (!useCalculatedPos)
                        target = Vector3D.ToVector3D(Destination.position);

                    if (ErrorFunction(target, Solution) > StopThreshold)
                        ApproachTarget(target);

                    if (DebugDraw) {
                        Debug.DrawLine(Effector.transform.position, target.ToVector3(), Color.green);
                        //Debug.DrawLine(Destination.transform.position, target, new Color(0, 0.5f, 0));
                    }
                    contador++;
                }
            }
            if (!GetComponent<PhysicalReaction>().initialHit) {
                caja.transform.position = Joints[Joints.Length - 1].gameObject.transform.position;
            } else {
                //Debug.Log("Change");
                caja.transform.parent = Joints[Joints.Length - 1].gameObject.transform;
            }
        }
    }

    public void ApproachTarget(Vector3D target) {
        for (int i = 0; i < Solution.Length; i++) {
            Solution[i] = Solution[i] - LearningRate * CalculateGradient(target, Solution, i, DeltaGradient);
            Joints[i].MoveArm(Solution[i]);
        }

    }


    public float CalculateGradient(Vector3D target, float[] Solution, int i, float delta) {
        float a = DistanceFromTarget(target, Solution);
        Solution[i] += delta;
        float b = DistanceFromTarget(target, Solution);
        return (b - a) / delta;
    }

    // Returns the distance from the target, given a solution
    public float DistanceFromTarget(Vector3D target, float[] Solution) {
        Vector3D point = ForwardKinematics(Solution);
        //Debug.Log("Original ->" + Vector3.Distance(target,point.ToVector3()));
        //Debug.Log("Nuevo ->" + Vector3D.Distance(Vector3D.ToVector3D(target), point));
        //Vector3D.Distance(Vector3D.ToVector3D(target), point);
        return Vector3D.Distance(target, point);
    }


    /* Simulates the forward kinematics,
     * given a solution. */


    public PositionRotation ForwardKinematics(float[] Solution) {
        //Vector3 prevPoint = Joints[0].transform.position;

        //Vector3 distanceToNextJoin = Joints[1].transform.position - prevPoint;

        //Quaternion temp = new Quaternion();

        //temp.w = Mathf.Cos(Solution[0] / 2);
        //temp.x = 0;
        //temp.y = 1*Mathf.Sin(Solution[0]/2);
        //temp.z = 0;

        //Vector3 rotatedVector = temp * distanceToNextJoin;

        Vector3D prevPoint = Vector3D.ToVector3D(Joints[0].transform.position);

        Quat rotation = new Quat(transform.rotation);

        //Vector3 nextPoint;

        for (int i = 1; i < Joints.Length; i++) {
            rotation = rotation * Quat.AxisAngleToMyQuat(Joints[i - 1].Axis, Solution[i - 1]);
            Vector3D nextPoint = prevPoint + rotation * Joints[i].StartOffset;
            if (DebugDraw) {
                Debug.DrawLine(prevPoint.ToVector3(), nextPoint.ToVector3(), Color.blue);
            }
            prevPoint = nextPoint;

        }

        //Vector3 arrayVectors[] = new Vector3[4];
        //for(int i = 1; i < 4; i++) {
        //    prevPoint = Joints[i - 1].transform.position;
        //    distanceToNextJoin = Joints[i].transform.position - prevPoint;
        //    rotatedVector = Joints[i-1].transform.rotation * distanceToNextJoin;

        //}


        // Takes object initial rotation into account
        //TODO



        // The end of the effector
        return new PositionRotation(prevPoint, rotation);
    }
}