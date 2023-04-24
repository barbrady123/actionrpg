using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private Rigidbody2D _rigidBody;
    private Animator _animatorPlayer;
    private Animator _animatorSword;
    private SpriteRenderer _spriteRenderer;

    public float BaseMoveSpeed = 5f;

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

    public float DashSpeedModifier;
    public float DashDuration;
    public int DashStaminaCost;

    private float _dashTimer;

    public int CurrentStamina;
    public int MaxStamina;
    public float StaminaRegenRate;
    private float _staminaRegen;

    public int SpinStaminaCost;
    public float SpinCooldown;
    private float _spinCooldownTimer;

    private bool _isSpinning;

    public int Coins;

    public void SpinComplete() { _isSpinning = false; }

    public void AddCoins(int value)
    {
        this.Coins += value;
    }
    public void AdjustStamina(int amount)
    {
        this.CurrentStamina = Mathf.Clamp(this.CurrentStamina + amount, 0, this.MaxStamina);
    }

    public void Knockback(Vector2 direction)
    {
        _knockbackCounter = this.KnockbackTime;
        _knockDir = direction;
        _hitEffect = Instantiate(this.HitEffectPrefab, transform.position, Quaternion.identity);
        _isKnockedBack = true;
    }

    public float TotalMoveSpeed() => 
        this.BaseMoveSpeed * 
        ((_dashTimer > 0f) ? this.DashSpeedModifier : 1.0f);

    public void Dash()
    {
        _dashTimer = this.DashDuration;
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
        _dashTimer = 0f;
        _staminaRegen = 0f;
        _isSpinning = false;
    }

    private void UpdateStamina()
    {
        if (this.CurrentStamina == this.MaxStamina)
            return;

        _staminaRegen += this.StaminaRegenRate * Time.deltaTime;
        if (_staminaRegen >= 1f)
        {
            AdjustStamina(1);
            _staminaRegen = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStamina();

        if (_isKnockedBack)
        {
            UpdateKnockback();
            return;
        }

        if (_dashTimer > 0f)
        {            
            _dashTimer = Mathf.Max(_dashTimer - Time.deltaTime, 0f);
            _rigidBody.velocity = (new Vector2(_rigidBody.velocity.x, _rigidBody.velocity.y)).normalized * TotalMoveSpeed();
            return;
        }

        if (_spinCooldownTimer > 0f)
        {
            _spinCooldownTimer = Mathf.Max(_spinCooldownTimer - Time.deltaTime, 0f);
        }

        float rawX = Input.GetAxisRaw(Global.Inputs.AxisHorizontal).Normalize();
        float rawY = Input.GetAxisRaw(Global.Inputs.AxisVertical).Normalize();

        var move = new Vector2(rawX, rawY).normalized * TotalMoveSpeed();

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

        // TODO: Need to work on a better state machine for this kind of thing, too many "not allowed" combinations...

        if (Input.GetMouseButtonDown(Global.Inputs.LeftButton) && (!_isSpinning))
        {
            _animatorSword.SetTrigger(Global.Parameters.Triggers.Attack);
        }

        if (Input.GetMouseButtonDown(Global.Inputs.RightButton) && (_spinCooldownTimer <= 0f) && (this.CurrentStamina >= this.SpinStaminaCost))
        {
            _animatorSword.SetTrigger(Global.Parameters.Triggers.Spin);
            _spinCooldownTimer = this.SpinCooldown;
            _isSpinning = true;
            this.AdjustStamina(-this.SpinStaminaCost);
        }

        if (Input.GetKeyDown(KeyCode.Space) && (_dashTimer <= 0f) && (this.CurrentStamina >= this.DashStaminaCost))
        {            
            _dashTimer = this.DashDuration;
            this.AdjustStamina(-this.DashStaminaCost);
        }
    }

    void UpdateKnockback()
    {
        _rigidBody.velocity = _knockDir * this.KnockbackForce;
        _knockbackCounter -= Time.deltaTime;
        _dashTimer = 0f;

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
