using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SongView : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _songName;
    [SerializeField]
    TextMeshProUGUI _artist;
    [SerializeField]
    Button _linkButton;

    string _url;

    public Button LinkButton { get => _linkButton;}

    public void Initialize(SongData data)
    {
        _songName.text = data.SongName;
        _artist.text=data.Artist;
    }

}


