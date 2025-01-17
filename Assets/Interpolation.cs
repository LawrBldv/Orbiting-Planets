using UnityEngine;

public class Interpolation : MonoBehaviour
{
    private Vector3 pointA, pointB;
    private float elapsedTime;
    [SerializeField] private float desiredDuration = 3f;
    [SerializeField] private AnimationCurve curve;

    private void Start()
    {
        pointA = transform.position;
        pointB = pointA + new Vector3(10f, 0f, 0f); // Example end point
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / desiredDuration;
            transform.position = Vector3.Lerp(pointA, pointB, curve.Evaluate(t));
        }

        if (Input.GetKey(KeyCode.C))
        {
            elapsedTime += Time.deltaTime;
            elapsedTime = Mathf.Clamp(elapsedTime, 0f, desiredDuration);

            Debug.Log($"Clamped Value: {elapsedTime}");
        }

        if (Input.GetKey(KeyCode.R))
        {
            elapsedTime = 0;
            transform.position = pointA;
        }
    }
}
