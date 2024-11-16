using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static SO_Song;

#if UNITY_EDITOR
using UnityEditor;
#endif


[CreateAssetMenu(fileName = "Song", menuName = "ScriptableObjects/SongData")]
public class SO_Song : ScriptableObject
{
    public SheetDataRecord[]sheetDataRecords;
    [SerializeField]
    private string url;//公開URLを張ること

    [System.Serializable]
    public class SheetDataRecord
    {
        public string date;

        public string title;


        public string songName;

        public string artist;
        public string time;
        public string url;
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
        sheetDataRecords = CSVSerializer.Deserialize<SheetDataRecord>(request.downloadHandler.text);

        // データの更新が完了したら、ScriptableObjectを保存する
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();

        Debug.Log(" データの更新を完了しました");
    }
#endif
}
//SheetDataのインスペクタにLoadSheetData()を呼び出すボタンを表示するクラス
#if UNITY_EDITOR
[CustomEditor(typeof(SO_Song))]
public class SheetDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // デフォルトのインスペクタを表示
        base.OnInspectorGUI();

        // データ更新ボタンを表示
        if (GUILayout.Button("データ更新"))
        {
            ((SO_Song)target).LoadSheetData();
            //((SO_Song)target).LoadSheetData();
        }
    }
}
#endif