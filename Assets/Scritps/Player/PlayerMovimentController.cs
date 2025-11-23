using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerMovimentController : MonoBehaviour
{
    [field: SerializeField] public float MoveSpeed { set; get; } = 10.0F;
    [field: SerializeField] public float RotateSpeed { set; get; } = 10.0F;
    [field: SerializeField] public float GravityForce { set; get; } = -60.0f;
    [field: SerializeField] public float JumpForce { set; get; } = 20f;
    [field: SerializeField] public MoveModeEnum MoveMode { get; set; } = MoveModeEnum.Walk;
    private bool _isSuperJumping;
    private bool _isSuperJumpingFinish;

    private float _playerVelocity;

    private CharacterController _characterController;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private Animator _anim;

    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
        _moveAction = InputSystem.actions.FindAction("Move");
        _jumpAction = InputSystem.actions.FindAction("Jump");
    }

    void Update()
    {

        _anim.SetBool("IsGrounded", _characterController.isGrounded);
        if (MoveMode == MoveModeEnum.Walk || _isSuperJumping)
            Move(_jumpAction.triggered, JumpForce, GravityForce);
    }

    public void Move(bool jump, float jumpForce, float gravityForce)
    {

        Vector2 input = _moveAction.ReadValue<Vector2>();
        // Rotación horizontal (izquierda/derecha)
        float turn = input.x;
        if (Mathf.Abs(turn) > 0.01f)
        {
            transform.Rotate(Vector3.up, turn * RotateSpeed * Time.deltaTime);
        }

        // Movimiento hacia adelante
        float forward = input.y;
        bool isWalking = forward != 0;

        // Solo avanza si Y > 0
        Vector3 move = Vector3.zero;
        if (forward != 0)
        {
            move = transform.forward * forward * MoveSpeed;
        }

        _anim.SetBool("IsWalking", isWalking);
        if (_isSuperJumping)
            _anim.SetBool("IsSuperJumping", _isSuperJumping);
        else
            _anim.SetBool("IsJumping", !_characterController.isGrounded);

        // --- SALTO ---
        if (_characterController.isGrounded)
        {
            if (_isSuperJumping && _isSuperJumpingFinish)
            {
                _isSuperJumpingFinish = false;
                _isSuperJumping = false;
                _anim.SetBool("IsSuperJumping", _isSuperJumping);
            }
            // Reset de la velocidad vertical
            if (_playerVelocity < 0)
                _playerVelocity = -2f;

            // Input de salto
            if (jump)
            {
                _playerVelocity = jumpForce;
            }
        }

        // --- GRAVEDAD ---
        _playerVelocity += gravityForce * Time.deltaTime;

        move.y = _playerVelocity;
        _characterController.Move(move * Time.deltaTime);
    }

    public void SetMoveMode(MoveModeEnum moveMode)
    {
        MoveMode = moveMode;
    }
    public void ActiveSuperJumping()
    {
        _isSuperJumping = true;
    }
    public void ActiveSuperJumpingFinish()
    {
        _isSuperJumpingFinish = true;
    }
    public bool IsGrounded()
    {
        return _characterController.isGrounded;
    }
}