using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellInfo : MonoBehaviour
{
    public Unit unitOnCell { get; private set; }
    private SquadInfo squadInfo;
    [SerializeField] HologramCopyActivator hologramCopy;

    private SquadSelect parentSquad;

    void Start()
    {
        squadInfo = GetComponentInParent<SquadInfo>();
        parentSquad = GetComponentInParent<SquadSelect>();
    }

    public void OnSelect()
    {
        if (parentSquad != null)
        {
            parentSquad.SquadOnSelect();
        }
    }

    public void GetUnit(Unit unit)
    {
        unitOnCell = unit;
        hologramCopy = unitOnCell.gameObject.GetComponent<HologramCopyActivator>();
        squadInfo.AddUnitToSquad(unit.gameObject);
    }

    public void ClearCell()
    {
        unitOnCell = null;
    }
}
