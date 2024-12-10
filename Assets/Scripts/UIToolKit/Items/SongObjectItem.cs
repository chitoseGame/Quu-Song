using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class SongObjectItem : VisualElement
{
    private VisualElement _songObject;
    //private Foldout _foldOut;
    private ScrollLabel _dateLabel;
    private ScrollLabel _titleLabel;
    private ScrollLabel _songLabel;
    private ScrollLabel _artistLabel;


    public SongObjectItem()
    {
        this.style.height =100;
        //_songObject = new VisualElement();
        ////_songObject.AddToClassList("song-object-list");
        ////_foldOut=new Foldout();
        //_songObject.AddToClassList("song-object-list");
        //_artistButton = new Button();
        //_artistButton.AddToClassList("song-object-button");
        ////_songObject.Add(_artistButton);
        //_songObject.Add(_artistButton);
        //_songButton = new Button();
        //_songButton.AddToClassList("song-object-button");
        ////_songObject.Add( _songButton);
        //_songObject.Add(_songButton);
        //this.style.flexDirection = FlexDirection.Row;
        //this.Add(_songObject);

        this.AddToClassList("song-object-list");
        _dateLabel = new ScrollLabel();
        _dateLabel.CreateLabel(this, "songDateLabel");
        _dateLabel.style.fontSize = Mathf.Clamp(Screen.width * 0.05f, 12f, 36f);
        _titleLabel = new ScrollLabel();
        _titleLabel.CreateLabel(this, "songTitleLabel");
        _titleLabel.style.fontSize = Mathf.Clamp(Screen.width * 0.05f, 12f, 36f);
        _songLabel = new ScrollLabel();
        _songLabel.CreateLabel(this, "songNameLabel");
        _songLabel.style.fontSize = Mathf.Clamp(Screen.width * 0.05f, 12f, 36f);
        _artistLabel = new ScrollLabel();
        _artistLabel.CreateLabel(this, "songArtistLabel");
        _artistLabel.style.fontSize = Mathf.Clamp(Screen.width * 0.05f, 12f, 36f);
    }

    public void SetButtonName(SongData data)
    {
        _dateLabel.text=data.Date;
        _titleLabel.text=data.Title;
        _songLabel.text=data.SongName;
        _artistLabel.text=data.Artist;
    }

}
