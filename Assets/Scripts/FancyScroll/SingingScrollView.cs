using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FancyScrollView;
using EasingCore;
using FancyScrollView.Example03;
public class SingingItemData
{
    public string Name { get; }
    public string URL { get; }
    public Texture2D Texture { get;}

    public SingingItemData(string name,string url, Texture2D texture)
    {
        Name = name;
        URL = url;
        Texture = texture;
    }
}
public class SingingScrollView : FancyScrollView<SingingItemData,Context>
{
    [SerializeField] Scroller scroller = default;
    [SerializeField] GameObject cellPrefab = default;

    protected override GameObject CellPrefab => cellPrefab;

    protected override void Initialize()
    {
        base.Initialize();

        Context.OnCellClicked = SelectCell;

        scroller.OnValueChanged(UpdatePosition);
        scroller.OnSelectionChanged(UpdateSelection);
    }

    void UpdateSelection(int index)
    {
        if (Context.SelectedIndex == index)
        {
            return;
        }

        Context.SelectedIndex = index;
        Refresh();
    }

    public void UpdateData(IList<SingingItemData> items)
    {
        UpdateContents(items);
        scroller.SetTotalCount(items.Count);
    }

    public void SelectCell(int index)
    {
        if (index < 0 || index >= ItemsSource.Count || index == Context.SelectedIndex)
        {
            return;
        }

        UpdateSelection(index);
        scroller.ScrollTo(index, 0.35f, Ease.OutCubic);
    }
}
