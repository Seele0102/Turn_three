using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCamera : MonoBehaviour
{
    public float rotateTime = 0.2f;
    private Transform player;
    private Transform playerHead;
    private Transform playerBody;
    private bool isRotating = false;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHead = GameObject.FindGameObjectWithTag("PlayerHead").transform;
        playerBody = GameObject.FindGameObjectWithTag("PlayerBody").transform;
        //记得在player那边加个tag
    }

    private void Update()
    {
        transform.position = player.position;
        transform.position = playerHead.position;
        transform.position = playerHead.position;
        Rotate();
    }

    void Rotate()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isRotating)
        {
            StartCoroutine(RotateAround(-45, rotateTime));
        }
        if (Input.GetKeyDown(KeyCode.E) && !isRotating)
        {
            StartCoroutine(RotateAround(45, rotateTime));
        }
    }

    IEnumerator RotateAround(float angel,float time)
    {
        float number = 60 * time;
        float nextAngel = angel / number;
        isRotating = true;

        for(int i = 0; i < number; i++)
        {
            transform.Rotate(new Vector3(0, 0, nextAngel));
            yield return new WaitForFixedUpdate();
        }

        isRotating=false;
    }


    /*player脚本
     * inputX = Input.GetAxisRaw("Horizontal");
     * inputY = Input.GetAxisRaw("Vertival");
     * Vector2 input = (transform.right * inputX + transform.up * inputY).nomalized;
     */


}
