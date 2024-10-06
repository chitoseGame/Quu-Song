using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SongListView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _date;
    [SerializeField]
    GameObject _contentRoot;

    public GameObject ContentRoot { get => _contentRoot; }
    public TMP_Text Date { get => _date;}
}
