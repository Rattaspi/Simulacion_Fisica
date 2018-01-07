using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentData : MonoBehaviour {
    public static PermanentData instance =null;
    public bool showHints;
    public bool hasValues;
    public float velocityX;
    public float elasticityC;
    public float contactTime;
    public float mechanichRes;
    public float armMass;
    public float sphereMass;
    public bool previousHide;
    private void Awake() {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        DontDestroyOnLoad(this);

    }

    public void SaveValues(float v, float e, float c, float m, float am, float sm) {
        hasValues = true;
        velocityX = v;
        elasticityC = e;
        contactTime = c;
        mechanichRes = m;
        armMass = am;
        sphereMass = sm;
    }

    // Use this for initialization
    void Start () {
        showHints = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
