using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    private bool IsHead = true;
    public static float San = 100, Health = 100;
    public float Speed;
    public static int CharacterNumber = 0;//角色参数
    public GameObject PlayerBody, PlayerHead;
    public Rigidbody2D HeadRB, BodyRB;
    public float ShootTime, ShootCD;//头部弹射CD
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
        PlayerHead = GameObject.FindGameObjectWithTag("PlayerHead");
        HeadRB = PlayerHead.GetComponent<Rigidbody2D>();
        Speed = 1;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        if(ShootTime>=0)
        {
            ShootTime -= Time.deltaTime;
        }
        if (IsHead)
        {
            San -= 0.5f*Time.deltaTime;
        }
        else
        {
            San += 0.1f*Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            HeadShot();
        }
    }
    private void HeadShot()
    {
        if (PlayerBody == null)
        {
            BodyRB = PlayerBody.GetComponent<Rigidbody2D>();
            //寻找身体
            if (PlayerBody != null)
            {
                IsHead = false;
            }
        }
        else
        {
            PlayerBody = null;
            ShootTime = ShootCD;
            //弹射
            IsHead = true;
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
    /*Damage应用方法：Damage(10);
     10表示为伤害量*/
    public void Damage(float damage)
    {
        Health -= damage;
    }
}