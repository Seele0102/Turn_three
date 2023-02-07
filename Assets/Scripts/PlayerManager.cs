using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo/*��ɫ��Ϣ*/
{
    public int playerNumber;//�жϵ�ǰ��ɫ
    public bool isHead, isTired, isTiring;//�ж��ֽ׶�״̬
    public int level;//�ȼ�
    public float experence;//����ֵ
    public float health, healthMax, healthDropRelief;//����������������˺�����
    public float san, sanMax, sanMaxDrop, sanRise, sanMaxDropTime;//san�����san�����san�½�
    public float relief;//����
    public float defense;//����
    public float attack, attackStrengthDrop, attackHit;//��������������������,������������
    public float speed;//�ƶ��ٶ�
    public float sprintLength, sprintStrengthDrop, sprint, sprintMax;//��̾��룬�����������
    public float strength, strengthMax, strengthDrop, strengthRise;//����������������ܶ�������������ֹ�߶��ظ�
    public float shoot, shootMax;
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
    public float sanDropen;
    public float ShootTime = 5, ShootMax = 5;
    public float SprintTime = 1, SprintMax = 1;//ͷ������CD�����CD
    public float H, V;
    private Vector3 move;
    private Vector3 Derection;//ָ����
    private Vector3 Angle;
    private float angle;
    public static int EnemyNumber, BodyNum;
    private Quaternion Turn;//Playerƫת
    //����
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
    //��ʼ����
    void Start()
    {
        GameStart(playerinfo);
        PlayerHead = GameObject.FindGameObjectWithTag("PlayerHead");
        HeadRB = PlayerHead.GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerHead.transform.parent = Player.transform;
        PlayerHead.transform.localPosition = Vector3.zero;
        PlayerHead.transform.localRotation = Quaternion.identity;
        EnemyNumber = 0;
        BodyNum = 0;
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
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.I))
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
        if (Input.GetKey(KeyCode.L))
        {
            playerinfo.speed = 0.004f;
            playerinfo.sprintLength = 5;
        }
        else
        {
            playerinfo.speed = 0.002f;
            playerinfo.sprintLength = 2.5f;
        }
        if (H != 0 || V != 0)
        {
            move.x = H * playerinfo.speed;
            move.y = V * playerinfo.speed;
            Player.transform.position += move;
            Angle.x = H;
            Angle.y = V;
            if (V >= 0)
            {
                angle = Vector3.Angle(new Vector3(1, 0, 0), Angle);
            }
            else
            {
                angle = 360f - Vector3.Angle(new Vector3(1, 0, 0), Angle);
            }
            angle = 2 * angle * math.PI / 360f;
            if (H > 0)
            {
                Turn.SetFromToRotation(new Vector3(1, 0, 0), new Vector3(-1, 0, 0));
                Player.transform.rotation = Turn;
            }
            else if (H < 0)
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
        if (Input.GetKeyDown(KeyCode.K) && SprintTime > SprintMax)
        {
            Derection.x = math.cos(angle) * playerinfo.sprintLength;
            Derection.y = math.sin(angle) * playerinfo.sprintLength;
            Player.transform.position += Derection;
            SprintTime = 0;
        }
    }
    private void OriginalSkill()
    {
        if (playerinfo.playerNumber == 1)
        {

        }
        else
        {

        }
    }
    private void Attack()
    {
        if (playerinfo.playerNumber == 1)
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
       //random
       if(true)
        {
            damage = 0;
        }
       if(damage>playerinfo.defense&&damage-playerinfo.defense>=damage/20)
        {
            damage -= playerinfo.defense;
        }
       else
        {
            damage /=20f;
        }
        damage =damage* (100f-playerinfo.relief) / 100f;
        playerinfo.health -= damage;
    }
    //׼���׶θ������Եı仯
    private void Prepare()
    {
        if (playerinfo.isHead)
        {
            playerinfo.healthMax = playerinfo.health = 1;
            if (sanDropen < playerinfo.sanMaxDrop && playerinfo.san >= 0)
            {
                sanDropen += (playerinfo.sanMax / playerinfo.sanMaxDropTime) * Time.deltaTime;
            }
            playerinfo.san -= sanDropen * Time.deltaTime;
            playerinfo.speed = 5;
        }
        else
        {
            sanDropen = 0;
            if (playerinfo.san < playerinfo.sanMax)
            {
                playerinfo.san += playerinfo.sanRise * Time.deltaTime;
            }
            playerinfo.speed = 10;
        }
        if (playerinfo.shoot < playerinfo.shootMax)
        {
            ShootMax += Time.deltaTime;
        }
        if (playerinfo.strength < playerinfo.strengthMax)
        {
            if (playerinfo.isTiring)
            {
                playerinfo.strength += 2 * playerinfo.strengthRise * Time.deltaTime;
            }
            else if (!playerinfo.isTired)
            {
                playerinfo.strength += playerinfo.strengthRise * Time.deltaTime;
            }
        }
    }
    //��Ϸ��ʼ������
    private void GameStart(PlayerInfo p)
    {
        p.isHead = p.isTired = p.isTiring = false;
        p.level = 1;
        p.experence = 0;
        p.health = p.healthMax = 100;
        p.healthDropRelief = 0;
        p.san = p.sanMax = 100;
        p.sanMaxDrop = 10;
        p.sanRise = 2;
        p.sanMaxDropTime = 10;
        p.relief = 0;
        p.defense = 10;
        p.attack = 10;
        p.attackStrengthDrop = 2;
        if (p.playerNumber == 1)
        {
            p.attackHit = 5;
        }
        else if (p.playerNumber == 2)
        {
            p.attackHit = 1;
        }
        p.speed = 10;
        p.sprintLength = 5;
        p.sprintLength = 10;
        p.strength = 200;
        p.strengthMax = 200;
        p.strengthDrop = 5;
        p.strengthRise = 10;
        p.sprint = 1;
        p.sprintMax = 1;
        p.shoot = 5;
        p.shootMax = 5;
    }
}