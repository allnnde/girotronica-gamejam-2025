using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

internal class SuperJuampPowerUp : MonoBehaviour, IPowerup
{
    public void Action()
    {
        Debug.Log("SuperJuampPowerUp Action");
    }

}


