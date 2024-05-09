using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {
    public static StageManager Instance { get; private set; }
    [SerializeField] GameObject[] stageArr;
    [SerializeField] int currentStage = 0;

    void Awake() {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start() {
        stageArr[currentStage].SetActive(true);
    }

    public void GoNextStage() {
        stageArr[currentStage].SetActive(false);
        currentStage++;
        stageArr[currentStage].SetActive(true);
        UIManager.Instance.uiStar.Setup();
    }
}
