using System;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPowerUpController : MonoBehaviour
{

    private InputAction _attackAction;
    private IPowerup _powerUp;
    void Awake()
    {
        _attackAction = InputSystem.actions.FindAction("Attack");
        _powerUp = this.GetOrAddComponent<ProtectPowerUp>();
    }

    void Update()
    {
        if (_attackAction.WasPressedThisFrame())
            _powerUp?.Action();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp")) {

            var newPowerUp = other.GetComponent<IPowerup>();

            if (newPowerUp != null)
            {
                _powerUp = newPowerUp;
            }
        } 
    } 
}