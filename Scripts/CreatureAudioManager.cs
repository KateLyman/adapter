using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] List<AudioClip> moveAudioClips;
    [SerializeField] List<AudioClip> fleeAudioClips;
    [SerializeField] List<AudioClip> hideAudioClips;
    [SerializeField] List<AudioClip> specialAudioClips;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMoveSound()
    {
        if (moveAudioClips.Count > 0)
        {

            AudioClip currentClip = moveAudioClips[Random.Range(0, moveAudioClips.Count)];
            audioSource.PlayOneShot(currentClip, 1.0f);
        }
    }

    public void PlayFleeSound()
    {
        if (fleeAudioClips.Count > 0)
        {
            AudioClip currentClip = fleeAudioClips[Random.Range(0, hideAudioClips.Count)];
            audioSource.PlayOneShot(currentClip, 1.0f);
        }
    }

    public void PlayHideSound()
    {
        if (hideAudioClips.Count > 0)
        {
            AudioClip currentClip = hideAudioClips[Random.Range(0, hideAudioClips.Count)];
            audioSource.PlayOneShot(currentClip, 1.0f);
        }
    }

    public void PlaySpecialSound()
    {
        if (specialAudioClips.Count > 0)
        {
            AudioClip currentClip = specialAudioClips[Random.Range(0, specialAudioClips.Count)];
            audioSource.PlayOneShot(currentClip, 1.0f);
        }
    }

    public void StopAllSound()
    {
        audioSource.Stop();
    }
}
