using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using System.Threading.Tasks;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif


[CreateAssetMenu(fileName = "SO_SongVideo", menuName = "ScriptableObjects/SongVideoData")]
public class SO_SongVideo : ScriptableObject
{
    public List<SongMovieData> songMovieData = new List<SongMovieData>();
    List<Texture2D> textures = new List<Texture2D>();
    SheetDataRecord[] sheetDataRecords;
    [SerializeField]
    private string url;//���JURL�𒣂邱��

    [System.Serializable]
    public class SheetDataRecord
    {
        public string date;
        public string songName;
        public string url;
    }


    [System.Serializable]
    public class SongMovieData
    {
        public int id;
        public string date;
        public string songName;
        public string url;
        public Texture2D thumbnail;

        public SongMovieData(int id,string date, string song, string url, Texture2D texture)
        {
            this.id = id;
            this.date = date;
            this.songName = song;
            this.url = url;
            this.thumbnail = texture;
        }
    }

    public SongMovieData GetSongMovieData(int id)
    {
        SongMovieData data = new SongMovieData(-1,"", "", "", null);
        foreach (var song in songMovieData)
        {
            if(song.id == id)
            {
                data = song;
                break;
            }
        }
        return data;
        }

#if UNITY_EDITOR

    public async void LoadSheetData()
    {
        songMovieData.Clear();
        // url����CSV�`���̕�������_�E�����[�h����
        using UnityWebRequest request = UnityWebRequest.Get(url);
        request.SendWebRequest();
        while (request.isDone == false)
        {
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
            }
        }

        // �_�E�����[�h����CSV���f�V���A���C�Y(SerializeField�ɓ���)����
        sheetDataRecords = CSVSerializer.Deserialize<SheetDataRecord>(request.downloadHandler.text);
        await LoadThumbnail2();

        for (int i = 0; i < sheetDataRecords.Length; i++)
        {
            songMovieData.Add(new SongMovieData(i,sheetDataRecords[i].date, sheetDataRecords[i].songName, sheetDataRecords[i].url, textures[i]));
        }
        // �f�[�^�̍X�V������������AScriptableObject��ۑ�����
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();

        Debug.Log(" �f�[�^�̍X�V���������܂���");
    }
#endif

    public string GetId(string url)
    {
        string[] urls = url.Split("=");
        if (urls.Length != 2)
        {
            return null;
        }
        return urls[urls.Length - 1];
    }
    public async Task LoadThumbnail()
    {
        textures.Clear();
        foreach (var data in sheetDataRecords)
        {
            string imageURL = $"https://img.youtube.com/vi/{GetId(data.url)}/hqdefault.jpg";
            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageURL))
            {
                var asyncOperation = request.SendWebRequest();
                while (!asyncOperation.isDone)
                {
                    await Task.Yield(); // �t���[����҂�
                }

                if (request.result == UnityWebRequest.Result.Success)
                {
                    Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                    //thumbnailImage.texture = texture;
                    textures.Add(texture);
                }
                else
                {
                    Debug.LogError($"Failed to load thumbnail: {request.error}");
                }
            }
        }

        //    var items = Enumerable.Range(0, sheetDataRecords.Length)
        //.Select(i => new SingingItemData($"{sheetDataRecords[i].songName}", sheetDataRecords[i].url, textures[i]))
        //.ToArray();

        //    scrollView.UpdateData(items);
        //    scrollView.SelectCell(0);
    }

    // �T���l�C����񓯊��Ń_�E�����[�h
    public async Task LoadThumbnail2()
    {
        textures.Clear();
        foreach (var data in sheetDataRecords)
        {
            if (string.IsNullOrEmpty(data.url))
            {
                Debug.LogWarning("Thumbnail URL is empty!");
                return;
            }
            string imageURL = $"https://img.youtube.com/vi/{GetId(data.url)}/hqdefault.jpg";
            using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(imageURL))
            {
                var asyncOperation = webRequest.SendWebRequest();

                while (!asyncOperation.isDone)
                {
                    await System.Threading.Tasks.Task.Yield(); // �񓯊��őҋ@
                }

                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    textures.Add(DownloadHandlerTexture.GetContent(webRequest));
                    Debug.Log("Thumbnail loaded successfully!");
                }
                else
                {
                    Debug.LogError($"Failed to load thumbnail: {webRequest.error}");
                }
            }
        }
    }

}
//SheetData�̃C���X�y�N�^��LoadSheetData()���Ăяo���{�^����\������N���X
#if UNITY_EDITOR
[CustomEditor(typeof(SO_SongVideo))]
public class VideoSheetDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // �f�t�H���g�̃C���X�y�N�^��\��
        base.OnInspectorGUI();

        // �f�[�^�X�V�{�^����\��
        if (GUILayout.Button("�f�[�^�X�V"))
        {
            ((SO_SongVideo)target).LoadSheetData();
            //((SO_SongVideo)target).LoadSheetData();
        }
    }
}
#endif