using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] TimePhases timePhases = null;
    [SerializeField] SpriteRenderer spriteRendererCurrent = null;
    [SerializeField] SpriteRenderer spriteRendererNew = null;
    [SerializeField] float phaseTransitTime = 2f;

    int currentPhase = 0;
    int maxPhases = 5;

    private void Start() 
    {
        maxPhases = timePhases.GetPhases().Length;
    }

    private void Update() 
    {
        // only for debug
        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangePhase(currentPhase + 1);
            currentPhase++;
        }
    }

    private void ChangePhase(int phaseNumber)
    {
        if (phaseNumber > maxPhases - 1) 
        {
            Debug.LogError(gameObject.name + " phase limit exeded");
            return;
        }

        TimePhases.Phase phase = timePhases.GetPhases()[phaseNumber];

        spriteRendererNew.color = new Color(
            spriteRendererNew.color.r,
            spriteRendererNew.color.g,
            spriteRendererNew.color.b,
            0f
        );
        spriteRendererNew.sprite = phase.phaseSprite;

        StartCoroutine(FadeIn(phaseTransitTime));
        StartCoroutine(FadeOut(phaseTransitTime));
    }

    

    private IEnumerator FadeIn(float transitionTime)
    {
        Color spriteColor = spriteRendererNew.color;
        float time = 0f;
        float startAlpha = spriteColor.a;
        float currentAlpha = startAlpha;

        while (time < transitionTime)
        {
            currentAlpha += (1 / transitionTime) * Time.deltaTime;
            Color newColor = new Color(
                spriteColor.r,
                spriteColor.g,
                spriteColor.b,
                currentAlpha
            );
            time += Time.deltaTime;
            spriteRendererNew.color = newColor;
            yield return null;
        }
        SwapSprites();
    }

    private IEnumerator FadeOut(float transitionTime)
    {
        Color spriteColor = spriteRendererCurrent.color;
        float time = 0f;
        float startAlpha = spriteColor.a;
        float currentAlpha = startAlpha;

        while (time < transitionTime)
        {
            currentAlpha -= (startAlpha / transitionTime) * Time.deltaTime;
            Color newColor = new Color(
                spriteColor.r,
                spriteColor.g,
                spriteColor.b,
                currentAlpha
            );
            time += Time.deltaTime;
            spriteRendererCurrent.color = newColor;
            yield return null;
        }
    }

    private void SwapSprites()
    {
        SpriteRenderer holder = spriteRendererCurrent;
        spriteRendererCurrent = spriteRendererNew;
        spriteRendererNew = holder;
    }

    
}
