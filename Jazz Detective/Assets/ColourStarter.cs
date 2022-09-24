using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourStarter : MonoBehaviour
{
    public Material[] materials;
    public Material[] accentMaterials;

    public GameObject[] baseParts;
    public GameObject[] accentParts;

    // Start is called before the first frame update
    void Start()
    {


        bool differentAccent = Random.Range(0, 4) == 1;
        bool accentsAlsoDifferent = Random.Range(0, 20) == 1;

        Material baseMat = materials[Random.Range(0, materials.Length)];
        Material accentMat;
        if (differentAccent)
        {
            accentMat = accentMaterials[Random.Range(0, accentMaterials.Length)];
        }
        else
        {
            accentMat = baseMat;
        }

        foreach (GameObject part in baseParts)
        {
            part.GetComponent<Renderer>().material = baseMat;
        }

        foreach (GameObject part in accentParts)
        {
            if (accentsAlsoDifferent && Random.Range(0, 2) == 1)
            {
                part.GetComponent<Renderer>().material = accentMat;
            }
            else if (differentAccent)
            {
                part.GetComponent<Renderer>().material = accentMat;
            } else
            {
                part.GetComponent<Renderer>().material = baseMat;
            }
        }

    }
}
