using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private Rigidbody2D _rigidBody;
    private Animator _animatorPlayer;
    private Animator _animatorSword;
    private SpriteRenderer _spriteRenderer;

    public float MoveSpeed = 5f;

    public Sprite[] PlayerDirectionSprites;

    private const int PlayerDown = 0;
    private const int PlayerSide = 1;
    private const int PlayerUp = 2;

    private bool _isKnockedBack;
    private float _knockbackCounter;
    private Vector2 _knockDir;

    public float KnockbackTime;
    public float KnockbackForce;

    public GameObject HitEffectPrefab;
    private GameObject _hitEffect;

    public bool MoveEffectsWithPlayer;

    public void Knockback(Vector2 direction)
    {
        _knockbackCounter = this.KnockbackTime;
        _knockDir = direction;
        _hitEffect = Instantiate(this.HitEffectPrefab, transform.position, Quaternion.identity);
        _isKnockedBack = true;
    }

    void Awake()
    {
        if ((Instance != null) && (Instance != this))
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animatorPlayer = GetComponent<Animator>();
        _animatorSword = transform.Find("SwordHolder").GetComponent<Animator>();
        _spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        _animatorSword.SetFloat(Global.Parameters.DirX, 0f);
        _animatorSword.SetFloat(Global.Parameters.DirY, -1f);
        _hitEffect = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isKnockedBack)
        {
            UpdateKnockback();
            return;
        }

        float rawX = Input.GetAxisRaw(Global.Inputs.AxisHorizontal).Normalize();
        float rawY = Input.GetAxisRaw(Global.Inputs.AxisVertical).Normalize();

        var move = new Vector2(rawX, rawY).normalized * this.MoveSpeed;

        _rigidBody.velocity = move;
        _animatorPlayer.SetFloat(Global.Parameters.Speed, move.magnitude);
        
        if (rawX != 0f)
        {
            _spriteRenderer.sprite = this.PlayerDirectionSprites[PlayerController.PlayerSide];
            _spriteRenderer.transform.localScale = 
                new Vector3(
                    (rawX < 0f) ? -1f : 1f,
                    _spriteRenderer.transform.localScale.y,
                    _spriteRenderer.transform.localScale.z);
        }
        else if (rawY != 0f)
        {
            _spriteRenderer.transform.localScale = Vector3.one;
            _spriteRenderer.sprite = this.PlayerDirectionSprites[(rawY > 0f) ? PlayerController.PlayerUp : PlayerController.PlayerDown];
        }

        if ((rawX != 0) || (rawY != 0))
        {
            _animatorSword.SetFloat(Global.Parameters.DirX, rawX);
            _animatorSword.SetFloat(Global.Parameters.DirY, rawY);
        }

        if (Input.GetMouseButtonDown(Global.Inputs.LeftButton)) 
        {
            _animatorSword.SetTrigger(Global.Parameters.Triggers.Attack);
        }
    }

    void UpdateKnockback()
    {
        _rigidBody.velocity = _knockDir * this.KnockbackForce;
        _knockbackCounter -= Time.deltaTime;
        if (this.MoveEffectsWithPlayer && (_hitEffect != null))
        {
            _hitEffect.transform.position = transform.position;
        }

        if (_knockbackCounter <= 0f)
        {
            _isKnockedBack = false;
            _knockDir = Vector2.zero;
            return;
        }

    }
}
