//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Networking;
//using System.IO;
//using Newtonsoft.Json; // JSON�ϊ��̂��߂ɕK�v

//public class YouTubeChatFetcher : MonoBehaviour
//{
////    [SerializeField]
////    SO_LiveData so_LiveData;
////    // YouTube Data API�̃L�[�����
////    private string apiKey = "AIzaSyCbG4CsZfpsAVT8VVDLOFgkuiYIUnUb3NY";
////    private string liveChatId = "LIVE_CHAT_ID"; // YouTube���C�u�̃`���b�gID
////    private string outputPath = "chat_history.json"; // �ۑ�����t�@�C����

////    void Start()
////    {
////        StartCoroutine(FetchChatHistory());
////    }

////    IEnumerator FetchChatHistory()
////    {
////        // ���C�u�`���b�g���b�Z�[�W�擾API�̃G���h�|�C���g
////        string url = $"https://www.googleapis.com/youtube/v3/liveChat/messages?liveChatId={liveChatId}&part=snippet,authorDetails&key={apiKey}";

////        UnityWebRequest request = UnityWebRequest.Get(url);
////        yield return request.SendWebRequest();

////        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
////        {
////            Debug.LogError($"Error: {request.error}");
////        }
////        else
////        {
////            // �擾����JSON�f�[�^�����
////            string jsonResponse = request.downloadHandler.text;
////            ChatResponse chatResponse = JsonConvert.DeserializeObject<ChatResponse>(jsonResponse);

////            // �K�v�ȃf�[�^�𒊏o
////            List<ChatMessage> chatMessages = new List<ChatMessage>();
////            foreach (var item in chatResponse.items)
////            {
////                ChatMessage message = new ChatMessage
////                {
////                    Message = item.snippet.displayMessage,
////                    UserName = item.authorDetails.displayName,
////                    UserId = item.authorDetails.channelId,
////                    Timestamp = item.snippet.publishedAt
////                };
////                chatMessages.Add(message);
////            }

////            // JSON�t�@�C���ɕۑ�
////            string jsonOutput = JsonConvert.SerializeObject(chatMessages, Formatting.Indented);
////            File.WriteAllText(outputPath, jsonOutput);

////            Debug.Log($"Chat history saved to {outputPath}");
////        }
////    }

//    // �N���X��`�iJSON��͗p�j
//    [System.Serializable]
//    public class ChatResponse
//    {
//        public List<Item> items;
//    }

//    [System.Serializable]
//    public class Item
//    {
//        public Snippet snippet;
//        public AuthorDetails authorDetails;
//    }

//    [System.Serializable]
//    public class Snippet
//    {
//        public string displayMessage;
//        public string publishedAt;
//    }

//    [System.Serializable]
//    public class AuthorDetails
//    {
//        public string displayName;
//        public string channelId;
//    }

//    [System.Serializable]
//    public class ChatMessage
//    {
//        public string Message;
//        public string UserName;
//        public string UserId;
//        public string Timestamp;
//    }
//}