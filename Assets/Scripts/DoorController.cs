using UnityEngine;

public class DoorController : MonoBehaviour
{
    public static DoorController Instance;

    public GameObject doorBlock;
    public Light winLight;
    public GameObject winZone;
    public ParticleSystem winParticles;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (doorBlock != null) doorBlock.SetActive(true);
        if (winLight != null) winLight.gameObject.SetActive(false);
        if (winZone != null) winZone.SetActive(false);
    }

    public ParticleSystem winDust;

    public void OpenDoor()
    {
        if (doorBlock != null) doorBlock.SetActive(false);
        if (winLight != null) winLight.gameObject.SetActive(true);
        if (winZone != null) winZone.SetActive(true);
        if (winParticles != null) winParticles.Play();
        if (winDust != null) winDust.Play();
    }
}