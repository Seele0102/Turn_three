using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    private bool IsGetPlayer = false;
    public static int CharacterNumber = 0;
    public GameObject PlayerBody, PlayerHead;
    public Rigidbody2D HeadRB, BodyRB;
    public float ShootTime, ShootCD;
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

    }
    // Update is called once per frame
    void Update()
    {
        if (CharacterNumber != 0)
        {
            if (!IsGetPlayer)
            {
                PlayerHead = GameObject.FindGameObjectWithTag("PlayerHead");
                HeadRB = PlayerHead.GetComponent<Rigidbody2D>();
                IsGetPlayer = true;
            }
            if (Input.GetKey(KeyCode.F) && ShootTime == 0)
            {
                ShootHead();
            }
            ShootTime -= Time.deltaTime;
            Move();
            if(Input.GetKey(KeyCode.U))
            {
                OriginalSkill();
            }
            if(Input.GetKey(KeyCode.J))
            {
                Attack();
            }
        }
    }
    private void ShootHead()
    {
        if (PlayerBody == null)
        {
            BodyRB = PlayerBody.GetComponent<Rigidbody2D>();
            //—∞’“…ÌÃÂ
        }
        else
        {
            PlayerBody = null;
            ShootTime = ShootCD;
            //µØ…‰
        }
    }
    private void Move()
    {

    }
    private void OriginalSkill()
    {
        if (CharacterNumber == 1)
        {

        }
        else if (CharacterNumber == 2)
        {

        }
        else
        {

        }
    }
    private void Attack()
    {

    }
}