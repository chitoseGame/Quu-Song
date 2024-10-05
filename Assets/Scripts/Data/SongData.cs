using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongData : MonoBehaviour
{
    string _songName;
    string _artist;

    public string SongName { get => _songName; }
    public string Artist { get => _artist;}

    public void SetData(string song,string artist)
    {
        _songName = song;
        _artist = artist;
    }
}
