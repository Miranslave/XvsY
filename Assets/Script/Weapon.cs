using System;
using System.Collections;
using Script.Struct;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponStat weaponstat;
    private float _cooldown;
    private bool _isOnCd;
    private Collider2D attackZone;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _cooldown = weaponstat.frequency;
        if (weaponstat.isRanged)
        {
            
        }
        else
        {
            
        }
    }
    

    public void Fire()
    {
      
            if (weaponstat.isRanged)
            {
                GameObject g = Instantiate(weaponstat.projectile);
                g.transform.position = transform.position + Vector3.right;
                g.GetComponent<Rigidbody2D>().AddForce(Vector3.right * weaponstat.projectilespeed,ForceMode2D.Impulse);
                _isOnCd = true;
            }
            else
            {
                
                Debug.Log("Attack mele");
                /*
                attackZone.enabled = true;
                _isOnCd = true;
                attackZone.enabled = false;
                */
            }
        
    }


    
    public IEnumerator StartCooldown()
    {
        while (true)
        {
            yield return new WaitForSeconds(weaponstat.frequency); 
            Fire();
        }
        
    }

   
    public void OnCollisionEnter2D(Collision2D other)
    {
        GameObject g = other.gameObject;
        if (g.CompareTag("Enemy"))
        {
            Debug.Log($"Enemy in mele zone {g.name}");
        }
        // Check for a mele attack 
        throw new NotImplementedException();
    }
}
