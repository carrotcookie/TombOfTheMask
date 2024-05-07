using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour {
    Rigidbody2D rigid = null;
    Vector2 inputVec = Vector2.zero;
    float moveSpeed = 5;
    bool isMove = false;

    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update() {
        GetInput();
        Move();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Enter �浹!!!");
        if (isMove) {
            if (collision.CompareTag("Terrain")) {
                isMove = false;
                rigid.velocity = Vector2.zero;
            }
        }
    }

    void GetInput() {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    void Move() {
        if (isMove) {
            Debug.Log("�����̴� ���Դϴ�.");
            return;
        }
        if (inputVec == Vector2.zero) {
            Debug.Log("�Է��� �����ϴ�.");
            return;
        }

        isMove = true;
        rigid.velocity = inputVec * moveSpeed;
    }
}
