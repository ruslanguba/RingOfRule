using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    private GameObject currentObjectToMove;
    bool findPalce;
    [SerializeField] private Transform transformHologram;
    [SerializeField] private Vector3 startPosition;

    [SerializeField] private LayerMask maskMoveHologramm;
    [SerializeField] private Camera m_Camera;

    [SerializeField] public bool CanPlace = false;
    [SerializeField] private bool canStartConstruction;

    [Header("Hologram color")]
    [SerializeField] private Color obstacleColor;
    [SerializeField] private Color noObstacleColor;

    [SerializeField] private Transform m_Transform;
    [SerializeField] private BoxCollider m_BoxCollider;

    [SerializeField] private MeshRenderer socketMeshRenderer;
    private Collider[] _hitColliders = new Collider[10];

    private void Start()
    {
        m_Camera = Camera.main;
    }

    private void Update()
    {
        if (findPalce)
        {
            FindPlace();
            if (Input.GetMouseButtonDown(0))
            {
                if (canStartConstruction)
                {
                    findPalce = false;
                    socketMeshRenderer.gameObject.SetActive(false);
                    StopCoroutine(CollisionDetected());
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                findPalce = false;
                StopCoroutine(CollisionDetected());
                CancelMove();
            }
        }
    }

    public void SetObjetToMove(GameObject objectToMove)
    {
        currentObjectToMove = objectToMove;
        socketMeshRenderer = currentObjectToMove.GetComponentInChildren<MeshRenderer>();
        m_BoxCollider = currentObjectToMove.GetComponent<BoxCollider>();
        transformHologram = currentObjectToMove.transform;
        m_Transform = currentObjectToMove.transform;
        startPosition = currentObjectToMove.transform.position;
        findPalce = true;
        StartCoroutine(CollisionDetected());
    }

    public void FindPlace()
    {
        Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;

        if (Physics.Raycast(ray, out _hit, 50, maskMoveHologramm))
        {
            transformHologram.position = new Vector3(_hit.point.x, _hit.point.y, _hit.point.z);
        }
    }

    private IEnumerator CollisionDetected()
    {
        while (findPalce)
        {
            // ”бедитесь, что m_Transform и m_BoxCollider инициализированы
            if (m_Transform == null || m_BoxCollider == null)
            {
                Debug.LogError("m_Transform или m_BoxCollider не инициализированы.");
                yield break;
            }

            // ¬ычисл€ем позицию и размеры коробки
            Vector3 boxColliderPosition = m_Transform.position + m_BoxCollider.center;
            Vector3 boxColliderSize = m_BoxCollider.size / 2;

            // ѕровер€ем наличие пересечений
            int numColliders = Physics.OverlapBoxNonAlloc(boxColliderPosition, boxColliderSize, _hitColliders, Quaternion.identity);

            // ”станавливаем canStartConstruction в зависимости от числа пересечений
            if (numColliders >= 2)
            {
                SetColorHologram(obstacleColor);
                canStartConstruction = false;
            }
            else
            {
                SetColorHologram(noObstacleColor);
                canStartConstruction = true;
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private void SetColorHologram(Color color)
    {
        socketMeshRenderer.material.color = color;
    }

    private void CancelMove()
    {
        if (currentObjectToMove != null)
        {
            currentObjectToMove.transform.position = startPosition;
        }
        if (currentObjectToMove.GetComponent<SquadGridCreator>() != null)
        {
            Destroy(currentObjectToMove);
        }
    }
}
