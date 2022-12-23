using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image Bar;
    public float health, maxHealth = 100;
    private float lerpSpeed = 3;
    private void Update()
    {
        BarFiller();
    }
    private void BarFiller()
    {
        Bar.fillAmount = Mathf.Lerp(Bar.fillAmount, health/maxHealth, lerpSpeed*Time.deltaTime);
    }
    private void AddHealth()
    {

    }
    private void ReduceHealth()
    {

    }
}
