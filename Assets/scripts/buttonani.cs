using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static LeanTween;

public class buttonani      : MonoBehaviour
{
    public Button button;
    public float hoverScale = 1.2f;
    public float hoverTime = 0.2f;
    public float clickScale = 0.8f;
    public float clickTime = 0.1f;

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = button.transform.localScale;
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        // Animate button click
        LeanTween.scale(button.gameObject, originalScale * clickScale, clickTime)
            .setEase(LeanTweenType.easeOutCubic)
            .setOnComplete(ResetScale);
    }

    private void ResetScale()
    {
        // Reset button scale after click animation
        LeanTween.scale(button.gameObject, originalScale, clickTime)
            .setEase(LeanTweenType.easeOutCubic);
    }

    public void OnPointerEnter()
    {
        // Animate button hover
        LeanTween.scale(button.gameObject, originalScale * hoverScale, hoverTime)
            .setEase(LeanTweenType.easeOutCubic);
    }

    public void OnPointerExit()
    {
        // Reset button scale after hover animation
        LeanTween.scale(button.gameObject, originalScale, hoverTime)
            .setEase(LeanTweenType.easeOutCubic);
    }
}
