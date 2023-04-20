using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal;
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
        _animatorSword.SetFloat("dirX", 0f);
        _animatorSword.SetFloat("dirY", -1f);
    }

    // Update is called once per frame
    void Update()
    {
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
        else
        {
            _spriteRenderer.transform.localScale = Vector3.one;
            _spriteRenderer.sprite = this.PlayerDirectionSprites[(rawY > 0f) ? PlayerController.PlayerUp : PlayerController.PlayerDown];
        }

        if ((rawX != 0) || (rawY != 0))
        {
            _animatorSword.SetFloat("dirX", rawX);
            _animatorSword.SetFloat("dirY", rawY);
        }

        if (Input.GetMouseButtonDown(Global.Inputs.LeftButton))
        {
            _animatorSword.SetTrigger("Attack");
        }
    }
}
