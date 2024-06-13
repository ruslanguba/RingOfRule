using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadSelect : MonoBehaviour
{
    private SelectController selectController;
    private SquadGridCreator squadGridCreator;
    private SquadUnitFormationChange squadUnitFormationChange;
    private SquadFormation squadFormation;
    private SquadInfo squadInfo;
    private MoveSquad squadMove;
    private List<GameObject> squadOriginalUnits;

    private bool isSelected;

    private void Start()
    {
        selectController = FindObjectOfType<SelectController>();
        squadGridCreator = GetComponent<SquadGridCreator>();
        squadUnitFormationChange = selectController.GetComponent<SquadUnitFormationChange>();
        squadInfo = GetComponent<SquadInfo>();
        squadMove = GetComponent<MoveSquad>();
        squadFormation = GetComponent<SquadFormation>();
    }
    public void SquadOnSelect()
    {
        squadGridCreator.ActivateUI();
        squadInfo.FindAllUnits();
    }

    public void SquadDiselect()
    {
        squadGridCreator.DeactivateUI();
        squadInfo.ClearSelectedUnits();
    }

    public void ChangeSquadFormation()
    {
        squadGridCreator.ChangeSquadGrid();
        ActivateHologrammCopies();
        selectController.StopSelectController();
        squadUnitFormationChange.IsFormationChanging(true);
    }

    public void AcceptChanges()
    {
        squadGridCreator.CreateSquad();
        squadFormation.SetSquadCentre();
        selectController.StartSelectController();
        DiactivateHologrammCopies();
        squadUnitFormationChange.IsFormationChanging(false);
    }

    public void ActivateHologrammCopies()
    {
        for (int i = 0; i < squadInfo.GetAllUnitsInSquad().Count; i++)
        {
            squadInfo.GetAllUnitsInSquad()[i].GetComponent<UnitHologramCopyMove>().ActivateHoloramCopy();
        }
    }
    public void DiactivateHologrammCopies()
    {
        for (int i = 0; i < squadInfo.GetAllUnitsInSquad().Count; i++)
        {
            squadInfo.GetAllUnitsInSquad()[i].GetComponent<UnitHologramCopyMove>().DiactivateHoloramCopy();
        }
    }
}
