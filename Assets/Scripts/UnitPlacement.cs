using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlacement : MonoBehaviour
{
    private Unit unit => GetComponent<Unit>();
    private UnitMoveToCell unitMove => GetComponent<UnitMoveToCell>();
    [SerializeField] private LayerMask cell;

    private void Awake()
    {
        FindNewCell();
    }

    public void FindNewCell()
    {
        Vector3 origin = new Vector3(transform.position.x, 0.2f, transform.position.z);
        RaycastHit hit;
        if (Physics.Raycast(origin, Vector3.down, out hit, 10, cell))
        {
            hit.collider.gameObject.GetComponent<CellInfo>().GetUnit(unit);
            unitMove.SetTarget(hit.collider.gameObject.transform);
            Debug.Log(hit.collider.gameObject.name);
            transform.rotation = hit.collider.transform.rotation;
        }
    }

    public void RefreshCell(CellInfo cell)
    {
        unitMove.SetTarget(cell.gameObject.transform);
    }
}
