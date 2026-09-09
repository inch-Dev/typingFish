using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponentInParent<Fish>())
        {
            Debug.Log("Getting fish");
            FishManager.instance.RemoveFish(collision.gameObject.GetComponentInParent<Fish>());
        }
    }
}
