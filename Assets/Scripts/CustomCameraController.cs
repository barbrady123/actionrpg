using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CustomCameraController : MonoBehaviour
{
    public static CustomCameraController Instance;

    public CameraMode DefaultCameraMode;
    private CameraMode _mode;

    public BoxCollider2D AreaBoxEntering;
    public BoxCollider2D AreaBox;

    public float MoveSpeed;

    private float _halfWidth;
    private float _halfHeight;

    private bool _betweenAreas;

    private Vector2 _vertBoundPointMin;
    private Vector2 _vertBoundPointMax;
    
    private CameraMode _enteringMode;
    private Vector2? _enteringBoundPointMin;
    private Vector2? _enteringBoundPointMax;

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
        _vertBoundPointMin = Vector2.zero;
        _vertBoundPointMax = Vector2.zero;
        _enteringMode = CameraMode.None;
        _enteringBoundPointMin = null;
        _enteringBoundPointMax = null;
        _mode = this.DefaultCameraMode;
    }

    void LateUpdate()
    {
        if (_mode == CameraMode.AreaBounded)
        {
            transform.position = new Vector3(
                Mathf.Clamp(PlayerController.Instance.transform.position.x, this.AreaBox.bounds.min.x + _halfWidth, this.AreaBox.bounds.max.x - _halfWidth),
                Mathf.Clamp(PlayerController.Instance.transform.position.y, this.AreaBox.bounds.min.y + _halfHeight, this.AreaBox.bounds.max.y - _halfHeight),
                transform.position.z);
        }
        else if (_mode == CameraMode.TargetLocked)
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
        else if (_mode == CameraMode.VerticalBounded)
        {
            var target = new Vector3(
                _vertBoundPointMin.x,
                Mathf.Clamp(PlayerController.Instance.transform.position.y, _vertBoundPointMin.y, _vertBoundPointMax.y),
                transform.position.z);

            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                this.MoveSpeed * Time.deltaTime);
        }
    }

    public void EnteringArea(
        BoxCollider2D collider,
        CameraMode? enteringMode = null,
        Vector2? enteringBoundPointMin = null,
        Vector2? enteringBoundPointMax = null)
    {
        this.AreaBoxEntering = collider;
        _enteringMode = enteringMode ?? CameraMode.None;
        _enteringBoundPointMin = enteringBoundPointMin;
        _enteringBoundPointMax = enteringBoundPointMax;

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

        if (_enteringMode == CameraMode.VerticalBounded)
        {
            _vertBoundPointMin = _enteringBoundPointMin.Value;
            _vertBoundPointMax = _enteringBoundPointMax.Value;
        }

        _mode = _enteringMode == CameraMode.None ? this.DefaultCameraMode : _enteringMode;
        _enteringMode = CameraMode.None;
        _betweenAreas = false;
    }
}
