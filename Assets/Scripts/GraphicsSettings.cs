using UnityEngine;

public class GraphicsSettings : MonoBehaviour
{
    public void ToggleShadows(bool enabled)
    {
        QualitySettings.shadows =
            enabled ? ShadowQuality.All : ShadowQuality.Disable;
    }
}