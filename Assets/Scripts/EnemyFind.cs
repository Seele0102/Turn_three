using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFind : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Enemy"))
        {
            PlayerManager.instance.Enemy[++PlayerManager.EnemyNumber] = collision.gameObject;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Enemy"))
        {
            PlayerManager.instance.Enemy[PlayerManager.EnemyNumber--] = null;
        }
    }
}
