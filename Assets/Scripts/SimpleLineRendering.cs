using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLineRendering : MonoBehaviour {
    bool drawForces;
    LineRenderer render;
    public Vector3D position1;
    public Vector3D position2;
    float timer;
    public bool gone;
    Color color;
    // Use this for initialization
    void Start () {
        color = new Color(0, 255, 0);
	}
	
    public void Go() {
        if (!gone) {
            drawForces = true;
            gone = true;
        }
    }

    public void SetUp(Vector3D pos1, Vector3D pos2, Color color) {
        position1 = pos1;
        position2 = pos2;
        this.color = color;
    }

    public void SetUp(Color color) {
        this.color = color;
    }

    public void SetUp(Vector3D pos1, Vector3D pos2) {
        position1 = pos1;
        position2 = pos2;
    }

    // Update is called once per frame
    void Update () {
        if (drawForces) {
            //Debug.Log(Emisor.gameObject.transform.position);
            //Debug.Log(forces[0]);
            //Debug.DrawLine(Emisor.gameObject.transform.position, ((new Vector3D(Emisor.gameObject.transform.position) + forces[0] / forces[0].Magnitude() * 25).ToVector3()), Color.red);

            if (render == null) {
                Debug.Log("Adding");
                render = gameObject.AddComponent<LineRenderer>();
            }

            render.material = new Material(Shader.Find("Particles/Additive"));
            render.widthMultiplier = 0.2f;
            render.positionCount = 2;
            Vector3D[] posiciones = new Vector3D[2] { position1,position2 };

            render.SetPositions(new Vector3[2] { posiciones[0].ToVector3(), posiciones[1].ToVector3() });
            render.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            render.startColor = color;
            render.endColor = color;

            timer += Time.deltaTime;
            if (timer > 1.0f)
                drawForces = false;

        } else {
            
            if (render != null) {
                Destroy(render);
            }
        }
    }
}
