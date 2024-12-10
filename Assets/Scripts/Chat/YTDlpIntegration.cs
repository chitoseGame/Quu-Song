using System.Diagnostics;
using System.IO;
using UnityEngine;

public class YTDlpIntegration : MonoBehaviour
{
    [SerializeField]
    SO_LiveData so_LiveData;
    public string videoUrl = "https://www.youtube.com/watch?v=3jAug1ph9Zg";
    public string ytDlpPath = "C:\\Python\\Python313\\Scripts\\yt-dlp.exe"; // yt-dlp�̎��s�t�@�C���̃p�X
    public string outputJsonPath = "jsonData3.json"; // �`���b�g�����̕ۑ���

    void Start()
    {
        DownloadChat();
    }

    void DownloadChat()
    {
        // yt-dlp�̃R�}���h
        string arguments = $"--write-subs --sub-langs live_chat --skip-download --output \"{outputJsonPath}\" {videoUrl}";

        Process process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = ytDlpPath,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory= "C:\\Python\\Python313\\Scripts"
            }
        };

        try
        {
            process.Start();

            // �G���[�o�͂��擾�i�K�v�Ȃ�f�o�b�O�Ɏg�p�j
            string stderr = process.StandardError.ReadToEnd();
            string stdout = process.StandardOutput.ReadToEnd();

            process.WaitForExit();

            if (process.ExitCode == 0)
            {
                //Debug.Log("Chat data downloaded successfully!");
            }
            else
            {
                //Debug.LogError($"Error downloading chat: {stderr}");
            }

            // JSON�f�[�^��Unity�ŏ���
            if (File.Exists(outputJsonPath))
            {
                ProcessChatData();
            }
        }catch(System.Exception e)
        {
            UnityEngine.Debug.LogError(e.Message);
        }
    }

    void ProcessChatData()
    {
        string jsonContent = File.ReadAllText(outputJsonPath);
        //Debug.Log($"Chat Data: {jsonContent}");

        // �K�v�ɉ�����JSON���p�[�X����Unity�ŗ��p�\�Ȍ`���ɕϊ�
    }
}