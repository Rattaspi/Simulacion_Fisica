using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CanvasScript : MonoBehaviour {
    public Slider velocityX;
    public Slider elasticityC; 
    public Slider contactTime;
    public Slider mecanicResistance;
    public GameObject hide;
    public GamePhysics effectGenerator;
    public PhysicalReaction reactor;

    // Use this for initialization
    void Start () {
        velocityX.value = effectGenerator.velocity.x;
        elasticityC.value = reactor.elasticityC;
        contactTime.value = reactor.contactTime;
        mecanicResistance.value = reactor.mechanichRes;
        //Debug.Log(reactor.mechanichRes);

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.H)) {
            hide.SetActive(!hide.activeInHierarchy);
        }
	}
}
