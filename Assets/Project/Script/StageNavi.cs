using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageNavi : MonoBehaviour {
    UIStage[] stages;
    int myOrder;

    void Awake() {
        stages = GetComponentsInChildren<UIStage>();    
    }

    public void SetOrder(int num) {
        myOrder = num;
        
        for (int i = 0; i < stages.Length; i++) {
            stages[i].SetText(i + myOrder);
        }
    }

    public void OnClick(int num) {
        GameManager.Instance.GoStage(num);
        UIManager.Instance.scrollView.SetActive(false);
    }
}
