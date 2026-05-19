using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettings : MonoBehaviour
{
    public Toggle shadowsToggle;

    void Start()
    {
        bool shadowsEnabled =
            PlayerPrefs.GetInt("Shadows", 1) == 1;

        shadowsToggle.isOn = shadowsEnabled;

        ApplyShadows(shadowsEnabled);
    }

    public void ToggleShadows(bool enabled)
    {
        ApplyShadows(enabled);

        PlayerPrefs.SetInt("Shadows", enabled ? 1 : 0);

        PlayerPrefs.Save();
    }

    void ApplyShadows(bool enabled)
    {
        QualitySettings.shadows =
            enabled ? ShadowQuality.All : ShadowQuality.Disable;
    }
}