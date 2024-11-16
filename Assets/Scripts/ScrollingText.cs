using UnityEngine;
using UnityEngine.UI;

public class ScrollingText : MonoBehaviour
{
    public float scrollSpeed = 50f; // スクロール速度
    private RectTransform rectTransform; // テキストのRectTransform
    private float startPosition; // スクロールの開始位置
    private float endPosition; // スクロールの終了位置
    private float screenWidth; // 画面の幅

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        // テキストの初期位置を保存
        startPosition = rectTransform.anchoredPosition.x;

        // 画面の幅を取得
        screenWidth = Screen.width;

        // 終了位置は画面左側の外に設定
        endPosition = -rectTransform.rect.width; // テキストの幅を基に終了位置を計算
    }

    void Update()
    {
        // テキストを左に移動させる
        rectTransform.anchoredPosition += new Vector2(-scrollSpeed * Time.deltaTime, 0);

        // テキストが左端を越えたら、右側の画面外に移動させる
        if (rectTransform.anchoredPosition.x < endPosition)
        {
            rectTransform.anchoredPosition = new Vector2(screenWidth, rectTransform.anchoredPosition.y);
        }
    }
}