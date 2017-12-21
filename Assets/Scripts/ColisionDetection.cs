using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionDetection: MonoBehaviour{

    public GameObject sphere;
    public GamePhysics fisicasEsfera;
    float sphereRadius=0.7f;
    public GameObject corner1;
    public GameObject corner2;
    public GameObject IKObject;
    // Use this for initialization
    void Start () {
        sphere.transform.localScale = new Vector3D(1, 1, 1).ToVector3();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!FindObjectOfType<GameLogic>().paused)
        {

            if (Intersects(Vector3D.ToVector3D(corner1.transform.position), Vector3D.ToVector3D(corner2.transform.position), Vector3D.ToVector3D(sphere.transform.position), sphereRadius))
            {
                if (!IKObject.GetComponent<PhysicalReaction>().active) {
                    IKObject.GetComponent<PhysicalReaction>().velocidadRecibida = new Vector3D(fisicasEsfera.velocity.x, fisicasEsfera.velocity.y, fisicasEsfera.velocity.z);
                    IKObject.GetComponent<PhysicalReaction>().active = true;
                    IKObject.GetComponent<PhysicalReaction>().sphereMass = fisicasEsfera.sphereMass;

                }
                IKObject.GetComponent<InverseKinematics>().move = false;
                fisicasEsfera.velocity.x = 0;
                fisicasEsfera.gameObject.GetComponentInChildren<ParticleSystem>().Stop(); 
            }
        }
	}

    float squared(float v) {
        return v * v;
    }

    //Código de intersección de nuestro amigo Ben Volgt
    //https://stackoverflow.com/questions/4578967/cube-sphere-intersection-test
    bool Intersects(Vector3D C1, Vector3D C2, Vector3D S, float R) {
        
            float dist_squared = R * R;
    /* assume C1 and C2 are element-wise sorted, if not, do that now */
            if (S.x < C1.x) dist_squared -= squared(S.x - C1.x);
            else if (S.x > C2.x) dist_squared -= squared(S.x - C2.x);
            if (S.y < C1.y) dist_squared -= squared(S.y - C1.y);
            else if (S.y > C2.y) dist_squared -= squared(S.y - C2.y);
            if (S.z < C1.z) dist_squared -= squared(S.z - C1.z);
            else if (S.z > C2.z) dist_squared -= squared(S.z - C2.z);
            return dist_squared > 0;
        }
    

}
