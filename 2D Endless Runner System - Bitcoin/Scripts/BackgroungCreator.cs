using UnityEngine;

[HelpURL("https://assetstore.unity.com/packages/slug/225659")]
public class BackgroungCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private Vector2 cellSize;

    /// <summary>
    /// Drawing the background
    /// </summary>
    private void Start()
    {
        // creating horizontal lines
        for (float i = -5f; i <= 5f; i += cellSize.y)
            Instantiate(prefab, Vector3.up * i, Quaternion.identity);
        // creating vertical lines
        for (float i = Line.edgeX - cellSize.x; i < -Line.edgeX + cellSize.x; i += cellSize.x)
            Instantiate(prefab, Vector3.right * i, Quaternion.Euler(0f, 0f, 90f));
    }
}