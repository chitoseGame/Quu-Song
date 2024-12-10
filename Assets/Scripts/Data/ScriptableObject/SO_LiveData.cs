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
    private string url;//���JURL�𒣂邱��

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
        ArchiveDataAry = CSVSerializer.Deserialize<ArchiveData>(request.downloadHandler.text).ToList();

        // �f�[�^�̍X�V������������AScriptableObject��ۑ�����
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();

        Debug.Log(" �f�[�^�̍X�V���������܂���");
    }
#endif
}
//SheetData�̃C���X�y�N�^��LoadSheetData()���Ăяo���{�^����\������N���X
#if UNITY_EDITOR
[CustomEditor(typeof(SO_LiveData))]
public class ArchiveDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // �f�t�H���g�̃C���X�y�N�^��\��
        base.OnInspectorGUI();

        // �f�[�^�X�V�{�^����\��
        if (GUILayout.Button("�f�[�^�X�V"))
        {
            ((SO_LiveData)target).LoadSheetData();
            //((SO_SongVideo)target).LoadSheetData();
        }
    }
}
#endif