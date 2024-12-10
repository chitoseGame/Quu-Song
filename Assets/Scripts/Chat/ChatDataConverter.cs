using UnityEngine;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
namespace chat
{

    public class ChatManager : MonoBehaviour
    {
        public SO_ChatData chatData; // �`���b�g�f�[�^�iUnity�̃X�N���v�^�u���I�u�W�F�N�g�j

        // JSON�t�@�C���̃p�X�iAssets���̃t�@�C���j
        public string jsonFilePath = "Assets/Path/To/Your/live_chat.json";

        void Start()
        {
            // JSON�t�@�C����ǂݍ���
            string jsonContent = File.ReadAllText(jsonFilePath);

            // JSON����͂��ă`���b�g���b�Z�[�W���X�g�ɕϊ�
            ProcessChatData(jsonContent);
        }

        // JSON����f�[�^���������郁�\�b�h
        void ProcessChatData(string jsonContent)
        {
            // ChatData�͓��I�ɐ�������K�v�����邽�߁A�܂��͂��̃C���X�^���X�𐶐�
            chatData.chatMessages.Clear(); // �����̃��b�Z�[�W���N���A

            // JSON�̃��[�g�I�u�W�F�N�g�̃f�[�^�����
            var jsonObj = JsonUtility.FromJson<JsonWrapper>(jsonContent);

            // 'replayChatItemAction'���܂܂�Ă���Ώ������s��
            if (jsonObj.replayChatItemAction != null)
            {
                foreach (var action in jsonObj.replayChatItemAction.actions)
                {
                    if (action.addChatItemAction != null && action.addChatItemAction.item != null)
                    {
                        var messageItem = action.addChatItemAction.item.liveChatViewerEngagementMessageRenderer;

                        if (messageItem != null)
                        {
                            ProcessedChatItem chatMessage = new ProcessedChatItem
                            (
                                messageItem.message.runs[0].text,
                                "Unknown",  // �K�v�ɉ����Ēl��ݒ�
                                "Unknown",    // �K�v�ɉ����Ēl��ݒ�
                                messageItem.timestampUsec
                            );

                            chatData.chatMessages.Add(chatMessage); // ���X�g�Ƀ��b�Z�[�W��ǉ�
                        }
                    }
                }
            }

            // Unity�G�f�B�^�[�ɔ��f���邽�߂ɕۑ�
            EditorUtility.SetDirty(chatData);
            AssetDatabase.SaveAssets();
        }
    }

    // JSON�̃��b�p�[�N���X�i�\���́j���`
    [System.Serializable]
    public class JsonWrapper
    {
        public ReplayChatItemAction replayChatItemAction;
    }

    [System.Serializable]
    public class ReplayChatItemAction
    {
        public List<Action> actions;
    }

    [System.Serializable]
    public class Action
    {
        public AddChatItemAction addChatItemAction;
    }

    [System.Serializable]
    public class AddChatItemAction
    {
        public Item item;
    }

    [System.Serializable]
    public class Item
    {
        public LiveChatViewerEngagementMessageRenderer liveChatViewerEngagementMessageRenderer;
    }

    [System.Serializable]
    public class LiveChatViewerEngagementMessageRenderer
    {
        public string timestampUsec;
        public Message message;
    }

    [System.Serializable]
    public class Message
    {
        public List<Run> runs;
    }

    [System.Serializable]
    public class Run
    {
        public string text;
    }
}
#endif