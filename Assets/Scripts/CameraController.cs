using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _target;

    public BoxCollider2D AreaBox;

    private float halfWidth;
    private float halfHeight;

    private void Start()
    {
        _target = PlayerController.Instance.transform;

        var camera = GetComponent<Camera>();

        halfHeight = camera.orthographicSize;
        halfWidth = halfHeight * camera.aspect;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(
            Mathf.Clamp(_target.position.x, this.AreaBox.bounds.min.x + halfWidth, this.AreaBox.bounds.max.x - halfWidth),
            Mathf.Clamp(_target.position.y, this.AreaBox.bounds.min.y + halfHeight, this.AreaBox.bounds.max.y - halfHeight),
            transform.position.z);
    }
}
