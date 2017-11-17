using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stuff : MonoBehaviour {	
	// Update is called once per frame
	void Update () {
        Debug.Log(Vector3.Cross(new Vector3(10, 4, 3), new Vector3(-1, 0, 0)));
        Vector3D mycross = Vector3D.Cross(new Vector3D(10, 4, 3), new Vector3D(-1, 0, 0));
        Debug.Log("(" + mycross.x + "," + mycross.y + "," + mycross.z + ")");

        //float dot = Vector3D.Dot(new Vector3D(10, 4, 3), new Vector3D(-1, 0, 0));
        //Debug.Log(dot);
    }
}
