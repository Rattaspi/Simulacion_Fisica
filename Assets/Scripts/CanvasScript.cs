using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CanvasScript : MonoBehaviour {
    public Slider velocityX;
    public Slider elasticityC; 
    public Slider contactTime;
    public Slider mecanicResistance;
    public Slider sphereMass;
    public Slider armMass;

    public GameObject hide;
    public GamePhysics effectGenerator;
    public PhysicalReaction reactor;
    public GameObject hint;

    // Use this for initialization
    void Start () {
        if (!PermanentData.instance.hasValues) {
            velocityX.value = effectGenerator.velocity.x;
            elasticityC.value = reactor.elasticityC;
            contactTime.value = reactor.contactTime;
            mecanicResistance.value = reactor.mechanichRes;
            armMass.value = reactor.armMass;
            sphereMass.value = effectGenerator.sphereMass;
            //Debug.Log(reactor.mechanichRes);
        } else {
            velocityX.value = PermanentData.instance.velocityX;
            elasticityC.value = PermanentData.instance.elasticityC;
            contactTime.value = PermanentData.instance.contactTime;
            mecanicResistance.value = PermanentData.instance.mechanichRes;
            armMass.value = PermanentData.instance.armMass;
            sphereMass.value = PermanentData.instance.sphereMass;
            hide.SetActive(PermanentData.instance.previousHide);

        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!PermanentData.instance.showHints) {
            hint.SetActive(true);
            PermanentData.instance.showHints = true;
        }
        if (Input.GetKeyDown(KeyCode.H)) {
            hide.SetActive(!hide.activeInHierarchy);
        }
        PermanentData.instance.previousHide = hide.activeInHierarchy;

    }
}
