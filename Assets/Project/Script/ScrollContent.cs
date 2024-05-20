using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollContent : MonoBehaviour {
    StageNavi[] childs;

    void Awake() {
        childs = GetComponentsInChildren<StageNavi>();
    }

    void Start() {
        for (int i = 0; i < childs.Length; i++) {
            childs[i].SetOrder(i * 4 + 1);
        }
    }
}
