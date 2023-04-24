using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int Value;

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

        PlayerController.Instance.AddCoins(this.Value);
        Destroy(gameObject);
    }
}
