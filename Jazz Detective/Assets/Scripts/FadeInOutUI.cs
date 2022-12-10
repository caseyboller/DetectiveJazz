using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class FadeInOutUI : MonoBehaviour
{

    public float inDuration = 3f;
    public float inDelay = 2f;
    
    public float outDuration = 5f;
    public float outDelay = 3f;

    public bool fadeOut = true;

    // Start is called before the first frame update
    void Start()
    {
        CanvasGroup canvasgroup = this.gameObject.GetComponent<CanvasGroup>();
        TextMeshProUGUI infoTextTMPro = this.gameObject.GetComponent<TextMeshProUGUI>();
        canvasgroup.alpha = 0f;
        if (fadeOut)
        {
        LeanTween.alphaCanvas(canvasgroup, 1.0f, inDuration).setDelay(inDelay).setOnComplete(FadeOut);
        } else
        {
            LeanTween.alphaCanvas(canvasgroup, 1.0f, inDuration).setDelay(inDelay);
        }
    }

    void FadeOut()
    {
        CanvasGroup canvasgroup = this.gameObject.GetComponent<CanvasGroup>();
        TextMeshProUGUI infoTextTMPro = this.gameObject.GetComponent<TextMeshProUGUI>();
        LeanTween.alphaCanvas(canvasgroup, 0.0f, outDuration).setDelay(outDelay).setDestroyOnComplete(true);
    }
}
