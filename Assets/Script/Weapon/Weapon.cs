using System;
using System.Collections;
using Script;
using Script.Struct;
using UnityEditor.Animations;
using UnityEngine;
using Unit = Unity.VisualScripting.Unit;

public class Weapon : MonoBehaviour
{
    [Header("Weapon info")]
    [SerializeField] private WeaponStat weaponstat;
    [SerializeField] private Ammo ammo;
    [SerializeField] private Sprite Icon;
    [SerializeField] private StatusEffect _statusEffect;
    public Sprite Icon1 => Icon;
    public StatusEffect StatusEffect
    {
        get => _statusEffect;
        set => _statusEffect = value;
    }

    private float _cooldown;
    
    [Header("Components")]
    private Collider2D attackZone;
    private Animator animator;
    private bool animatorup;
    
    private static readonly int Attack = Animator.StringToHash("attack");
    private BaseUnit _unit;

    public bool GetIsRanged()
    {
        return weaponstat.isRanged;
    }

    public float GetAmmoDmg()
    {
        return ammo.Damage;
    }
    public void SetAmmoStatus(StatusEffect statusEffect)
    {
        ammo.StatusEffect = statusEffect;
    }

    public void SetWeaponStatus(StatusEffect statusEffect)
    {
        _statusEffect = statusEffect;
    }
    public float GetRange()
    {
        return weaponstat.range;
    }

    public float GetDmg()
    {
        return weaponstat.damage;
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

        bool isCriticalStrike  = _unit.CheckCrit();
        AmmoAddingEffect(g);
        if (isCriticalStrike)
        {
            g.GetComponent<Ammo>().Damage *= 1.5f;
            Debug.Log("CRITICAL STRIKE");
        }
        g.transform.position = transform.position + Vector3.right*0.2f;
        g.GetComponent<Rigidbody2D>().AddForce(Vector3.right * ammo.Speed,ForceMode2D.Impulse);
    }


    public void CastMagic()
    {
        GameObject g = Instantiate(ammo.Prefab);
        bool isCriticalStrike  = _unit.CheckCrit();
        AmmoAddingEffect(g);
        if (isCriticalStrike)
        {
            g.GetComponent<Ammo>().Damage *= 1.5f;
            Debug.Log("CRITICAL STRIKE");
        }
        g.GetComponent<Summoned>().toFollowed = _unit.enemy_Gameobject;
        g.GetComponent<Summoned>().Offset = new Vector3(0,0.38f,0);
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
            Enemy enemyhit =  g.GetComponent<Enemy>();
            bool isCriticalStrike  = _unit.CheckCrit();
            if (isCriticalStrike)
            {
                enemyhit.TakeDmg(weaponstat.damage * 1.5f);
                Debug.Log("CRITICAL STRIKE");
            }
            else
            {
                enemyhit.TakeDmg(weaponstat.damage);
            }
            
            if (_statusEffect)
            {
                _statusEffect.Apply(enemyhit);
            }
        }
        // Check for a mele attack 
    }

    private void AmmoAddingEffect(GameObject g)
    {
        Projectile p = g.GetComponent<Projectile>();
        p.statusEffect = _statusEffect;
    }
}
