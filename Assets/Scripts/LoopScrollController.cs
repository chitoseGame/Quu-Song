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
        // �I�u�W�F�N�g�v�[�����쐬
        _pool = new ObjectPool<GameObject>(
            // �I�u�W�F�N�g��������
            () => CreateListObject(),
            // �I�u�W�F�N�g���v�[������擾����鎞�̏���
            o => o.SetActive(true),
            // �I�u�W�F�N�g���v�[���ɖ߂���鎞�̏���
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
    // LoopScrollPrefabSource�̎���
    // GameObject���V�����\���̂��߂ɕK�v�ɂȂ������ɌĂ΂��
    GameObject LoopScrollPrefabSource.GetObject(int index)
    {
        // �I�u�W�F�N�g�v�[������GameObject���擾
        return _pool.Get();
    }
    // LoopScrollPrefabSource�̎���
    // GameObject���s�v�ɂȂ������ɌĂ΂��
    void LoopScrollPrefabSource.ReturnObject(Transform trans)
    {
        // �I�u�W�F�N�g�v�[����GameObject��ԋp
        _pool.Release(trans.gameObject);
    }

    // LoopScrollDataSource�̎���
    // �v�f���\������鎞�̏���������
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