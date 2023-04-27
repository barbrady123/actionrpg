using UnityEngine;

public class CustomCameraController : MonoBehaviour
{
    public static CustomCameraController Instance;

    public CameraMode Mode;

    public BoxCollider2D AreaBoxEntering;
    public BoxCollider2D AreaBox;

    public float MoveSpeed;

    private float _halfWidth;
    private float _halfHeight;

    private bool _betweenAreas;

    void Awake()
    {
        if ((Instance != null) && (Instance != this))
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        var camera = GetComponent<Camera>();
        _halfHeight = camera.orthographicSize;
        _halfWidth = _halfHeight * camera.aspect;
        this.AreaBoxEntering = null;
        _betweenAreas = true;
    }

    void LateUpdate()
    {
        if (this.Mode == CameraMode.AreaBounded)
        {
            transform.position = new Vector3(
                Mathf.Clamp(PlayerController.Instance.transform.position.x, this.AreaBox.bounds.min.x + _halfWidth, this.AreaBox.bounds.max.x - _halfWidth),
                Mathf.Clamp(PlayerController.Instance.transform.position.y, this.AreaBox.bounds.min.y + _halfHeight, this.AreaBox.bounds.max.y - _halfHeight),
                transform.position.z);
        }
        else if (this.Mode == CameraMode.TargetLocked)
        {
            var target = new Vector3(
                this.AreaBox.transform.position.x,
                this.AreaBox.transform.position.y,
                transform.position.z);

            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                this.MoveSpeed * Time.deltaTime);
        }
    }

    public void EnteringArea(BoxCollider2D collider)
    {
        this.AreaBoxEntering = collider;
        if (_betweenAreas)
        {
            SwitchArea();
        }
    }

    public void SwitchArea()
    {
        if (this.AreaBoxEntering == null)
        {
            _betweenAreas = true;
            return;
        }

        this.AreaBox = this.AreaBoxEntering;
        this.AreaBoxEntering = null;
        _betweenAreas = false;
    }
}
