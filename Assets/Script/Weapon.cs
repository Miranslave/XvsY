using System;
using System.Collections;
using Script.Struct;
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
        _isOnCd = false;
        _cooldown = weaponstat.frequency;
        if (weaponstat.isRanged)
        {
            
        }
        else
        {
            
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (_cooldown > 0)
        {
            Debug.Log("activation arme");
            _cooldown -= Time.deltaTime;
        }
        else
        {
            _cooldown = weaponstat.frequency;
        }
    }

    public void Fire()
    {
        if (_isOnCd)
        {
            return;
        }
        else
        {
            if (weaponstat.isRanged)
            {
                GameObject g = Instantiate(weaponstat.projectile);
                g.transform.position = transform.position + Vector3.right;
                g.GetComponent<Rigidbody>().AddForce(Vector3.right * weaponstat.projectilespeed);
                _isOnCd = true;
            }
            else
            {
                attackZone.enabled = true;
                _isOnCd = true;
                attackZone.enabled = false;
            }
        }
    }


    
    public IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(weaponstat.frequency); // Attente entre les soleils
        _isOnCd = false;
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
