using UnityEngine;
using UnityEngine.UI;

public class MoveToCenter : MonoBehaviour
{
    public float delay = 0.0f; // The delay before the movement starts
    public float duration = 0.5f; // The duration of the movement

    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - Screen.height - 500, transform.position.z); 

        // Get the current position of the UI element
        Vector3 currentPos = transform.position;

        // Calculate the new position for the UI element in the center of the screen
        Vector3 newPos = new Vector3(Screen.width / 2, Screen.height / 2, currentPos.z);

        // Use LeanTween to smoothly move the UI element along the curved path
        LeanTween.move(gameObject, newPos, duration)
                 .setDelay(delay)
                 .setEase(LeanTweenType.easeInOutSine);
    }
}
