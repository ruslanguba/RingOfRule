using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SquadGridCreator : MonoBehaviour
{
    [SerializeField] private SquadInfo squadInfo;
    private SquadFormation squadFormation;
    private int rows = 1;
    private int columns = 1;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private GameObject GridSettings;
    [SerializeField] private GameObject GridMovement;
    [SerializeField] private GameObject Pivots;
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject[,] cells;
    private SquadMoveButtonsSeting moveSquadButtonSettings;
    private SquadCanvasAlignment alignment;

    private void Start()
    {
        moveSquadButtonSettings = GetComponent<SquadMoveButtonsSeting>();
        squadInfo = GetComponent<SquadInfo>();
        squadFormation = GetComponent<SquadFormation>();
        alignment = GetComponent<SquadCanvasAlignment>();
        GenerateCell();
    }
    public void AddRow()
    {
        rows++;
        GenerateCell();
    }

    public void AddColumn()
    {
        columns++;
        GenerateCell();
    }

    public void DeleteLastRow()
    {
        if (rows > 1)
        {
            rows--;
            GameObject[,] newCells = new GameObject[rows, columns];
            List<GameObject> cellsToDestroy = new List<GameObject>();
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    newCells[x, y] = cells[x, y];
                }
            }

            for (int y = 0; y < columns; y++)
            {
                cellsToDestroy.Add(cells[rows, y]);
                Destroy(cells[rows, y]);
            }

            cells = newCells;
            squadInfo.RemoveDeletedCells(cellsToDestroy);
            cellsToDestroy.Clear();
        }
    }

    public void DeleteLastColumn()
    {
        if (columns > 1)
        {
            columns--;
            GameObject[,] newCells = new GameObject[rows, columns];
            List<GameObject> cellsToDestroy = new List<GameObject>();

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    newCells[x, y] = cells[x, y];
                }
            }

            for (int x = 0; x < rows; x++)
            {
                cellsToDestroy.Add(cells[x, columns]);
                Destroy(cells[x, columns]);
            }

            cells = newCells;
            squadInfo.RemoveDeletedCells(cellsToDestroy);
            cellsToDestroy.Clear();
        }
    }

    private void GenerateCell()
    {
        GameObject[,] newCells = new GameObject[rows, columns];

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                if (cells != null && x < cells.GetLength(0) && y < cells.GetLength(1))
                {
                    newCells[x, y] = cells[x, y];
                }
                else
                {
                    Vector3 localPosition = new Vector3(x, Pivots.transform.position.y, -y);
                    Vector3 position = transform.TransformPoint(localPosition);
                    Vector3 instPosition = new Vector3(position.x, Pivots.transform.localPosition.y, position.z);
                    Quaternion rotation = transform.rotation; // Вращение, унаследованное от родительского объекта
                    GameObject cell = Instantiate(cellPrefab, instPosition, rotation);
                    cell.name = $"X: {x}, Y: {y}";
                    cell.transform.SetParent(transform);
                    newCells[x, y] = cell;
                }
            }
        }
        cells = newCells;
        squadInfo.UpdateCells(cells);
    }
    
    public void CreateSquad() //Button
    {
        foreach (var cell in cells)
        {
            cell.transform.SetParent(null);
        }
        alignment.ActivateMoveButtons();
        Vector3 squadCentr = Vector3.Lerp(cells[rows - 1, columns - 1].transform.position, cells[0, 0].transform.position, 0.5f);
        UI.transform.SetParent(null);
        Pivots.transform.SetParent(null);
        alignment.SetArrowsPosition(rows, columns, squadCentr);
        transform.position = squadCentr;
        UI.transform.SetParent(transform);
        Pivots.transform.SetParent(transform);
        foreach (var cell in cells)
        {
            cell.transform.SetParent(transform);
        }
        squadInfo.Initialize(cells);
        squadFormation.Initialize(rows, columns, cells);
    }

    public void SetNewSquadCenter()
    {
        squadFormation.Initialize(rows, columns, cells);
        squadFormation.SetSquadCentre();
        alignment.ActivateMoveButtons();
    }

    public void ChangeSquadGrid() // Button
    {
        Vector3 gridZeroPoint = cells[0, 0].transform.position;
        GridSettings.SetActive(true);
        GridMovement.SetActive(false);
        UI.transform.SetParent(null);
        UI.transform.position = new Vector3(gridZeroPoint.x, 0.1f, gridZeroPoint.z);
        Pivots.transform.SetParent(null);
        foreach (var cell in cells)
        {
            cell.transform.SetParent(null);
        }
        transform.position = new Vector3(gridZeroPoint.x, 0, gridZeroPoint.z);
        foreach (var cell in cells)
        {
            cell.transform.SetParent(transform);
        }
        Pivots.transform.SetParent(transform);
    }

    public void CancelSquadChanges()
    {
        Destroy(gameObject);
    }

    public void ActivateUI()
    {
        UI.gameObject.SetActive(true);
    }

    public void DeactivateUI()
    {
        UI.gameObject.SetActive(false);
    }

}
