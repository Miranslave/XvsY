using UnityEngine;
using UnityEngine.UIElements;

public class PageFlipperUI : MonoBehaviour
{
    [Header("Page UI Elements")]
    public RectTransform page;      // Le conteneur (celui qui tourne)
    public GameObject pageFront;    // Face avant
    public GameObject pageBack;    // Face arrière
    public RectTransform button;

    [Header("Rotation Settings")]
    public float flipDuration = 1.0f;   // Temps total du flip (secondes)
    public bool flipping = false;

    private float flipProgress = 0f;    // entre 0 et 1
    private bool flippingForward = true;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!flipping) return;

        // Avance le timer
        flipProgress += Time.deltaTime / flipDuration;
        float t = Mathf.SmoothStep(0f, 1f, flipProgress);
        float angle = Mathf.Lerp(flippingForward ? 0f : 180f, flippingForward ? 180f : 0f, t);

        // Rotation
        page.localRotation = Quaternion.Euler(0f, angle, 0f);
        if (button) button.localRotation = page.localRotation;

        // Faces visibles
        bool showFront = (angle <= 90f);
        pageFront.SetActive(showFront);
        pageBack.SetActive(!showFront);

        // Fin du flip
        if (flipProgress >= 1f)
        {
            flipping = false;
            flipProgress = 0f;

            // Angle final
            page.localRotation = Quaternion.Euler(0f, flippingForward ? 180f : 0f, 0f);
            if (button) button.localRotation = page.localRotation;

            // Inverse sens
            flippingForward = !flippingForward;

            // Correction visibilité
            if (flippingForward)
            {
                pageFront.SetActive(true);
                pageBack.SetActive(false);
            }
            else
            {
                pageFront.SetActive(false);
                pageBack.SetActive(true);
            }
        }
    
    }
    
    public void Flip(float flipDuration)
    {
        if (!flipping) return;

        // Avance le timer
        flipProgress += Time.deltaTime / flipDuration;
        float t = Mathf.SmoothStep(0f, 1f, flipProgress);
        float angle = Mathf.Lerp(flippingForward ? 0f : 180f, flippingForward ? 180f : 0f, t);

        // Rotation
        page.localRotation = Quaternion.Euler(0f, angle, 0f);
        if (button) button.localRotation = page.localRotation;

        // Faces visibles
        bool showFront = (angle <= 90f);
        pageFront.SetActive(showFront);
        pageBack.SetActive(!showFront);

        // Fin du flip
        if (flipProgress >= 1f)
        {
            flipping = false;
            flipProgress = 0f;

            // Angle final
            page.localRotation = Quaternion.Euler(0f, flippingForward ? 180f : 0f, 0f);
            if (button) button.localRotation = page.localRotation;

            // Inverse sens
            flippingForward = !flippingForward;

            // Correction visibilité
            if (flippingForward)
            {
                pageFront.SetActive(true);
                pageBack.SetActive(false);
            }
            else
            {
                pageFront.SetActive(false);
                pageBack.SetActive(true);
            }
        }
    }

    // Appelle cette fonction pour tourner la page
    public void FlipPage()
    {
        if (!flipping)
        {
            flipping = true;
            flipProgress = 0f;
        }
    }



    public void ResetPage()
    {
        
    }

    // Prepare Page data
    public void EnterData()
    {
        
    }
}
