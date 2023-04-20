using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D Rigidbody;
    private Animator _animator;

    public float MoveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float rawX = Input.GetAxisRaw(Global.Inputs.AxisHorizontal).Normalize();
        float rawY = Input.GetAxisRaw(Global.Inputs.AxisVertical).Normalize();

        var move = new Vector2(rawX, rawY).normalized * this.MoveSpeed;

        this.Rigidbody.velocity = move;
        _animator.SetFloat(Global.Parameters.Speed, move.magnitude);
    }
}
