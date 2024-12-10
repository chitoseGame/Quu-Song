//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UIElements;
//public class UTK_View : BaseUTKView
//{
//    [SerializeField]
//    SO_Song _so_songLive;

//    private ListView _songListView;
//    private VisualElement _scrollContent;
//    private VisualElement _songListContent;

//    private MyDictionary _dateDic = new MyDictionary(); //���ɂ������̃f�B���N�g��
//    private List<SongData> _songData;
//    // Start is called before the first frame update
//    void Awake()
//    {
//        _rootElement = _uiDocument.rootVisualElement;
//        _songListContent = _rootElement.Query<VisualElement>("unity-content");

//        for (int i = 0; i < _so_songLive.sheetDataRecords.Length; i++)
//        {
//            _dateDic.Add(_so_songLive.sheetDataRecords[i].date);
//        }

//        int index = 0;

//        _songData = new List<SongData>();

//        _songListView = _rootElement.Query<ListView>("songList");
//        _songListView.itemsSource = _songData;

//        _songListView.makeItem = () =>
//        {
//            var element = new UTK_Song();
//            //element.style.height = 80;
//            //element.songButton.RegisterCallback<ClickEvent>((evt) => OnClickSong(element));
//            return element;
//        };
//        _songListView.bindItem = (element, i) =>
//        {
//            var item = (UTK_Song)element;
//            element.userData = _songData[i];
//            item.SetButtonName(_songData[i].Artist, _songData[i].SongName);
//            item.songButton.clicked+=()=>OnClickSong(element);
//        };
//        _songListView.style.flexGrow = 1;
//        _songListView.fixedItemHeight = 100;
//        var scrollView = _songListView.Q<ScrollView>();
//        // スクロールバーの幅を設定
//        var verticalScroller = scrollView.Query<Scroller>().First();
//        if (verticalScroller != null)
//        {
//            verticalScroller.style.width = 50; // 幅を設定
//            var lowButton = verticalScroller.Q("unity-low-button");
//            if(lowButton!=null)
//            {
//                lowButton.style.width = 50; // 幅を設定
//            }
//            var highButton = verticalScroller.Q("unity-high-button");
//            if(highButton!=null)
//            {
//                highButton.style.width = 50; // 幅を設定
//            }
//        }

//        var verticalSlider = verticalScroller.Q("unity-slider");
        
//        if (verticalSlider != null)
//        {
//            verticalSlider.style.width = 50; // スライダーの幅を設定
//            var dragger = verticalSlider.Q("unity-dragger");
//            if(dragger!=null)
//            {
//                dragger.style.width = 50; // スライダーの幅を設定
//            }
//        }

//        //_songListView.style.height = 300;
//        VisualElement content = _songListView.Query<VisualElement>("unity-content-container");
//        content.style.flexGrow = 1;
//        //content.

//        string dateTime = "";
//        string url = "";
//        string title = "";
//        //SheetData sheetData = new SheetData();
//        for (int i = 0; i < _dateDic.indexMax; i++)
//        {
//            List<SongData> songList = new List<SongData>();
//            dateTime = _so_songLive.sheetDataRecords[index].date;
//            url = _so_songLive.sheetDataRecords[index].url;
//            title = _so_songLive.sheetDataRecords[index].title;
//            for (int j = 0; j < _dateDic.GetQtyByIndex(i); j++)
//            {
//                songList.Add(new SongData(_so_songLive.sheetDataRecords[index].songName, _so_songLive.sheetDataRecords[index].artist, _so_songLive.sheetDataRecords[index].time));
//                index++;
//            }

//            //sheetData.data = songList;
//            _sheetData.Add(new SheetData(dateTime, title, url, songList));
//            //Debug.LogError(_sheetData[i].data.Count);

//        }
//        _songListView.Rebuild();
//        //TODO:
//        //�Ȃ̃��X�g��SongList,Date�̃��X�g��_sheetData�Ő�������

//        //for (int i = 0; i < _so_songLive.sheetDataRecords.Length; i++)
//        //{
//        //    UTK_Song song = new UTK_Song();
//        //    //song.CreateSong(_songListContent);
//        //    song.SetButtonName(_so_songLive.sheetDataRecords[i].artist, _so_songLive.sheetDataRecords[i].songName);
//        //}


//    }

//    void OnClickSong(VisualElement element)
//    {
        
//        var data = (SongData)element.userData;
//        string link = $"{data.Url}&t={ConvertSecondText(data.Time)}s";
//        //Debug.LogError($"Open URL:{link}");
//        Application.OpenURL(link);
//    }
//    public string ConvertSecondText(string dateTime)
//    {
//        // 「:」が1つしかない場合は、時間が省略されたとみなして「0:」を追加
//        if (dateTime.Split(':').Length == 2)
//        {
//            dateTime = "0:" + dateTime;
//        }
//        Debug.LogError(dateTime);
//        TimeSpan timeSpan = TimeSpan.Parse(dateTime);
//        double totalSeconds = timeSpan.TotalSeconds;
//        //Debug.LogError($"Second:{totalSeconds}");

//        return $"{totalSeconds}";
//    }

//}
