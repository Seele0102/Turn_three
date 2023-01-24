using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class PlayerInfo
{
    public float p_attack,p_defense,p_maxhealth,p_maxsan;//����������������������������san
    public float p_health, p_endurance, p_san;//��ǰ��������ǰ��������ǰsan
    public float p_sandrop, p_sanrise, p_enddrop, p_endrise;//san�������½��������������½�
    public float p_orispeed, p_runspeed, p_sprintL;
    public bool Calthrop;//��ɫ���ԣ���Ϊ���̣���Ϊ��¶
}

public class PlayerManager : MonoBehaviour
{ 
    public static PlayerManager instance;
    public static bool IsHead;//�Ƿ�Ϊͷ��
    public static PlayerInfo playerinfo;
    private GameObject PlayerBody, PlayerHead, Player;//��ɫObject
    public GameObject[] Enemy;//������Χ�ڵĵ���
    public GameObject[] PlayerBodys;//��ȡ��Χ�ڵ�����
    private Rigidbody2D HeadRB, BodyRB;
    public static float Speed;//�ٶ�
    public static float SprintLength=2.5f;//λ�ƾ���
    public float ShootTime = 5, ShootMax = 5;
    public float SprintTime=1,SprintMax=1;//ͷ������CD�����CD
    public float H, V;
    private Vector3 move;
    private Vector3 Derection;//ָ����
    private Vector3 Angle;
    private float angle;
    public static int EnemyNumber,BodyNum;
    private Quaternion Turn;//Playerƫת
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
        //GameStart(playerinfo);
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
            //����
            IsHead = true;
        }
    }
    //WASD�ƶ�,L��̣����Playerƫת
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
    //K���
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
        if(playerinfo.Calthrop)
        {

        }
        else
        {

        }
    }
    private void Attack()
    {
        if(playerinfo.Calthrop)
        {

        }
        else
        {

        }
    }
    /*DamageӦ�÷�����Damage(10);
     10��ʾΪ�˺���*/
    public void Damage(float damage)
    {
        playerinfo.p_health -= damage;
    }
    //׼���׶θ������Եı仯
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
            if(playerinfo.p_sandrop<5)
            {
                playerinfo.p_sandrop += 0.25f*Time.deltaTime;
            }
            playerinfo.p_san -= playerinfo.p_sandrop;
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
        p.p_health = 100;
    }
}