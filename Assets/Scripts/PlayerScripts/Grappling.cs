using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grappling : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private PlayerAnims pA;
    private PlayerBehavior pB;
    private GravityScalePhysX gSPX;
    private DivePunch dP;
    public Transform cam;
    public Transform gunTip;
    public LayerMask whatIsGrappleable;
    public LineRenderer lr;
    [SerializeField] private AudioClip hexdogGrapple;

    [Header("Grappling")]
    public float maxGrappleDistance;
    public float grappleDelayTime;
    public float overshootYAxis;

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    public float grapplingCd;
    private float grapplingCdTimer;


    private bool grappling;

    private void Start()
    {
        pA = GetComponent<PlayerAnims>();
        pB = GetComponent<PlayerBehavior>();
        gSPX = gameObject.GetComponent<GravityScalePhysX>();
        dP = GetComponent<DivePunch>();
    }

    private void Update()
    {
        if (grapplingCdTimer > 0) 
        {
            grapplingCdTimer -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if (grappling) 
        { 
           lr.SetPosition(0, gunTip.position);
        }
    }

    public void StartGrapple(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("I'm starting to grapple.");
            SoundFXManager.Instance.PlaySoundFXClip(hexdogGrapple, transform, 0.5f);
            if (grapplingCdTimer > 0)
            {
                return;
            }

            grappling = true;
            // pA.m_Animator.SetBool("isGrappling", grappling);
            // pA.m_Animator.SetBool("isStopped", false);
            gSPX.gravityScale = 0f;

            RaycastHit hit;
            if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, whatIsGrappleable))
            {
                grapplePoint = hit.point;

                Invoke(nameof(ExecuteGrapple), grappleDelayTime);
            }
            else
            {
                grapplePoint = cam.position + cam.forward * maxGrappleDistance;

                Invoke(nameof(StopGrapple), grappleDelayTime);
            }

            lr.enabled = true;
            lr.SetPosition(1, grapplePoint);
        }
    }

    private void ExecuteGrapple()
    {
        gSPX.gravityScale = dP.gravConstant;

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        if (grapplePointRelativeYPos < 0) 
        {
            highestPointOnArc = overshootYAxis;
        } 

        pB.JumpToPosition(grapplePoint, highestPointOnArc);

        Invoke(nameof(StopGrapple), 1f);
    }

    public void StopGrapple()
    {
        gSPX.gravityScale = dP.gravConstant;

        grappling = false;

        // pA.m_Animator.SetBool("isGrappling", grappling);
        // pA.m_Animator.SetBool("isStopped", true);

        grapplingCdTimer = grapplingCd;

        lr.enabled = false;

    }

    public bool IsGrappling()
    {
        return grappling;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}
