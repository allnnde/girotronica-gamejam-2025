using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ProtectPowerUp))]
[RequireComponent(typeof(SuperJumpPowerUp))]
[RequireComponent(typeof(SuperArmPowerUp))]
public class PlayerPowerUpController : MonoBehaviour
{

    private InputAction _attackAction;
    private IPowerUp _currectPowerUp;
    void Awake()
    {
        _attackAction = InputSystem.actions.FindAction("Attack");

        DIsableAllPowerUps();

        _currectPowerUp = this.GetOrAddComponent<ProtectPowerUp>();
        (_currectPowerUp as Behaviour).enabled = true;

    }

    private void DIsableAllPowerUps()
    {
        var powersUps = GetComponents<IPowerUp>();
        foreach (Behaviour item in powersUps)
        {
            item.enabled = false;
        }
    }

    void Update()
    {
        if (_attackAction.WasPressedThisFrame())
            _currectPowerUp?.Action();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("PowerUp"))
        {

            DIsableAllPowerUps();


            switch (other.tag)
            {
                case "ProtectPowerUp":
                    _currectPowerUp = this.GetOrAddComponent<ProtectPowerUp>();
                    break;
                case "SuperJumpPowerUp":
                    _currectPowerUp = this.GetOrAddComponent<SuperJumpPowerUp>();
                    break;
                case "SuperArmPowerUp":
                    _currectPowerUp = this.GetOrAddComponent<SuperArmPowerUp>();
                    break;
                case "SuperFlyPowerUp":
                    _currectPowerUp = this.GetOrAddComponent<SuperFlyPowerUp>();
                    break;
                default:
                    break;
            }
        (_currectPowerUp as Behaviour).enabled = true;

        }
    }
}