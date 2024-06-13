using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSquad : MonoBehaviour
{
    [SerializeField] SquadInfo squadInfo;
    public int SquadMobility;
    [SerializeField] private int currentMobility;
    public float MobilityChangeFactor = 1;
    private int moveCost = 1;
    [SerializeField] Transform LeftPivot;
    [SerializeField] Transform RightPivot;

    void Start()
    {
        squadInfo = GetComponent<SquadInfo>();
        RefreshMobility();
    }

    public void RefreshMobility()
    {
        currentMobility = SquadMobility;
    }

    public void  MoveForvard()
    {
        if (currentMobility > 0)
        {
            transform.Translate(Vector3.forward);
            currentMobility--;
            StertMoveUnit();
        }
    }

    public void MoveBack()
    {
        if (currentMobility > 0)
        {
            transform.Translate(Vector3.back);
            currentMobility = currentMobility -2;
            StertMoveUnit();
        }
    }

    public void MoveLeft()
    {
        if (currentMobility >= 2)
        {
            transform.Translate(Vector3.left);
            currentMobility = currentMobility - 2;
            StertMoveUnit();
        }
    }

    public void MoveRight()
    {
        if (currentMobility >= 2)
        {
            transform.Translate(Vector3.right); ;
            currentMobility = currentMobility - 2;
            StertMoveUnit();
        }
    }

    public void TurnRight()
    {
        if (currentMobility > 0)
        {
            transform.RotateAround(RightPivot.position, Vector3.up, angleDegreesABX());
            currentMobility--;
            StertMoveUnit();
        }
    }

    public void TurnLeft()
    {
        if (currentMobility > 0)
        {
            transform.RotateAround(LeftPivot.position, Vector3.up, -angleDegreesABX());
            currentMobility--;
            StertMoveUnit();
        }
    }

    private float angleDegreesABX()
    {
        float distance = Vector3.Distance(LeftPivot.position, RightPivot.position);
        float AX = distance;
        float AB = distance;
        float BX = 1;

        float cosABX = (AX * AX + AB * AB - BX * BX) / (2 * AX * AB);
        float angleABX = Mathf.Acos(cosABX);

        float angleDegreesABX = angleABX * 180 / Mathf.PI;
        return angleDegreesABX;
    }

    public void StertMoveUnit()
    {
        for(int i = 0; i < squadInfo.GetAllUnitsInSquad().Count; i++)
        {
            squadInfo.GetAllUnitsInSquad()[i].GetComponent<UnitMoveToCell>().StartMoveToTarget();
        }
    }
}
