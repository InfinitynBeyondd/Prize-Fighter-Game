using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hexdogSFX : MonoBehaviour
{
    
    [SerializeField] private AudioClip[] footsteps;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound()
    {
        
        SoundFXManager.Instance.PlayRandomSoundFXClip(footsteps, transform, 0.3f, 0.2f);

    }
}
