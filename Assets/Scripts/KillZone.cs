using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] private Snake sanke;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Snake")
        {
            if (sanke.bodyParts.Count > 0)
            {
                for (int i = 0; i < sanke.bodyParts.Count; i++)
                {
                    Destroy(sanke.bodyParts[i].gameObject);
                }
            }
            Destroy(other.gameObject);
            GameManager.instance.LoseGame();
        }
    }
}