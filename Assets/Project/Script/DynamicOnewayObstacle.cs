using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicOnewayObstacle : MonoBehaviour {
    [SerializeField] GameObject thorn;
    float colorBlinkTimer = 0;
    Color color1 = Color.magenta;
    Color color2 = Color.cyan;
    SpriteRenderer spriter;

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            GrowThorn();
        }
    }

    void Awake() {
        spriter = GetComponent<SpriteRenderer>();
    }

    void Update() {
        ColorBlink();
    }

    void ColorBlink() {
        colorBlinkTimer += Time.deltaTime;

        if (colorBlinkTimer >= 0.3333f) {
            spriter.color = spriter.color == color1 ? color2 : color1;
            colorBlinkTimer = 0;
        }
    }

    void GrowThorn() {
        if (thorn.activeSelf == false) {
            thorn.SetActive(true);
        }
    }
}
