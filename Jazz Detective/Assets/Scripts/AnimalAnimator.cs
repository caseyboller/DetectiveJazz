using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAnimator : MonoBehaviour
{
    public AnimalWander wander;

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
    public Transform capsule;
    public Transform jas;

    bool frontLeftFootMoving = false;
    bool frontRightFootMoving = false;
    bool backLeftFootMoving = false;
    bool backRightFootMoving = false;

    private AudioSource audioSource;
    public AudioClip[] audioClips;

    public float footStandThreshold = 0.5f;
    public float footWalkThreshold = 1f;
    public float footStopThreshold = 0.05f;


    private Vector3 vel;
    private Vector3 lastPos;


    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
        jas = GameObject.FindGameObjectWithTag("Player").transform;
        wander = GetComponent<AnimalWander>();
        audioSource = GetComponentInChildren<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        if (lastPos != transform.position)
        {
            vel = transform.position - lastPos;
            vel /= Time.deltaTime;
            lastPos = transform.position;
        }

        if (vel.magnitude > 1f)
        {
            WalkFeet(ref frontLeftFoot, ref frontRightFoot, ref frontLeftFootTarget, ref frontRightFootTarget, ref frontLeftFootMoving, ref frontRightFootMoving);
            WalkFeet(ref backLeftFoot, ref backRightFoot, ref backLeftFootTarget, ref backRightFootTarget, ref backLeftFootMoving, ref backRightFootMoving);
        }
        else
        {
            StandFeet(ref frontLeftFoot, ref frontRightFoot, ref frontLeftFootTarget, ref frontRightFootTarget, ref frontLeftFootMoving, ref frontRightFootMoving);
            StandFeet(ref backLeftFoot, ref backRightFoot, ref backLeftFootTarget, ref backRightFootTarget, ref backLeftFootMoving, ref backRightFootMoving);
        }

        if (wander.isSitting)
        {
            Vector3 lookPos = jas.position - rb.transform.position;
            Vector3 rotation = Quaternion.LookRotation(lookPos).eulerAngles;
            rotation.x = -105f;
            rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, Quaternion.Euler(rotation), Time.deltaTime * 2f);
        } else if (wander.isResettingRotation)
        {
            Vector3 rotation = Quaternion.LookRotation(transform.forward).eulerAngles;
            rotation.x = -90f;
            rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, Quaternion.Euler(rotation), Time.deltaTime * 2f);
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

            if (Vector3.Distance(rightFoot.position, rightFootTarget.position) > footStandThreshold)
            {
                rightFootMoving = true;
            }
            else if (rightFootMoving && Vector3.Distance(rightFoot.position, rightFootTarget.position) < footStopThreshold)
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

            if (Vector3.Distance(leftFoot.position, leftFootTarget.position) > footStandThreshold)
            {
                leftFootMoving = true;
            }
            else if (leftFootMoving && Vector3.Distance(leftFoot.position, leftFootTarget.position) < footStopThreshold)
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

            if (Vector3.Distance(rightFoot.position, rightWalkTarget) > footWalkThreshold)
            {
                rightFootMoving = true;
            }
            else if (rightFootMoving && Vector3.Distance(rightFoot.position, rightWalkTarget) < footStopThreshold)
            {
                rightFootMoving = false;
                if (Random.Range(0, 4) == 1)
                {
                    audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length - 1)]);
                }
            }

        }

        if (!rightFootMoving)
        {
            if (leftFootMoving)
            {
                leftFoot.position = Vector3.MoveTowards(leftFoot.position, leftWalkTarget, Time.deltaTime * moveFootSpeed);
            }

            if (Vector3.Distance(leftFoot.position, leftWalkTarget) > footWalkThreshold)
            {
                leftFootMoving = true;
            }
            else if (leftFootMoving && Vector3.Distance(leftFoot.position, leftWalkTarget) < footStopThreshold)
            {
                leftFootMoving = false;
                if (Random.Range(0, 4) == 1)
                {
                    audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length - 1)]);
                }
            }

        }
    }
}
