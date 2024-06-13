using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadInfo : MonoBehaviour
{
    [SerializeField] private List<GameObject> AllCells;
    [SerializeField] private List<GameObject> unitsInSquad;

    public List<GameObject> GetAllUnitsInSquad()
    {
        return unitsInSquad;
    }
    public void Initialize(GameObject[,] cells)
    {
        foreach (var cell in cells)
        {
            AllCells.Add(cell);
        }
    }

    public void UpdateCells(GameObject[,] cells)
    {

        foreach (var cell in cells)
        {
            if (!AllCells.Contains(cell))
            {
                AllCells.Add(cell);
            }
        }
    }

    public void RemoveDeletedCells(List<GameObject> removedCells)
    {
        foreach (var removedCell in removedCells)
        {
            if (AllCells.Contains(removedCell))
            {
                AllCells.Remove(removedCell);
            }
        }
        removedCells.Clear(); // Clear the removal list after removing deleted cells
    }

    public void AddUnitToSquad(GameObject unit)
    {
        if(!unitsInSquad.Contains(unit))
        {
            unitsInSquad.Add(unit);
        }
    }

    public void FindAllUnits()
    {
        foreach (var cell in AllCells)
        {
            if(cell.GetComponent<CellInfo>().unitOnCell != null)
            {
                if(!unitsInSquad.Contains(cell.GetComponent<CellInfo>().unitOnCell.gameObject))
                {
                    unitsInSquad.Add(cell.GetComponent<CellInfo>().unitOnCell.gameObject);
                }
            }
        }
    }

    public void ClearSelectedUnits()
    {
        unitsInSquad.Clear();
    }

    public bool isPossebleToDelate()
    {
        return true;
    }
}
