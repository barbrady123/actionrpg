using UnityEngine;

public class Door : MonoBehaviour
{
    private BoxCollider2D _collider = null;

    public BoxCollider2D Collider
    {
        get => _collider = _collider ?? GetComponent<BoxCollider2D>();
    }
}
