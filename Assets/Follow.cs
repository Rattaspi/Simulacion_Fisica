﻿using System.Collections; using System.Collections.Generic; using UnityEngine;  public class Follow : MonoBehaviour {     public GameObject target;  	// Use this for initialization 	void Start () {         transform.position = target.transform.position; 	} 	 	// Update is called once per frame 	void Update () {         transform.position = target.transform.position;     } } 