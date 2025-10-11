using System;
using System.Collections;
using Script;
using Script.Struct;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using Unit = Unity.VisualScripting.Unit;

public class Weapon : MonoBehaviour
{
    [Header("Weapon info")]
    [SerializeField] private WeaponStat weaponstat;
    [SerializeField] private Ammo ammo;
    [SerializeField] private Ammo ammo_clone;
    [SerializeField] private Sprite Icon;
    [SerializeField] private StatusEffect _statusEffect;
    [SerializeField] private Coroutine WeaponCooldownCoroutine;
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
    private bool weaponIsFiring = false;
    
    public bool GetIsRanged()
    {
        return weaponstat.isRanged;
    }

    public float GetAmmoDmg()
    {
        return ammo_clone.BaseDamage;
    }
    public void SetAmmoStatus(StatusEffect statusEffect)
    {
        ammo_clone.StatusEffect = Instantiate(statusEffect);
    }

    public void SetWeaponStatus(StatusEffect statusEffect)
    {
        _statusEffect = Instantiate(statusEffect);
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

    private void Awake()
    {
        ammo_clone = Instantiate(ammo);
        if (weaponstat.isRanged)
        {
            ammo_clone.Prefab.GetComponent<Projectile>().ammo = ammo;
        }
    }

    private void Start()
    {
        _unit = gameObject.GetComponentInParent<BaseUnit>();
        WeaponCooldownCoroutine = StartCoroutine(StartWeaponCooldown());
        
        _cooldown = weaponstat.frequency;

        if (animator = GetComponent<Animator>())
        {
            animatorup = true;
        }
        else
        {
            animatorup = false;
        }
    }

    public void Update()
    {
        if (_unit.EnemyInSight && WeaponCooldownCoroutine == null & !weaponIsFiring)
        {
            WeaponCooldownCoroutine = StartCoroutine(StartWeaponCooldown());
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
        }
    }
    
    
    public IEnumerator StartWeaponCooldown()
    {
        weaponIsFiring = true;
        while (true && _unit.EnemyInSight)
        {
            yield return new WaitForSeconds(weaponstat.frequency);
            if (_unit.EnemyInSight)
            {
                Fire();  
            }
        }

        weaponIsFiring = false;
    }


    public void FireArrow()
    {
        
        GameObject g = Instantiate(ammo_clone.Prefab);

        bool isCriticalStrike  = _unit.CheckCrit();
        AmmoAddingEffect(g);
        if (isCriticalStrike)
        {
            g.GetComponent<Projectile>().SetCriticalStrike();
            Debug.Log("CRITICAL STRIKE");
        }
        g.transform.position = transform.position + Vector3.right*0.2f;
        g.GetComponent<Rigidbody2D>().AddForce(Vector3.right * ammo_clone.Speed,ForceMode2D.Impulse);
    }


    public void CastMagic()
    {
        GameObject g = Instantiate(ammo_clone.Prefab);
        bool isCriticalStrike  = _unit.CheckCrit();
        AmmoAddingEffect(g);
        if (isCriticalStrike)
        {
            g.GetComponent<Projectile>().SetCriticalStrike();
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
                
                enemyhit.TakeDmg(weaponstat.damage * 1.5f,true);
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
