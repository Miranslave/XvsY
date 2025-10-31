using System;
using Script;
using UnityEngine;
using UnityEngine.Serialization;

public class PresentationBandManager : MonoBehaviour
{
    private Animator _animator;
    public GameObject character;
    public GameObject weapon;
    public Sprite new_unit,new_weapon,new_ability;
    public enum PresentationState { Inactive, Entering, Active, Exiting }
    public PresentationState state = PresentationState.Inactive;// 0 prez inactive - 1 prez run to mid - 2 prez end

     public ParticleSystem _particleSystem;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    public void Innit()
    {
        
        if (state != PresentationState.Inactive) return;
        
        state = PresentationState.Entering;
        gameObject.SetActive(true);
        
        if (character.TryGetComponent(out SpriteRenderer sr))
        {
            sr.sprite = new_unit;
        }

        
        if (weapon.TryGetComponent(out SpriteRenderer wsr))
        {
            wsr.sprite = new_weapon;
        }
        else
        {
            Debug.LogWarning("❗ Aucun SpriteRenderer trouvé sur character !");
        }
        _animator.speed = 1f;
        //_animator.SetTrigger("Enter");
    }
    
    public void OnEnterFinished()
    {
        state = PresentationState.Active;
        Debug.Log("✅ Présentation active — en attente d’un input...");
    }

    public void TriggerParticles()
    {
        _particleSystem.Play();
    }

    public void End()
    {
        if (state != PresentationState.Active)
        {
            Debug.Log("⏳ Fin ignorée : pas encore en phase Active.");
            return;
        }

        state = PresentationState.Exiting;
        _animator.SetTrigger("Out");
    }

    public void ExitFinished()
    {
        Reset();
        GameEvents.RequestResume();
    }

    private void Reset()
    {
        state = PresentationState.Inactive;
        _animator.speed = 0f;
        gameObject.SetActive(false);
        Debug.Log("🔄 Présentation réinitialisée.");
    }
    
}
