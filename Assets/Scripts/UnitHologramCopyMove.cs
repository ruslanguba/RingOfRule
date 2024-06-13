using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHologramCopyMove : MonoBehaviour
{
    [SerializeField] GameObject hologramCopyOfUnit;
    [SerializeField] GameObject originalUnit;

    public void ActivateHoloramCopy()
    {
        hologramCopyOfUnit.SetActive(true);
        originalUnit.SetActive(false);
    }

    public void DiactivateHoloramCopy()
    {
        hologramCopyOfUnit.SetActive(false);
        originalUnit.SetActive(true);
    }

}
