using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Player : MonoBehaviour {
    [SerializeField] Tilemap tilemap;
    SpriteRenderer spriter;
    bool isMove = false;
    Vector2 inputVec = Vector2.zero;

    void Awake() {
        spriter = GetComponent<SpriteRenderer>();
    }

    void Update() {
        GetInput();
        Move();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Thorn")) {
            // 사망 처리
            Debug.Log("가시에 질려 사망하였습니다.");
        }
    }

    void GetInput() {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    void Move() {
        if (isMove)
            return;
        if (inputVec == Vector2.zero || inputVec == Vector2.one)
            return;


        isMove = true;
        StartCoroutine(MoveTo(inputVec));
    }

    void Stop(Vector2 dirVec) {
        Rotate(dirVec);
        isMove = false;

        // 완전히 이동하고 나서 장애물 체크를 실행
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dirVec, Mathf.Infinity, LayerMask.GetMask("Obstacle"));
        if (!hit)
            return;

        

        if (hit.collider.CompareTag("Thorn")) {
            // 사망 처리
            Debug.Log("가시에 질려 사망하였습니다.");
        }
    }

    void Rotate(Vector2 dirVec) {
        Vector3 rotVec = Vector3.zero;

        if (dirVec.x != 0) {
            if (dirVec.x == 1)
                rotVec = new Vector3(0, 0, 90);
            else
                rotVec = new Vector3(0, 0, -90);
        }
        else if (dirVec.y != 0) {
            if (dirVec.y == 1)
                rotVec = new Vector3(0, 0, 180);
            else
                rotVec = new Vector3(0, 0, 0);
        }

        transform.rotation = Quaternion.Euler(rotVec);
        spriter.flipX = Random.Range(0, 2) == 0 ? false : true;
    }

    Vector3 GetNextPosition(Vector2 dirVec) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dirVec, Mathf.Infinity, LayerMask.GetMask("Tilemap"));
        Vector3Int cellPos = tilemap.WorldToCell(hit.point);
        Vector3 nextPos = tilemap.GetCellCenterWorld(cellPos);
        return nextPos;
    }

    IEnumerator MoveTo(Vector2 dirVec) {
        Vector3 startPos = transform.position;
        Vector3 endPos = GetNextPosition(dirVec);

        float timer = 0;
        float duration = 0.05f * Vector3.Distance(startPos, endPos); // 속도가 일정한 상태에서 거리가 늘면 duration도 늘어남

        while (true) {
            timer += Time.deltaTime / duration;

            if (timer < 1f)
                transform.position = Vector2.Lerp(startPos, endPos, timer);
            else {
                transform.position = endPos;
                Stop(dirVec);
                break;
            }

            yield return null;
        }
    }
}
