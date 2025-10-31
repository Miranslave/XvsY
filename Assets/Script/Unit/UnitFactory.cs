using Script;
using Unity.VisualScripting;
using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    public PlaceUnit PlaceUnit_current;
    public PresentationBandManager _presentationBandManager;
    public PlayerManager _PlayerManager;
    public GameObject toSend;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _PlayerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        PlaceUnit_current = GetComponent<PlaceUnit>();
    }


    public BaseUnit Assemble(GameObject race, GameObject weapon, SpecialCapacity ability)
    {
        Clean();
        toSend = Instantiate(race, PlaceUnit_current.transform, true);
        GameObject toSendWeapon = Instantiate(weapon, toSend.transform);
        //toSendWeapon.transform.position += new Vector3(0.3f, 0, 0); 
        var toSendBaseUnit =toSend.GetComponent<BaseUnit>();
        toSendBaseUnit.weapon = toSendWeapon.GetComponent<Weapon>();
        toSendBaseUnit.specialCapacity = ability;
        PlaceUnit_current.RolledUnitPrefab = toSend;
        toSendBaseUnit.Innit();
        Deactivate(toSend);
        return toSendBaseUnit;
    }

    
    
    public void Deactivate(GameObject toSend)
    {
        toSend.SetActive(false);
    }

    public void Clean()
    {
        if (PlaceUnit_current.RolledUnitPrefab)
        {
            PlaceUnit_current.CleanCurrentPrefab();
        }
    }
}
