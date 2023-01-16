using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    public GameObject pauseMenuUI;  
    public GameObject settingMenuUI;
    // Update is called once per frame
    void Update()
    {
        if(PlayerManager.IsHead)
        {

        }
        else
        {

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           Pause();
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void Remake()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Setting()
    {
        settingMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }
    public void OnExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
