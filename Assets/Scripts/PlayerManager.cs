using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    private bool IsHead = true;
    public bool IsA, IsW, IsS, IsD;
    public static float San = 100, Health = 100;
    public static int CharacterNumber = 0;//角色参数
    public GameObject PlayerBody, PlayerHead, Player;
    public Rigidbody2D HeadRB, BodyRB;
    public float ADSpeed, WSSpeed;//横向速度，纵向速度
    public float SprintLength;//位移距离
    public float ShootTime = 5, ShootMax = 5;
    public float SprintTime=1.5f,SprintMax=1.5f;//头部弹射CD，冲刺CD
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
        ADSpeed = 0.002f;
        WSSpeed = 0.001f;
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
        if(Input.GetKeyDown(KeyCode.J))
        {
            Attack();
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
            Player.transform.position -= new Vector3(ADSpeed, 0, 0);
            IsA=true;
        }
        else
        {
            IsA=false;
        }
        if (Input.GetKey(KeyCode.W))
        {
            Player.transform.position += new Vector3(0, WSSpeed, 0);
            IsW=true;
        }
        else
        {
            IsW=false;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Player.transform.position -= new Vector3(0, WSSpeed, 0);
            IsS=true;
        }
        else
        {
            IsS=false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Player.transform.position += new Vector3(ADSpeed, 0, 0);
            IsD=true;
        }
        else
        {
            IsD = false;
        }
    }
    //K冲刺
    private void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.K)&&SprintTime>SprintMax)
        {
            //向当前方向冲刺
            SprintTime = 0;
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