using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager Instance { get; private set; }
    public GameObject clearPanel;
    public GameObject scrollView;
    public StageNavi stageNavi;
    [SerializeField] GameObject topBar;
    [SerializeField] UICoin uiCoin;
    [SerializeField] UIStar uiStar;
    [SerializeField] UIMenu uiMenu;

    void Awake() {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start() {
        topBar.SetActive(false);
        clearPanel.SetActive(false);
    }

    public void IncreaseCoin() {
        uiCoin.RenewCoinCount();
    }

    public void IncreaseStar() {
        uiStar.RenewActiveStar();
    }

    public void HideTopBar() {
        topBar.SetActive(false);
    }

    public void ShowTopBar() {
        topBar.SetActive(true);
    }
}
