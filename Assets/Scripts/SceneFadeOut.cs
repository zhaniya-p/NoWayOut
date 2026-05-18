using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFadeOut : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.5f;

    private bool isLoading = false;

    public void LoadScene(string sceneName)
    {
        if (!isLoading)
        {
            StartCoroutine(FadeAndLoad(sceneName));
        }
    }

    IEnumerator FadeAndLoad(string sceneName)
    {
        isLoading = true;

        fadeImage.gameObject.SetActive(true);

        Color color = fadeImage.color;

        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);

            fadeImage.color = new Color(color.r, color.g, color.b, alpha);

            yield return null;
        }

        fadeImage.color = new Color(color.r, color.g, color.b, 1f);

        SceneManager.LoadScene(sceneName);
    }
}