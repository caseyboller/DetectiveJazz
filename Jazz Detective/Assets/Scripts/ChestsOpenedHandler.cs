using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChestsOpenedHandler : MonoBehaviour
{

    public Dictionary<ChestDetails, bool> chests = new Dictionary<ChestDetails, bool>();

    public MoveToCenter note;


    private TextMeshProUGUI textUI;

    // Start is called before the first frame update
    void Start()
    {
        textUI = this.gameObject.GetComponent<TextMeshProUGUI>();
        GameObject[] chestsArr = GameObject.FindGameObjectsWithTag("Chest");
        foreach (var chest in chestsArr)
        {
            chests.Add(chest.GetComponent<ChestDetails>(), false);
        }
        UpdateText();
    }

    public void OpenChest(ChestDetails chest)
    {
        if (chests.ContainsKey(chest))
        {
            if (chests[chest] == false)
            {
                chests[chest] = true;
                UpdateText();
                ShowNote(chest);
                StartCoroutine(ActivateItem(chest));
            }
        }
    }

    private void ShowNote(ChestDetails chest)
    {
        note.OpenClose(chest);
    }

    private void UpdateText()
    {
        int chestsOpened = 0;
        foreach (var val in chests.Values)
        {
            if (val == true)
            {
                chestsOpened++;
            }
        }
        textUI.text = "chests opened: " + chestsOpened + "/" + chests.Keys.Count;
    }

    IEnumerator ActivateItem(ChestDetails chest)
    {
        yield return new WaitForSeconds(3);
        if (chest.item != null)
        {
            chest.item.SetActive(true);
        }
        
    }
}
