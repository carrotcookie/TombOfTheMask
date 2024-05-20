using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStage : MonoBehaviour {
    Text text;
    int myNum;

    void Awake() {
        text = GetComponentInChildren<Text>();
    }

    public void SetText(int num) {
        myNum = num;
        text.text = myNum.ToString();
    }

    public void OnClick() {
        UIManager.Instance.stageNavi.OnClick(myNum);
    }
}
