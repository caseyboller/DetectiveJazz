using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 5f;
    public float turnSmoothing = 0.1f;
    private float turnSmoothVel;

    public Transform body;
    public float leanAmount;
    float leanSmoothing = 0.2f;
    float leanSmoothVel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;


        if (direction.magnitude >= 0.1f)
        {
            Debug.Log("LEANING");
            // Lean
            var leanAngle = Mathf.SmoothDampAngle(body.transform.eulerAngles.x, leanAmount, ref leanSmoothVel, leanSmoothing);
            body.localRotation = Quaternion.Euler(leanAngle, 0f, 0f);

            // Spin
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothing);

            // Rotate
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        } else
        {
            // Lean
            var leanAngle = Mathf.SmoothDampAngle(body.transform.eulerAngles.x, 0f, ref leanSmoothVel, leanSmoothing);
            body.localRotation = Quaternion.Euler(leanAngle, 0f, 0f);
        }


    }
}
