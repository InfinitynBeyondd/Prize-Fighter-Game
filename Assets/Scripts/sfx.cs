using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx : MonoBehaviour
{

    [Header("HEXDOGSFX")]
    [SerializeField] private AudioClip[] footsteps;
    [Header("CLAWSFX")]
    [SerializeField] private AudioClip clawClose;
    [SerializeField] private AudioClip clawDescend;
    [SerializeField] private AudioClip clawHunting;
    [Header("CONEFX")]
    [SerializeField] private AudioClip coneHit;
    [SerializeField] private AudioClip[] coneMove;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HexdogFootsteps()
    {

        SoundFXManager.Instance.PlayRandomSoundFXClip(footsteps, transform, 0.3f, 0.2f);
    }

    public void ClawClose()
    {
        SoundFXManager.Instance.PlaySoundFXClip(clawClose, transform, 0.7f, 0.2f);
    }
    public void ClawDescend()
    {

        SoundFXManager.Instance.PlaySoundFXClip(clawDescend, transform, 0.7f, 0.2f);
    }
    public void ClawHunting()
    {

        SoundFXManager.Instance.PlaySoundFXClip(clawHunting, transform, 0.7f, 0.2f);
    }
    public void ConeHit()
    {
        SoundFXManager.Instance.PlaySoundFXClip(coneHit, transform, 0.9f, 0.5f);
    }
    public void ConeMove()
    {
        SoundFXManager.Instance.PlayRandomSoundFXClip(coneMove, transform, 0.7f, 0.2f);
    }
}
