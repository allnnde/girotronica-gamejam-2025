using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class ProtectPowerUp : MonoBehaviour, IPowerUp
{

    private PlayerMovimentController cc;
    private Image iamge;

    [field: SerializeField] public float Dutation { get; set; } = 4F;

    private float _timeCounter { get; set; } = 2F;

    public void Action()
    {
        cc.SetMoveMode(MoveModeEnum.Stop);
    }

    private void Awake()
    {
        cc = GetComponent<PlayerMovimentController>();

        var ui = GameObject.Find("UI");
        iamge = ui.transform.Find("ProtectedPanel").GetComponent<Image>();
    }

    private void Update()
    {
        if (cc.MoveMode == MoveModeEnum.Stop)
        {
            if (_timeCounter > Dutation)
            {
                cc.SetMoveMode(MoveModeEnum.Walk);
                _timeCounter = 0;
                return;
            }
            var alpha = CalculateAlpha(_timeCounter);

            iamge.color = new Color(iamge.color.r, iamge.color.g, iamge.color.b, alpha);

            _timeCounter += Time.deltaTime;
        }
    }

    public float CalculateAlpha(float x) {

        return -Mathf.Pow(x, 2) + Dutation * x;
    
    }
}

