using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStar : MonoBehaviour {
    Image[] starArr;
    int activeStarCount = 0;

    public void RenewActiveStar() {
        starArr[activeStarCount].color = Color.yellow;
        activeStarCount++;
    }

    void Awake() {
        starArr = GetComponentsInChildren<Image>();
    }

    void OnEnable() {
        Setup();
    }

    void Setup() {
        activeStarCount = 0;

        for (int i = 0; i < starArr.Length; i++) {
            starArr[activeStarCount].color = new Color(0.5f, 0, 1, 1);
        }
    }
}
