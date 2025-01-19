using UnityEngine;

public class MoonOrbit : MonoBehaviour
{
    public Transform Earth, Moon;

    [SerializeField] private float gravitationalConstant = 0.1f;
    [SerializeField] private float moonMass = 1f;
    [SerializeField] private float earthMass = 1000f;
    [SerializeField] private float initialOrbitSpeed = 5f;
    [SerializeField] private float stabilizingSpeed = 0.1f;
    [SerializeField] private Vector3 orbitPlaneNormal = Vector3.up; // Customizable orbit plane

    private Vector3 moonVelocity;

    private void Start()
    {
        Vector3 directionToEarth = (Earth.position - Moon.position).normalized;
        moonVelocity = Vector3.Cross(directionToEarth, orbitPlaneNormal).normalized * initialOrbitSpeed;
    }

    private void Update()
    {
        Vector3 directionToEarth = Earth.position - Moon.position;
        float distance = directionToEarth.magnitude;

        distance = Mathf.Clamp(distance, 2f, 50f);

        float forceMagnitude = gravitationalConstant * (moonMass * earthMass) / (distance * distance);
        Vector3 gravitationalForce = directionToEarth.normalized * forceMagnitude;

        moonVelocity += gravitationalForce * Time.deltaTime;

        Vector3 desiredVelocity = Vector3.Cross(directionToEarth.normalized, orbitPlaneNormal).normalized * moonVelocity.magnitude;
        moonVelocity = Vector3.Lerp(moonVelocity, desiredVelocity, Time.deltaTime * stabilizingSpeed);

        moonVelocity = Vector3.ProjectOnPlane(moonVelocity, orbitPlaneNormal); // Constrain to orbit plane
        Moon.position += moonVelocity * Time.deltaTime;

        Debug.DrawLine(Moon.position, Earth.position, Color.green);
        Debug.DrawRay(Moon.position, moonVelocity, Color.red);
    }
}
