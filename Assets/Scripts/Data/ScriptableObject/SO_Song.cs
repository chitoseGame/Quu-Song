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
    private string url;//���JURL�𒣂邱��

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

        // �f�[�^�̍X�V������������AScriptableObject��ۑ�����
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();

        Debug.Log(" �f�[�^�̍X�V���������܂���");
    }
#endif
}
//SheetData�̃C���X�y�N�^��LoadSheetData()���Ăяo���{�^����\������N���X
#if UNITY_EDITOR
[CustomEditor(typeof(SO_Song))]
public class SheetDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // �f�t�H���g�̃C���X�y�N�^��\��
        base.OnInspectorGUI();

        // �f�[�^�X�V�{�^����\��
        if (GUILayout.Button("�f�[�^�X�V"))
        {
            ((SO_Song)target).LoadSheetData();
            //((SO_Song)target).LoadSheetData();
        }
    }
}
#endif