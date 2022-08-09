using UnityEngine;

[HelpURL("https://assetstore.unity.com/packages/slug/225659")]
public class Player : MonoBehaviour
{
    public static Player _instance { get; private set; }

    [HideInInspector]
    public float speedX = 4f;

    [HideInInspector]
    public int direction;

    [SerializeField]
    private Touch touch;

    [SerializeField]
    private Animation camAnim;

    [SerializeField]
    private float acceleration, floorY, ceilingY, speedY, dependenceSpeedXYRatio;

    /// <summary>
    /// Realisation of the Singleton pattern, which guarantees that there will be a single instance of this class
    /// </summary>
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Acceleration of the player and checking for reaching the upper and lower limits
    /// </summary>
    private void Update()
    {
        if (direction != 0)
            speedX += acceleration * Time.deltaTime;
        transform.position += Vector3.up * (direction * speedY + speedX * direction * dependenceSpeedXYRatio) * Time.deltaTime;
        if (transform.position.y < floorY || transform.position.y > ceilingY)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, floorY, ceilingY), 0);
            touch.OnPointerDown(null);
            camAnim.Play();
        }
    }
}