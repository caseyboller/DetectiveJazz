using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormWiggler : MonoBehaviour
{
    private Rigidbody rb;
    public float wiggleForce = 100f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(WiggleForth());
    }

    IEnumerator WiggleForth()
    {
        rb.AddForce(transform.forward * wiggleForce + Vector3.up * wiggleForce * 0.5f);
        yield return new WaitForSeconds(1);
        Debug.Log("WIGGLE");
        StartCoroutine(WiggleForth());
    }
}
