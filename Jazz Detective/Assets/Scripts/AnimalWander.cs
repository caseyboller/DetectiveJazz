using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalWander : MonoBehaviour
{

    public Transform patTarget;

    public float moveSpeed = 3f;
    public float rotSpeed = 100f;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    public bool isWalking = false;
    public bool isSitting = false;
    public bool isResettingRotation = false;

    public GameObject[] eyes;
    public GameObject[] happyEyes;
    public bool beingPet = false;

    public AudioClip[] barks;
    public AudioSource barkSource;

    public GameObject barkExclamation;

    private void Start()
    {
        beingPet = false;
        StartCoroutine(Bark(5f, 60f, true));
        StartCoroutine(BlinkRandom());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isSitting || isResettingRotation)
        {
            return;
        }

        if (isWandering == false)
        {
            StartCoroutine(Wander());
        }
        if (isRotatingRight == true)
        {
            transform.Rotate(transform.up * Time.fixedDeltaTime * rotSpeed);
        }
        if (isRotatingLeft == true)
        {
            transform.Rotate(transform.up * Time.fixedDeltaTime * -rotSpeed);
        }
        if (isWalking == true)
        {
            transform.position += (transform.forward * moveSpeed * Time.fixedDeltaTime);
        }

    }
    public void WatchJas()
    {
        if (!(isSitting || isResettingRotation))
        {
            if (Random.Range(0, 3) > 1)
            {
                StartCoroutine(Bark(0.5f, 5f, false));
            }

            StartCoroutine(SitTime());
        }
    }

    public void SetBeingPet(bool isBeingPet)
    {
        Debug.Log("SET " + gameObject.name + " BEING PET " + (isBeingPet ? " T " : " F "));
        beingPet = isBeingPet;
        SetEyesOpen(!isBeingPet);
    }

    public void SetEyesOpen(bool open)
    {

        // Open
        foreach (var eye in eyes)
        {
            eye.SetActive(open);
        }

        // Squint
        foreach (var eye in happyEyes)
        {
            eye.SetActive(!open);
        }

    }

    IEnumerator BlinkRandom()
    {
        yield return new WaitForSeconds(Random.Range(2, 5));
        if (!beingPet)
        {
            Debug.Log("BLINK");
            SetEyesOpen(false);
        }
        yield return new WaitForSeconds(0.5f);
        if (!beingPet)
        {
            Debug.Log("UNBLINK");

            SetEyesOpen(true);
        }
        StartCoroutine(BlinkRandom());

    }
    IEnumerator SitTime()
    {
        isSitting = true;
        yield return new WaitForSeconds(9);
        isSitting = false;
        isResettingRotation = true;
        yield return new WaitForSeconds(1);
        isResettingRotation = false;
    }
    IEnumerator Bark(float minimumWait, float maximumWait, bool recursive)
    {
        yield return new WaitForSeconds(Random.Range(minimumWait, maximumWait));
        barkExclamation.SetActive(true);
        barkSource.Stop();
        barkSource.clip = barks[UnityEngine.Random.Range(0, barks.Length)];
        barkSource.Play();
        yield return new WaitForSeconds(1);
        barkExclamation.SetActive(false);
        if (recursive)
        {
            StartCoroutine(Bark(minimumWait, maximumWait, true));
        }
    }


    IEnumerator Wander()
    {
        if (isSitting)
        {
            yield break;
        }
        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 4);
        int rotateLorR = Random.Range(1, 3);
        int rotateAndWalk = Random.Range(1, 3);
        int walkWait = Random.Range(1, 5);
        int walkTime = Random.Range(1, 6);


        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(rotateWait);
        isWalking = (rotateAndWalk == 1);
        if (rotateLorR == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;
        }
        else
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingLeft = false;
        }
        isWandering = false;
    }

}