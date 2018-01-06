using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour {
    RawImage a;
    public bool rising;
    // Use this for initialization
    void Start() {
        a = GetComponent<RawImage>();
        a.color = new Color(a.color.r, a.color.g, a.color.b, 1);

    }

    // Update is called once per frame
    void Update() {
        if (gameObject.activeInHierarchy) {
            if (rising) {
                a.color = new Color(a.color.r, a.color.g, a.color.b, a.color.a + 1f*0.005f);
                if (a.color.a >= 1) {
                    rising = false;
                }
            } else {
                a.color = new Color(a.color.r, a.color.g, a.color.b, a.color.a - 1f* 0.005f);
                if (a.color.a <= 0.5f) {
                    rising = true;
                }
            }

            if (Input.anyKey)
                gameObject.SetActive(false);

        }
    }
}
