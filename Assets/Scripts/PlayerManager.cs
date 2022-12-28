using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    private bool IsHead = true;
    public static float San = 100, Health = 100;
    public static int CharacterNumber = 0;//角色参数
    public GameObject PlayerBody, PlayerHead, Player;
    public Rigidbody2D HeadRB, BodyRB;
    public float ShootTime=5, ShootMax=5,SprintTime=2,SprintMax=1.5f,Speed=0.002f;//头部弹射CD，冲刺CD，角色速度
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
        Player = PlayerHead;
    }
    // Update is called once per frame
    void Update()
    {
        Prepare();
        Move();
        Sprint();
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
            ShootTime = ShootMax;
            //弹射
            IsHead = true;
        }
    }
    //WASD移动
    private void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Player.transform.position -= new Vector3(Speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            Player.transform.position += new Vector3(0, Speed, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Player.transform.position -= new Vector3(0, Speed, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Player.transform.position += new Vector3(Speed, 0, 0);
        }
        Speed = 0.002f;
    }
    //K冲刺
    private void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (SprintTime >= SprintMax)
            {
                SprintTime = 0;
                Speed = 2;
            }
        }
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
    //准备阶段各项属性的变化
    private void Prepare()
    {
        if (SprintTime <= SprintMax)
        {
            SprintTime += Time.deltaTime;
        }
        if (ShootTime <= ShootMax)
        {
            ShootTime += Time.deltaTime;
        }
        if (IsHead)
        {
            San -= 0.5f * Time.deltaTime;
        }
        else if(San<=100)
        {
            San += 0.1f * Time.deltaTime;
        }
    }
}