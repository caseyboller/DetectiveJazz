using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{

    public GameObject rightFoot;
    public GameObject leftFoot;
    public Transform rightWalkTarget;
    public Transform leftWalkTarget;
    public Transform rightStandTarget;
    public Transform leftStandTarget;

    public bool leftFootMoving = false;
    public bool rightFootMoving = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!leftFootMoving)
        {
            if (rightFootMoving) {
                rightFoot.transform.position = Vector3.MoveTowards(rightFoot.transform.position, rightWalkTarget.position, Time.deltaTime * 20f);
            }

            if (Vector3.Distance(rightFoot.transform.position, rightWalkTarget.position) > 2)
            {
                rightFootMoving = true;
            } else if (Vector3.Distance(rightFoot.transform.position, rightWalkTarget.position) < 0.1f)
            {
                rightFootMoving = false;
            }

        }

        if (!rightFootMoving)
        {
            if (leftFootMoving)
            {
                leftFoot.transform.position = Vector3.MoveTowards(leftFoot.transform.position, leftWalkTarget.position, Time.deltaTime * 20f);
            }

            if (Vector3.Distance(leftFoot.transform.position, leftWalkTarget.position) > 2)
            {
                leftFootMoving = true;
            }
            else if (Vector3.Distance(leftFoot.transform.position, leftWalkTarget.position) < 0.1f)
            {
                leftFootMoving = false;
            }

        }
    }
}
