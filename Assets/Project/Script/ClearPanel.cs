using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearPanel : MonoBehaviour {
    [SerializeField] Slider dotSlider;
    [SerializeField] Text stageText;
    [SerializeField] Text dotText;

    void OnEnable() {
        StageScore score = GameManager.Instance.GetClearStageScore();
        StageData data = GameManager.Instance.GetStageData(score.stageID);
        stageText.text = score.stageID.ToString();
        dotSlider.value = (float) score.dot / data.dotPositions.Count;
        dotText.text = string.Format($"{score.dot} / {data.dotPositions.Count}");
    }
}
