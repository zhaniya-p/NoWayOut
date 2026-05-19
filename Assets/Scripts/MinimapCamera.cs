using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    public Transform player;
    public float height = 50f;

    void LateUpdate()
    {
        if (player == null) return;
        transform.position = new Vector3(
            player.position.x,
            player.position.y + height,
            player.position.z
        );
    }
}