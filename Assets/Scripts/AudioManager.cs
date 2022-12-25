using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance => instance;

    public float masterVolumePercent { get; private set; }//主音量
    public float sfxVolumePercent { get; private set; }//音效音量
    public float musicVolumePercent { get; private set; }//背景音乐音量
    AudioSource[] musicSources;//背景音乐数组
    int activeMusicSourceIndex;//背景音乐数组下标

    Transform audioListener;//音效位置
    Transform playerT;//玩家位置
    SoundLibrary soundLibrary;//音效库
    AudioSource sfx2DSource;//2D音效

    public enum AudioChannel { Master,sfx,Music}

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;//初始化单例
            musicSources = new AudioSource[2];//初始化数组
            for (int i = 0; i < musicSources.Length; i++)//添加音效组件
            {
                GameObject newMusicSource = new GameObject("Music source" + i + 1);
                musicSources[i] = newMusicSource.AddComponent<AudioSource>();
                newMusicSource.transform.parent = transform;
            }
            audioListener = FindObjectOfType<AudioListener>().transform;//得到音效位置
            /*if (FindObjectOfType<Player>() != null)           //获得player脚本
            {
                playerT = FindObjectOfType<Player>().transform;
            }*/
            soundLibrary = GetComponent<SoundLibrary>();//得到音效库
            GameObject newSfx2DSource = new GameObject("sfx2DSource");
            sfx2DSource = newSfx2DSource.AddComponent<AudioSource>();
            sfx2DSource.transform.parent = transform;
            masterVolumePercent = PlayerPrefs.GetFloat("master vol", 1);
            sfxVolumePercent = PlayerPrefs.GetFloat("sfx vol", 1);
            musicVolumePercent = PlayerPrefs.GetFloat("music vol",1);
        }
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

    //立体音效，使用方法:AudioManager.Instance.PlaySound(音效,位置);
    public void PlaySound(AudioClip clip,Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(clip, pos, masterVolumePercent * sfxVolumePercent);
    }

    //SoundLibrary里group的音效，可以随机播放组里所有的音效
    //使用方法：AudioManager.instance.PlaySound("Group ID",位置）
    public void PlaySound(string name,Vector3 pos)
    {
        PlaySound(soundLibrary.GetClipFromName(name), pos);
    }

    //播放2D音效,使用方法同上
    public void PlaySound2D(string name)
    {
        sfx2DSource.PlayOneShot(soundLibrary.GetClipFromName(name), masterVolumePercent * sfxVolumePercent);
    }

    //调节音量
    public void SetVolume(float volumePeercent, AudioChannel audioChannel)
    {
        switch (audioChannel)
        {
            case AudioChannel.Master:
                masterVolumePercent = volumePeercent;
                break;
            case AudioChannel.sfx:
                sfxVolumePercent = volumePeercent;
                break;
            case AudioChannel.Music:
                musicVolumePercent = volumePeercent;
                break;
        }
        musicSources[0].volume = masterVolumePercent * musicVolumePercent;
        musicSources[1].volume = masterVolumePercent * musicVolumePercent;
        PlayerPrefs.SetFloat("master vol", masterVolumePercent);
        PlayerPrefs.SetFloat("sfx vol", sfxVolumePercent);
        PlayerPrefs.SetFloat("music vol", musicVolumePercent);
        PlayerPrefs.Save();
    }
    private void Update()
    {
        if(playerT != null)
        {
            audioListener.position = playerT.position;//把玩家位置赋值给音效
        }
    }

}
