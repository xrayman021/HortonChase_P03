using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    
    [SerializeField] protected MeshRenderer _renderer;
    [SerializeField] private GameObject _highlight;


    public virtual void Init(int x, int y)
    {
       
    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }
}
