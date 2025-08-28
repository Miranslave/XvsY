using System;
using System.Collections;
using Script.Struct;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponStat weaponstat;
    [SerializeField] private Ammo ammo;
    private float _cooldown;
    private Collider2D attackZone;
    private Animator animator;
    private bool animatorup;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        
       
        _cooldown = weaponstat.frequency;
        if (weaponstat.isRanged)
        {
            ammo.Prefab.GetComponent<Projectile>().ammo = ammo;
        }
        else
        {

        }
        
        if (animator = GetComponent<Animator>())
        {
            animatorup = true;
        }
        else
        {
            animatorup = false;
        }
    }
    

    public void Fire()
    {
      
            if (weaponstat.isRanged)
            {
                GameObject g = Instantiate(ammo.Prefab);
                g.transform.position = transform.position + Vector3.right;
                g.GetComponent<Rigidbody2D>().AddForce(Vector3.right * ammo.Speed,ForceMode2D.Impulse);
            }
            else
            {
                if (animatorup)
                {
                    animator.SetTrigger("attack");
                }
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
            g.GetComponent<Enemy>().TakeDmg(1);
        }
        // Check for a mele attack 
    }
}
