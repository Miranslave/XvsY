using System.Collections.Generic;
using UnityEngine;

public class SlotsUI : MonoBehaviour
{

    [SerializeField] private Animator _animator;
    [SerializeField] private Animation _animation;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public  List<Sprite> _list;
    private GameObject g;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void LaunchSlot(GameObject weapon)
    {
        g = weapon;
        _animator.SetTrigger("ROLLS");
    }

    public void SetEndSprite()
    {
        if(!g) return;
        switch (g.name)
        {
            case "Sword":
                Debug.Log("sword sprite");
                _spriteRenderer.color = Color.cyan;
                _spriteRenderer.sprite = _list[0];
                break;
            case "Bow":
                Debug.Log("bow sprite");
                _spriteRenderer.color = Color.red;
                _spriteRenderer.sprite = _list[1];
                break;
        }

    }


}
