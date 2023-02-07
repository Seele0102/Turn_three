using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo/*角色信息*/
{
    public int playerNumber;//判断当前角色
    public bool isHead;//判断现阶段状态
    public int level;//等级
    public float experence;//经验值
    public float health,healthMax,healthDropRelief;//生命，最大生命，伤害减免
    public float san, sanMax,sanMaxDrop,sanRise;//san，最大san，最大san下降
    public float relief;//闪避
    public float defense;//防御
    public float attack,attackStrengthDrop,attackHit;//攻击力，攻击所需体力
    public float speed;//移动速度
    public float sprintLength,sprintStrengthDrop;//冲刺距离，冲刺所需体力
    public float strength,strengthMax,strengthDrop,strengthRise;//体力，最大体力，跑动所需体力，静止走动回复
}


public class PlayerManager : MonoBehaviour
{ 
    public static PlayerManager instance;
    public static bool IsHead;//是否为头部
    public static PlayerInfo playerinfo;
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
        GameStart(playerinfo);
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
        Vector2 input = (transform.right * H + transform.up * V).normalized;
        if (Input.GetKey(KeyCode.L)&&!FailToRun)
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
        if(playerinfo.playerNumber==1)
        {

        }
        else
        {

        }
    }
    private void Attack()
    {
        if(playerinfo.playerNumber == 1)
        {

        }
        else
        {

        }
    }
    /*Damage应用方法：Damage(10);
     10表示为伤害量*/
    public void Damage(float damage)
    {
        playerinfo.health -= damage;
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
            if(playerinfo.sanMaxDrop<5)
            {
                playerinfo.sanMaxDrop += 0.25f*Time.deltaTime;
            }
            playerinfo.san -= playerinfo.sanMaxDrop;
        }
        /*else if(playerinfo.p_san<100)
        {
            playerinfo.p_san += playerinfo.p_sanrise * Time.deltaTime;
            playerinfo.p_sandrop = 0;
        }
        if(playerinfo.p_endurance<=0)
        {
            FailToRun = true;
        }
        if(playerinfo.p_endurance >=100&&FailToRun)
        {
            FailToRun = false;
        }
        if(Input.GetKey(KeyCode.L)&&!FailToRun)
        {
            playerinfo.p_endurance -= playerinfo.p_enddrop* Time.deltaTime;
        }
        else
        {
            playerinfo.p_endurance += playerinfo.p_enddrop*Time.deltaTime;
        }*/
    }
    private void GameStart(PlayerInfo p)
    {
        p.isHead = false;
        p.level = 1;
        p.experence= 0;
        p.health =p.healthMax= 100;
        p.healthDropRelief = 0;
        p.san = p.sanMax = 100;
    }
}
/*
    public float san, sanMax,sanMaxDrop,sanRise;//san，最大san，最大san下降
    public float relief;//闪避
    public float defense;//防御
    public float attack,attackStrengthDrop,attackHit;//攻击力，攻击所需体力
    public float speed;//移动速度
    public float sprintLength,sprintStrengthDrop;//冲刺距离，冲刺所需体力
    public float strength,strengthMax,strengthDrop,strengthRise;//体力，最大体力，跑动所需体力，静止走动回复
*/