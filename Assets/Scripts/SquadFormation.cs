using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadFormation : MonoBehaviour
{
    //private SquadMoveButtonsSeting squadMoveButtonsSeting;
    private SquadCanvasAlignment alignment;
    [SerializeField] private List<GameObject> frontCells;
    [SerializeField] private List<GameObject> rearCells;
    [SerializeField] private List<GameObject> leftCells;
    [SerializeField] private List<GameObject> rightCells;
    [SerializeField] private GameObject[,] allCells;
    [SerializeField] private Vector3 squadCenter;
    [SerializeField] private int _columns;
    [SerializeField] private int _rows;
    [SerializeField] private float offset;
    [SerializeField] private float buttonoffsetX;
    [SerializeField] private float buttonoffsetZ;
    private Vector3 initialPosition;

    public void Initialize(int rows, int columns, GameObject[,] cells)
    {
        alignment = GetComponent<SquadCanvasAlignment>();
        for (int y = 0; y < columns; y++)
        {
            leftCells.Add(cells[0, y]);
            rightCells.Add(cells[rows - 1, y]);
        }

        for (int x = 0; x < rows; x++)
        {
            frontCells.Add(cells[x, 0]);
            rearCells.Add(cells[x, columns - 1]);
        }
        _columns = columns;
        _rows = rows;
        allCells = cells;

        if (allCells[0, 0] != null)
        {
            initialPosition = allCells[0, 0].transform.position;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpreadOutFormation();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            LineFormation();
        }
    }
    public void SpreadOutFormation()
    {
        if (allCells[0, 0] != null)
        {
            initialPosition = allCells[0, 0].transform.localPosition;
        }

        // ѕроходим по каждой клетке в массиве и расставл€ем их в шахматном пор€дке
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                // ¬ычисл€ем новую позицию клетки с учетом смещени€ по оси X
                float newX = initialPosition.x + i * offset + (j % 2 == 0 ? 0 : offset / 2);
                float newZ = initialPosition.z + j * -offset;

                // ѕеремещаем клетку в новую позицию
                if (allCells[i, j] != null)
                {
                    allCells[i, j].transform.localPosition = new Vector3(newX, allCells[i, j].transform.position.y, newZ);
                }
            }
        }
        buttonoffsetX = _rows * offset;
        buttonoffsetZ = _columns * offset;
        SetSquadCentre();
        //squadMoveButtonsSeting.SetArrowsPosition(buttonoffsetX, buttonoffsetZ, squadCenter);
    }

    public void LineFormation()
    {
        if (allCells[0, 0] != null)
        {
            initialPosition = allCells[0, 0].transform.localPosition;
        }

        // ѕроходим по каждой клетке в массиве и расставл€ем их в шахматном пор€дке
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                // ¬ычисл€ем новую позицию клетки с учетом смещени€ по оси X
                float newX = initialPosition.x + i;
                float newZ = initialPosition.z + j*-1;

                // ѕеремещаем клетку в новую позицию
                if (allCells[i, j] != null)
                {
                    allCells[i, j].transform.localPosition = new Vector3(newX, allCells[i, j].transform.position.y, newZ);
                }
            }
        }
        buttonoffsetX = _rows;
        buttonoffsetZ = _columns;
        SetSquadCentre();
        GetComponent<SquadGridCreator>().ChangeSquadGrid();
        //squadMoveButtonsSeting.SetArrowsPosition(buttonoffsetX, buttonoffsetZ, squadCenter);
    }

    public void SetSquadCentre()
    {
        foreach (var cell in allCells)
        {
            cell.transform.SetParent(null);
        }
        squadCenter = Vector3.Lerp(allCells[_rows - 1, _columns - 1].transform.position, allCells[0, 0].transform.position, 0.5f);
        transform.position = squadCenter;
        foreach (var cell in allCells)
        {
            cell.transform.SetParent(transform);
        }
        alignment.SetArrowsPosition(buttonoffsetX, buttonoffsetZ, squadCenter);
        GetComponent<MoveSquad>().StertMoveUnit();
    }
}





