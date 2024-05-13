using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {
    public static StageManager Instance { get; private set; }
    [SerializeField] GameObject[] stageArr;
    public int currentStage = 0;

    public int totalDots;
    public int dots;
    public int coins;
    public int stars;

    void Awake() {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start() {
        stageArr[currentStage].SetActive(true);
    }

    public void GoStage(int stage) {
        for (int i = 0; i < stageArr.Length; i++)
            stageArr[i].SetActive(false);
        stageArr[stage].SetActive(true);
    }

    public void GoNextStage() {
        stageArr[currentStage].SetActive(false);
        currentStage++;
        stageArr[currentStage].SetActive(true);
        UIManager.Instance.uiStar.Setup();
    }

    public void IncreaseDots() {
        dots += 1;
    }
    public void IncreaseCoins() {
        coins += 1;
    }
    public void IncreaseStars() {
        stars += 1;
    }
}
