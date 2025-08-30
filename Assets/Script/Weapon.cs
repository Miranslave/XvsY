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
        StartCoroutine(StartWeaponCooldown());
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
        /*
            if (weaponstat.isRanged)
            {
                GameObject g = Instantiate(ammo.Prefab);
                g.transform.position = transform.position + Vector3.right;
                g.GetComponent<Rigidbody2D>().AddForce(Vector3.right * ammo.Speed,ForceMode2D.Impulse);
            }
            else
            {
                Debug.Log("Attack mele");
            }*/
            PlayAttackAnimation();
            
    }

    private void PlayAttackAnimation()
    {
        if (animatorup)
        { 
            animator.SetTrigger("attack");
            float spawntimer = animator.GetCurrentAnimatorStateInfo(0).length/2;
            //StartCoroutine(InstantiateProjectile(ammo, spawntimer));
        }
    }

   
    
    
    public IEnumerator StartWeaponCooldown()
    {
        while (true)
        {
            yield return new WaitForSeconds(weaponstat.frequency); 
            Fire();
        }
        
    }


    public void FireArrow()
    {
        GameObject g = Instantiate(ammo.Prefab);
        g.transform.position = transform.position + Vector3.right*0.2f;
        g.GetComponent<Rigidbody2D>().AddForce(Vector3.right * ammo.Speed,ForceMode2D.Impulse);
    }
    
    private IEnumerator InstantiateProjectile(Ammo projectile, float delay, int numberofprojectile = 1)
    {
        yield return new WaitForSeconds(delay);
        if (numberofprojectile > 1)
        {
            for (int i = 0; i < numberofprojectile; i++)
            {
                GameObject g = Instantiate(projectile.Prefab);
                g.transform.position = transform.position + Vector3.right;
                g.GetComponent<Rigidbody2D>().AddForce(Vector3.right * projectile.Speed,ForceMode2D.Impulse);
            }
        }
        else
        {
            GameObject g = Instantiate(projectile.Prefab);
            g.transform.position = transform.position + Vector3.right;
            g.GetComponent<Rigidbody2D>().AddForce(Vector3.right * projectile.Speed,ForceMode2D.Impulse);
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
