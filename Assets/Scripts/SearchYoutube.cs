using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SearchYoutube : MonoBehaviour
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
    [SerializeField]
    SO_SongVideo _songVideo;
    [SerializeField] SingingScrollView scrollView = default;
    string URL = "https://www.youtube.com/watch?v=iN70936k-Qw";
    
    private List<Texture2D> textures= new List<Texture2D>();
    public RawImage thumbnailImage;       // サムネイルを表示するUIコンポーネント

    List<string> urlList = new List<string>();

    public SO_SongVideo SongVideo { get => _songVideo; }

    //public List<Texture> Textures { get => textures; }

    private void Start()
    {
        foreach (var video in _songVideo.sheetDataRecords)
        {
            string url = $"https://img.youtube.com/vi/{GetId(video.url)}/hqdefault.jpg";
            urlList.Add(url);
        }
        
        StartCoroutine(LoadThumbnail());
    }
    public string GetId(string url)
    {
        string[] urls = url.Split("=");
        if (urls.Length != 2)
        {
            return null;
        }
        return urls[urls.Length - 1];
    }
    private IEnumerator LoadThumbnail()
    {
        foreach (var url in urlList)
        {
            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
            {
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                    //thumbnailImage.texture = texture;
                    textures.Add(texture);
                }
                else
                {
                    Debug.LogError($"Failed to load thumbnail: {request.error}");
                }
            }
        }
        var items = Enumerable.Range(0, _songVideo.sheetDataRecords.Length)
    .Select(i => new SingingItemData($"{_songVideo.sheetDataRecords[i].songName}", _songVideo.sheetDataRecords[i].url, textures[i]))
    .ToArray();

        scrollView.UpdateData(items);
        scrollView.SelectCell(0);
    }

}
