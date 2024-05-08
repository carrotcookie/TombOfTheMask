using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICoin : MonoBehaviour {
    Text coinCountText;
    int coinCount;

    void Awake() {
        coinCountText = GetComponentInChildren<Text>();
    }

    public void RenewCoinCount() {
        coinCount++;
        coinCountText.text = coinCount.ToString();
    }
}
