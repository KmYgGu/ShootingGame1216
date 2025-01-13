using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BGM_TYPE
{
    BGM_Normal = 0,
    BGM_Boss01,
    BGM_Boss02,
    BGM_Boss03
}

public enum SFX_TYPE
{
    SFX_Fire = 0,
    SFX_Explosion,
}

public class SoundManager : SingletonDestroy<SoundManager>//���������� ����ϴ� �Ҹ��̴� �̱���
{
    //BGM ���ο� Ŭ���� ����̵Ǹ� ������ ������� ���� ����,
    [SerializeField] private AudioSource bgm_Audio;
    [SerializeField] private List<AudioClip> bgmlist;

    public void ChangeBGM(BGM_TYPE newbgm)
    {
        StartCoroutine(ChangeBGMClip(bgmlist[(int)newbgm]));
    }


    private float current;
    private float percent;
    IEnumerator ChangeBGMClip(AudioClip newClip)
    {
        current = percent = 0;
        while(percent < 1f)
        {
            current += Time.deltaTime;
            percent = current / 1.0f; // 1�ʿ� ���ļ� ������ 0���� �����
            bgm_Audio.volume = Mathf.Lerp(1f, 0f, percent);
            yield return null;
        }

        bgm_Audio.clip = newClip;
        bgm_Audio.Play();
        current = percent = 0f;

        //1�ʿ� ���ļ� ������ 1���� ���̴�
        while (percent < 1f)
        {
            current += Time.deltaTime;
            percent = current / 1.0f;
            bgm_Audio.volume = Mathf.Lerp(0f, 1f, percent);
            yield return null;
        }
    }

    //SFX. ���ο� Ŭ���� ����� �Ǹ�, ������ ����� �Ͱ��� �������
    //AudioSource�� �ٲ㰡�鼭 ���
    private int cursor = 0;
    [SerializeField] private List<AudioSource> sfxPlayers;//������ִ� �÷��̾�
    [SerializeField] private List<AudioClip> sfx_List;//����Ǵ� ����

    public void PlaySFX(SFX_TYPE sfx)
    {
        sfxPlayers[cursor].clip = sfx_List[(int)sfx];
        sfxPlayers[cursor].Play();
        cursor++;
        if (cursor > sfxPlayers.Count-1) 
        {
            cursor = 0;
        }
    }
}
