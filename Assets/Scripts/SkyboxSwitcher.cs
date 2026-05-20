using UnityEngine;
using System.Collections;

public class SkyboxSwitcher : MonoBehaviour
{
    [Header("Skyboxes")]
    public Material eveningSkybox;
    public Material nightSkybox;

    [Header("Directional Light")]
    public Light directionalLight;

    [Header("Transition")]
    public float transitionDelay = 1f;

    public void SwitchToNight()
    {
        StartCoroutine(ChangeToNight());
    }

    IEnumerator ChangeToNight()
    {
        yield return new WaitForSeconds(transitionDelay);

        // Switch skybox
        RenderSettings.skybox = nightSkybox;

        DynamicGI.UpdateEnvironment();

        // Disable directional light
        if (directionalLight != null)
        {
            directionalLight.enabled = false;
        }
    }

    public void SwitchToEvening()
    {
        // Restore evening skybox
        RenderSettings.skybox = eveningSkybox;

        DynamicGI.UpdateEnvironment();

        // Enable directional light
        if (directionalLight != null)
        {
            directionalLight.enabled = true;
        }
    }
}