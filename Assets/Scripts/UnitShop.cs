using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitShop : MonoBehaviour
{
    public bool StatusBuy { get; private set; }

    [SerializeField] private ShopUnitData[] buildingData;

    [Header("Hologram settings")]
    [SerializeField] private GameObject prefabHologram = null;

    //private GMode gMode;
    private AllUnitsStorage unitsStorage;

    private int indexUnit;
    private CostructHologram hologramUnit;
    private Transform transformHologram;

    [SerializeField] private LayerMask maskMoveHologramm;
    [SerializeField] private LayerMask Cell;
    private Camera m_Camera;

    public bool CanPlace = false;
    private bool isSquadCreateing;
    void Start()
    {
        //gMode = GetComponent<GMode>();
        unitsStorage = GetComponent<AllUnitsStorage>();
        m_Camera = Camera.main;
    }
    private void Update()
    {
        if(StatusBuy)
        {
            MoveHologramm();
            if(Input.GetMouseButtonDown(0))
            {
                PurchaseBuilding();
            }
            if(Input.GetMouseButtonDown(1))
            {
                CancelBuy();
            }
        }
        
    }
    public void SelectCurrentUnit(int indexShop)
    {
        indexUnit = indexShop;
        if (StatusBuy)
        {
            hologramUnit.InitializeHologram(buildingData[indexUnit]);
            return;
        }
        hologramUnit = Instantiate(prefabHologram, Vector3.zero, Quaternion.identity).GetComponent<CostructHologram>();
        hologramUnit.InitializeHologram(buildingData[indexUnit]);
        transformHologram = hologramUnit.transform;
        StatusBuy = true;
    }

    public void MoveHologramm()
    {
        Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;
        if (Physics.Raycast(ray, out _hit, 50, Cell))
        {
            GameObject currentCell = _hit.collider.gameObject;
            transformHologram.position = currentCell.transform.position;
            CanPlace = true;
        }
        else if(Physics.Raycast(ray, out _hit, 50, maskMoveHologramm)) 
        {
            transformHologram.position = new Vector3(_hit.point.x, 0.1f, _hit.point.z);
            CanPlace = false;
        }
    }

    public void PurchaseBuilding()
    {
        if (unitsStorage.avaliableUnits > 0)
            StartCoroutine(AcceptBuing());
    }

    private IEnumerator AcceptBuing()
    {
        yield return new WaitForFixedUpdate();

        if(hologramUnit.AcceptBuying())
        {
            //gMode.Gold = gMode.Gold - buildingData[indexBuyBuilding].Cost;
            //StatusBuy = false;
            unitsStorage.UseUnit();
        }
    }

    public void CancelBuy()
    {
        StatusBuy = false;
        Destroy(hologramUnit.gameObject);
    }

}
