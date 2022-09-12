using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAnimator : MonoBehaviour
{
    public Transform frontRightFoot;
    public Transform frontLeftFoot;
    public Transform backRightFoot;
    public Transform backLeftFoot;
    public Transform frontRightFootTarget;
    public Transform frontLeftFootTarget;
    public Transform backRightFootTarget;
    public Transform backLeftFootTarget;    
    public float moveFootSpeed = 25f;

    public Rigidbody rb;

    bool frontLeftFootMoving = false;
    bool frontRightFootMoving = false;
    bool backLeftFootMoving = false;
    bool backRightFootMoving = false;

    public bool isMoving = false;

    public float moveSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            WalkFeet(ref frontLeftFoot, ref frontRightFoot, ref frontLeftFootTarget, ref frontRightFootTarget, ref frontLeftFootMoving, ref frontRightFootMoving);
            WalkFeet(ref backLeftFoot, ref backRightFoot, ref backLeftFootTarget, ref backRightFootTarget, ref backLeftFootMoving, ref backRightFootMoving);
        }
        else
        {
            StandFeet(ref frontLeftFoot, ref frontRightFoot, ref frontLeftFootTarget, ref frontRightFootTarget, ref frontLeftFootMoving, ref frontRightFootMoving);
            StandFeet(ref backLeftFoot, ref backRightFoot, ref backLeftFootTarget, ref backRightFootTarget, ref backLeftFootMoving, ref backRightFootMoving);
        }

        if (isMoving)
        {
            rb.MovePosition(rb.position + rb.transform.up * -moveSpeed);
        }

    }

    private void StandFeet(ref Transform leftFoot, ref Transform rightFoot, ref Transform leftFootTarget, ref Transform rightFootTarget, ref bool leftFootMoving, ref bool rightFootMoving)
    {
        if (!leftFootMoving)
        {
            if (rightFootMoving)
            {
                rightFoot.position = Vector3.MoveTowards(rightFoot.position, rightFootTarget.position, Time.deltaTime * moveFootSpeed);
            }

            if (Vector3.Distance(rightFoot.position, rightFootTarget.position) > 0.2f)
            {
                rightFootMoving = true;
            }
            else if (Vector3.Distance(rightFoot.position, rightFootTarget.position) < 0.05f)
            {
                rightFootMoving = false;
            }

        }

        if (!rightFootMoving)
        {
            if (leftFootMoving)
            {
                leftFoot.position = Vector3.MoveTowards(leftFoot.position, leftFootTarget.position, Time.deltaTime * moveFootSpeed);
            }

            if (Vector3.Distance(leftFoot.position, leftFootTarget.position) > 0.2f)
            {
                leftFootMoving = true;
            }
            else if (Vector3.Distance(leftFoot.position, leftFootTarget.position) < 0.05f)
            {
                leftFootMoving = false;
            }

        }
    }

    private void WalkFeet(ref Transform leftFoot, ref Transform rightFoot, ref Transform leftFootTarget, ref Transform rightFootTarget, ref bool leftFootMoving, ref bool rightFootMoving)
    {
        Vector3 rightWalkTarget = rightFootTarget.position + rightFootTarget.up * -0.5f;
        Vector3 leftWalkTarget = leftFootTarget.position + leftFootTarget.up * -0.5f;

        if (!leftFootMoving)
        {
            if (rightFootMoving)
            {
                rightFoot.position = Vector3.MoveTowards(rightFoot.position, rightWalkTarget, Time.deltaTime * moveFootSpeed);
            }

            if (Vector3.Distance(rightFoot.position, rightWalkTarget) > 1)
            {
                rightFootMoving = true;
            }
            else if (Vector3.Distance(rightFoot.position, rightWalkTarget) < 0.1f)
            {
                rightFootMoving = false;
            }

        }

        if (!rightFootMoving)
        {
            if (leftFootMoving)
            {
                leftFoot.position = Vector3.MoveTowards(leftFoot.position, leftWalkTarget, Time.deltaTime * moveFootSpeed);
            }

            if (Vector3.Distance(leftFoot.position, leftWalkTarget) > 1)
            {
                leftFootMoving = true;
            }
            else if (Vector3.Distance(leftFoot.position, leftWalkTarget) < 0.1f)
            {
                leftFootMoving = false;
            }

        }
    }
}
