using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SuperJumpPowerUp : MonoBehaviour, IPowerUp
{
    [field: SerializeField] public float JumpForce { get; set; } = 100f;

    public void Action()
    {
        var cc = GetComponent<PlayerMovimentController>();

        cc.Move(true, JumpForce, cc.GravityForce);

    }

}


