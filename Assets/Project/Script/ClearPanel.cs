using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearPanel : MonoBehaviour {
    [SerializeField] Slider dotSlider;
    [SerializeField] Text stageText;
    [SerializeField] Text dotText;

    void OnEnable() {
        dotSlider.value = (float)StageManager.Instance.dots / StageManager.Instance.totalDots;
        stageText.text = "Stage " + (StageManager.Instance.currentStage + 1);
        dotText.text = $"{StageManager.Instance.dots} / {StageManager.Instance.totalDots}";
    }
}
