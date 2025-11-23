using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class ProtectPowerUp : MonoBehaviour, IPowerUp
{

    private PlayerMovimentController cc;
    private Image image;

    [field: SerializeField] public float Duration { get; set; } = 4F;

    private float _timeCounter { get; set; } = 2F;
    private Animator _anim;

    public void Action()
    {
        cc.SetMoveMode(MoveModeEnum.Stop);
    }

    private void Awake()
    {
        cc = GetComponent<PlayerMovimentController>();
        _anim = GetComponent<Animator>();

        var ui = GameObject.Find("UI");
        image = ui.transform.Find("ProtectedPanel").GetComponent<Image>();
    }

    private void Update()
    {
        if (cc.MoveMode == MoveModeEnum.Stop)
        { 
            _anim.SetBool("IsHidden",true);
            
            if (_timeCounter > Duration)
            {
                _anim.SetBool("IsHidden", false);
                cc.SetMoveMode(MoveModeEnum.Walk);
                _timeCounter = 0;
                return;
            }
            var alpha = CalculateAlpha(_timeCounter);

            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);

            _timeCounter += Time.deltaTime;
        }
    }

    public float CalculateAlpha(float t)
    {
        float rise = Duration * 0.25f;   // 25% subir
        float hold = Duration * 0.50f;   // 50% mantener negro
        float fall = Duration * 0.25f;   // 25% bajar

        if (t < rise)
            return Mathf.Lerp(0, 1, t / rise);         // subida

        if (t < rise + hold)
            return 1f;                                 // mantener negro

        return Mathf.Lerp(1, 0, (t - (rise + hold)) / fall); // bajada
    }
}

