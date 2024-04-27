using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    SpriteRenderer spriter;
    float timer;

    void Awake() {
        spriter = GetComponent<SpriteRenderer>();
    }

    void Update() {
        timer += Time.deltaTime;

        if (timer >= 0.2f) {
            spriter.color = spriter.color == Color.magenta ? Color.yellow : Color.magenta;
            timer = 0;
        }
    }
}