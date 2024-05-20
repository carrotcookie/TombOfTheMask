using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.CompareTag("Player"))
            return;
        UIManager.Instance.HideTopBar();
        UIManager.Instance.clearPanel.SetActive(true);
        GameManager.Instance.HideMap();
        //GameManager.Instance.GoNextStage();
    }
}
