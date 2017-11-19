using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stuff : MonoBehaviour {
    
    // Update is called once per frame
    void Update () {
        //Debug.Log(new Quaternion(2,2,2,2) * new Vector3(2, 2, 2));
        // Debug.Log(new Quaternion(2, 2, 2, 2) * new Vector3D(2, 2, 2));
        Debug.Log(new Quaternion(2, 5, 9, 25) * new Vector3(3, 6, 5));
        (new Quaternion(2, 5, 9, 25) * new Vector3D(3, 6, 5)).Print();
    }

}
