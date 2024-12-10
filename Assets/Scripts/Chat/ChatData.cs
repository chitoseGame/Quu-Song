// JSON�\���ɑΉ�����N���X
[System.Serializable]
public class ChatItem
{
    public string message;
    public Author author;
    public string timestamp;
}

[System.Serializable]
public class Author
{
    public string name;
    public string id;
}

// �K�v�ȃf�[�^�̂ݒ��o�����N���X
[System.Serializable]
public class ProcessedChatItem
{
    public string Message;
    public string UserName;
    public string UserId;
    public string Timestamp;

    public ProcessedChatItem(string message,string name,string id,string time)
    {
        this.Message = message;
        this.UserName = name;
        this.UserId = id;
        this.Timestamp = time;
    }
}