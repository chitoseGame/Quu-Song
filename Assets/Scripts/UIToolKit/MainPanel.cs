using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainPanel : BaseUTKView
{
    private VisualElement _songRoot;
    Button _allSongBtn;
    Button _searchBtn;

    TextField _searchField;
    Label _placeholderLabel;

    Button _songLiveBtn;
    Button _songVideoBtn;

    public Button AllSongBtn { get => _allSongBtn; }
    public Button SearchBtn { get => _searchBtn; }
    public TextField SearchField { get => _searchField; }
    public Button SongLiveBtn { get => _songLiveBtn; }
    public Button SongVideoBtn { get => _songVideoBtn; }

    private void Awake()
    {
        _rootElement = _uiDocument.rootVisualElement;
        _songRoot = _rootElement.Query<VisualElement>("SongRoot");
        _allSongBtn = _songRoot.Query<Button>("AllSong");
        _allSongBtn.style.unityFont = UIToolkitManager.Instance.JapaneseFont;
        _searchBtn = _songRoot.Query<Button>("SearchList");
        _searchBtn.style.unityFont = UIToolkitManager.Instance.JapaneseFont;
        _searchField = _songRoot.Query<TextField>("SongNameField");
        _searchField.style.unityFont = UIToolkitManager.Instance.JapaneseFont;
        _placeholderLabel = _searchField.Query<Label>("placeholderText");
        _placeholderLabel.style.unityFont = UIToolkitManager.Instance.JapaneseFont;

        VisualElement bottom = _rootElement.Query<VisualElement>("BottomButton");
        _songLiveBtn = bottom.Query<Button>("SongLiveButton");
        _songVideoBtn = bottom.Query<Button>("SongVideoButton");
        _searchField.RegisterValueChangedCallback(text =>
        {
            //Debug.Log(string.IsNullOrEmpty(text.newValue));
            _placeholderLabel.style.display = string.IsNullOrEmpty(text.newValue) ? DisplayStyle.Flex : DisplayStyle.None;
        });
    }

    /// <summary>
    /// SongRootオブジェクトのアクティブ切り替え
    /// </summary>
    /// <param name="value"></param>
    public void ActiveSongRoot(bool value)
    {
        _songRoot.style.display = value ? DisplayStyle.Flex : DisplayStyle.None;
    }

}
