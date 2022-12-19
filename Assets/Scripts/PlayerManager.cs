using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public GameObject PlayerBody, PlayerHead, PlayerAll;
    private float NowSpeed=0;
    public float MaxSpeed,DeltaSpeed,d_DeltaSpeed;
    // Start is called before the first frame update
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
        PlayerBody = GameObject.Find("PlayerBody");
        PlayerHead = GameObject.Find("PlayerHead");
        PlayerAll = GameObject.Find("PlayerAll");
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void Move()
    {
        if(NowSpeed<=MaxSpeed)
        {
            NowSpeed += DeltaSpeed;
        }
    }
    private void ShootUp()
    {

    }
}