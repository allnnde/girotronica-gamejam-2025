using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PuppertIteractorController : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Contains("Player") && Keyboard.current.eKey.wasPressedThisFrame)
        {

            var ppm = other.GetComponent<PlayerPowerUpController>();
            switch (tag)
            {
                case "ProtectPowerUp":
                    ppm.SetCurrectPowerUp<ProtectPowerUp>(this.gameObject);
                    break;
                case "SuperJumpPowerUp":
                    ppm.SetCurrectPowerUp<SuperJumpPowerUp>(this.gameObject);
                    break;
                case "SuperArmPowerUp":
                    ppm.SetCurrectPowerUp<SuperArmPowerUp>(this.gameObject);
                    break;
                case "SuperFlyPowerUp":
                    ppm.SetCurrectPowerUp<SuperFlyPowerUp>(this.gameObject);
                    break;
                default:
                    break;
            }
        }
    }
}
