using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

[RequireComponent(typeof(LoopScrollRect))]
[DisallowMultipleComponent]
public sealed class LoopScrollController : MonoBehaviour, LoopScrollPrefabSource, LoopScrollDataSource
{

    [SerializeField] private GameObject _listViewPrefab;
    [SerializeField] private SongView _songViewPrefab;

    [SerializeField]
    SO_Song _so_SongData;

    private ObjectPool<GameObject> _pool;
    private List<SheetData> _sheetData;
    //private Dictionary<string, int> _dateDic;
    private MyDictionary _dateDic = new MyDictionary();


    private void Start()
    {
        //_dateDic = new Dictionary<string, int>();
        for(int i=0;i<_so_SongData.sheetDataRecords.Length;i++)
        {
            _dateDic.Add(_so_SongData.sheetDataRecords[i].date);
        }
        int index = 0;
        
        _sheetData = new List<SheetData>();
        string dateTime = "";
        string url = "";
        string title = "";
        //SheetData sheetData = new SheetData();
        for (int i=0;i<_dateDic.indexMax;i++)
        {
            List<SongData> songList = new List<SongData>();
            dateTime = _so_SongData.sheetDataRecords[index].date;
            url = _so_SongData.sheetDataRecords[index].url;
            title = _so_SongData.sheetDataRecords[index].title;
           for (int j=0;j<_dateDic.GetQtyByIndex(i);j++)
            {
                songList.Add(new SongData(_so_SongData.sheetDataRecords[index].songName, _so_SongData.sheetDataRecords[index].artist));
                index++;
            }
            
            //sheetData.data = songList;
            _sheetData.Add(new SheetData(dateTime,title,url,songList));
            //Debug.LogError(_sheetData[i].data.Count);
        }
        Initialize(_sheetData.Count);
    }
    public void Initialize(int listCount)
    {
        // オブジェクトプールを作成
        _pool = new ObjectPool<GameObject>(
            // オブジェクト生成処理
            () => CreateListObject(),
            // オブジェクトがプールから取得される時の処理
            o => o.SetActive(true),
            // オブジェクトがプールに戻される時の処理
            o =>
            {
                o.transform.SetParent(transform);
                o.SetActive(false);
            });

        var scrollRect = GetComponent<LoopScrollRect>();
        scrollRect.prefabSource = this;
        scrollRect.dataSource = this;
        scrollRect.totalCount = listCount;
        scrollRect.RefillCells();
    }
    int index = 0;
    GameObject CreateListObject()
    {
        var obj = Instantiate(_listViewPrefab);
        var adSong = obj.GetComponent<SongListView>();
        
        foreach (var song in _sheetData[index].data)
        {
            var songObj = Instantiate(_songViewPrefab, adSong.ContentRoot.transform);
            songObj.Initialize(song);
            //Debug.LogError($"song:{song.SongName}/{song.Artist}");
        }
        index++;
       
        return obj;
    }
    // LoopScrollPrefabSourceの実装
    // GameObjectが新しく表示のために必要になった時に呼ばれる
    GameObject LoopScrollPrefabSource.GetObject(int index)
    {
        // オブジェクトプールからGameObjectを取得
        return _pool.Get();
    }
    // LoopScrollPrefabSourceの実装
    // GameObjectが不要になった時に呼ばれる
    void LoopScrollPrefabSource.ReturnObject(Transform trans)
    {
        // オブジェクトプールにGameObjectを返却
        _pool.Release(trans.gameObject);
    }

    // LoopScrollDataSourceの実装
    // 要素が表示される時の処理を書く
    void LoopScrollDataSource.ProvideData(Transform trans, int index)
    {
        //
        var listView = trans.GetComponent<SongListView>();
        if (listView != null)
        {
            //Debug.LogError(_sheetData[index].date);
            //if (!string.IsNullOrEmpty(_sheetData[index].date))
            {
                listView.Date.text = _sheetData[index].date;
            }
        }
    }
}