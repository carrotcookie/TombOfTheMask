using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour {
    [SerializeField] Tilemap tilemap;
    bool isMove = false;
    float xInput;
    float yInput;

    void Update() {
        GetInput();
        Move();
    }

    Vector3 GetNextPosition(Vector2 dirVec) {
        //RaycastHit2D[] hitArr = Physics2D.RaycastAll(transform.position, dirVec, Mathf.Infinity, LayerMask.GetMask("Tilemap"));
        //Debug.Log(hitArr.Length);
        //for(int i = 0; i < hitArr.Length; i++) {
        //    Debug.Log(hitArr[i].point);
        //}
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dirVec, Mathf.Infinity, LayerMask.GetMask("Tilemap"));
        Vector3Int cellPos = tilemap.WorldToCell(hit.point);
        Vector3 nextPos = tilemap.GetCellCenterWorld(cellPos);
        return nextPos;
    }

    void GetInput() {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
    }

    void Move() {
        if (isMove)
            return;
        if (xInput == 0 && yInput == 0)
            return;


        isMove = true;
        Vector2 dirVec = new Vector2(xInput, yInput);
        Vector3 endPos = GetNextPosition(dirVec);
        StartCoroutine(MoveTo(endPos));
    }

    IEnumerator MoveTo(Vector2 endPos) {
        Vector3 startPos = transform.position;
        float timer = 0;
        float duration = 0.05f * Vector3.Distance(startPos, endPos);

        while (true) {
            timer += Time.deltaTime / duration;

            if (timer < 1f)
                transform.position = Vector2.Lerp(startPos, endPos, timer);
            else {
                timer = 1f;
                transform.position = endPos;
                isMove = false;
                break;
            }

            yield return null;
        }
    }
}
