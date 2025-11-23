using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SuperJumpPowerUp : MonoBehaviour, IPowerUp
{
    private PlayerMovimentController cc;

    [field: SerializeField] public float JumpForce { get; set; } = 100f;

    private void Awake()
    {

        cc = GetComponent<PlayerMovimentController>();
    }

    public void Action()
    {
        cc.ActiveSuperJumping();

    }

    internal void SuperJump()
    {
        cc.Move(true, JumpForce, cc.GravityForce);
    }
}


