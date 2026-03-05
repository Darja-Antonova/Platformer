using UnityEngine;

public class HealthOrbRespawn : MonoBehaviour
{
    public void OrbRespawn()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
