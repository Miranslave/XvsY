using Script;
using Unity.VisualScripting;
using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    public PlaceUnit PlaceUnit_current;
    public PresentationBandManager _presentationBandManager;
    public PlayerManager _PlayerManager;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _PlayerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        PlaceUnit_current = GetComponent<PlaceUnit>();
    }


    public void Assemble(GameObject race, GameObject weapon, SpecialCapacity ability)
    {
        Clean();
        GameObject toSend = Instantiate(race);
        GameObject toSendWeapon = Instantiate(weapon, toSend.transform);
        //toSendWeapon.transform.position += new Vector3(0.3f, 0, 0); 
        toSend.GetComponent<BaseUnit>().weapon = toSendWeapon.GetComponent<Weapon>();
        toSend.GetComponent<BaseUnit>().specialCapacity = ability;
        PlaceUnit_current.RolledUnitPrefab = toSend;
        toSend.GetComponent<BaseUnit>().Innit();
        
        // BAND TO DO PRESENTATION 
        
        if (_PlayerManager.CheckIfNewUnit(toSend.GetComponent<BaseUnit>()))
        {
            SpriteRenderer srU = toSend.GetComponent<BaseUnit>().spriteRenderer;
            SpriteRenderer srW = toSendWeapon.GetComponent<SpriteRenderer>();
            _presentationBandManager.new_unit = srU.sprite;
            _presentationBandManager.new_weapon = srW.sprite;
            _presentationBandManager.Innit();
        }
        
        // END
        toSend.transform.SetParent(PlaceUnit_current.transform);
        Deactivate(toSend);
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
