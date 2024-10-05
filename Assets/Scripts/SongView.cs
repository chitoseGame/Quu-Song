using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SongView : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _songName;
    [SerializeField]
    TextMeshProUGUI _artist;

    public void Initialize(SongData data)
    {
        _songName.text = data.name;
        _artist.text=data.Artist;
    }
}
