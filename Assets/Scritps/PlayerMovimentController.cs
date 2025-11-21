using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovimentController : MonoBehaviour
{
    [field: SerializeField] private float MoveSpeed { set; get; } = 10.0F;
    [field: SerializeField] public float RotateSpeed { set; get; } = 10.0F;
    [field: SerializeField] private float GravityForce { set; get; } = -20.0f;
    [field: SerializeField]  public float JumpForce { set; get; } = 20f;
    
    private float _playerVelocity;

    private CharacterController _characterController;
    private InputAction _moveAction;
    private InputAction _jumpAction;

    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _moveAction = InputSystem.actions.FindAction("Move");
        _jumpAction = InputSystem.actions.FindAction("Jump");
    }

    void Update()
    {
        Move(_jumpAction.triggered, JumpForce);
    }

    public void Move(bool jump, float jumpForce)
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

        // Solo avanza si Y > 0
        Vector3 move = Vector3.zero;
        if (forward != 0)
        {
            move = transform.forward * forward * MoveSpeed;
        }

        // --- SALTO ---
        if (_characterController.isGrounded)
        {
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
        _playerVelocity += GravityForce * Time.deltaTime;

        move.y = _playerVelocity;
        _characterController.Move(move * Time.deltaTime);
    }
}