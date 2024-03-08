using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScalePhysX : MonoBehaviour
{
    // Rigidbody2D has an editable gravity scale, but Rigidbody3D does not.
    // This script helps to apply the gravity scale to PhysX, the engine catered to 3D RBs. 

    public float gravityScale = 1.0f;

    // Global Gravity doesn't appear in the inspector. Modify it here in the code
    // (or via scripting) to define a different default gravity for all objects.

    public static float globalGravity = -9.81f; // Default Gravity Value of Unity 3D projects.

    Rigidbody m_rb;

    void OnEnable()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.useGravity = false;
    }

    void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        m_rb.AddForce(gravity, ForceMode.Acceleration);
    }
}
