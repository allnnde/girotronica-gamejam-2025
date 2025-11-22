using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SuperFlyPowerUp : MonoBehaviour, IPowerUp
{
    private PlayerMovimentController cc;

    [field: SerializeField] public float GrabityForce { get; set; } = 100f;
    private void Awake()
    {
        cc = GetComponent<PlayerMovimentController>();

    }

    public void Action()
    {
        cc.SetMoveMode(MoveModeEnum.Fly);
    }

    private void Update()
    {
        if (cc.MoveMode == MoveModeEnum.Fly)
            cc.Move(false, cc.JumpForce, -5);

        if(cc.IsGrounded())
            cc.SetMoveMode(MoveModeEnum.Walk);
    }

}


