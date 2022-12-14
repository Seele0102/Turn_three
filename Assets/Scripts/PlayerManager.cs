using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{ 
    public static PlayerManager instance;
    public static bool IsHead;//是否为头部
    public static float San = 100, Health = 100,Endurance=100;//血量与San值
    public static float MaxHealth = 100,MaxEnd=100;//最大血量
    public static float SanDropSpeed = 0.5f,SanRiseSpeed=0.1f;//San值相关数据
    public static float EndDropSpeed=20, EndRiseSpeed=2.5f;//体力槽相关数值
    public static int CharacterNumber = 0;//角色参数
    public static float p_Attack, p_Defense;
    private GameObject PlayerBody, PlayerHead, Player;//角色Object
    public GameObject[] Enemy;//攻击范围内的敌人
    public GameObject[] PlayerBodys;//索取范围内的身体
    private Rigidbody2D HeadRB, BodyRB;
    public static float Speed;//速度
    public static float SprintLength=2.5f;//位移距离
    public float ShootTime = 5, ShootMax = 5;
    public float SprintTime=1,SprintMax=1;//头部弹射CD，冲刺CD
    public float H, V;
    private Vector3 move;
    private Vector3 Derection;//指向方向
    private Vector3 Angle;
    private float angle;
    public static int EnemyNumber,BodyNum;
    private Quaternion Turn;//Player偏转
    private bool FailToRun;
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
        Speed = 0.002f;
        PlayerHead = GameObject.FindGameObjectWithTag("PlayerHead");
        HeadRB = PlayerHead.GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerHead.transform.parent = Player.transform;
        PlayerHead.transform.localPosition= Vector3.zero; 
        PlayerHead.transform.localRotation = Quaternion.identity;
        EnemyNumber = 0;
        BodyNum = 0;
        FailToRun= false;
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
        if(Input.GetKeyDown(KeyCode.I)) 
        {
            OriginalSkill();
        }
    }
    private void HeadShot()
    {
        if (PlayerBody == null)
        {
            
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
    //WASD移动,L冲刺，外加Player偏转
    private void Move()
    {
        H = Input.GetAxisRaw("Horizontal");
        V = Input.GetAxisRaw("Vertical");
        if(Input.GetKey(KeyCode.L)&&!FailToRun)
        {
            Speed = 0.004f;
            SprintLength = 5;
        }
        else
        {
            Speed = 0.002f;
            SprintLength = 2.5f;
        }
        if (H != 0 || V != 0)
        {
            move.x = H*Speed;
            move.y = V*Speed;
            Player.transform.position += move;
            Angle.x = H;
            Angle.y = V;
            if(V>=0)
            {
                angle= Vector3.Angle(new Vector3(1,0,0),Angle);
            }
            else
            {
                angle = 360f-Vector3.Angle(new Vector3(1, 0, 0), Angle);
            }
            angle = 2 * angle * math.PI / 360f;
            if (H>0)
            {
                Turn.SetFromToRotation(new Vector3(1, 0, 0), new Vector3(-1, 0, 0));
                Player.transform.rotation = Turn;
            }
            else if (H<0)
            {
                Turn.SetFromToRotation(new Vector3(1, 0, 0), new Vector3(1, 0, 0));
                Player.transform.rotation = Turn;
            }
        }
    }
    //K冲刺
    //Done
    private void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.K)&&SprintTime>SprintMax)
        {
            Derection.x = math.cos(angle) * SprintLength;
            Derection.y = math.sin(angle) * SprintLength;
            Player.transform.position += Derection;
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
        switch (CharacterNumber)
        {
            case 1:

            case 2:
            case 3:
            default:break;
        }
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
            San -= SanDropSpeed * Time.deltaTime;
        }
        else if(San<=100)
        {
            San += SanRiseSpeed * Time.deltaTime;
        }
        if(Endurance<=0)
        {
            FailToRun = true;
        }
        if(Endurance>=MaxEnd&&FailToRun)
        {
            FailToRun = false;
        }
        if(Input.GetKey(KeyCode.L)&&!FailToRun)
        {
            Endurance-= EndDropSpeed* Time.deltaTime;
        }
        else
        {
            Endurance += EndRiseSpeed*Time.deltaTime;
        }
    }
}