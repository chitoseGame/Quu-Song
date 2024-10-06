using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SongData
{

    string _songName;
    string _artist;

    public string SongName { get => _songName; }
    public string Artist { get => _artist; }

    public SongData(string song, string artist)
    {
        _songName = song;
        _artist = artist;
    }
}

