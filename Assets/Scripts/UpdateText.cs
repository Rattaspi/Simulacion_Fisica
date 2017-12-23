using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpdateText : MonoBehaviour {
    public GameObject sliderObject;
    public string variable;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = variable + " " + ((Mathf.Floor(sliderObject.GetComponent<Slider>().value*100))/100);
	}
}
