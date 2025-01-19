using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neptune : Planeta
{
    // Override InitializeOrbit method from PlanetGravity
    protected override void InitializeOrbit()
    {
        gravitationalConstant = 2f;  // Override default value if needed
        planetMass = 1250f;            // Speed and rate of attraction (planet mass)
        
        for (int i = 0; i < Moons.Length; i++)
        {
            if (Moons[i] == null)
            {
                Debug.LogError("Moon reference is null at index " + i);
                continue;
            }

            Transform MoonTransform = Moons[i];  // Get moon's transform
            Moon moonScript = MoonTransform.GetComponent<Moon>(); // Access the Moon script on the moon object

            if (moonScript != null)
            {
                float moonMass = moonScript.moonMass;               // Access individual moon's mass
                float initialOrbitSpeed = moonScript.initialOrbitSpeed; // Access individual moon's orbit speed
                float stabilizingSpeed = moonScript.stabilizingSpeed; // Access individual moon's stabilizing speed

                // Calculate the direction to the planet and its initial velocity
                Vector3 directionToPlanet = (Planet.position - MoonTransform.position).normalized;
                Vector3 initialVelocity = Vector3.Cross(directionToPlanet, Vector3.up) * initialOrbitSpeed;
                moonVelocities[i] = initialVelocity;  // Store the initial velocity for this moon
            }
            else
            {
                Debug.LogError("Moon script not found on moon object at index " + i);
            }
        }
    }
    void LateUpdate(){
    float yRotation = -5 * Time.deltaTime;
        // Apply rotation
        transform.Rotate(0, yRotation, 0);
    }
}
