using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footsteps : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip[] footstepsounds;

    private AudioClip randomclip()
    {
        return footstepsounds[Random.Range(0, footstepsounds.Length)];
    }
    private void steps()
    {
        AudioClip clip = randomclip();
        audiosource.PlayOneShot(clip);

    }
}
