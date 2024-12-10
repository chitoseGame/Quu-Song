using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class BaseUTKView : MonoBehaviour
{
    [SerializeField]
    protected UIDocument _uiDocument;
    protected VisualElement _rootElement;


    private void Awake()
    {
        // UI ToolkitのルートVisualElementを取得
        _rootElement = _uiDocument.rootVisualElement;
        // 親要素のサイズを取得
        var screenWidth = Screen.width;
        var screenHeight = Screen.height;

        // 動的なサイズ調整
        _rootElement.style.width = new Length(screenWidth, LengthUnit.Pixel);
        _rootElement.style.height = new Length(screenHeight, LengthUnit.Pixel);

        // セーフエリアを適用
        ApplySafeArea(Screen.safeArea);
    }

    private void ApplySafeArea(Rect safeArea)
    {
        // 画面全体のサイズ
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // セーフエリアの各方向のマージンを計算
        float left = safeArea.xMin / screenWidth * 100; // 左端のパーセンテージ
        float right = (screenWidth - safeArea.xMax) / screenWidth * 100; // 右端のパーセンテージ
        float top = (screenHeight - safeArea.yMax) / screenHeight * 100; // 上端のパーセンテージ
        float bottom = safeArea.yMin / screenHeight * 100; // 下端のパーセンテージ

        // セーフエリアのマージンを適用
        _rootElement.style.paddingLeft = new Length(left, LengthUnit.Percent);
        _rootElement.style.paddingRight = new Length(right, LengthUnit.Percent);
        _rootElement.style.paddingTop = new Length(top, LengthUnit.Percent);
        _rootElement.style.paddingBottom = new Length(bottom, LengthUnit.Percent);
    }
    public void SetActive(bool value)
    {
        _rootElement.style.display = value ? DisplayStyle.Flex : DisplayStyle.None;
    }
}
