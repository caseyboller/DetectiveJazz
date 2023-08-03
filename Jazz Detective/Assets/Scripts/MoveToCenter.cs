using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveToCenter : MonoBehaviour
{
    public float delay = 0.0f; // The delay before the movement starts
    public float duration = 0.5f; // The duration of the movement
    public TextMeshProUGUI text;
    public Vector3 startPos = new Vector3(0, -500, 0);

    public AudioSource audioSource;
    public AudioClip[] audioClips;

    public bool isShowing;

    private void Start()
    {
        startPos = new Vector3(0, -Screen.height, 0);
        transform.position = startPos;
    }

    public void OpenClose(ChestDetails chest)
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        isShowing = true;
        gameObject.SetActive(true);
        text.text = chest.text.text;

        transform.position = startPos;

        // Calculate the new position for the UI element in the center of the screen
        Vector3 newPos = new Vector3(Screen.width / 2, Screen.height / 2, startPos.z);

        audioSource.PlayOneShot(audioClips[0]);

        // Use LeanTween to smoothly move the UI element along the curved path
        LeanTween.move(gameObject, newPos, duration)
                 .setDelay(delay)
                 .setEase(LeanTweenType.easeInOutSine);
    }

    public void MoveOut()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (!isShowing)
        {
            return;
        }
        audioSource.PlayOneShot(audioClips[1]);

        isShowing = false;
        // Use LeanTween to smoothly move the UI element along the curved path
        LeanTween.move(gameObject, startPos, duration)
                 .setDelay(delay)
                 .setEase(LeanTweenType.easeInOutSine)
                 .setOnComplete(Deactivate);
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
