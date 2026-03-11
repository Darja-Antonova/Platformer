using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    public float timeToTogglePlatform = 2;
    public float currentTime = 0;
    public bool enabled = true;

    void Start()
    {
        enabled = true;
    }

    void Update()
    {
        currentTime = Time.deltaTime;
        if(currentTime >= timeToTogglePlatform)
        {
            currentTime = 0;
            TogglePlatform();
        }
    }

    void TogglePlatform()
    {
        enabled = !enabled;
    }
}
