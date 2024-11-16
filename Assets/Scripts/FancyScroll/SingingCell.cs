using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FancyScrollView;
using UnityEngine.UI;
using FancyScrollView.Example03;
using TMPro;
public class SingingCell : FancyCell<SingingItemData,Context>
{
    [SerializeField] Animator animator = default;
    [SerializeField] TextMeshProUGUI message = default;
    [SerializeField] Image image = default;
    [SerializeField] RawImage imageLarge = default;
    [SerializeField] Button button = default;
    [SerializeField] Button imageButton = default;

    static class AnimatorHash
    {
        public static readonly int Scroll = Animator.StringToHash("scroll");
    }

    void Start()
    {
        button.onClick.AddListener(() => Context.OnCellClicked?.Invoke(Index));
        var so=FindObjectOfType<SearchYoutube>();
        imageButton.onClick.AddListener(() =>
        {
            Debug.LogError($"{so.SongVideo.sheetDataRecords[Context.SelectedIndex].url}");
            string url = so.SongVideo.sheetDataRecords[Context.SelectedIndex].url;
            Application.OpenURL(url);
        });
    }

    public override void UpdateContent(SingingItemData itemData)
    {
        message.text = itemData.Name;

        var selected = Context.SelectedIndex == Index;
        imageLarge.texture = itemData.Texture;
        imageLarge.SetNativeSize();
    }

    public override void UpdatePosition(float position)
    {
        currentPosition = position;

        if (animator.isActiveAndEnabled)
        {
            animator.Play(AnimatorHash.Scroll, -1, position);
        }

        animator.speed = 0;
    }

    // GameObject が非アクティブになると Animator がリセットされてしまうため
    // 現在位置を保持しておいて OnEnable のタイミングで現在位置を再設定します
    float currentPosition = 0;

    void OnEnable() => UpdatePosition(currentPosition);
}
