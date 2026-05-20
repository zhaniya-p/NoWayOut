using UnityEngine;
using System.Collections;

public class SceneFadeIn : MonoBehaviour
{
    public CanvasGroup fadeGroup;

    public float fadeDuration = 2f;

    void Start()
    {
        Time.timeScale = 1f;

        fadeGroup.gameObject.SetActive(true);

        fadeGroup.alpha = 1f;

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;

            fadeGroup.alpha =
                Mathf.Lerp(1f, 0f, timer / fadeDuration);

            yield return null;
        }

        fadeGroup.alpha = 0f;

        fadeGroup.gameObject.SetActive(false);
    }
}