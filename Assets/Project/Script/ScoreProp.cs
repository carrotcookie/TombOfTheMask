using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScoreProp : MonoBehaviour {
    SpriteRenderer spriter;
    float timer;

    protected abstract void UpdateLogic();
    protected abstract void RenewScore();
    protected abstract void Component();

    void Awake() {
        spriter = GetComponent<SpriteRenderer>();
        Component();
    }

    void Update() {
        ChangeColor();
        UpdateLogic();
    }

    void OnEnable() {
        timer = 0f;
    }

    void OnDisable() {
        timer = 0f;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.CompareTag("Player"))
            return;

        RenewScore();
        gameObject.SetActive(false);
    }

    void ChangeColor() {
        timer += Time.deltaTime;

        if (timer >= 0.2f) {
            spriter.color = spriter.color == Color.magenta ? Color.yellow : Color.magenta;
            timer = 0;
        }
    }
}