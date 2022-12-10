using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Transform targetObject;
    public ParticleSystem particles;

    public bool rotating = false;
    public bool opening = false;

    public bool hasBeenOpened = false;

    public ChestsOpenedHandler chestHandler;

    private void Start()
    {
        chestHandler = GameObject.FindGameObjectWithTag("ChestsOpenedHandler").GetComponent<ChestsOpenedHandler>();
    }

    private void Update()
    {
        if (rotating)
        {
            Vector3 direction;
            if (opening)
            {

                direction = new Vector3(targetObject.rotation.eulerAngles.x, targetObject.rotation.eulerAngles.y, 120);
            }
            else
            {
                direction = new Vector3(targetObject.rotation.eulerAngles.x, targetObject.rotation.eulerAngles.y, 0);

            }

            Quaternion targetRotation = Quaternion.Euler(direction);
            if (1 - Mathf.Abs(Quaternion.Dot(targetObject.rotation, targetRotation)) > 0.01f)
            {
                targetObject.rotation = Quaternion.Lerp(targetObject.rotation, targetRotation, Time.deltaTime * 1);
            }
            else
            {
                rotating = false;
            }
        }
    }

    public void Open()
    {
        chestHandler.OpenChest(gameObject.GetComponent<ChestDetails>());
        rotating = true;
        opening = true;
        StartCoroutine(Close());
    }

    IEnumerator Close()
    {
        yield return new WaitForSeconds(0.2f);
        if (!hasBeenOpened)
        {
            particles.Play();
            hasBeenOpened = true;
        }
        yield return new WaitForSeconds(10);
        rotating = true;
        opening = false;
    }
}