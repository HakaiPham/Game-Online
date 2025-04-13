using UnityEngine;

public class ButtonHoverParticle : MonoBehaviour
{
    public ParticleSystem hoverParticle;

    public Color color1 = Color.cyan;
    public Color color2 = Color.magenta;
    public float colorChangeSpeed = 3f;

    private ParticleSystem.MainModule mainModule;
    private bool isPlaying = false;

    void Start()
    {
        if (hoverParticle != null)
        {
            mainModule = hoverParticle.main;
        }
    }

    void Update()
    {
        if (isPlaying)
        {
            float t = Mathf.PingPong(Time.time * colorChangeSpeed, 1f);
            Color currentColor = Color.Lerp(color1, color2, t);
            mainModule.startColor = currentColor;
        }
    }

    public void PlayAtPosition(Vector3 position)
    {
        if (hoverParticle != null)
        {
            hoverParticle.transform.position = position;
            hoverParticle.Play();
            isPlaying = true;
        }
    }

    public void Stop()
    {
        if (hoverParticle != null)
        {
            hoverParticle.Stop();
            isPlaying = false;
        }
    }
}
