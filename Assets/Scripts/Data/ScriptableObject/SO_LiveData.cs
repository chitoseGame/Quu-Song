using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;


#if UNITY_EDITOR
using UnityEditor;
#endif


[CreateAssetMenu(fileName = "SO_LiveData", menuName = "ScriptableObjects/LiveData")]
public class SO_LiveData : ScriptableObject
{
    public List<ArchiveData> ArchiveDataAry;
    [SerializeField]
    private string url;//公開URLを張ること

    [System.Serializable]
    public class ArchiveData
    {
        public string data;
        public string title;
        public string url;
        public string type;
        public string time;
    }

#if UNITY_EDITOR

    public void LoadSheetData()
    {
        // urlからCSV形式の文字列をダウンロードする
        using UnityWebRequest request = UnityWebRequest.Get(url);
        request.SendWebRequest();
        while (request.isDone == false)
        {
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
            }
        }

        // ダウンロードしたCSVをデシリアライズ(SerializeFieldに入力)する
        ArchiveDataAry = CSVSerializer.Deserialize<ArchiveData>(request.downloadHandler.text).ToList();

        // データの更新が完了したら、ScriptableObjectを保存する
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();

        Debug.Log(" データの更新を完了しました");
    }
#endif
}
//SheetDataのインスペクタにLoadSheetData()を呼び出すボタンを表示するクラス
#if UNITY_EDITOR
[CustomEditor(typeof(SO_LiveData))]
public class ArchiveDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // デフォルトのインスペクタを表示
        base.OnInspectorGUI();

        // データ更新ボタンを表示
        if (GUILayout.Button("データ更新"))
        {
            ((SO_LiveData)target).LoadSheetData();
            //((SO_SongVideo)target).LoadSheetData();
        }
    }
}
#endif