using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SongListView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _date;
    [SerializeField]
    private TMP_Text _title;
    [SerializeField]
    GameObject _contentRoot;

    public GameObject ContentRoot { get => _contentRoot; }
    public TMP_Text Date { get => _date;}
    public TMP_Text Title { get => _title; }

    private void Update()
    {
        //this.gameObject.SetActive(_contentRoot.transform.childCount != 0);
    }
}
