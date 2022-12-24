using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionMenu;
    public GameObject collectionMenu;



    //��ʼ��ť
    public void StartButton()
    {
        SceneManager.LoadScene("Game");//������Ϸ����
    }

    //���ý���
    public void OptionButton()
    {
        mainMenu.SetActive(false);
        optionMenu.SetActive(true);
    }

    //ͼ������
    public void CollectionButton()
    {
        mainMenu.SetActive(false);
        collectionMenu.SetActive(true);
    }

    //�˳�
    public void ExitButton()
    {
        Application.Quit();
    }

}
