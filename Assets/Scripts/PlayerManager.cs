using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public GameObject PlayerBody, PlayerHead;
    public Rigidbody2D HeadRB, BodyRB;
    public float ShootTime,ShootCD;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        PlayerHead = GameObject.Find("PlayerHead");
        HeadRB = PlayerHead.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F)&&ShootTime==0)
        {
            ShootHead();
        }
        ShootTime -= Time.deltaTime;
    }
    private void ShootHead()
    {
        if (PlayerBody == null)
        {
            BodyRB=PlayerBody.GetComponent<Rigidbody2D>();
            //—∞’“…ÌÃÂ
        }
        else
        {
            PlayerBody = null;
            ShootTime = ShootCD;
            //µØ…‰
        }
    }
}