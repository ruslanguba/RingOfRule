using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostructHologram : MonoBehaviour
{
    private bool canStartConstruction;
    private ShopUnitData unitData;
    public GameObject ObjectToHologramm;
    private UnitShop shop;

    [Header("Hologram color")]
    [SerializeField] private Color obstacleColor;
    [SerializeField] private Color noObstacleColor;

    [Header("Socket mesh")]
    [SerializeField] private GameObject socketMesh;
    [SerializeField] private Transform socketTransform;

    private Transform m_Transform;
    private BoxCollider m_BoxCollider;

    private MeshFilter socketMeshFilter;
    private MeshRenderer socketMeshRenderer;

    private void Awake()
    {
        m_Transform= GetComponent<Transform>();
        m_BoxCollider = GetComponent<BoxCollider>();

        shop = FindObjectOfType<UnitShop>();
        socketMeshFilter = socketMesh.GetComponent<MeshFilter>();
        socketMeshRenderer = socketMesh.GetComponent<MeshRenderer>();
    }

    public void InitializeHologram(ShopUnitData unitData)
    {
        ComponentSetting(unitData.PrefabUnit);
        this.unitData = unitData;

        StopAllCoroutines();
        StartCoroutine(CollisionDetected());
    }

    private void ProcessPlacement()
    {
        GameObject NewUnit = Instantiate(unitData.PrefabUnit, m_Transform.position, Quaternion.identity);
        NewUnit.name = unitData.PrefabUnit.name;
    }

    public bool AcceptBuying()
    {
        if (canStartConstruction)
        {
            ProcessPlacement();
            return true;
        }
        else
        {
            return false; 
        }
    }

    private IEnumerator CollisionDetected()
    {
        Collider[] _hitColliders = new Collider[2];
        while (true)
        {
            // ------------------------- Хороший метод для премещения отдельных юнитов --------------------------------------
            //Vector3 boxColliderPosition = m_Transform.position + m_BoxCollider.center;

            //if (Physics.OverlapBoxNonAlloc(boxColliderPosition, m_BoxCollider.size / 2, _hitColliders, Quaternion.identity) >= 2)
            //{
            //    SetColorHologram(obstacleColor);
            //    canStartConstruction = false;
            //}
            //else
            //{
            //    SetColorHologram(noObstacleColor);
            //    canStartConstruction = true;
            //}

            if (!shop.CanPlace)
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

    private void ComponentSetting(GameObject prefubBuilding)
    {
        m_BoxCollider.size = prefubBuilding.GetComponent<BoxCollider>().size;
        m_BoxCollider.center = prefubBuilding.GetComponent <BoxCollider>().center;
        GameObject buildingObject = prefubBuilding.GetComponentInChildren<MeshFilter>().gameObject;
        socketMeshFilter.sharedMesh = buildingObject.GetComponent<MeshFilter>().sharedMesh;
        Transform _transformBuilding = buildingObject.GetComponent<Transform>();
        socketTransform.localPosition = _transformBuilding.localPosition;
        socketTransform.localRotation = _transformBuilding.localRotation;
        socketTransform.localScale = _transformBuilding.localScale;
    }

    private void SetColorHologram(Color color)
    {
        socketMeshRenderer.material.color = color;
    }
}
