using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetManager : MonoBehaviour {
    public static GoogleSheetManager Instance { get; private set; }
    [SerializeField] string defaultURL = "https://docs.google.com/spreadsheets/d/1O0lqooyzRi2Dy7IV_Veh3Kyu9NxeB25VbM0a6IlABoE";
    Dictionary<string, string> sheetID = new Dictionary<string, string>();

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        sheetID.Add("Stage", "0");
        sheetID.Add("Player", "1829429247");
    }
    
    public void GetStageData(StageDataSO stageDataSO) {
        string sheetID = GetSheetID("Stage");
        string url = GetTSVAddress(sheetID);
        StartCoroutine(RequestData(url, stageDataSO));
    }

    string GetSheetID(string sheetName) {
        if (!sheetID.ContainsKey(sheetName))
            return null;
        else
            return sheetID[sheetName];
    }

    string GetTSVAddress(string sheetID) {
        return $"{defaultURL}/export?format=tsv&gid={sheetID}";
    }

    IEnumerator RequestData(string url, StageDataSO stageDataSO) {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        string tsv = www.downloadHandler.text;

        string[] rows = tsv.Split('\n');
        string[] categories = rows[0].Split('\t');
        int rowSize = rows.Length;          // 0번 인덱스는 사용안함
        int colSize = categories.Length;

        for (int i = 1; i < rowSize; i++) {
            if (stageDataSO.stageDatas.Count >= i)
                continue;

            StageData stageData = new StageData();
            string[] infos = rows[i].Split('\t');

            for (int j = 0; j < colSize; j++)
                stageData.id = int.Parse(infos[0]);

            GetTerrainPositions(stageData.terrainPositions, infos[1]);
            GetStaticObsInfos(stageData.staticObsInfos, infos[2]);
            GetDetectionObsInfos(stageData.detectionObsInfos, infos[3]);
            GetDotPositions(stageData.dotPositions, infos[4]);
            GetCoinPositions(stageData.coinPositions, infos[5]);
            GetStarPositions(stageData.starPositions, infos[6]);
            stageData.exitPosition = GetExitPosition(infos[7]);

            stageDataSO.stageDatas.Add(stageData);
        }
    }

    void GetTerrainPositions(List<Vector3> terrainPositions, string positions) {
        foreach (string pos in positions.Split(' ')) {
            float x = float.Parse(pos.Split(',')[0]);
            float y = float.Parse(pos.Split(',')[1]);

            terrainPositions.Add(new Vector3(x, y, 0));
        }
    }

    void GetDotPositions(List<Vector3> dotPositions, string positions) {
        foreach (string pos in positions.Split(' ')) {
            float x = float.Parse(pos.Split(',')[0]);
            float y = float.Parse(pos.Split(',')[1]);

            dotPositions.Add(new Vector3(x, y, 0));
        }
    }

    void GetCoinPositions(List<Vector3> coinPositions, string positions) {
        foreach (string pos in positions.Split(' ')) {
            float x = float.Parse(pos.Split(',')[0]);
            float y = float.Parse(pos.Split(',')[1]);

            coinPositions.Add(new Vector3(x, y, 0));
        }
    }

    void GetStarPositions(Vector3[] starPositions, string positions) {
        int i = 0;
        foreach (string pos in positions.Split(' ')) {
            float x = float.Parse(pos.Split(',')[0]);
            float y = float.Parse(pos.Split(',')[1]);
            starPositions[i].x = x;
            starPositions[i].y = y;
            i++;
        }
    }

    Vector3 GetExitPosition(string pos) {
        float x = float.Parse(pos.Split(',')[0]);
        float y = float.Parse(pos.Split(',')[1]);
        return new Vector3(x, y, 0);
    }

    void GetStaticObsInfos(List<Vector3> staticObsInfos, string infos) {
        if (infos == "empty")
            return;

        foreach (string info in infos.Split(' ')) {
            float x = float.Parse(info.Split(',')[0]);
            float y = float.Parse(info.Split(',')[1]);
            float angle = float.Parse(info.Split(',')[2]);

            staticObsInfos.Add(new Vector3(x, y, 0));
            staticObsInfos.Add(new Vector3(0, 0, angle));
        }
    }

    void GetDetectionObsInfos(List<Vector3> detectionObsInfos, string infos) {
        if (infos == "empty")
            return;

        foreach (string info in infos.Split(' ')) {
            float x = float.Parse(info.Split(',')[0]);
            float y = float.Parse(info.Split(',')[1]);
            float angle = float.Parse(info.Split(',')[2]);

            detectionObsInfos.Add(new Vector3(x, y, 0));
            detectionObsInfos.Add(new Vector3(0, 0, angle));
        }
    }
}
