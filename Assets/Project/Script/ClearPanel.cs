using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ClearPanel : MonoBehaviour {
    [SerializeField] Text stageText;
    [SerializeField] Text dotText;
    [SerializeField] Slider dotSlider;
    [SerializeField] DOTweenAnimation[] stars;
    StageScore stageScore;

    void Start() {
        gameObject.SetActive(false);
    }

    public void Setup() {
        stageScore = GameManager.Instance.GetClearStageScore();

        stageText.text = stageScore.stageID.ToString();
        dotText.text = string.Format($"{stageScore.dot} / {stageScore.totalDot}");
        dotSlider.value = (float)stageScore.dot / stageScore.totalDot;
        for (int i = 0; i < stageScore.star; i++) {
            stars[i].DOPlay();
        }
    }
}
