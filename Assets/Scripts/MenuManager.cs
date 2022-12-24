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



    //开始按钮
    public void StartButton()
    {
        SceneManager.LoadScene("Game");//进入游戏界面
    }

    //设置界面
    public void OptionButton()
    {
        mainMenu.SetActive(false);
        optionMenu.SetActive(true);
    }

    //图鉴界面
    public void CollectionButton()
    {
        mainMenu.SetActive(false);
        collectionMenu.SetActive(true);
    }

    //退出
    public void ExitButton()
    {
        Application.Quit();
    }

}
