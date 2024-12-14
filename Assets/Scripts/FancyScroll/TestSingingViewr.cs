using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Threading.Tasks;
public class TestSingingViewr : MonoBehaviour
{
    private static readonly string[] FILENAMES = new string[]
   {
        "default.jpg",
        "mqdefault.jpg",
        "hqdefault.jpg",
        "sddefault.jpg",
        "maxresdefault.jpg",
   };

    public enum Quality
    {
        Default,
        Mid,
        High,
        HQ,
        FHD
    }

    [SerializeField] SingingScrollView scrollView = default;

    //List<Texture2D> textures = new List<Texture2D>();
    public RawImage thumbnailImage;       // サムネイルを表示するUIコンポーネント

    List<string> urlList = new List<string>();



    //public List<Texture> Textures { get => textures; }

    private async void Start()
    {
        await LoadThumbnail();
    }
    public string GetId(string url)
    {
        string[] urls = url.Split("=");
        if (urls.Length != 2)
        {
        }
        return urls[urls.Length - 1];
    }
    private async Task LoadThumbnail()
    {
        foreach (var video in UIToolkitManager.Instance.SongVideo.songMovieData)
        {
            string url = $"https://img.youtube.com/vi/{GetId(video.url)}/hqdefault.jpg";
            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
            {
                var asyncOperation = request.SendWebRequest();
                while (!asyncOperation.isDone)
                {
                    await Task.Yield(); // フレームを待つ
                }

                if (request.result == UnityWebRequest.Result.Success)
                {
                    Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                    //thumbnailImage.texture = texture;
                    video.thumbnail=(texture);
                }
                else
                {
                    Debug.LogError($"サムネイル取得失敗: {request.error}");
                    break;
                }
            }
        }
        var items = Enumerable.Range(0, UIToolkitManager.Instance.SongVideo.songMovieData.Count)
    .Select(i => new SingingItemData($"{UIToolkitManager.Instance.SongVideo.songMovieData[i].songName}", UIToolkitManager.Instance.SongVideo.songMovieData[i].url, UIToolkitManager.Instance.SongVideo.songMovieData[i].thumbnail))
    .ToArray();

        scrollView.UpdateData(items);
        scrollView.SelectCell(0);
    }

}
