using UnityEngine;
using UnityEngine.UI;

public class PlayerIcon : MonoBehaviour
{
    public Transform player;
    public Camera minimapCamera;
    public RectTransform minimapRect;
    private RectTransform iconRect;

    void Start()
    {
        iconRect = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (player == null || minimapCamera == null) return;

        Vector3 viewportPos = minimapCamera.WorldToViewportPoint(player.position);

        float x = (viewportPos.x - 0.5f) * minimapRect.sizeDelta.x;
        float y = (viewportPos.y - 0.5f) * minimapRect.sizeDelta.y;

        iconRect.anchoredPosition = new Vector2(x, y);
    }
}