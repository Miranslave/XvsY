using System.Collections;
using Script;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipUI : MonoBehaviour
{
    public GameObject tooltipUI;
    public TMP_Text nameText, hpText, dmgText;
    public Image spriteIcon;
    
    [SerializeField] private LayerMask layertohit;
    [SerializeField] private GridManager g;
    
    public float hoverDelay = 0.2f;
    private float hoverTimer = 0f;
    
    private Coroutine showTooltipRoutine;
    private Unit currentUnit;

    // Update is called once per frame
    void Update()
    {
        if(g._ishighlightcursor)
            Checktooltip();
    }
    
    
    
    void Checktooltip(){
        
        tooltipUI.transform.position = g.mouspos + new Vector2(20f, -20f);

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(g.mouspos), Vector2.zero,layertohit);
        if (hit.collider != null)
        {
            Unit unit = hit.collider.GetComponent<Unit>();
            if (unit != null)
            {
                if (unit != currentUnit)
                {
                    // Nouvelle unité hover → reset
                    currentUnit = unit;
                    if (showTooltipRoutine != null) StopCoroutine(showTooltipRoutine);
                    tooltipUI.SetActive(false);
                    showTooltipRoutine = StartCoroutine(ShowTooltipAfterDelay(unit));
                }
            }
        }else
        {
            currentUnit = null;
            if (showTooltipRoutine != null) StopCoroutine(showTooltipRoutine);
            tooltipUI.SetActive(false);
        }
    }
    
    private IEnumerator ShowTooltipAfterDelay(Unit unit)
    {
        yield return new WaitForSeconds(hoverDelay);

        // Vérifie qu’on est toujours sur la même unité
        if (unit == currentUnit)
        {
            tooltipUI.SetActive(true);
            spriteIcon.sprite = unit.icon;
            nameText.text = unit.name;
            hpText.text = $"HP: {unit.healthComponent.getCurrentHealth()}";
            dmgText.text = $"DMG: {unit.weapon.GetDmg()}";
        }
    }
    
    
}
