using System.Collections;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    [SerializeField] private Transform[] targets; // Array of targets for the camera to look at
    [SerializeField] private float[] distances; // Array of distances corresponding to each target
    [SerializeField] private float transitionSpeed = 2f; // Speed of the camera transition
    [SerializeField] private AudioClip moveSound; // Sound to play during transitions

    private AudioSource audioSource;
    private int currentIndex = 0;
    private bool isTransitioning = false;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = moveSound;
    }

    // Update is called once per frame
    void Update()
    {
        if (targets.Length == 0 || distances.Length == 0 || targets.Length != distances.Length)
        {
            Debug.LogWarning("Ensure targets and distances arrays are properly assigned and have the same length.");
            return;
        }

        if (Input.GetKeyDown(KeyCode.A) && !isTransitioning)
        {
            currentIndex = (currentIndex - 1 + targets.Length) % targets.Length;
            StartCoroutine(MoveCamera());
        }
        else if (Input.GetKeyDown(KeyCode.D) && !isTransitioning)
        {
            currentIndex = (currentIndex + 1) % targets.Length;
            StartCoroutine(MoveCamera());
        }
    }

    private IEnumerator MoveCamera()
    {
        isTransitioning = true;

        // Play sound effect
        audioSource.Play();

        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        Vector3 direction = (transform.position - targets[currentIndex].position).normalized;
        Vector3 targetPosition = targets[currentIndex].position + direction * distances[currentIndex];
        Quaternion targetRotation = Quaternion.LookRotation(targets[currentIndex].position - targetPosition);

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * transitionSpeed;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }

        isTransitioning = false;
    }
}
