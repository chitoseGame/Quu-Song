using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChatData", menuName = "ScriptableObjects/ChatData", order = 1)]
public class SO_ChatData : ScriptableObject
{
    public List<ProcessedChatItem> chatMessages = new List<ProcessedChatItem>(); // チャットメッセージのリスト 
}
