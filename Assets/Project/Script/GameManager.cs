using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class StageData {
    public int id;
    public List<Vector3> terrainPositions = new List<Vector3>();
    public List<Vector3> staticObsInfos = new List<Vector3>();      // 2개씩 가져가야 함(위치, 회전)
    public List<Vector3> detectionObsInfos = new List<Vector3>();   // 2개씩 가져가야 함(위치, 회전)
    public List<Vector3> dotPositions = new List<Vector3>();
    public List<Vector3> coinPositions = new List<Vector3>();
    public Vector3[] starPositions = new Vector3[3];
    public Vector3 exitPosition;
}
[System.Serializable]
public struct StageScore {
    public int stageID;
    public int totalDot;
    public int dot;
    public int coin;
    public int star;
}

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    [SerializeField] int mapID;
    public Player Player;
    [SerializeField] Tilemap[] tilemapArr;
    [SerializeField] StageDataSO stageDataSO;
    [SerializeField] TileBase tileBase;
    [SerializeField] GameObject[] prefabs;
    [SerializeField] StageScore score;
    Dictionary<string, List<GameObject>> pools = new Dictionary<string, List<GameObject>>();

    public void IncreaseDot() {
        score.dot += 1;
    }

    public void IncreaseCoin() {
        score.coin += 1;
    }

    public void IncreaseStar() {
        score.star += 1;
    }

    public StageScore GetClearStageScore() {
        return score;
    }

    public int GetStageTotalDots(int num) {
        return stageDataSO.stageDatas[num - 1].dotPositions.Count;
    }

    void Awake() {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start() {
        GoogleSheetManager.Instance.GetStageData(stageDataSO);
        GenerateObsAndProp();
    }

    void DeActivePools() {
        foreach (KeyValuePair<string, List<GameObject>> kvp in pools) {
            for (int i = 0; i < kvp.Value.Count; i++)
                kvp.Value[i].SetActive(false);
        }
    }

    void GenerateObsAndProp() {
        int staticObsCount = 20;
        int detectionObsCount = 20;
        int dotCount = 200;
        int coinCount = 50;
        int starCount = 3;
        int exitCount = 1;
        GameObject obj;

        pools["Static"] = new List<GameObject>();
        for (int i = 0; i < staticObsCount; i++) {
            obj = Instantiate(prefabs[0], tilemapArr[1].transform);
            obj.SetActive(false);
            pools["Static"].Add(obj);
        }
            
        pools["Detection"] = new List<GameObject>();
        for (int i = 0; i < detectionObsCount; i++) {
            obj = Instantiate(prefabs[1], tilemapArr[1].transform);
            obj.SetActive(false);
            pools["Detection"].Add(obj);
        }

        pools["Dot"] = new List<GameObject>();
        for (int i = 0; i < dotCount; i++) {
            obj = Instantiate(prefabs[2], tilemapArr[2].transform);
            obj.SetActive(false);
            pools["Dot"].Add(obj);
        }

        pools["Coin"] = new List<GameObject>();
        for (int i = 0; i < coinCount; i++) {
            obj = Instantiate(prefabs[3], tilemapArr[2].transform);
            obj.SetActive(false);
            pools["Coin"].Add(obj);
        }

        pools["Star"] = new List<GameObject>();
        for (int i = 0; i < starCount; i++) {
            obj = Instantiate(prefabs[4], tilemapArr[2].transform);
            obj.SetActive(false);
            pools["Star"].Add(obj);
        }

        pools["Exit"] = new List<GameObject>();
        for (int i = 0; i < exitCount; i++) {
            obj = Instantiate(prefabs[5], tilemapArr[2].transform);
            obj.SetActive(false);
            pools["Exit"].Add(obj);
        }

    }

    public void ShowMap() {
        for (int i = 0; i < tilemapArr.Length; i++) {
            tilemapArr[i].gameObject.SetActive(true);
        }
    }

    public void HideMap() {
        for (int i = 0; i < tilemapArr.Length; i++) {
            tilemapArr[i].gameObject.SetActive(false);
        }
    }

    public void GoNextStage() {
        UIManager.Instance.clearPanel.SetActive(false);
        ShowMap();
        GoStage(++mapID);
    }

    public void GoStage(int num) {
        score.stageID = num;
        score.totalDot = stageDataSO.stageDatas[num - 1].dotPositions.Count;
        mapID = num;
        Player.gameObject.SetActive(true);
        UIManager.Instance.ShowTopBar();
        Tilemap terrain = tilemapArr[0];
        BoundsInt bounds = terrain.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++) {
            for (int y = bounds.yMin; y < bounds.yMax; y++) {
                Vector3Int cellPos = new Vector3Int(x, y, 0);
                TileBase tileBase = terrain.GetTile(cellPos);

                if (tileBase != null)
                    terrain.SetTile(cellPos, null);
            }
        }

        foreach (Vector3 pos in stageDataSO.stageDatas[mapID - 1].terrainPositions) {
            Vector3Int cellPos = terrain.WorldToCell(pos);
            terrain.SetTile(cellPos, this.tileBase);
        }

        DeActivePools();
        for (int i = 0; i < stageDataSO.stageDatas[mapID - 1].staticObsInfos.Count; i += 2) {
            pools["Static"][i / 2].transform.localPosition = stageDataSO.stageDatas[mapID - 1].staticObsInfos[i];
            pools["Static"][i / 2].transform.localRotation = Quaternion.Euler(stageDataSO.stageDatas[mapID - 1].staticObsInfos[i + 1]);
            pools["Static"][i / 2].SetActive(true);
        }
        for (int i = 0; i < stageDataSO.stageDatas[mapID - 1].detectionObsInfos.Count; i += 2) {
            pools["Detection"][i / 2].transform.localPosition = stageDataSO.stageDatas[mapID - 1].detectionObsInfos[i];
            pools["Detection"][i / 2].transform.localRotation = Quaternion.Euler(stageDataSO.stageDatas[mapID - 1].detectionObsInfos[i + 1]);
            pools["Detection"][i / 2].SetActive(true);
        }
        for (int i = 0; i < stageDataSO.stageDatas[mapID - 1].dotPositions.Count; i++) {
            pools["Dot"][i].transform.localPosition = stageDataSO.stageDatas[mapID - 1].dotPositions[i];
            pools["Dot"][i].SetActive(true);
        }
        for (int i = 0; i < stageDataSO.stageDatas[mapID - 1].coinPositions.Count; i++) {
            pools["Coin"][i].transform.localPosition = stageDataSO.stageDatas[mapID - 1].coinPositions[i];
            pools["Coin"][i].SetActive(true);
        }
        for (int i = 0; i < stageDataSO.stageDatas[mapID - 1].starPositions.Length; i++) {
            pools["Star"][i].transform.localPosition = stageDataSO.stageDatas[mapID - 1].starPositions[i];
            pools["Star"][i].SetActive(true);
        }
        pools["Exit"][0].transform.localPosition = stageDataSO.stageDatas[mapID - 1].exitPosition;
        pools["Exit"][0].SetActive(true);
    }
}
