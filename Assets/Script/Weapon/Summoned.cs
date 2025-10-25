using System;
using Script;
using Unity.VisualScripting;
using UnityEngine;

public class Summoned : MonoBehaviour,IPausable
{

    public GameObject toFollowed;
    public Vector3 Offset;
    public Animator _animmator;


    private void Start()
    {
        _animmator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (toFollowed)
        {
            Follow(toFollowed,Offset);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    private void Follow(GameObject g,Vector3 offset)
    {
        this.transform.position = g.transform.position + offset;
    }
    private void Follow(GameObject g)
    {
        this.transform.position = g.transform.position;
    }

    private void EndSummoned()
    {
        Destroy(this.gameObject);
    }

    public void OnPause()
    {
        _animmator.speed = 0f;
    }

    public void OnResume()
    {
        _animmator.speed = 1f;
    }
}
