using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/UnitData", fileName = "UnitData")]
public class ShopUnitData : ScriptableObject
{
    public string Name;
    public int Cost;
    public float TimeConstruction;
    public GameObject PrefabUnit;
}
