using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanBar : MonoBehaviour
{
    public Image sanImage;
    public Image sanEffectImage;
    public float san;
    [SerializeField] private float maxSan;
    [SerializeField] private float hurtSpeed = 0.005f;
    private void Start()
    {
        san = maxSan;
    }
    private void Update()
    {
        sanImage.fillAmount = san / maxSan;
        if (sanEffectImage.fillAmount > sanImage.fillAmount)
        {
            sanEffectImage.fillAmount -= hurtSpeed;
        }
        else
        {
            sanEffectImage.fillAmount = sanImage.fillAmount;
        }
    }
}
