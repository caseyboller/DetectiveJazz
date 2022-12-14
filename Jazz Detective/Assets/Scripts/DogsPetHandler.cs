using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DogsPetHandler : MonoBehaviour
{

    public Dictionary<GameObject, bool> dogs = new Dictionary<GameObject, bool>();
    

    private TextMeshProUGUI textUI;

    // Start is called before the first frame update
    void Start()
    {
        textUI = this.gameObject.GetComponent<TextMeshProUGUI>();
        GameObject[] dogsArr = GameObject.FindGameObjectsWithTag("Dog");
        foreach (var dog in dogsArr) {
            dogs.Add(dog, false);
        }
        UpdateText();
    }

    public void ToggleHelp(bool enable)
    {
        foreach (var pair in dogs)
        {
            if (pair.Value == false)
            {
                pair.Key.GetComponentInChildren<AnimalWander>()
                    .helpExclamation.SetActive(enable);
            }
        }
    }

    public void PetDog(GameObject dog)
    {
        Debug.Log("Pet dog  " + dog.name);
        if (dogs.ContainsKey(dog))
        {
            dogs[dog] = true;
            UpdateText();
            Debug.Log("Pet dog set true " + dog.name);
        }
    }

    private void UpdateText()
    {
        int dogsPet = 0;
        foreach (var val in dogs.Values)
        {
            if (val == true)
            {
                dogsPet++;
            }
        }
        Debug.Log("Dogs pet: " + dogsPet);
        textUI.text = "dogs pet: " + dogsPet + "/" + dogs.Keys.Count;
    }
}
