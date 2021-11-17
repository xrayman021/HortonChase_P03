using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    //[SerializeField] private List<Unit> _units;

    void Awake()
    {
        Instance = this;
    }

    public async void Attack(Faction faction)
    {

    }
}
