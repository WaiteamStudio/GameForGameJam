﻿/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public static class SoundManager {

    public enum Sound {
        PlayerMoveFireForm,
        PlayerAttack,
        PlayerGetDamagedFireForm,
        EnemyHit,
        EnemyDie,
        Treasure,
        ButtonOver,
        ButtonClick,
        PlayerJump,
        EnemyMove,
        PlayerMoveWaterForm,
        EnemyGetDamaged,
        PlayerGetDamagedWaterForm,
        Teleportation,
        FormSwitchFire,
        FormSwitchWater,
    }
    public enum AudioGroup
    {
        sounds,
        music,
        global
    }

    private static Dictionary<Sound, float> soundTimerDictionary = new Dictionary<Sound, float>()
    {
         [Sound.PlayerMoveFireForm] = 0f,
    };
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;
    private static GameAssets gameAssets;
    private static GameAssets GameAssets
    {
        get {
            if(gameAssets == null)
                gameAssets = ServiceLocator.current.Get<GameAssets>();
            return gameAssets;
            }
    }

    public static void PlaySound(Sound sound, Vector3 position) {
        if (CanPlaySound(sound)) {
            GameObject soundGameObject = new GameObject("Sound");
            soundGameObject.transform.position = position;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);
            audioSource.maxDistance = 100f;
            audioSource.spatialBlend = 1f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.dopplerLevel = 0f;
            audioSource.Play();

            Object.Destroy(soundGameObject, audioSource.clip.length);
        }
    }

    public static void PlaySound(Sound sound, AudioGroup audioGroup = AudioGroup.sounds) {
        if (CanPlaySound(sound)) {
            if (oneShotGameObject == null) {
                oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
                oneShotAudioSource.outputAudioMixerGroup = GetAudioMixerGroup(audioGroup);
            }
            AudioClip clip = GetAudioClip(sound);
            if (clip != null)
            {
                oneShotAudioSource.PlayOneShot(clip);
            }
        }
    }

    private static bool CanPlaySound(Sound sound) {
        switch (sound) {
        default:
            return true;
        case Sound.PlayerMoveFireForm:
        case Sound.PlayerMoveWaterForm:
        case Sound.EnemyMove:
            if (soundTimerDictionary.ContainsKey(sound)) {
                float lastTimePlayed = soundTimerDictionary[sound];
                float playerMoveTimerMax = .15f;
                if (lastTimePlayed + playerMoveTimerMax < Time.time) {
                    soundTimerDictionary[sound] = Time.time;
                    return true;
                } else {
                    return false;
                }
            } else {
                return true;
            }
            //break;
        }
    }
    private static AudioMixerGroup GetAudioMixerGroup(AudioGroup audioGroup)
    {
        switch (audioGroup)
        {
            case AudioGroup.global:
                return GameAssets.GlobalAudioMixer;
            case AudioGroup.music:
                return GameAssets.MusicAudioMixer;
            case AudioGroup.sounds:
                return GameAssets.SoundsAudioMixerGroup;
            default:
                return gameAssets.GlobalAudioMixer;
        }
    }
    private static AudioClip GetAudioClip(Sound sound) {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.soundAudioClipArray) {
            if (soundAudioClip.sound == sound) {
                return soundAudioClip.audioClip;
            }
        }
        Debug.Log("Sound " + sound + " not found!");
        return null;
    }
}
