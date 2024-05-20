using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public struct PlayerProp {
    public int dots;
    public int coins;
    public int stars;
}

public class Player : MonoBehaviour {
    [SerializeField] Tilemap tilemap;
    SpriteRenderer spriter;
    bool isMove = false;
    Vector2 inputVec = Vector2.zero;
    Coroutine moveCoroutine;
    PlayerProp prop;

    public void UpdateProp(int dots, int coins, int stars) {
        prop.dots = dots;
        prop.coins += coins;
        prop.stars = stars;
    }

    public PlayerProp GetProp() {
        return prop;
    }

    void Awake() {
        spriter = GetComponent<SpriteRenderer>();
    }

    void Start() {
        gameObject.SetActive(false);
    }

    void Update() {
        GetInput();
        Move();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Thorn")) {
            // ��� ó��
            Debug.Log("���ÿ� ���� ����Ͽ����ϴ�.");
        }
        else if (collision.CompareTag("Exit")) {
            Debug.Log("�ⱸ ����!!");
            Setup();
        }
    }

    void Setup() {
        StopCoroutine(moveCoroutine);
        isMove = false;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        gameObject.SetActive(false);
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
        moveCoroutine = StartCoroutine(MoveTo(inputVec));
    }

    void CheckObstacle(Vector2 dirVec) {
        // ������ �̵��ϰ� ���� ��ֹ� üũ�� ����
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dirVec, Mathf.Infinity, LayerMask.GetMask("Obstacle"));
        if (!hit) { // �ε��� ��ֹ� ���� ��
            return;
        }

        if (hit.collider.CompareTag("Static")) { // ���ÿ� �ε����� ��
            // ��� ó��
            Debug.Log("���ÿ� ���� ����Ͽ����ϴ�.");
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
        //spriter.flipX = Random.Range(0, 2) == 0 ? false : true;
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

        if (startPos == endPos) {
            isMove = false;
            yield break;
        }

        float timer = 0;
        float duration = 0.05f * Vector3.Distance(startPos, endPos); // �ӵ��� ������ ���¿��� �Ÿ��� �ø� duration�� �þ

        while (true) {
            timer += Time.deltaTime / duration;

            if (timer < 1f)
                transform.position = Vector2.Lerp(startPos, endPos, timer);
            else {
                transform.position = endPos;
                Rotate(dirVec);
                CheckObstacle(dirVec);
                isMove = false;
                break;
            }

            yield return null;
        }
    }
}
