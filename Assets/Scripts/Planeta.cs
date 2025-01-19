using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Planeta : MonoBehaviour
{
    public Transform Planet;
    [SerializeField] protected Transform[] Moons;

    protected float gravitationalConstant = 0.5f;
    protected float planetMass = 1000f;

    protected Vector3[] moonVelocities;

    // Start is called before the first frame update
    private void Start()
    {
        moonVelocities = new Vector3[Moons.Length];
        InitializeOrbit();
    }

    protected abstract void InitializeOrbit();

    private void Update()
    {
        for (int i = 0; i < Moons.Length; i++)
        {
            Transform MoonTransform = Moons[i]; // Each moon's Transform
            Moon moonScript = MoonTransform.GetComponent<Moon>(); // Access Moon script

            if (moonScript != null)
            {
                float moonMass = moonScript.moonMass;             // Access individual moon's mass
                float initialOrbitSpeed = moonScript.initialOrbitSpeed; // Access individual moon's orbit speed
                float stabilizingSpeed = moonScript.stabilizingSpeed; // Access individual moon's stabilizing speed

                // Direction to the planet
                Vector3 directionToPlanet = Planet.position - MoonTransform.position;
                float distance = directionToPlanet.magnitude;

                // Clamping the distance to avoid extreme values
                distance = Mathf.Clamp(distance, min: 2f, max: 50f);

                // Gravitational force calculation
                float forceMagnitude = gravitationalConstant * (moonMass * planetMass) / (distance * distance);
                Vector3 gravitationalForce = directionToPlanet.normalized * forceMagnitude;

                // Update moon's velocity
                moonVelocities[i] += gravitationalForce * Time.deltaTime;

                // Calculate desired velocity to keep it in orbit
                Vector3 desiredVelocity = Vector3.Cross(directionToPlanet.normalized, Vector3.up) * initialOrbitSpeed;
                moonVelocities[i] = Vector3.Lerp(moonVelocities[i], desiredVelocity, stabilizingSpeed * Time.deltaTime);

                // Ensure that the Y component is zero for a 2D orbit effect
                moonVelocities[i].y = 0;

                // Update the moon's position
                MoonTransform.position += moonVelocities[i] * Time.deltaTime;

                // Debugging lines and rays
                Debug.DrawLine(MoonTransform.position, Planet.position, Color.green);
                Debug.DrawRay(MoonTransform.position, moonVelocities[i], Color.red);
            }
            else
            {
                Debug.LogError("Moon script not found on moon object at index " + i);
            }
        }
    }
}
