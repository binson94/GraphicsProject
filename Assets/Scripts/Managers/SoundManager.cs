/******
작성자 : 정혁진
최근 수정 내용 : 효과음 재생을 위한 클래스
 ******/

using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    /// <summary> 효과음 캐싱 </summary>
    static Dictionary<Define.SFX, AudioClip> _sfxClips = new Dictionary<Define.SFX, AudioClip>();

    /// <summary> 효과음 재생 </summary>
    public static void Play(AudioSource source, Define.SFX sfx)
    {
        AudioClip audioClip = GetOrAddAudioClip(sfx);
        source.PlayOneShot(audioClip);
    }

    /// <summary> 효과음 불러오기 </summary>
    static AudioClip  GetOrAddAudioClip(Define.SFX sfx)
    {
        AudioClip audioClip = null;

        if (_sfxClips.TryGetValue(sfx, out audioClip) == false)
        {
            string path = $"Sounds/{sfx}";
            audioClip = Resources.Load<AudioClip>(path);
            _sfxClips.Add(sfx, audioClip);
        }

        if (audioClip == null)
            Debug.LogError($"AudioClip Missing ! {sfx}");

        return audioClip;
    }
}

