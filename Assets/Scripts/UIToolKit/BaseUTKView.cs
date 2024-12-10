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
        // UI Toolkit�̃��[�gVisualElement���擾
        _rootElement = _uiDocument.rootVisualElement;
        // �e�v�f�̃T�C�Y���擾
        var screenWidth = Screen.width;
        var screenHeight = Screen.height;

        // ���I�ȃT�C�Y����
        _rootElement.style.width = new Length(screenWidth, LengthUnit.Pixel);
        _rootElement.style.height = new Length(screenHeight, LengthUnit.Pixel);

        // �Z�[�t�G���A��K�p
        ApplySafeArea(Screen.safeArea);
    }

    private void ApplySafeArea(Rect safeArea)
    {
        // ��ʑS�̂̃T�C�Y
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // �Z�[�t�G���A�̊e�����̃}�[�W�����v�Z
        float left = safeArea.xMin / screenWidth * 100; // ���[�̃p�[�Z���e�[�W
        float right = (screenWidth - safeArea.xMax) / screenWidth * 100; // �E�[�̃p�[�Z���e�[�W
        float top = (screenHeight - safeArea.yMax) / screenHeight * 100; // ��[�̃p�[�Z���e�[�W
        float bottom = safeArea.yMin / screenHeight * 100; // ���[�̃p�[�Z���e�[�W

        // �Z�[�t�G���A�̃}�[�W����K�p
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
