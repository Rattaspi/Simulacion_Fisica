using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {
    public GameObject endEffector;

	// Use this for initialization
	void Start () {
        transform.position = endEffector.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = endEffector.transform.position;
    }
}
