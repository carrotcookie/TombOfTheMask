using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScannerEffect : MonoBehaviour {
    public Image[] images; // A, B, C, D 이미지를 배열로 설정
    public Image scannerLine; // 스캔 라인을 나타내는 이미지
    public float scanDuration = 2.0f; // 스캔 효과의 전체 지속 시간

    private RectTransform scannerRect;
    private RectTransform[] imageRects;
    private float startX;
    private float endX;
    private float scannerWidth;

    void Start() {
        // RectTransform 초기화
        scannerRect = scannerLine.GetComponent<RectTransform>();
        imageRects = new RectTransform[images.Length];
        for (int i = 0; i < images.Length; i++) {
            imageRects[i] = images[i].GetComponent<RectTransform>();
        }

        // 스캔 라인의 시작 위치와 끝 위치 설정
        startX = imageRects[0].anchoredPosition.x - imageRects[0].rect.width / 2;
        endX = imageRects[images.Length - 1].anchoredPosition.x + imageRects[images.Length - 1].rect.width / 2;
        scannerWidth = scannerRect.rect.width;

        // 스캔 효과 코루틴 시작
        StartCoroutine(ScanEffectCoroutine());
    }

    IEnumerator ScanEffectCoroutine() {
        yield return new WaitForSeconds(2);
        float elapsedTime = 0f;

        while (elapsedTime < scanDuration) {
            elapsedTime += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(elapsedTime / scanDuration);
            float scannerPosX = Mathf.Lerp(startX, endX, normalizedTime);
            scannerRect.anchoredPosition = new Vector2(scannerPosX, scannerRect.anchoredPosition.y);

            // 각 이미지의 투명도 조절
            foreach (Image image in images) {
                float imageCenterX = image.GetComponent<RectTransform>().anchoredPosition.x;
                float distanceToScanner = Mathf.Abs(scannerPosX - imageCenterX);
                float alpha = Mathf.Clamp01(1 - (distanceToScanner / scannerWidth));
                Color color = image.color;
                color.a = alpha;
                image.color = color;
            }

            yield return null;
        }

        // 스캔이 끝난 후 모든 이미지를 완전히 나타나도록 설정
        foreach (Image image in images) {
            Color color = image.color;
            color.a = 1f;
            image.color = color;
        }
    }
}
