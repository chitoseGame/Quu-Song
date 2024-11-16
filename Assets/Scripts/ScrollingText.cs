using UnityEngine;
using UnityEngine.UI;

public class ScrollingText : MonoBehaviour
{
    public float scrollSpeed = 50f; // �X�N���[�����x
    private RectTransform rectTransform; // �e�L�X�g��RectTransform
    private float startPosition; // �X�N���[���̊J�n�ʒu
    private float endPosition; // �X�N���[���̏I���ʒu
    private float screenWidth; // ��ʂ̕�

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        // �e�L�X�g�̏����ʒu��ۑ�
        startPosition = rectTransform.anchoredPosition.x;

        // ��ʂ̕����擾
        screenWidth = Screen.width;

        // �I���ʒu�͉�ʍ����̊O�ɐݒ�
        endPosition = -rectTransform.rect.width; // �e�L�X�g�̕�����ɏI���ʒu���v�Z
    }

    void Update()
    {
        // �e�L�X�g�����Ɉړ�������
        rectTransform.anchoredPosition += new Vector2(-scrollSpeed * Time.deltaTime, 0);

        // �e�L�X�g�����[���z������A�E���̉�ʊO�Ɉړ�������
        if (rectTransform.anchoredPosition.x < endPosition)
        {
            rectTransform.anchoredPosition = new Vector2(screenWidth, rectTransform.anchoredPosition.y);
        }
    }
}