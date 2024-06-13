using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllUnitsStorage : MonoBehaviour
{
    public int avaliableUnits;

    public void UseUnit()
    {
        avaliableUnits--;
    }
}
