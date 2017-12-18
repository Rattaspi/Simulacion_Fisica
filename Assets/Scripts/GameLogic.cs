using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour {
    public bool paused = true;
    public Camera[] cameras = new Camera[5];
    public GameObject sphere;
    private int showingState = 0;
    private bool once = false;

	// Use this for initialization
	void Start () {
        CameraBehavior(5);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R)){
            Reset();
        }else if (Input.GetKeyDown(KeyCode.P)){
            if (!once) { //Esto solo quiero que se haga la primera vez que se inicia la escena
                CameraBehavior(0);
                once = true;
            }
            //Gestion del sistema de particulas de la esfera
            if (sphere.GetComponentInChildren<ParticleSystem>().isStopped) {
                sphere.GetComponentInChildren<ParticleSystem>().Play();
            } else {
                sphere.GetComponentInChildren<ParticleSystem>().Stop();
            }
            paused = !paused;
        }

        if (Input.GetKeyDown(KeyCode.Alpha0)) CameraBehavior(0);
        else if (Input.GetKeyDown(KeyCode.Alpha1)) CameraBehavior(1);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) CameraBehavior(2);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) CameraBehavior(3);
        else if (Input.GetKeyDown(KeyCode.Alpha4)) CameraBehavior(4);
    }

    private void CameraBehavior(int state) {
        switch(state){
            case 0:
                cameras[0].gameObject.SetActive(true);
                cameras[1].gameObject.SetActive(true);
                cameras[2].gameObject.SetActive(true);
                cameras[3].gameObject.SetActive(true);
                cameras[4].gameObject.SetActive(false);
                cameras[0].rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                cameras[1].rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                cameras[2].rect = new Rect(0, 0, 0.5f, 0.5f);
                cameras[3].rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                break;

            case 1:
                cameras[0].gameObject.SetActive(true);
                cameras[0].rect = new Rect(0, 0, 1, 1);
                cameras[1].gameObject.SetActive(false);
                cameras[2].gameObject.SetActive(false);
                cameras[3].gameObject.SetActive(false);
                break;

            case 2:
                cameras[0].gameObject.SetActive(false);
                cameras[1].gameObject.SetActive(true);
                cameras[1].rect = new Rect(0, 0, 1, 1);
                cameras[2].gameObject.SetActive(false);
                cameras[3].gameObject.SetActive(false);
                break;

            case 3:
                cameras[0].gameObject.SetActive(false);
                cameras[1].gameObject.SetActive(false);
                cameras[2].gameObject.SetActive(true);
                cameras[2].rect = new Rect(0, 0, 1, 1);
                cameras[3].gameObject.SetActive(false);
                break;

            case 4:
                cameras[0].gameObject.SetActive(false);
                cameras[1].gameObject.SetActive(false);
                cameras[2].gameObject.SetActive(false);
                cameras[3].gameObject.SetActive(true);
                cameras[3].rect = new Rect(0, 0, 1, 1);
                break;

            case 5:
                cameras[0].gameObject.SetActive(false);
                cameras[1].gameObject.SetActive(false);
                cameras[2].gameObject.SetActive(false);
                cameras[3].gameObject.SetActive(false);
                cameras[4].gameObject.SetActive(true);
                break;

            default:
                break;
        }
    }

    void Reset(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
