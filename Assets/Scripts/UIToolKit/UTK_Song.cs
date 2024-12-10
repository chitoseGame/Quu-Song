using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UTK_Song : Foldout
{
    private VisualElement _songObject;
    //private Foldout _foldOut;
    private Button _artistButton;
    private Button _songButton;

    public Button songButton { get { return _songButton; } }

    public UTK_Song( )
    {
        //this.style.height =80;
        _songObject=new VisualElement();
        //_songObject.AddToClassList("song-object-list");
        //_foldOut=new Foldout();
        _songObject.AddToClassList("song-object-list");
        _artistButton = new Button();
        _artistButton.AddToClassList("song-object-button");
        //_songObject.Add(_artistButton);
        _songObject.Add(_artistButton);
        _songButton = new Button();
        _songButton.AddToClassList("song-object-button");
        //_songObject.Add( _songButton);
        _songObject.Add( _songButton);
        this.style.flexDirection = FlexDirection.Row;
        this.Add( _songObject );
    }

    public void SetButtonName(string artist,string song)
    {
        _artistButton.text = artist;
        _songButton.text = song;
    }
}
