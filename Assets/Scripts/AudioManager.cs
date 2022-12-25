using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance => instance;

    float masterVolumePercent = 1;//������
    float sfxVolumePercent = 1;//��Ч����
    float musicVolumePercent = 1;//������������
    AudioSource[] musicSources;//������������
    int activeMusicSourceIndex;//�������������±�

    Transform audioListener;//��Чλ��
    Transform playerT;//���λ��

    private void Awake()
    {
        instance = this;//��ʼ������
        musicSources = new AudioSource[2];//��ʼ������
        for(int i = 0; i < musicSources.Length; i++)//�����Ч���
        {
            GameObject newMusicSource = new GameObject("Music source" + i + 1);
            musicSources[i] = newMusicSource.AddComponent<AudioSource>();
            newMusicSource.transform.parent = transform;
        }
        audioListener = FindObjectOfType<AudioListener>().transform;//�õ���Чλ��
        //playerT = FindObjectOfType<Player>().transform;//Player�ű����õ����λ��
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

    //������Ч��ʹ�÷���:AudioManager.Instance.PlaySound(clip,pos);
    public void PlaySound(AudioClip clip,Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(clip, pos,masterVolumePercent*sfxVolumePercent);
    }

    private void Update()
    {
        if(playerT != null)
        {
            audioListener.position = playerT.position;//�����λ�ø�ֵ����Ч
        }
    }

}
