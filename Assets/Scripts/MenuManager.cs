using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] private GameObject _selectedHeroObject;

    void Awake()
    {
        Instance = this;
    }

    public void ShowSelectedHero(BaseHero hero)
    {
        if(hero == null)
        {
            _selectedHeroObject.SetActive(false);
            return; 
        }
        
        _selectedHeroObject.GetComponentInChildren<Text>().text = hero.UnitName;
        _selectedHeroObject.SetActive(true);
    }
}
