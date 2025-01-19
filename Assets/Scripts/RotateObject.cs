using UnityEngine;

public class RotateObject : MonoBehaviour
{

    [SerializeField]
    private float ySpeed = 15f; // Rotation speed around the Y-axis

    void Update()
    {
        float yRotation = ySpeed * Time.deltaTime;

        // Apply rotation
        transform.Rotate(0, yRotation, 0);

        RenderSettings.skybox.SetFloat("_Rotation", Time.time * -0.5f);
    }
}