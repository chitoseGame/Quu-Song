using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SongData
{

    string _songName;
    string _artist;
    string _time;

    public string SongName { get => _songName; }
    public string Artist { get => _artist; }
    public string Time { get => _time; }

    public SongData(string song, string artist,string time)
    {
        _songName = song;
        _artist = artist;
        _time = time;
    }
}

