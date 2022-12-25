using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance => instance;

    public float masterVolumePercent { get; private set; }//������
    public float sfxVolumePercent { get; private set; }//��Ч����
    public float musicVolumePercent { get; private set; }//������������
    AudioSource[] musicSources;//������������
    int activeMusicSourceIndex;//�������������±�

    Transform audioListener;//��Чλ��
    Transform playerT;//���λ��
    SoundLibrary soundLibrary;//��Ч��
    AudioSource sfx2DSource;//2D��Ч

    public enum AudioChannel { Master,sfx,Music}

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;//��ʼ������
            musicSources = new AudioSource[2];//��ʼ������
            for (int i = 0; i < musicSources.Length; i++)//�����Ч���
            {
                GameObject newMusicSource = new GameObject("Music source" + i + 1);
                musicSources[i] = newMusicSource.AddComponent<AudioSource>();
                newMusicSource.transform.parent = transform;
            }
            audioListener = FindObjectOfType<AudioListener>().transform;//�õ���Чλ��
            /*if (FindObjectOfType<Player>() != null)           //���player�ű�
            {
                playerT = FindObjectOfType<Player>().transform;
            }*/
            soundLibrary = GetComponent<SoundLibrary>();//�õ���Ч��
            GameObject newSfx2DSource = new GameObject("sfx2DSource");
            sfx2DSource = newSfx2DSource.AddComponent<AudioSource>();
            sfx2DSource.transform.parent = transform;
            masterVolumePercent = PlayerPrefs.GetFloat("master vol", 1);
            sfxVolumePercent = PlayerPrefs.GetFloat("sfx vol", 1);
            musicVolumePercent = PlayerPrefs.GetFloat("music vol",1);
        }
    }

    //��������
    public void PlayMusic(AudioClip clip,float duration)
    {
        activeMusicSourceIndex = 1 - activeMusicSourceIndex;//���������±� ֻ����0~1�л�
        musicSources[activeMusicSourceIndex].clip = clip;//��ֵ��Ƭ����
        musicSources[activeMusicSourceIndex].Play();//��������
        StartCoroutine(AnimateMusicCroofade(duration));//ʵ�����ֵ��뵭��Ч��
    }

    IEnumerator AnimateMusicCroofade(float duration)
    {
        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * (1 / duration);//���õ��뵭�����ٶ�
            musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(0, musicVolumePercent * masterVolumePercent, percent);//��Ҫ���ŵ����ֵ��뵭��
            musicSources[1 - activeMusicSourceIndex].volume = Mathf.Lerp(musicVolumePercent * masterVolumePercent, 0, percent);//���ڲ��ŵ����ֵ��뵭��
            yield return null;
        }
    }

    //������Ч��ʹ�÷���:AudioManager.Instance.PlaySound(��Ч,λ��);
    public void PlaySound(AudioClip clip,Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(clip, pos, masterVolumePercent * sfxVolumePercent);
    }

    //SoundLibrary��group����Ч��������������������е���Ч
    //ʹ�÷�����AudioManager.instance.PlaySound("Group ID",λ�ã�
    public void PlaySound(string name,Vector3 pos)
    {
        PlaySound(soundLibrary.GetClipFromName(name), pos);
    }

    //����2D��Ч,ʹ�÷���ͬ��
    public void PlaySound2D(string name)
    {
        sfx2DSource.PlayOneShot(soundLibrary.GetClipFromName(name), masterVolumePercent * sfxVolumePercent);
    }

    //��������
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
            audioListener.position = playerT.position;//�����λ�ø�ֵ����Ч
        }
    }

}
