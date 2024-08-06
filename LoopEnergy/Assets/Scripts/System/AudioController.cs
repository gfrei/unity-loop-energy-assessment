using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioClip rotateClip;
    [SerializeField] private AudioClip completeLevelClip;
    [SerializeField] private AudioClip buttonPressedClip;


    public void PlayRotateSfx() => sfxAudioSource.PlayOneShot(rotateClip);
    public void PlayButtonSfx() => sfxAudioSource.PlayOneShot(buttonPressedClip);
    public void PlayCompleteLevelSfx() => sfxAudioSource.PlayOneShot(completeLevelClip);

}
