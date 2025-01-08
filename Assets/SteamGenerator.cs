using UnityEngine;

public class SteamGenerator : MonoBehaviour
{
    void Start()
    {
        // Create a new GameObject for the Particle System
        GameObject particleSystemObject = new GameObject("SteamParticleSystem");

        // Add a Particle System component to the GameObject
        ParticleSystem ps = particleSystemObject.AddComponent<ParticleSystem>();

        // Access the Particle System's main module
        var main = ps.main;
        main.duration = 5f;
        main.loop = true;
        main.startLifetime = new ParticleSystem.MinMaxCurve(2f, 5f); // Random between 2 and 5 seconds
        main.startSpeed = new ParticleSystem.MinMaxCurve(1f, 3f); // Random between 1 and 3
        main.startSize = new ParticleSystem.MinMaxCurve(0.2f, 0.5f); // Random size
        main.startColor = new Color(0.8f, 0.8f, 0.8f, 0.5f); // Light gray with transparency
        main.simulationSpace = ParticleSystemSimulationSpace.World;

        // Configure Emission
        var emission = ps.emission;
        emission.rateOverTime = 20f; // Particles per second

        // Configure Shape
        var shape = ps.shape;
        shape.shapeType = ParticleSystemShapeType.Cone;
        shape.angle = 15f;
        shape.radius = 0.3f;

        // Configure Velocity over Lifetime
        var velocityOverLifetime = ps.velocityOverLifetime;
        velocityOverLifetime.enabled = true;

        // Use a constant value for Y velocity
        velocityOverLifetime.y = new ParticleSystem.MinMaxCurve(1f, 2f); // Random between 1 and 2

        // Set X and Z velocity explicitly to constants (or zero if no movement is needed)
        velocityOverLifetime.x = new ParticleSystem.MinMaxCurve(0f); // No movement in X
        velocityOverLifetime.z = new ParticleSystem.MinMaxCurve(0f); // No movement in Z

        // Configure Color over Lifetime
        var colorOverLifetime = ps.colorOverLifetime;
        colorOverLifetime.enabled = true;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(new Color(0.8f, 0.8f, 0.8f, 1f), 0f),
                new GradientColorKey(new Color(0.8f, 0.8f, 0.8f, 0f), 1f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1f, 0f),
                new GradientAlphaKey(0f, 1f)
            }
        );
        colorOverLifetime.color = new ParticleSystem.MinMaxGradient(gradient);

        // Configure Size over Lifetime
        var sizeOverLifetime = ps.sizeOverLifetime;
        sizeOverLifetime.enabled = true;
        sizeOverLifetime.size = new ParticleSystem.MinMaxCurve(0.5f, 1f);

        // Configure Noise
        var noise = ps.noise;
        noise.enabled = true;
        noise.strength = 0.2f;
        noise.frequency = 1f;

        // Optional: Assign material to renderer
        var renderer = ps.GetComponent<ParticleSystemRenderer>();
        renderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
        renderer.material.color = new Color(0.8f, 0.8f, 0.8f, 0.5f);

        // Set the position of the particle system
        particleSystemObject.transform.position = new Vector3(0, 0, 0);
    }
}
