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
    public float sphereMass;

    private void Awake(){
        sphereMass = 1;
        gLuna = -1.622f;
        position = new Vector3D(transform.position.x, transform.position.y, transform.position.z);
        velocity.Set(velocityInspector.x, velocityInspector.y, velocityInspector.z);
        acceleration = new Vector3D(0, gLuna, 0);
    }

    void Start () {
        GetComponentInChildren<ParticleSystem>().Stop();
        Time.timeScale = 0.5f;
	}

    public void SetVelocityX(float a){
        velocityInspector.x = a;
    }

    void Update(){

        Debug.DrawLine(gameObject.transform.position, (new Vector3D(gameObject.transform.position) + velocity / velocity.Magnitude() * 25).ToVector3(),Color.blue);

        if (!FindObjectOfType<GameLogic>().paused){
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
        //Antes de empezar el movimiento puedes mover la esfera para situar la posicion inicial del disparo
        } else {
            Vector3D vSide = new Vector3D(0, 0, 10 * Time.deltaTime);
            Vector3D vUp = new Vector3D(0, 10 * Time.deltaTime, 0);
            if (Input.GetKey(KeyCode.RightArrow)) {
                transform.position = transform.position - vSide.ToVector3();
            }else if (Input.GetKey(KeyCode.LeftArrow)) {
                transform.position = transform.position + vSide.ToVector3();
            }
            if (Input.GetKey(KeyCode.UpArrow)) {
                transform.position = transform.position + vUp.ToVector3();
            } else if (Input.GetKey(KeyCode.DownArrow)) {
                transform.position = transform.position - vUp.ToVector3();
            }
        }
    }
}
