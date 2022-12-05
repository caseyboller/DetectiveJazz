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

    public Transform leftHandRestTarget;
    public Transform rightHandRestTarget;
    public Transform leftHand;
    public Transform rightHand;
    public float handBobSpeedDefault = 5f;
    public float handBobHeight = 5f;
    public float moveHandSpeed = 25f;

    private AudioSource audioSource;
    public AudioClip[] audioClips;

    public Transform waveTarget;

    bool leftFootMoving = false;
    bool rightFootMoving = false;

    public bool petting = false;
    Transform pettingTarget = null;
    AnimalWander closestDog = null;

    public GameObject[] chests;



    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> chestz = new List<GameObject>();
        foreach (var d in GameObject.FindGameObjectsWithTag("Chest"))
        {
            chestz.Add(d);
        }
        chests = chestz.ToArray();

        List<AnimalWander> dogz = new List<AnimalWander>();
        foreach (var d in GameObject.FindGameObjectsWithTag("Dog"))
        {
            dogz.Add(d.GetComponentInChildren<AnimalWander>());
        }
        dogs = dogz.ToArray();

        audioSource = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        Transform rHandTarget = rightHandRestTarget;
        Vector3 lHandTarget;


        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        direction = new Vector3(horizontal, 0, vertical).normalized;

        bool waving = false;
        if (Input.GetKey(KeyCode.E))
        {
            if (!waving)
            {
                waving = true;
                rHandTarget = waveTarget;

                foreach (AnimalWander dog in dogs)
                {
                    if (Mathf.Abs(Vector3.Distance(transform.position, dog.transform.position)) < 25f)
                    {
                        dog.WatchJas();
                    }
                }
            }
        }

        bool closeDogFound = false;
        // Pet dog with hold F
        if (Input.GetKeyDown(KeyCode.F))
        {
            var closestDist = float.PositiveInfinity;
            foreach (AnimalWander dog in dogs)
            {
                var dist = Mathf.Abs(Vector3.Distance(transform.position, dog.transform.position));
                if (dist < 5f)
                {
                    if (closestDog == null || dist < closestDist)
                    {
                        closestDog = dog;
                        closestDist = dist;
                        closeDogFound = true;
                    }
                }
            }

            if (closeDogFound && closestDog != null)
            {
                petting = true;
                closestDog.WatchJas();
                closestDog.SetBeingPet(true);
                pettingTarget = closestDog.patTarget;
            }
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            petting = false;
            if (closestDog != null)
            {
                closestDog.SetBeingPet(false);
            }

        }

        //// HAND ANIMATION



        if (petting)
        {
            rHandTarget = pettingTarget;
        }

        var handSpeedMod = 5f;

        if (direction.magnitude > 0.1f)
        {
            WalkFeet();

            handSpeedMod = 10f;

            // Always bob left hand
            lHandTarget = leftHandRestTarget.position - transform.forward * 1f;
            BobHand((handSpeedMod * 1.5f) * handBobSpeedDefault, leftHand, lHandTarget);

            // Right hand state dependent
            if (!(waving || petting))
            {
                BobHand((handSpeedMod * 1.5f) * handBobSpeedDefault, rightHand, rightHandRestTarget.position - transform.forward * 1f);
            }
            else
            {
                Wave((handSpeedMod * 1.2f) * handBobSpeedDefault, rightHand, rHandTarget);
            }

        }
        else
        {
            StandFeet();
            
            // Always bob left hand
            lHandTarget = leftHandRestTarget.position;
            BobHand((handSpeedMod * 1f) * handBobSpeedDefault, leftHand, lHandTarget);

            // Right hand state dependent
            if (!(waving || petting))
            {
                BobHand((handSpeedMod * 1f) * handBobSpeedDefault, rightHand, rightHandRestTarget.position);
            }
            else
            {
                Wave((handSpeedMod * 1.2f) * handBobSpeedDefault, rightHand, rHandTarget);
            }
        }






        // Open chests with press F
        bool chestOpened = false;
        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach (GameObject chest in chests)
            {
                if (!chestOpened && Mathf.Abs(Vector3.Distance(transform.position, chest.transform.position)) < 5f)
                {
                    chest.GetComponentInParent<Rotator>().Open();
                    chestOpened = true;
                }
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
                audioSource.PlayOneShot(audioClips[UnityEngine.Random.Range(0, audioClips.Length - 1)]);
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
                audioSource.PlayOneShot(audioClips[UnityEngine.Random.Range(0, audioClips.Length - 1)]);
            }

        }
    }
}
