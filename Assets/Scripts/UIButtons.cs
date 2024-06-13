using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtons : MonoBehaviour
{
    private SelectController selectController;

    private void Start()
    {
        selectController = GetComponent<SelectController>();
    }
    public void ChangeSquadFormation()
    {
        if (selectController.GetSelectedSquad() != null)
        {
            selectController.GetSelectedSquad().ChangeSquadFormation();
        }
    }

    public void AcceptChanges()
    {
        selectController.GetSelectedSquad().AcceptChanges();
    }
}
