using System;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ProtectPowerUp))]
[RequireComponent(typeof(SuperJumpPowerUp))]
[RequireComponent(typeof(SuperArmPowerUp))]
[RequireComponent(typeof(SuperFlyPowerUp))]
public class PlayerPowerUpController : MonoBehaviour
{

    private InputAction _attackAction;
    private IPowerUp _currectPowerUp;
    private Animator _anim;
    private GameObject _currentPuppet;

    void Awake()
    {
        _attackAction = InputSystem.actions.FindAction("Attack");

        DisableAllPowerUps();
        _anim = GetComponent<Animator>();

        _currectPowerUp = this.GetOrAddComponent<ProtectPowerUp>();
        (_currectPowerUp as Behaviour).enabled = true;

    }

    private void DisableAllPowerUps()
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
    public void SetCurrectPowerUp<T>(GameObject puppet) where T : UnityEngine.Component
    {
        SetCurrectPowerUp<T>();
        _currentPuppet = puppet;
        _anim.SetTrigger("IsStich");




    }

    private void SetCurrectPowerUp<T>() where T : Component
    {
        DisableAllPowerUps();
        _currectPowerUp = this.GetOrAddComponent<T>() as IPowerUp;
        Debug.Log(_currectPowerUp);
        (_currectPowerUp as Behaviour).enabled = true;
    }

    public void FinishSetCurrectPowerUp() {
        if (_currentPuppet) {

            var sp = this.transform.parent.GetComponentInChildren<ParticleSystem>();
            sp.transform.position = this.transform.position;
            sp.Play();


            switch (_currentPuppet.tag)
            {
                case "ProtectPowerUp":
                    ChangeModel<ProtectPowerUp>("Player_Bunny");
                    break;
                case "SuperJumpPowerUp":
                    ChangeModel<SuperJumpPowerUp>("BunnyFrog_BASE");
                    ChangePuppetModel("Frog");
                    break;
                case "SuperArmPowerUp":
                    ChangeModel<SuperArmPowerUp>("BunnyBear_BASE");
                    ChangePuppetModel("Bear");
                    break;
                case "SuperFlyPowerUp":
                    ChangeModel<SuperFlyPowerUp>("BunnyPenguin_BASE");
                    ChangePuppetModel("Penguin");
                    break;
                default:
                    break;
            }


            _currentPuppet = null;
        }
    
    }

    private void ChangePuppetModel(string name)
    {
        var idle = _currentPuppet.transform.parent.Find(name +"_Idle");
        var corpse = _currentPuppet.transform.parent.Find(name + "_Corpse");
        corpse.gameObject.SetActive(true);
        idle.gameObject.SetActive(false);
        _currentPuppet.SetActive(false);
    }

    private void ChangeModel<T>(string name) where T : UnityEngine.Component
    {
        var model = transform.parent.Find(name).gameObject;
        model.transform.position = transform.position;
        model.transform.rotation = transform.rotation;
        model.SetActive(true);
        model.GetComponent<PlayerPowerUpController>().SetCurrectPowerUp<T>();
        var camera = GameObject.Find("CinemachineCamera").GetComponent<CinemachineCamera>();
        camera.Follow = model.transform;

        this.gameObject.SetActive(false);
    }
}