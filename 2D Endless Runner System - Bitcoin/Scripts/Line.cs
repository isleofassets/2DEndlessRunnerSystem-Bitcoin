using UnityEngine;
using System.Collections;

[HelpURL("https://assetstore.unity.com/packages/slug/225659")]
[RequireComponent(typeof(LineRenderer))]
public class Line : MonoBehaviour
{
    public static float edgeX;

    [SerializeField]
    private float garbageCollectorDelay;

    private LineRenderer lineRenderer;

    /// <summary>
    /// Adding a new point to the player's trail
    /// </summary>
    public void SetNewPoint()
    {
        Vector3[] mass = new Vector3[lineRenderer.positionCount + 1];
        for (int i = 0; i < lineRenderer.positionCount; i++)
            mass[i] = lineRenderer.GetPosition(i);
        mass[lineRenderer.positionCount] = new Vector3(-1f, transform.position.y, 0f);
        lineRenderer.positionCount = mass.Length;
        lineRenderer.SetPositions(mass);
    }

    /// <summary>
    /// Clearing traces behind the screen borders
    /// </summary>
    private IEnumerator GarbageCollector()
    {
        WaitForSeconds delay = new WaitForSeconds(garbageCollectorDelay);
        while (true)
        {
            yield return delay;
            while (lineRenderer.GetPosition(0).x < edgeX && lineRenderer.GetPosition(1).x < edgeX)
            {
                Vector3[] mass = new Vector3[lineRenderer.positionCount - 1];
                for (int i = 0; i < mass.Length; i++)
                    mass[i] = lineRenderer.GetPosition(i + 1);
                lineRenderer.positionCount = mass.Length;
                lineRenderer.SetPositions(mass);
            }
        }
    }

    /// <summary>
    /// Calculation of the point at which the trail from the player will be cleared, starting the Garbage Collector
    /// </summary>
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        edgeX = -4.5f * Screen.width / Screen.height;
    }

    /// <summary>
    /// Caching of components
    /// </summary>
    private void Start()
    {
        StartCoroutine(GarbageCollector());
    }

    /// <summary>
    /// Movement of the player's trail
    /// </summary>
    private void Update()
    {
        Vector3[] mass = new Vector3[lineRenderer.positionCount];
        for (int i = 0; i < lineRenderer.positionCount - 1; i++)
            mass[i] = lineRenderer.GetPosition(i) - Vector3.right * Player._instance.speedX * 0.3f * Time.deltaTime;
        mass[lineRenderer.positionCount - 1] = new Vector3(-1f, transform.position.y, 0f);
        lineRenderer.SetPositions(mass);
    }
}