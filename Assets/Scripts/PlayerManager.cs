using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    private bool IsHead = true;
    public static float San = 100, Health = 100;
    public float Speed;
    public static int CharacterNumber = 0;//��ɫ����
    public GameObject PlayerBody, PlayerHead;
    public Rigidbody2D HeadRB, BodyRB;
    public float ShootTime, ShootCD;//ͷ������CD
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
            //Ѱ������
            if (PlayerBody != null)
            {
                IsHead = false;
            }
        }
        else
        {
            PlayerBody = null;
            ShootTime = ShootCD;
            //����
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
    /*DamageӦ�÷�����Damage(10);
     10��ʾΪ�˺���*/
    public void Damage(float damage)
    {
        Health -= damage;
    }
}