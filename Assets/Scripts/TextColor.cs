using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RainbowColorCycle : MonoBehaviour
{
    public Image[] letters;
    public ParticleSystem topLeftParticle;
    public ParticleSystem bottomRightParticle;

    [Range(0.1f, 5f)]
    public float colorChangeSpeed = 1f;

    private Color[] rainbowColors = new Color[]
    {
        Color.red,
        new Color(1f, 0.5f, 0f),
        Color.yellow,
        Color.green,
        Color.cyan,
        Color.blue,
        new Color(0.6f, 0f, 1f)
    };

    private int currentColorIndex = 0;
    private int nextColorIndex = 1;
    private float t = 0f;

    void Update()
    {
        // Nếu không ở Menu scene thì tắt particle và return
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            if (topLeftParticle != null && topLeftParticle.isPlaying)
                topLeftParticle.Stop();

            if (bottomRightParticle != null && bottomRightParticle.isPlaying)
                bottomRightParticle.Stop();

            return;
        }

        t += Time.deltaTime * colorChangeSpeed;

        if (t > 1f)
        {
            t = 0f;
            currentColorIndex = nextColorIndex;
            nextColorIndex = (nextColorIndex + 1) % rainbowColors.Length;
        }

        Color lerpedColor = Color.Lerp(rainbowColors[currentColorIndex], rainbowColors[nextColorIndex], t);

        foreach (var img in letters)
        {
            if (img != null)
                img.color = lerpedColor;
        }

        if (topLeftParticle != null)
        {
            var main = topLeftParticle.main;
            main.startColor = lerpedColor;
            if (!topLeftParticle.isPlaying) topLeftParticle.Play();
        }

        if (bottomRightParticle != null)
        {
            var main = bottomRightParticle.main;
            main.startColor = lerpedColor;
            if (!bottomRightParticle.isPlaying) bottomRightParticle.Play();
        }
    }
}
