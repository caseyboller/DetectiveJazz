using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalWander : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotSpeed = 100f;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    public bool isWalking = false;
    public bool isSitting = false;
    public bool isResettingRotation = false;

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
            StartCoroutine(SitTime());
        }
    }

    IEnumerator SitTime()
    {
        isSitting = true;
        yield return new WaitForSeconds(10);
        isSitting = false;
        isResettingRotation = true;
        yield return new WaitForSeconds(5);
        isResettingRotation = false;
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