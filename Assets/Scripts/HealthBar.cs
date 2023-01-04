using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image hpImage;
    public Image hpEffectImage;

    [HideInInspector]
    public float hp;
    //脚本挂在player下的canvas，当玩家受击时，other.GetComponentInChildren<HealthBar>().hp-=伤害
    
    [SerializeField] private float maxHp;
    [SerializeField] private float hurtSpeed = 0.005f;

    private void Start()
    {
        hp = maxHp;
    }

    private void Update()
    {
        hpImage.fillAmount = hp / maxHp;
        if (hpEffectImage.fillAmount > hpImage.fillAmount)
        {
            hpEffectImage.fillAmount -= hurtSpeed;
        }
        else
        {
            hpEffectImage.fillAmount = hpImage.fillAmount;
        }

    }
}
