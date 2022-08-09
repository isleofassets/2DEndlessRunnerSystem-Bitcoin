using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[HelpURL("https://assetstore.unity.com/packages/slug/225659")]
[RequireComponent(typeof(AudioSource))]
public class Touch : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private AudioClip clickClip, collisionClip;

    [SerializeField]
    private Line line;

    [SerializeField]
    private float delay;

    private AudioSource audioSource;
    private bool isCanChangeDirection = true;

    /// <summary>
    /// Changing the direction of movement when you tap on the screen
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isCanChangeDirection)
            return;
        Player._instance.direction = Player._instance.direction == 1 ? -1 : 1;
        line.SetNewPoint();
        StartCoroutine(DoubleTapProtector());
        audioSource.PlayOneShot(eventData == null ? collisionClip : clickClip);
    }

    /// <summary>
    /// Double-tap protection
    /// </summary>
    private IEnumerator DoubleTapProtector()
    {
        isCanChangeDirection = false;
        yield return new WaitForSeconds(delay);
        isCanChangeDirection = true;
    }

    /// <summary>
    /// Caching of components
    /// </summary>
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}