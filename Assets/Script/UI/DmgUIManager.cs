using System;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class DmgUIManager : MonoBehaviour
{
    [SerializeField] private Color _color;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private float scale;
    private float endX, endY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        endX = transform.position.x + UnityEngine.Random.Range(-0.3f, 0.3f);
        endY = transform.position.y + 1f;
        this.transform.localScale = scale * Vector3.one;
    }
    

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

        textMesh.color = TextColor;
        textMesh.SetText(damageAmount.ToString());
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(Scale, 0.6f).SetEase(Ease.OutBack));
        seq.Join(transform.DOMove(new Vector3(endX,endY,transform.position.z), 1f));
        seq.Join(textMesh.DOFade(0f, 1f));
        seq.OnComplete(() => Destroy(gameObject));
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        DOTween.Kill(transform);
    }
}
