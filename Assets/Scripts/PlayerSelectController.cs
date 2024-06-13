using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectController : MonoBehaviour
{
    [SerializeField] private LayerMask Cell;
    private Camera m_Camera;
    private bool isSmthSelected;

    private void Start()
    {
        m_Camera = Camera.main;
    }
    public void SelectObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hit;
            if (Physics.Raycast(ray, out _hit, 50, Cell))
            {
                GameObject currentCell = _hit.collider.gameObject;
            }
        }
    }
}
