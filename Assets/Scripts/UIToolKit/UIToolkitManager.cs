using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Config;
using UnityEngine.UIElements;
using System;
using UTK;
public class UIToolkitManager : Singleton<UIToolkitManager>
{

    [SerializeField]
    private SongView _songView;
    [SerializeField]
    private MainPanel _mainPanel;

    [SerializeField]
    private TestSingingViewr _songVideoViewr;
    [SerializeField]
    SO_Song _so_songLive;
    [SerializeField]
    SO_SongVideo _songVideo;
    [SerializeField]
    private Font _japaneseFont;



    public SO_SongVideo SongVideo { get => _songVideo; }
    public Font JapaneseFont { get { return _japaneseFont; } }



    private List<SongData> _songData;
    private void Awake()
    {
        
    }

    private void Start()
    {
        SetEvent();

        _songData = new List<SongData>();
        _songView.CreateListView(_songData, OnClickSong);

        _mainPanel.SetActive(true);
        _songView.SetActive(false);
    }
    void SetEvent()
    {
        _mainPanel.AllSongBtn.clicked+=OnAllSong;
        _mainPanel.SearchBtn.clicked += OnSearchSong;
        _mainPanel.SongLiveBtn.clicked+=OnSongLive;
        _mainPanel.SongVideoBtn.clicked+=OnSongVideo;

        _songView.BackButton.clicked += OnBackPanel;
    }
    /// <summary>
    /// 全曲表示ボタンのイベント
    /// </summary>
    void OnAllSong()
    {
        _mainPanel.SetActive(false);
        _songView.SetActive(true);
        for (int i = 0; i < _so_songLive.sheetDataRecords.Length; i++)
        {
            var so = _so_songLive.sheetDataRecords[i];
            SongData data = new SongData(so.date, so.title, so.url, so.songName, so.artist, so.time);
            _songData.Add(data);
        }
    }
    /// <summary>
    /// 曲検索ボタンのイベント
    /// </summary>
    void OnSearchSong()
    {
        _mainPanel.SetActive(false);
        _songView.SetActive(true);
        for (int i = 0; i < _so_songLive.sheetDataRecords.Length; i++)
        {
            var so = _so_songLive.sheetDataRecords[i];

            if (so.songName.Contains(_mainPanel.SearchField.value))
            {
                SongData data = new SongData(so.date, so.title, so.url, so.songName, so.artist, so.time);
                _songData.Add(data);
            }
        }
    }

    /// <summary>
    /// 戻るボタンのイベント
    /// </summary>
    void OnBackPanel()
    {
        _songData.Clear();
        _mainPanel.SetActive(true);
        _songView.SetActive(false);
    }

    /// <summary>
    /// 歌ボタンのクリックイベント
    /// </summary>
    /// <param name="element"></param>
    public void OnClickSong(VisualElement element)
    {
        var data = (SongData)element.userData;
        string link = $"{data.Url}&t={ConvertSecondText(data.Time)}s";
        //Debug.LogError($"Open URL:{link}");
        Application.OpenURL(link);
    }

    void OnSongLive()
    {
        _mainPanel.ActiveSongRoot(true);
        _songVideoViewr.gameObject.SetActive(false);
    }

    void OnSongVideo()
    {
        _mainPanel.ActiveSongRoot(false);
        _songVideoViewr.gameObject.SetActive(true);
    }

    /// <summary>
    /// DateTimeから秒数の文字列に変換
    /// </summary>
    /// <param name="dateTime">もとになるデータ</param>
    /// <returns></returns>
    public string ConvertSecondText(string dateTime)
    {
        // 「:」が1つしかない場合は、時間が省略されたとみなして「0:」を追加
        if (dateTime.Split(':').Length == 2)
        {
            dateTime = "0:" + dateTime;
        }
        Debug.Log(dateTime);
        TimeSpan timeSpan = TimeSpan.Parse(dateTime);
        double totalSeconds = timeSpan.TotalSeconds;
        //Debug.LogError($"Second:{totalSeconds}");

        return $"{totalSeconds}";
    }
}
