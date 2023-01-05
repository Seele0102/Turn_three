using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FindHead : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("PlayerBody"))
        {
            PlayerManager.instance.PlayerBodys[++PlayerManager.BodyNum]= collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("PlayerBody"))
        {
            PlayerManager.instance.PlayerBodys[PlayerManager.BodyNum--]= collision.gameObject;
        }
    }
}
