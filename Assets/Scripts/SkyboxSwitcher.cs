using UnityEngine;
using System.Collections;

public class SkyboxSwitcher : MonoBehaviour
{
    [Header("Skyboxes")]
    public Material eveningSkybox;
    public Material nightSkybox;

    [Header("Directional Light")]
    public Light directionalLight;

    [Header("Night Settings")]
    public Color nightLightColor = new Color(0.4f, 0.45f, 0.6f);
    public float nightIntensity = 0.2f;

    [Header("Rotation")]
    public Vector3 nightRotation = new Vector3(340f, -30f, 0f);

    [Header("Transition")]
    public float transitionDelay = 1f;

    private Color originalColor;
    private float originalIntensity;
    private Quaternion originalRotation;

    void Start()
    {
        if (directionalLight != null)
        {
            originalColor = directionalLight.color;
            originalIntensity = directionalLight.intensity;
            originalRotation = directionalLight.transform.rotation;
        }
    }

    public void SwitchToNight()
    {
        StartCoroutine(ChangeToNight());
    }

    IEnumerator ChangeToNight()
    {
        yield return new WaitForSeconds(transitionDelay);

        // Change skybox
        RenderSettings.skybox = nightSkybox;

        DynamicGI.UpdateEnvironment();

        // Modify directional light
        if (directionalLight != null)
        {
            directionalLight.color = nightLightColor;

            directionalLight.intensity = nightIntensity;

            directionalLight.transform.rotation =
                Quaternion.Euler(nightRotation);
        }
    }

    public void SwitchToEvening()
    {
        RenderSettings.skybox = eveningSkybox;

        DynamicGI.UpdateEnvironment();

        if (directionalLight != null)
        {
            directionalLight.color = originalColor;

            directionalLight.intensity = originalIntensity;

            directionalLight.transform.rotation =
                originalRotation;
        }
    }
}