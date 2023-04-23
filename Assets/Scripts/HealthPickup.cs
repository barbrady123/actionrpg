using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int Health;

    public float DespawnTime = 10.0f;

    private void Start()
    {
        if (this.DespawnTime <= 0f)
            return;

        Destroy(gameObject, this.DespawnTime);
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag != Global.Tags.Player)
            return;

        PlayerHealthController.Instance.AddHealth(this.Health);
        Destroy(gameObject);
    }
}
