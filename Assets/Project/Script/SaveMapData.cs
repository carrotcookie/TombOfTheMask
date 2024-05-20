using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using UnityEngine.UI;

public class SaveMapData : MonoBehaviour {
    [SerializeField] GameObject stageObj;
    Dictionary<string, Tilemap> tilemapDict = new Dictionary<string, Tilemap>();

    void Start() {
        foreach (Tilemap tilemap in stageObj.GetComponentsInChildren<Tilemap>())
            tilemapDict.Add(tilemap.name, tilemap);

        GetTerrainPositionList();
        print("------------------------");
        GetObstaclePositionList();
        print("------------------------");
        GetPropPositionList();
    }

    List<Vector3> GetTerrainPositionList() {
        List<Vector3> result = new List<Vector3>();
        BoundsInt bound = tilemapDict["Terrain"].cellBounds;

        for (int x = bound.xMin; x < bound.xMax; x++) {
            for (int y = bound.yMin; y < bound.yMax; y++) {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                TileBase tileBase = tilemapDict["Terrain"].GetTile(tilePosition);

                if (tileBase != null)
                    result.Add(tilemapDict["Terrain"].GetCellCenterWorld(tilePosition));
            }
        }

        string s = null;
        for (int i = 0; i < result.Count; i++) {
            s += (result[i].x.ToString() + ',' + result[i].y.ToString() + ' ');
        }
        print(s);
        return result;
    }

    Dictionary<string, Dictionary<Vector3, float>> GetObstaclePositionList() {
        Dictionary<string, Dictionary<Vector3, float>> result = new Dictionary<string, Dictionary<Vector3, float>>();
        string[] obstacleTagArr = new string[] { "Static", "Detection" };

        foreach (string tagName in obstacleTagArr)
            result.Add(tagName, new Dictionary<Vector3, float>());

        foreach (Transform tf in tilemapDict["Obstacle"].GetComponentsInChildren<Transform>().Skip(1))
            result[tf.tag][tf.localPosition] = tf.rotation.eulerAngles.z;            

        foreach (string tagName in obstacleTagArr) {
            string s = null;

            foreach (KeyValuePair<Vector3, float> kvp in result[tagName]) {
                s += (kvp.Key.x.ToString() + ',' + kvp.Key.y.ToString() + ',' + kvp.Value.ToString() + ' ');
            }
            print(s);
        }

        return result;
    }

    Dictionary<string, List<Vector3>> GetPropPositionList() {
        Dictionary<string, List<Vector3>> result = new Dictionary<string, List<Vector3>>();
        string[] propTagArr = new string[] { "Dot", "Coin", "Star", "Exit" };

        foreach (string tagName in propTagArr)
            result.Add(tagName, new List<Vector3>());

        foreach (Transform tf in tilemapDict["Prop"].GetComponentsInChildren<Transform>().Skip(1))
            result[tf.tag].Add(tf.localPosition);

        foreach (string tagName in propTagArr) {
            string s = null;

            foreach (Vector3 pos in result[tagName]) {
                s += (pos.x.ToString() + ',' + pos.y.ToString() + ' ');
            }
            print(s);
        }

        return result;
    }
}
