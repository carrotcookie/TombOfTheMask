using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScannerEffect : MonoBehaviour {
    public Image[] images; // A, B, C, D �̹����� �迭�� ����
    public Image scannerLine; // ��ĵ ������ ��Ÿ���� �̹���
    public float scanDuration = 2.0f; // ��ĵ ȿ���� ��ü ���� �ð�

    private RectTransform scannerRect;
    private RectTransform[] imageRects;
    private float startX;
    private float endX;
    private float scannerWidth;

    void Start() {
        // RectTransform �ʱ�ȭ
        scannerRect = scannerLine.GetComponent<RectTransform>();
        imageRects = new RectTransform[images.Length];
        for (int i = 0; i < images.Length; i++) {
            imageRects[i] = images[i].GetComponent<RectTransform>();
        }

        // ��ĵ ������ ���� ��ġ�� �� ��ġ ����
        startX = imageRects[0].anchoredPosition.x - imageRects[0].rect.width / 2;
        endX = imageRects[images.Length - 1].anchoredPosition.x + imageRects[images.Length - 1].rect.width / 2;
        scannerWidth = scannerRect.rect.width;

        // ��ĵ ȿ�� �ڷ�ƾ ����
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

            // �� �̹����� ���� ����
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

        // ��ĵ�� ���� �� ��� �̹����� ������ ��Ÿ������ ����
        foreach (Image image in images) {
            Color color = image.color;
            color.a = 1f;
            image.color = color;
        }
    }
}
