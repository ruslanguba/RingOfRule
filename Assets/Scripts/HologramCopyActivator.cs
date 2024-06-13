using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramCopyActivator : MonoBehaviour
{
    [SerializeField] private GameObject realUnit;
    private bool isRealUnitMove;

    public void StartMoveToCopy()
    {
        StartCoroutine(RealUnitMoveToCopy());
    }

    IEnumerator RealUnitMoveToCopy()
    {
        if(isRealUnitMove)
        {
            yield return new WaitForSeconds(0.2f);
            if (Vector3.Distance(transform.position, realUnit.transform.position) < 0.5)
            {
                gameObject.SetActive(false);
                isRealUnitMove = false;
                StopCoroutine(RealUnitMoveToCopy());
            }
        }
    }
}
