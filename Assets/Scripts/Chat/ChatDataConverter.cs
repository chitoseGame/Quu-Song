using UnityEngine;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
namespace chat
{

    public class ChatManager : MonoBehaviour
    {
        public SO_ChatData chatData; // チャットデータ（Unityのスクリプタブルオブジェクト）

        // JSONファイルのパス（Assets内のファイル）
        public string jsonFilePath = "Assets/Path/To/Your/live_chat.json";

        void Start()
        {
            // JSONファイルを読み込む
            string jsonContent = File.ReadAllText(jsonFilePath);

            // JSONを解析してチャットメッセージリストに変換
            ProcessChatData(jsonContent);
        }

        // JSONからデータを処理するメソッド
        void ProcessChatData(string jsonContent)
        {
            // ChatDataは動的に生成する必要があるため、まずはそのインスタンスを生成
            chatData.chatMessages.Clear(); // 既存のメッセージをクリア

            // JSONのルートオブジェクトのデータを解析
            var jsonObj = JsonUtility.FromJson<JsonWrapper>(jsonContent);

            // 'replayChatItemAction'が含まれていれば処理を行う
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
                                "Unknown",  // 必要に応じて値を設定
                                "Unknown",    // 必要に応じて値を設定
                                messageItem.timestampUsec
                            );

                            chatData.chatMessages.Add(chatMessage); // リストにメッセージを追加
                        }
                    }
                }
            }

            // Unityエディターに反映するために保存
            EditorUtility.SetDirty(chatData);
            AssetDatabase.SaveAssets();
        }
    }

    // JSONのラッパークラス（構造体）を定義
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