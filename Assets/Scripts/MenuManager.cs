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
    public Toggle fullscreenToggle;

    public Slider[] volumeSlider;

    private void Start()
    {
        volumeSlider[0].value = AudioManager.Instance.masterVolumePercent;
        volumeSlider[1].value = AudioManager.Instance.musicVolumePercent;
        volumeSlider[2].value = AudioManager.Instance.sfxVolumePercent;
        for(int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].isOn = i == activeScreenIndex;
        }
        bool isFullscreen = (PlayerPrefs.GetInt("fullscreen")==1)?true:false;
        fullscreenToggle.isOn = isFullscreen;
    }

    //��ʼ��ť,���ֿ�ʼ��Ϸ�ͼ�����Ϸ
    public void StartButton()
    {
        mainMenu.SetActive(false);
        startMenu.SetActive(true);
    }

    //���ؿ�ʼ����
    public void StartToMainButton()
    {
        startMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("��֪����");
    }

    //�����ý���
    public void OptionButton()
    {
        mainMenu.SetActive(false);
        optionMenu.SetActive(true);
    }

    public void OptionToMainMenu()
    {
        optionMenu.SetActive(false);
        mainMenu.SetActive(true) ;
    }

    //ͼ������
    public void CollectionButton()
    {
        mainMenu.SetActive(false);
        collectionMenu.SetActive(true);
    }

    public void CollectionToMainMenu()
    {
        collectionMenu.SetActive(false);
        mainMenu.SetActive(true);
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
            PlayerPrefs.SetInt("screen res index",activeScreenIndex);
            PlayerPrefs.Save();
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
            PlayerPrefs.SetInt("fullscreen", (isFullscreen) ? 1 : 0);
            PlayerPrefs.Save();
        }
        else
        {
            SetScreenResolution(activeScreenIndex);
        }
    }

    public void SetMasterVolume(float value)
    {
        AudioManager.Instance.SetVolume(value, AudioManager.AudioChannel.Master);
    }

    public void SetMusicVolume(float value)
    {
        AudioManager.Instance.SetVolume(value, AudioManager.AudioChannel.Music);
    }

    public void SetSfxVolume(float value)
    {
        AudioManager.Instance.SetVolume(value, AudioManager.AudioChannel.sfx);
    }
}
