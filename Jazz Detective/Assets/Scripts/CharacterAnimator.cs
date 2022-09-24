using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public AnimalWander[] dogs;

    public GameObject rightFoot;
    public GameObject leftFoot;
    public Transform rightFootTarget;
    public Transform leftFootTarget;
    public float moveFootSpeed = 25f;

    public Transform leftHandTarget;
    public Transform rightHandTarget;
    public Transform leftHand;
    public Transform rightHand;
    public float handBobSpeed = 1f;
    public float handBobHeight = 5f;
    public float moveHandSpeed = 25f;

    public Transform waveTarget;

    bool leftFootMoving = false;
    bool rightFootMoving = false;

    Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {
        List<AnimalWander> dogz = new List<AnimalWander>();
        foreach(var d in GameObject.FindGameObjectsWithTag("Dog"))
        {
            dogz.Add(d.GetComponentInChildren<AnimalWander>());
        }
        dogs = dogz.ToArray();
    }

    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        direction = new Vector3(horizontal, 0, vertical).normalized;

        bool waving = false;
        if (Input.GetKey(KeyCode.E))
        {
            if (!waving)
            {
                waving = true;
                Debug.Log("WAVING");
                foreach (AnimalWander dog in dogs)
                {
                    if (Mathf.Abs(Vector3.Distance(transform.position, dog.transform.position)) < 25f)
                    {
                        dog.WatchJas();
                    }
                }
            }

        }

        if (direction.magnitude > 0.1f)
        {
            WalkFeet();
                BobHand(5f * handBobSpeed, leftHand, leftHandTarget.position - transform.forward * 1f);
            if (!waving)
            {
                BobHand(5f * handBobSpeed, rightHand, rightHandTarget.position - transform.forward * 1f);
            }
            else
            {
                Wave(5f * handBobSpeed, rightHand, waveTarget);
            }

        }
        else
        {
            StandFeet();
            BobHand(handBobSpeed, leftHand, leftHandTarget.position);
            if (!waving)
            {
                BobHand(handBobSpeed, rightHand, rightHandTarget.position);
            }
            else
            {
                Wave(5f * handBobSpeed, rightHand, waveTarget);
            }
        }

    }

    private void Wave(float handBobSpeed, Transform hand, Transform handTarget)
    {
        float newX = Mathf.Sin(Time.time * handBobSpeed) * 0.3f;
        Vector3 target = handTarget.position + (handTarget.right * newX);
        hand.transform.position = Vector3.MoveTowards(hand.transform.position, target, Time.deltaTime * moveHandSpeed);
    }

    private void BobHand(float handBobSpeed, Transform hand, Vector3 handTarget)
    {
        float newY = Mathf.Sin(Time.time * handBobSpeed) * 0.1f;
        Vector3 target = new Vector3(handTarget.x, handTarget.y + newY, handTarget.z);
        hand.transform.position = Vector3.MoveTowards(hand.transform.position, target, Time.deltaTime * moveHandSpeed);
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
