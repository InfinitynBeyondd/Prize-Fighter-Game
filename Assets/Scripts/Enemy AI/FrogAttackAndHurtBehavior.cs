using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAttackAndHurtBehavior : MonoBehaviour
{
    public Collider Hurtbox;
    public Collider Hitbox;
    public Collider JumpBox;

    private void OnTriggerEnter(Collider other)
    {
        return;
    }

}
