using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/StageDataSO", fileName = "StageDataSO", order = int.MaxValue)]
public class StageDataSO : ScriptableObject {
    public List<StageData> stageDatas = new List<StageData>();
}
