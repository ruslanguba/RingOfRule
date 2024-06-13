using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadUnitFormationChange : MonoBehaviour
{
    private Camera m_Camera;
    [SerializeField] private LayerMask Cell;
    [SerializeField] private LayerMask maskMoveHologramm;
    [SerializeField] private GameObject unitToMove;
    [SerializeField] private CellInfo startCell;
    [SerializeField] private CellInfo newCell;
    [SerializeField] bool isUnitMoving;
    [SerializeField] bool canPlace;
    [SerializeField] bool isFormationChanging;
    void Start()
    {
        m_Camera = Camera.main;
    }
    private void Update()
    {
        if(isFormationChanging)
        {
            SelectUnitToChange();
            FindNewPosForUnit();
        }
    }
    public void SelectUnitToChange()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isUnitMoving)
            {
                SelectUnitToMove();
            }
            else
            {
                SelectNewCellToUnit();
            }
        }
       

        //if (Input.GetMouseButtonDown(1))
        //{
        //    if (currentSquad != null)
        //    {
        //        currentSquad.SquadDiselect();
        //        currentSquad = null;
        //        isSmthSelected = false;
        //    }
        //}
    }

    public void SelectUnitToMove()
    {
        Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;

        if (Physics.Raycast(ray, out _hit, 100, Cell))
        {
            startCell = _hit.collider.gameObject.GetComponent<CellInfo>();
            if (startCell.unitOnCell != null)
            {
                startCell.unitOnCell.GetComponent<UnitHologramCopyMove>().DiactivateHoloramCopy();
                unitToMove = startCell.unitOnCell.gameObject;
                isUnitMoving = true;
            }
        }
    }

    public void SelectNewCellToUnit()
    {
        if (canPlace)
        {
            if (newCell.unitOnCell != null)
            {
                newCell.unitOnCell.transform.position = startCell.transform.position;
                newCell.unitOnCell.GetComponent<UnitPlacement>().FindNewCell();
                Debug.Log(newCell.name);
            }
            else
            {
                startCell.ClearCell();
            }
            isUnitMoving = false;
            unitToMove.GetComponent<UnitPlacement>().FindNewCell();
            unitToMove.GetComponent<UnitHologramCopyMove>().ActivateHoloramCopy();
        }
    }

    public void IsFormationChanging(bool isChanging)
    {
        isFormationChanging = isChanging;
    }

    public void FindNewPosForUnit()
    {
        Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;
        if (isUnitMoving)
        {
            if (Physics.Raycast(ray, out _hit, 50, Cell))
            {
                GameObject currentCell = _hit.collider.gameObject;
                newCell = currentCell.GetComponent<CellInfo>();
                unitToMove.transform.position = currentCell.transform.position;
                canPlace = true;
            }

            else if (Physics.Raycast(ray, out _hit, 50, maskMoveHologramm))
            {
                unitToMove.transform.position = new Vector3(_hit.point.x, 0, _hit.point.z);
                canPlace = false;
            }

            if (Input.GetMouseButtonDown(1))
            {
                unitToMove.transform.position = startCell.transform.position;
                isUnitMoving = false;
            }
        }
    }
}
