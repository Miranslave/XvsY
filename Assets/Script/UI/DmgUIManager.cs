using System;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class DmgUIManager : MonoBehaviour
{
    [SerializeField] private Color _color;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private float scale;
    [SerializeField] private Sequence seq;
    private float endX, endY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        endY = transform.position.y + 1f;
        this.transform.localScale = scale * Vector3.one;
    }
    
    
    // function to override Color of the text and the scale 
    public void Setup(float damageAmount,Color color ,bool iscrit = false,float insidescaling = 0.6f)
    {
        endX = transform.position.x + UnityEngine.Random.Range(-1.5f, 1.5f);
        textMesh.color = color;
        textMesh.SetText(damageAmount.ToString());
        seq = DOTween.Sequence();
        seq.Append(transform.DOScale(insidescaling, 0.6f).SetEase(Ease.OutBack));
        seq.Join(transform.DOMove(new Vector3(endX,endY,transform.position.z), 1f));
        seq.Join(textMesh.DOFade(0f, 1f));
        seq.OnComplete(() => Destroy(gameObject));
    }
    
    
    
    
    // pour ne plus avoir les DotWeen warning gérer la mort en mettant fin manuellement au DoTWeen
    public void Setup(float damageAmount,bool iscrit)
    {
        float Scale;
        Color TextColor;
        if (iscrit)
        {
            Scale = 0.6f;
            TextColor = Color.red;
        }
        else
        {
            Scale = 0.4f;
            TextColor = Color.orange;
        }
        endX = transform.position.x + UnityEngine.Random.Range(-1.5f, 1.5f);
        textMesh.color = TextColor;
        textMesh.SetText(damageAmount.ToString());
        seq = DOTween.Sequence();
        seq.Append(transform.DOScale(Scale, 0.6f).SetEase(Ease.OutBack));
        seq.Join(transform.DOMove(new Vector3(endX,endY,transform.position.z), 1f));
        seq.Join(textMesh.DOFade(0f, 1f));
        seq.OnComplete(() => Destroy(gameObject));
    }

    

    //Make sure to kill the TWEEN to make dodge shady message
    public void CleanKill()
    {
        seq?.Kill();
    }




}
