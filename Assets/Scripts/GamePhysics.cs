using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePhysics : MonoBehaviour{
    public float gLuna = -1.622f;
    private Vector3D position;
    public Vector3 velocityInspector = new Vector3();
    public Vector3D velocity = new Vector3D();
    private Vector3D acceleration;
    bool unPaused = false;
    public bool done = false;

    private void Awake()
    {
        gLuna = -1.622f;
        position = new Vector3D(transform.position.x, transform.position.y, transform.position.z);
        velocity.Set(velocityInspector.x, velocityInspector.y, velocityInspector.z);
        acceleration = new Vector3D(0, gLuna, 0);
    }

    void Start () {

        Time.timeScale = 0.5f;
	}

    public void SetVelocityX(float a)
    {
        velocityInspector.x = a;
    }

    void Update()
    {
        if (!FindObjectOfType<GameLogic>().paused)
        {
            if (!unPaused) { 
                Awake();
                unPaused = true;
                done = true;
            }
            position.Set(
                position.x + velocity.x * Time.deltaTime,
                position.y + velocity.y * Time.deltaTime,
                position.z + velocity.z * Time.deltaTime);
            transform.position = new Vector3(position.x, position.y, position.z);

            velocity.Set(
                velocity.x + acceleration.x * Time.deltaTime,
                velocity.y + acceleration.y * Time.deltaTime,
                velocity.z + acceleration.z * Time.deltaTime);

            //velocity.Print();
        }
    }
}
