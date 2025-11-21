using System;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ProtectPowerUp))]
[RequireComponent(typeof(SuperJumpPowerUp))]
public class PlayerPowerUpController : MonoBehaviour
{

    private InputAction _attackAction;
    private ProtectPowerUp _protectPowerUp;
    private SuperJumpPowerUp _superJumpPowerUp;
    private IPowerUp _currectPowerUp;
    void Awake()
    {
        _attackAction = InputSystem.actions.FindAction("Attack");
        _protectPowerUp = this.GetOrAddComponent<ProtectPowerUp>();
        _superJumpPowerUp = this.GetOrAddComponent<SuperJumpPowerUp>();
        _currectPowerUp = _protectPowerUp;
    }

    void Update()
    {
        if (_attackAction.WasPressedThisFrame())
            _currectPowerUp?.Action();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp")) {
           
            var newPowerUp = other.GetComponent<IPowerUp>();

            switch (newPowerUp)
            {
                case ProtectPowerUp:
                    _protectPowerUp.enabled = true;
                    _superJumpPowerUp.enabled = false;
                    _currectPowerUp = _protectPowerUp;
                    break;
                case SuperJumpPowerUp:
                    _protectPowerUp.enabled = false ;
                    _superJumpPowerUp.enabled = true;
                    _currectPowerUp = _superJumpPowerUp;
                    break;
                default:
                    break;
            }

        } 
    } 
}