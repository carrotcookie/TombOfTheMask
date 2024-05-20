using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradationBloom : MonoBehaviour {
    [SerializeField] Color startColor;
    [SerializeField] Color endColor;
    [SerializeField] float intensity;
    [SerializeField] float startDuration;
    [SerializeField] float endDuration;
    Text text;
    float timer;
    bool goBack;

    void Awake() {
        text = GetComponent<Text>();
    }

    void OnEnable() {
        timer = 0;
        goBack = false;
    }

    void Start() {
        if (startColor == Color.black)
            startColor = text.color;
    }

    void Update() {
        if (!goBack) {
            timer += Time.deltaTime;

            if (timer > startDuration) {
                text.color = endColor;
                goBack = true;
                timer = 0;
            } 
        }
        else {
            timer += Time.deltaTime;

            if (timer > endDuration) {
                text.color = startColor;
                goBack = false;
                timer = 0;
            }
        }
    }
}
