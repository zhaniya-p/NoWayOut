using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneFadeIn : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 2f;

    void Start()
    {
        // Ensure fade panel is active
        fadeImage.gameObject.SetActive(true);

        // Force black at scene start
        Color color = fadeImage.color;
        fadeImage.color = new Color(color.r, color.g, color.b, 1f);

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        Color color = fadeImage.color;

        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            float alpha =
                Mathf.Lerp(1f, 0f, timer / fadeDuration);

            fadeImage.color =
                new Color(color.r, color.g, color.b, alpha);

            yield return null;
        }

        fadeImage.color =
            new Color(color.r, color.g, color.b, 0f);

        fadeImage.gameObject.SetActive(false);
    }
}