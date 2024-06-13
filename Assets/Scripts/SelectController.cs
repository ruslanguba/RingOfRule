using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectController : MonoBehaviour
{
    [SerializeField] private SquadSelect currentSquad;
    [SerializeField] private LayerMask Cell;
    [SerializeField] private LayerMask Ground;
    private Camera m_Camera;
    private bool isSmthSelected;
    private bool isCurrentFormationChanging;
    private UnitShop unitShop;

    public SquadSelect GetSelectedSquad()
    {
        return currentSquad; 
    }

    private void Start()
    {
        unitShop = GetComponent<UnitShop>();
        m_Camera = Camera.main;
    }

    private void Update()
    {
        SelectObject();
    }

    public void SelectObject()
    {
        if (!isCurrentFormationChanging && !unitShop.StatusBuy)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit _hit;

                if (Physics.Raycast(ray, out _hit, 100, Cell))
                {
                    GameObject currentCell = _hit.collider.gameObject;
                    currentSquad = currentCell.GetComponentInParent<SquadSelect>();
                    currentSquad.SquadOnSelect();
                    isSmthSelected = true;
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                if (currentSquad != null)
                {
                    Debug.Log(unitShop.StatusBuy);
                    currentSquad.SquadDiselect();
                    currentSquad = null;
                    isSmthSelected = false;
                    isCurrentFormationChanging = false;
                }
            }
        }
    }

    public void StopSelectController()
    {
        isCurrentFormationChanging = true;
    }

    public void StartSelectController()
    {
        isCurrentFormationChanging = false;
    }
}
