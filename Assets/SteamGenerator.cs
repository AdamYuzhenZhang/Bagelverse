using UnityEngine;

public class SteamGenerator : MonoBehaviour
{
    [Header("Particle Settings")]
    [SerializeField] private int maxParticles = 100;
    [SerializeField] private float emissionRate = 10f;
    [SerializeField] private Vector2 particleLifetime = new Vector2(2f, 3f);
    [SerializeField] private Vector2 particleSize = new Vector2(0.2f, 0.4f);
    [SerializeField] private float startSpeed = 1f;
    [SerializeField] private Color steamColor = Color.white;

    [Header("Steam Behavior")]
    [SerializeField] private float spreadAngle = 15f;
    [SerializeField] private float fadeSpeed = 0.5f;

    private ParticleSystem steamParticles;
    private ParticleSystem.MainModule mainModule;
    private ParticleSystem.EmissionModule emissionModule;

    private void Awake()
    {
        InitializeParticleSystem();
        ConfigureParticleSystem();
    }

    private void InitializeParticleSystem()
    {
        steamParticles = gameObject.AddComponent<ParticleSystem>();
        mainModule = steamParticles.main;
        emissionModule = steamParticles.emission;
    }

    private void ConfigureParticleSystem()
    {
        // Main module configuration
        mainModule.maxParticles = maxParticles;
        mainModule.startLifetime = new ParticleSystem.MinMaxCurve(particleLifetime.x, particleLifetime.y);
        mainModule.startSize = new ParticleSystem.MinMaxCurve(particleSize.x, particleSize.y);
        mainModule.startSpeed = startSpeed;
        mainModule.loop = true;
        mainModule.simulationSpace = ParticleSystemSimulationSpace.World;
        mainModule.startColor = steamColor;

        // Emission module configuration
        emissionModule.rateOverTime = emissionRate;

        // Shape module configuration
        var shape = steamParticles.shape;
        shape.angle = spreadAngle;
        shape.shapeType = ParticleSystemShapeType.Cone;

        // Color module configuration
        var colorOverLifetime = steamParticles.colorOverLifetime;
        colorOverLifetime.enabled = true;

        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(steamColor, 0.0f),
                new GradientColorKey(steamColor, 1.0f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(0.3f, 0.0f),
                new GradientAlphaKey(0.0f, 1.0f)
            }
        );

        colorOverLifetime.color = gradient;

        SteamOff();
    }

    public void SteamOn()
    {
        steamParticles.Play();
    }

    public void SteamOff()
    {
        steamParticles.Stop();
    }

    public bool IsSteamActive()
    {
        return steamParticles.isPlaying;
    }

    public void SetSteamColor(Color newColor)
    {
        steamColor = newColor;
        mainModule.startColor = steamColor;
        ConfigureParticleSystem();
    }
}