using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{

    public GameObject rightFoot;
    public GameObject leftFoot;
    public Transform rightFootTarget;
    public Transform leftFootTarget;
    public float moveFootSpeed = 25f;

    bool leftFootMoving = false;
    bool rightFootMoving = false;

    Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude > 0.1f)
        {
            WalkFeet();
        } else
        {
            StandFeet();
        }
    }

    private void StandFeet()
    {
        if (!leftFootMoving)
        {
            if (rightFootMoving)
            {
                rightFoot.transform.position = Vector3.MoveTowards(rightFoot.transform.position, rightFootTarget.position, Time.deltaTime * moveFootSpeed);
            }

            if (Vector3.Distance(rightFoot.transform.position, rightFootTarget.position) > 0.5f)
            {
                rightFootMoving = true;
            }
            else if (Vector3.Distance(rightFoot.transform.position, rightFootTarget.position) < 0.1f)
            {
                rightFootMoving = false;
            }

        }

        if (!rightFootMoving)
        {
            if (leftFootMoving)
            {
                leftFoot.transform.position = Vector3.MoveTowards(leftFoot.transform.position, leftFootTarget.position, Time.deltaTime * moveFootSpeed);
            }

            if (Vector3.Distance(leftFoot.transform.position, leftFootTarget.position) > 0.5f)
            {
                leftFootMoving = true;
            }
            else if (Vector3.Distance(leftFoot.transform.position, leftFootTarget.position) < 0.1f)
            {
                leftFootMoving = false;
            }

        }
    }

    private void WalkFeet()
    {
        Vector3 rightWalkTarget = rightFootTarget.position + transform.forward * 1f;
        Vector3 leftWalkTarget = leftFootTarget.position + transform.forward * 1f;

        if (!leftFootMoving)
        {
            if (rightFootMoving)
            {
                rightFoot.transform.position = Vector3.MoveTowards(rightFoot.transform.position, rightWalkTarget, Time.deltaTime * moveFootSpeed);
            }

            if (Vector3.Distance(rightFoot.transform.position, rightWalkTarget) > 2)
            {
                rightFootMoving = true;
            }
            else if (Vector3.Distance(rightFoot.transform.position, rightWalkTarget) < 0.1f)
            {
                rightFootMoving = false;
            }

        }

        if (!rightFootMoving)
        {
            if (leftFootMoving)
            {
                leftFoot.transform.position = Vector3.MoveTowards(leftFoot.transform.position, leftWalkTarget, Time.deltaTime * moveFootSpeed);
            }

            if (Vector3.Distance(leftFoot.transform.position, leftWalkTarget) > 2)
            {
                leftFootMoving = true;
            }
            else if (Vector3.Distance(leftFoot.transform.position, leftWalkTarget) < 0.1f)
            {
                leftFootMoving = false;
            }

        }
    }
}
