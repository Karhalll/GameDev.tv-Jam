using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevJam.Utils
{
    public class SpriteFader : MonoBehaviour
    {
        public void SpritesTransition(ref SpriteRenderer firstSprite, ref SpriteRenderer secondSprite, float transitonTime)
        {
            StartCoroutine(FadeIn(secondSprite, transitonTime));
            StartCoroutine(FadeOut(firstSprite, transitonTime));
        }

        private IEnumerator FadeIn(SpriteRenderer spriteToFade, float transitionTime)
        {
            Color spriteColor = spriteToFade.color;
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
                spriteToFade.color = newColor;
                yield return null;
            }
        }

        private IEnumerator FadeOut(SpriteRenderer spriteToFade, float transitionTime)
        {
            Color spriteColor = spriteToFade.color;
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
                spriteToFade.color = newColor;
                yield return null;
            }
        }
    }
}