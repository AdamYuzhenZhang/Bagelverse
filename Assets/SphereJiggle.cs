using UnityEngine;

public class SphereJiggle : MonoBehaviour
{
    [Header("Jiggle Settings")]
    [SerializeField] private float frequency = 10f;
    [SerializeField] private float magnitude = 0.1f;
    [SerializeField] private float rotationSpeed = 50f;

    private Vector3 startPosition;
    private float timeOffset;
    private bool isJiggling = false;

    private void Start()
    {
        startPosition = transform.position;
        timeOffset = Random.Range(0f, 100f);
    }

    private void Update()
    {
        if (!isJiggling) return;

        float time = Time.time + timeOffset;

        // Vertical-only jiggle
        float yOffset = Mathf.Sin(time * frequency) * magnitude;
        transform.position = startPosition + new Vector3(0, yOffset, 0);

        // Rotation jiggle
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    public void StartJiggle()
    {
        isJiggling = true;
    }

    public void StopJiggle()
    {
        isJiggling = false;
        transform.position = startPosition;
    }

    public bool IsJiggling()
    {
        return isJiggling;
    }
}