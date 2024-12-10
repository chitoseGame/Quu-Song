using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class ScrollLabel : VisualElement
{
    private Label scrollingLabel;
    //private VisualElement root;
    private float textPosition; // テキストの現在位置
    private const float Speed = 50f; // テキストのスクロール速度（ピクセル/秒）
    private float startPositionX; // ラベルの開始位置
    private float targetPositionX; // ラベルが最終的に移動する位置
    private float labelWidth; // ラベルの幅
    private float containerWidth; // 親コンテナの幅

    public string text
    {
        get { return scrollingLabel.text; }
        set {
            scrollingLabel.text = value;
            // 初期位置とラベル幅を取得
            labelWidth = scrollingLabel.resolvedStyle.width;
            startPositionX = containerWidth;
            targetPositionX = -labelWidth;
        }
    }


    public void CreateLabel(VisualElement root,string style)
    {
        // ルート要素を取得
        //root = GetComponent<UIDocument>().rootVisualElement;

        // コンテナを作成
        var container = new VisualElement();
        container.style.flexDirection = FlexDirection.Row;
        container.style.overflow = Overflow.Hidden; // テキストがはみ出さないように設定
        container.style.width = Length.Percent(25); // コンテナの幅
        container.style.height = new StyleLength(100); // 高さ
        container.style.borderTopWidth = 1;
        container.style.borderBottomWidth = 1;
        container.style.borderRightWidth = 1;
        container.style.borderLeftWidth = 1;
        container.style.borderTopColor = new StyleColor(Color.black);
        container.style.borderBottomColor = new StyleColor(Color.black);
        container.style.borderLeftColor = new StyleColor(Color.black);
        container.style.borderRightColor = new StyleColor(Color.black);

        // ラベルを作成
        scrollingLabel = new Label();
        scrollingLabel.AddToClassList(style);
        scrollingLabel.style.position = Position.Absolute; // 絶対位置
        container.Add(scrollingLabel);

        root.Add(container);

        // 初期位置とラベル幅を取得
        //containerWidth = container.resolvedStyle.width;
        //if(containerWidth==float.NaN)
        //{
        //    Debug.LogError(root.style.width.value.value);
        //    containerWidth = root.style.width.value.value / 4;
        //}
        //Debug.LogError(containerWidth);
        //UpdateScroll();

    }
   
    //private async void StartScroll()
    //{
    //    // 親要素とラベルの幅を取得
    //    float containerWidth = resolvedStyle.width;
    //    float textWidth = scrollingLabel.resolvedStyle.width;

    //    // 値が取得できるか確認（デバッグ用）
    //    Debug.Log($"Container Width: {containerWidth}");
    //    Debug.Log($"Text Width: {textWidth}");

    //    while (true)
    //    {
    //        // 初期位置を右端に設定
    //        float currentPosition = containerWidth;

    //        // テキストが左端を通り過ぎるまで移動
    //        while (currentPosition + textWidth > 0)
    //        {
    //            currentPosition -= Speed * Time.deltaTime;
    //            scrollingLabel.style.left = currentPosition;
    //            Debug.LogError(currentPosition);
    //            // フレームごとの更新を待機
    //            await Task.Yield();
    //        }
    //    }
    //}
    //public async void UpdateScroll()
    //{
    //    while(containerWidth!=float.NaN)
    //    {
    //        await Task.Yield();
    //    }

    //    Debug.LogError("よばれた");
    //    while (true)
    //    {
    //        // ラベルを自動的にスクロール
    //        float newPositionX = scrollingLabel.resolvedStyle.left - (Speed * Time.deltaTime);

    //        // ラベルがスクロールしきったら位置をリセット
    //        if (newPositionX <= targetPositionX)
    //        {
    //            newPositionX = startPositionX; // スクロールを再開
    //        }

    //        // ラベルの位置を更新
    //        scrollingLabel.style.left = newPositionX;
    //        await Task.Yield();
    //    }
    //}
}
