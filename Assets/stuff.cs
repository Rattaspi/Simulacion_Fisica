using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stuff : MonoBehaviour {
    
    // Update is called once per frame
    void Update () {
        //Debug.Log(new Quaternion(2,2,2,2) * new Vector3(2, 2, 2));
        // Debug.Log(new Quaternion(2, 2, 2, 2) * new Vector3D(2, 2, 2));
        Debug.Log(Vector3D.Sum(new Vector3D(1), new Vector3D(1)).x);
    }

}
