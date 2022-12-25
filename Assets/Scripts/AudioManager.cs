using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance => instance;

    float masterVolumePercent = 1;//主音量
    float sfxVolumePercent = 1;//音效音量
    float musicVolumePercent = 1;//背景音乐音量
    AudioSource[] musicSources;//背景音乐数组
    int activeMusicSourceIndex;//背景音乐数组下标

    Transform audioListener;//音效位置
    Transform playerT;//玩家位置

    private void Awake()
    {
        instance = this;//初始化单例
        musicSources = new AudioSource[2];//初始化数组
        for(int i = 0; i < musicSources.Length; i++)//添加音效组件
        {
            GameObject newMusicSource = new GameObject("Music source" + i + 1);
            musicSources[i] = newMusicSource.AddComponent<AudioSource>();
            newMusicSource.transform.parent = transform;
        }
        audioListener = FindObjectOfType<AudioListener>().transform;//得到音效位置
        //playerT = FindObjectOfType<Player>().transform;//Player脚本，得到玩家位置
    }

    //播放音乐
    public void PlayMusic(AudioClip clip,float duration)
    {
        activeMusicSourceIndex = 1 - activeMusicSourceIndex;//设置数组下标 只会在0~1切换
        musicSources[activeMusicSourceIndex].clip = clip;//赋值切片音乐
        musicSources[activeMusicSourceIndex].Play();//播放音乐
        StartCoroutine(AnimateMusicCroofade(duration));//实现音乐淡入淡出效果
    }

    IEnumerator AnimateMusicCroofade(float duration)
    {
        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * (1 / duration);//设置淡入淡出的速度
            musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(0, musicVolumePercent * masterVolumePercent, percent);//需要播放的音乐淡入淡出
            musicSources[1 - activeMusicSourceIndex].volume = Mathf.Lerp(musicVolumePercent * masterVolumePercent, 0, percent);//正在播放的音乐淡入淡出
            yield return null;
        }
    }

    //立体音效，使用方法:AudioManager.Instance.PlaySound(clip,pos);
    public void PlaySound(AudioClip clip,Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(clip, pos,masterVolumePercent*sfxVolumePercent);
    }

    private void Update()
    {
        if(playerT != null)
        {
            audioListener.position = playerT.position;//把玩家位置赋值给音效
        }
    }

}
