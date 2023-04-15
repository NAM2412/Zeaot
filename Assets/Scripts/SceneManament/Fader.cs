using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SCeneManagement
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] float timeToFadeOut = 3f;
        [SerializeField] float timeToFadeIn = 2f;
        CanvasGroup canvasGroup;
        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            StartCoroutine(FadeOutIn());

        }

        public IEnumerator FadeOutIn()
        {
            yield return FadeOut(timeToFadeOut);
            Debug.Log("Fade out");
            yield return FadeIn(timeToFadeIn);
            Debug.Log("Fade in");
        }
        public IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }
        }

        IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }
    }

}
