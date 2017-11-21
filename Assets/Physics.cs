using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour{
    private float gLuna;
    private Vector3D position;
    public Vector3 velocityInspector = new Vector3();
    private Vector3D velocity = new Vector3D();
    private Vector3D acceleration;
	
	void Start () {
        gLuna = -1.622f;
        position = new Vector3D(transform.position.x, transform.position.y, transform.position.z);
        velocity.Set(velocityInspector.x, velocityInspector.y, velocityInspector.z);
        acceleration = new Vector3D(0, gLuna, 0);
        Time.timeScale = 0.5f;
	}

	void Update () {
        position.Set(
            position.x + velocity.x * Time.deltaTime,
            position.y + velocity.y * Time.deltaTime,
            position.z + velocity.z * Time.deltaTime);
        transform.position = new Vector3(position.x, position.y, position.z);

        velocity.Set(
            velocity.x + acceleration.x * Time.deltaTime,
            velocity.y + acceleration.y * Time.deltaTime,
            velocity.z + acceleration.z * Time.deltaTime);

        velocity.Print();
    }
}
