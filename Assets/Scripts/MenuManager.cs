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
    public GameObject startMenu;

    //分辨率设置
    public Toggle[] resolutionToggles;
    int activeScreenIndex;
    public int[] screenWidths;


    //开始按钮,出现开始游戏和继续游戏
    public void StartButton()
    {
        mainMenu.SetActive(false);
        startMenu.SetActive(true);
    }

    //打开设置界面
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

    //屏幕分辨率
    public void SetScreenResolution(int i)
    {
        if (resolutionToggles[i].isOn)
        {
            activeScreenIndex = i;
            float aspectRatio = 16 / 9f;
            Screen.SetResolution(screenWidths[i], (int)(screenWidths[i]/aspectRatio),false);
        }
    }

    //设置全屏
    public void SetFullscreen(bool isFullscreen)
    {
        for(int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].interactable = !isFullscreen;
        }
        if (isFullscreen)
        {
            Resolution[] allResolutions = Screen.resolutions;
            Resolution maxResolutions = allResolutions[allResolutions.Length-1];
            Screen.SetResolution(maxResolutions.width, maxResolutions.height, true);
        }
        else
        {
            SetScreenResolution(activeScreenIndex);
        }
    }

}
