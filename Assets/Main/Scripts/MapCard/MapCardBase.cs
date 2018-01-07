using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapCardBase : MonoBehaviour
{
    [SerializeField]
    private Material matOpened;
    [SerializeField]
    private Material matCurrent;
    [SerializeField]
    private Material matClosed;
    [SerializeField]
    private Material matShop;
    [SerializeField]
    private Material matMonster;
    [SerializeField]
    private Material matClick;
    public GameObject prefab;
    public TextMeshPro infoBoard;
    // Use this for initialization
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }


}
