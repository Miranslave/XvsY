using Script;
using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    public PlaceUnit PlaceUnit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlaceUnit = GetComponent<PlaceUnit>();
    }


    public void Assemble(GameObject race, GameObject weapon, Component ability = null)
    {
        GameObject toSend = Instantiate(race);
        GameObject toSendWeapon = Instantiate(weapon, toSend.transform);
        toSend.GetComponent<BaseUnit>().weapon = toSendWeapon.GetComponent<Weapon>();
        PlaceUnit.prefab = toSend;
        Deactivate(toSend);
    }

    public void Deactivate(GameObject toSend)
    {
        toSend.SetActive(false);
    }
   
}
