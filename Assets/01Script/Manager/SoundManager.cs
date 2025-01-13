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

public class SoundManager : SingletonDestroy<SoundManager>//전투에서만 사용하는 소리이니 싱글톤
{
    //BGM 새로운 클립이 재생이되면 기존의 재생중인 사운드 종료,
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
            percent = current / 1.0f; // 1초에 거쳐서 볼륨을 0으로 만든다
            bgm_Audio.volume = Mathf.Lerp(1f, 0f, percent);
            yield return null;
        }

        bgm_Audio.clip = newClip;
        bgm_Audio.Play();
        current = percent = 0f;

        //1초에 거쳐서 볼륨을 1까지 높이는
        while (percent < 1f)
        {
            current += Time.deltaTime;
            percent = current / 1.0f;
            bgm_Audio.volume = Mathf.Lerp(0f, 1f, percent);
            yield return null;
        }
    }

    //SFX. 새로운 클립이 재생이 되면, 기존에 재생된 것과는 상관없이
    //AudioSource를 바꿔가면서 재생
    private int cursor = 0;
    [SerializeField] private List<AudioSource> sfxPlayers;//재생해주는 플레이어
    [SerializeField] private List<AudioClip> sfx_List;//재생되는 사운드

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
