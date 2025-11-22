using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


    public class ProtectPowerUp : MonoBehaviour, IPowerUp
    {
        public void Action()
    {
        Debug.Log("ProtectPowerUp Action");
    }
}

