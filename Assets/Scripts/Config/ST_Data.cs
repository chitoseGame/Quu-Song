using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SongData
{
    string _date;
    string _title;
    string _url;
    string _songName;
    string _artist;
    string _time;

    public string Date { get => _date; }
    public string Title { get => _title; }
    public string Url { get => _url; }
    public string SongName { get => _songName; }
    public string Artist { get => _artist; }
    public string Time { get => _time; }


    public SongData(string date,string title,string url,string song, string artist,string time)
    {
        _date = date;
        _title = title;
        _url = url;
        _songName = song;
        _artist = artist;
        _time = time;
    }
}

