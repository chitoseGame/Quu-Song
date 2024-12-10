using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class ScrollLabel : VisualElement
{
    private Label scrollingLabel;
    //private VisualElement root;
    private float textPosition; // �e�L�X�g�̌��݈ʒu
    private const float Speed = 50f; // �e�L�X�g�̃X�N���[�����x�i�s�N�Z��/�b�j
    private float startPositionX; // ���x���̊J�n�ʒu
    private float targetPositionX; // ���x�����ŏI�I�Ɉړ�����ʒu
    private float labelWidth; // ���x���̕�
    private float containerWidth; // �e�R���e�i�̕�

    public string text
    {
        get { return scrollingLabel.text; }
        set {
            scrollingLabel.text = value;
            // �����ʒu�ƃ��x�������擾
            labelWidth = scrollingLabel.resolvedStyle.width;
            startPositionX = containerWidth;
            targetPositionX = -labelWidth;
        }
    }


    public void CreateLabel(VisualElement root,string style)
    {
        // ���[�g�v�f���擾
        //root = GetComponent<UIDocument>().rootVisualElement;

        // �R���e�i���쐬
        var container = new VisualElement();
        container.style.flexDirection = FlexDirection.Row;
        container.style.overflow = Overflow.Hidden; // �e�L�X�g���͂ݏo���Ȃ��悤�ɐݒ�
        container.style.width = Length.Percent(25); // �R���e�i�̕�
        container.style.height = new StyleLength(100); // ����
        container.style.borderTopWidth = 1;
        container.style.borderBottomWidth = 1;
        container.style.borderRightWidth = 1;
        container.style.borderLeftWidth = 1;
        container.style.borderTopColor = new StyleColor(Color.black);
        container.style.borderBottomColor = new StyleColor(Color.black);
        container.style.borderLeftColor = new StyleColor(Color.black);
        container.style.borderRightColor = new StyleColor(Color.black);

        // ���x�����쐬
        scrollingLabel = new Label();
        scrollingLabel.AddToClassList(style);
        scrollingLabel.style.position = Position.Absolute; // ��Έʒu
        container.Add(scrollingLabel);

        root.Add(container);

        // �����ʒu�ƃ��x�������擾
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
    //    // �e�v�f�ƃ��x���̕����擾
    //    float containerWidth = resolvedStyle.width;
    //    float textWidth = scrollingLabel.resolvedStyle.width;

    //    // �l���擾�ł��邩�m�F�i�f�o�b�O�p�j
    //    Debug.Log($"Container Width: {containerWidth}");
    //    Debug.Log($"Text Width: {textWidth}");

    //    while (true)
    //    {
    //        // �����ʒu���E�[�ɐݒ�
    //        float currentPosition = containerWidth;

    //        // �e�L�X�g�����[��ʂ�߂���܂ňړ�
    //        while (currentPosition + textWidth > 0)
    //        {
    //            currentPosition -= Speed * Time.deltaTime;
    //            scrollingLabel.style.left = currentPosition;
    //            Debug.LogError(currentPosition);
    //            // �t���[�����Ƃ̍X�V��ҋ@
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

    //    Debug.LogError("��΂ꂽ");
    //    while (true)
    //    {
    //        // ���x���������I�ɃX�N���[��
    //        float newPositionX = scrollingLabel.resolvedStyle.left - (Speed * Time.deltaTime);

    //        // ���x�����X�N���[������������ʒu�����Z�b�g
    //        if (newPositionX <= targetPositionX)
    //        {
    //            newPositionX = startPositionX; // �X�N���[�����ĊJ
    //        }

    //        // ���x���̈ʒu���X�V
    //        scrollingLabel.style.left = newPositionX;
    //        await Task.Yield();
    //    }
    //}
}
