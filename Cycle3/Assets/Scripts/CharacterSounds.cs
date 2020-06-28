using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSounds : MonoBehaviour
{

    private AudioSource audioSource;
    public List<AudioClip> deathClips;
    public List<AudioClip> noticedClips;
    public List<AudioClip> attackClips;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

   


    public void PlayDeathClip()
    {
        int randomIndex = Random.Range(0, deathClips.Count);
        audioSource.clip = deathClips[randomIndex];
        audioSource.Play();
    }
    public void PlayNoticedClip()
    {
        int randomIndex = Random.Range(0, noticedClips.Count);
        audioSource.clip = noticedClips[randomIndex];
        audioSource.Play();
    }
    public void PlayAttackClip()
    {
        int randomIndex = Random.Range(0, attackClips.Count);
        audioSource.clip = attackClips[randomIndex];
        audioSource.Play();
    }
}
