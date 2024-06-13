using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMoveToCell : MonoBehaviour
{
    [SerializeField] private Transform cellTarget;
    [SerializeField] private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetTarget(Transform target)
    {
        cellTarget = target;
        StartMoveToTarget();
    }

    public void StartMoveToTarget()
    {
        agent.SetDestination(cellTarget.position);
        StartCoroutine(CheckDistanceToCell());
    }

    IEnumerator CheckDistanceToCell()
    {
        float distance = Vector3.Distance(transform.position, cellTarget.transform.position);
        yield return new WaitForSeconds(0.1f);
        if(distance > 0.1)
        {
            StartCoroutine(CheckDistanceToCell());
        }
        else
        {
            transform.rotation = cellTarget.transform.rotation;
            StopCoroutine(CheckDistanceToCell());
        }
    }
}
