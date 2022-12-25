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

    //�ֱ�������
    public Toggle[] resolutionToggles;
    int activeScreenIndex;
    public int[] screenWidths;


    //��ʼ��ť,���ֿ�ʼ��Ϸ�ͼ�����Ϸ
    public void StartButton()
    {
        mainMenu.SetActive(false);
        startMenu.SetActive(true);
    }

    //�����ý���
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

    //��Ļ�ֱ���
    public void SetScreenResolution(int i)
    {
        if (resolutionToggles[i].isOn)
        {
            activeScreenIndex = i;
            float aspectRatio = 16 / 9f;
            Screen.SetResolution(screenWidths[i], (int)(screenWidths[i]/aspectRatio),false);
        }
    }

    //����ȫ��
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
