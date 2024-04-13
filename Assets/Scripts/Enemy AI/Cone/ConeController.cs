using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeController : MonoBehaviour
{
    [SerializeField] Rigidbody coneBody; // Cone's RB.
    [SerializeField] Transform coneTargets; // Transform of the cone's targets.
    [SerializeField] Transform thisConesRotation; // Transform of the cone's targets.

    [SerializeField] Transform[] patrolTargetsArray; // Array where all the children of coneTargets are stored.
    Queue<Transform> patrolTargetsQueue = new Queue<Transform>(); // Queue used to create patrolTargetsArray.
    [SerializeField] int patrolTargetIndex; // Index of the array that the cone is headed towards.
    public float speedBetweenTargets; // The speed at which the cone travels between its targets.
    

    // Start is called before the first frame update
    void Start()
    {
        coneBody = GetComponent<Rigidbody>();        
        coneTargets = transform.parent.GetChild(2);
        //thisConesRotation = GetComponentInChildren<Transform>();

        patrolTargetIndex = 0;
        speedBetweenTargets = 5f;

        SetPatrolTargets();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // FixedUpdate is called once per physics change
    void FixedUpdate()
    {
        ConeMoveToTarget();
    }

    // This function enqueues all the children of PATROL TARGETS to patrolTargetsQueue. This automatically adjusts the array size to fit all the targets.
    private void SetPatrolTargets()
    {

        for (int i = 0; i < coneTargets.childCount; i++)
        {
            patrolTargetsQueue.Enqueue(coneTargets.GetChild(i));
        }

        patrolTargetsArray = new Transform[coneTargets.childCount];

        patrolTargetsQueue.CopyTo(patrolTargetsArray, 0);
    }

    // Controls cone's movement to the targets.
    void ConeMoveToTarget()
    {
        coneBody.position = Vector3.MoveTowards(coneBody.position, patrolTargetsArray[patrolTargetIndex].position, speedBetweenTargets * Time.deltaTime);

        //ConeFaceTarget();

        if (coneBody.position == patrolTargetsArray[patrolTargetIndex].position)
        {
            if (patrolTargetIndex + 1 < patrolTargetsArray.Length)
            {
                patrolTargetIndex++;
            }
            else
            {
                patrolTargetIndex = 0;
            }

        }
    }

    // Snaps cone's rotation to the target it is headed for. This doesn't work so we'll refine it during the polish period.
    private void ConeFaceTarget()
    {
        Vector3 targetPosition = new Vector3(patrolTargetsArray[patrolTargetIndex].position.x, 0, patrolTargetsArray[patrolTargetIndex].position.z);
        Vector3 thisConesPosition = new Vector3(thisConesRotation.transform.position.x, 0, thisConesRotation.position.z);
        Vector3 coneFacingDirection = (thisConesPosition - targetPosition);

        Quaternion coneRotation = Quaternion.LookRotation(coneFacingDirection);

        transform.rotation = coneRotation;
    }

}