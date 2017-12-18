using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpdateText : MonoBehaviour {
    public GameObject sliderObject;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = "Velocidad X: " + sliderObject.GetComponent<Slider>().value;
	}
}
