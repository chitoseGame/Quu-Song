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

    public Button AllSongBtn { get => _allSongBtn; }
    public Button SearchBtn { get => _searchBtn; }
    public TextField SearchField { get => _searchField;}

    private void Awake()
    {
        _rootElement = _uiDocument.rootVisualElement;
        _songRoot = _rootElement.Query<VisualElement>("SongRoot");
        _allSongBtn = _songRoot.Query<Button>("AllSong");
        _allSongBtn.style.unityFont =UIToolkitManager.Instance.JapaneseFont;
        _searchBtn = _songRoot.Query<Button>("SearchList");
        _searchBtn.style.unityFont = UIToolkitManager.Instance.JapaneseFont;
        _searchField = _songRoot.Query<TextField>("SongNameField");
        _searchField.style.unityFont = UIToolkitManager.Instance.JapaneseFont;
        _placeholderLabel = _searchField.Query<Label>("placeholderText");
        _placeholderLabel.style.unityFont = UIToolkitManager.Instance.JapaneseFont;

        _searchField.RegisterValueChangedCallback(text => 
        {
            Debug.Log(string.IsNullOrEmpty(text.newValue));
            _placeholderLabel.style.display = string.IsNullOrEmpty(text.newValue) ? DisplayStyle.Flex : DisplayStyle.None;
        });
        //gameObject.SetActive(false);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }


}
