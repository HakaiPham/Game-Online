using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ButtonHoverParticle particleManager;
    public Color colorA = Color.cyan;
    public Color colorB = new Color(1f, 0.4f, 0.7f);
    public float colorChangeSpeed = 2f;

    private Image buttonImage;
    private Coroutine colorCoroutine;

    void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (particleManager != null)
            particleManager.PlayAtPosition(transform.position);

        colorCoroutine = StartCoroutine(ChangeColorLoop());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (particleManager != null)
            particleManager.Stop();

        if (colorCoroutine != null)
            StopCoroutine(colorCoroutine);

        buttonImage.color = Color.white;
    }

    IEnumerator ChangeColorLoop()
    {
        float t = 0f;
        while (true)
        {
            t += Time.deltaTime * colorChangeSpeed;
            buttonImage.color = Color.Lerp(colorA, colorB, Mathf.PingPong(t, 1));
            yield return null;
        }
    }
}
