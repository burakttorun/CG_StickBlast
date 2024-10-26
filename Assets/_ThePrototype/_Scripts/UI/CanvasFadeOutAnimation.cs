using UnityEngine;

public class CanvasFadeOutAnimation : MonoBehaviour
{
    [SerializeField] private float duration = 1.0f;
    [SerializeField] private float moveAmount = 50f;

    public CanvasGroup canvasGroup;
    public Vector3 initialPosition;
    public float elapsedTime;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    void OnEnable()
    {
        elapsedTime = 0f;
        canvasGroup.alpha = 1f;
        transform.position = initialPosition;
        StartCoroutine(FadeOutAndMoveUp());
    }

    private System.Collections.IEnumerator FadeOutAndMoveUp()
    {
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            canvasGroup.alpha = Mathf.Lerp(1, 0, t);
            transform.position = Vector3.Lerp(initialPosition, initialPosition + Vector3.up * moveAmount, t);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}