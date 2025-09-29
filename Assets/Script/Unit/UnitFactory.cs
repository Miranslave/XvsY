using Script;
using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    public PlaceUnit PlaceUnit_current;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlaceUnit_current = GetComponent<PlaceUnit>();
    }


    public void Assemble(GameObject race, GameObject weapon, Component ability = null)
    {
        Clean();
        GameObject toSend = Instantiate(race);
        GameObject toSendWeapon = Instantiate(weapon, toSend.transform);
        //toSendWeapon.transform.position += new Vector3(0.3f, 0, 0); 
        toSend.GetComponent<BaseUnit>().weapon = toSendWeapon.GetComponent<Weapon>();
        PlaceUnit_current.rolledUnitPrefab = toSend;
        Deactivate(toSend);
    }

    public void Deactivate(GameObject toSend)
    {
        toSend.SetActive(false);
    }

    public void Clean()
    {
        if (PlaceUnit_current.rolledUnitPrefab)
        {
            PlaceUnit_current.CleanCurrentPrefab();
        }
    }
}
