using System;
using System.Collections;
using Script;
using Script.Struct;
using UnityEditor.Animations;
using UnityEngine;
using Unit = Unity.VisualScripting.Unit;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponStat weaponstat;
    [SerializeField] private Ammo ammo;
    private float _cooldown;
    private Collider2D attackZone;
    private Animator animator;
    private bool animatorup;
    
    private static readonly int Attack = Animator.StringToHash("attack");
    private BaseUnit _unit;



    public float GetRange()
    {
        return weaponstat.range;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _unit = gameObject.GetComponentInParent<BaseUnit>();
        StartCoroutine(StartWeaponCooldown());
        _cooldown = weaponstat.frequency;
        if (weaponstat.isRanged)
        {
            ammo.Prefab.GetComponent<Projectile>().ammo = ammo;
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

    private void Update()
    {
        //CheckIfEnemyInLane();
    }


    public void Fire()
    {
            PlayAttackAnimation();
    }

    private void PlayAttackAnimation()
    {
        if (animatorup)
        { 
            animator.SetTrigger(Attack);
            //StartCoroutine(InstantiateProjectile(ammo, spawntimer));
        }
    }
    
   
    
    
    public IEnumerator StartWeaponCooldown()
    {
        while (true)
        {
            yield return new WaitForSeconds(weaponstat.frequency);
            if (_unit.EnemyInSight)
            {
                Fire();  
            }
            
        }
        
    }


    public void FireArrow()
    {
        GameObject g = Instantiate(ammo.Prefab);
        g.transform.position = transform.position + Vector3.right*0.2f;
        g.GetComponent<Rigidbody2D>().AddForce(Vector3.right * ammo.Speed,ForceMode2D.Impulse);
    }

    public void OnUpgrade()
    {
        weaponstat.frequency = weaponstat.frequency * 0.9f;
        weaponstat.damage = weaponstat.damage + 1;
    }
    
    
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject g = other.gameObject;
        if (g.CompareTag("Enemy"))
        {
            g.GetComponent<Enemy>().TakeDmg(weaponstat.damage);
        }
        // Check for a mele attack 
    }
}
