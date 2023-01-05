using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Image Left, Right;
    public static bool IsHead;//�Ƿ�Ϊͷ��
    public static float San = 100, Health = 100;//Ѫ����Sanֵ
    public static float MaxHealth = 100;//���Ѫ��
    public static float SanDropSpeed = 0.5f,SanRiseSpeed=0.1f;//Sanֵ�������
    public static int CharacterNumber = 0;//��ɫ����
    private GameObject PlayerBody, PlayerHead, Player;//��ɫObject
    public GameObject[] Enemy;//������Χ�ڵĵ���
    public GameObject[] PlayerBodys;//��ȡ��Χ�ڵ�����
    private Rigidbody2D HeadRB, BodyRB;
    public static float Speed;//�ٶ�
    public static float SprintLength=1;//λ�ƾ���
    public float ShootTime = 5, ShootMax = 5;
    public float SprintTime=1,SprintMax=1;//ͷ������CD�����CD
    public float H, V;
    private Vector3 move;
    private Vector3 Derection;//ָ����
    private Vector3 test;
    private float angle;
    public static int EnemyNumber,BodyNum;
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
            //Ѱ������
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
    //WASD�ƶ�
    //Done
    private void Move()
    {
        H = Input.GetAxisRaw("Horizontal");
        V = Input.GetAxisRaw("Vertical");
        if (H != 0 || V != 0)
        {
            move.x = H*Speed;
            move.y = V*Speed;
            Player.transform.position += move;
            test.x = H;
            test.y = V;
            if(V>=0)
            {
                angle= Vector3.Angle(new Vector3(1,0,0),test);
            }
            else
            {
                angle = 360f-Vector3.Angle(new Vector3(1, 0, 0), test);
            }
            angle = 2 * angle * math.PI / 360f;
            if(H>0)
            {
                
            }
            else if (H<0)
            {
                
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
    /*DamageӦ�÷�����Damage(10);
     10��ʾΪ�˺���*/
    public void Damage(float damage)
    {
        Health -= damage;
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
            San -= SanDropSpeed * Time.deltaTime;
        }
        else if(San<=100)
        {
            San += SanRiseSpeed * Time.deltaTime;
        }
    }
}