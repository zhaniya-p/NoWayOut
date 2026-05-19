using UnityEngine;

public class WinZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.TriggerWin();
        }
    }
}