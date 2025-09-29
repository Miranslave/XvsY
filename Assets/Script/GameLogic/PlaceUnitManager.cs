using System.Collections.Generic;
using Script;
using UnityEngine;

public class PlaceUnitManager : MonoBehaviour
{

    public List<PlaceUnit> PlaceUnits_list;

    public PlaceUnit current_placeunit;
    
    public int current_index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlaceUnits_list.Clear();
        PlaceUnit p;
        
        foreach (Transform g in transform)
        {
            if(p = g.GetComponent<PlaceUnit>())
                PlaceUnits_list.Add(p);
        }
        
        current_index = 0;
        current_placeunit = PlaceUnits_list[current_index];
    }

    public PlaceUnit GetCurrentPlaceUnit()
    {
        return current_placeunit;
    }

    public void Pass()
    {
        current_index = current_index % PlaceUnits_list.Count+1;
        current_placeunit = PlaceUnits_list[current_index];
    }


    public bool GetFirstUnusedPlaceUnit(out PlaceUnit p)
    {
        p = null;
        foreach (var pU in PlaceUnits_list)
        {
            if (pU.rolledUnitPrefab == null)
            {
                p = pU;
                return true;
            }
        }
        return false;
    }
}
