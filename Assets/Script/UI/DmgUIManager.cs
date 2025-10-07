using System;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class DmgUIManager : MonoBehaviour
{
    [SerializeField] private float lifetime = 1f;
    [SerializeField] private Color _color;
    [SerializeField] private TextMeshProUGUI textMesh;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }
    

    public void Setup(float damageAmount)
    {
        //textMesh = GetComponent<TextMeshPro>();
        textMesh.SetText(damageAmount.ToString());
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack));  // pop
        seq.Join(transform.DOMoveY(transform.position.y + 1f, 1f));      // monte
        seq.Join(textMesh.DOFade(0f, 1f));                              // fondu
        seq.OnComplete(() => Destroy(gameObject));
    }
    // Update is called once per frame
    void Update()
    {

    }
    
    
}
