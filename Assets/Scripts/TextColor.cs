using UnityEngine;
using UnityEngine.UI;

public class RainbowColorCycle : MonoBehaviour
{
    public Image[] letters; // G, Dot1, O, Dot2, D, 2

    [Range(0.1f, 5f)]
    public float colorChangeSpeed = 1f;

    private Color[] rainbowColors = new Color[]
    {
        Color.red,
        new Color(1f, 0.5f, 0f), // Orange
        Color.yellow,
        Color.green,
        Color.cyan,
        Color.blue,
        new Color(0.6f, 0f, 1f) // Violet
    };

    private int currentColorIndex = 0;
    private int nextColorIndex = 1;
    private float t = 0f;

    void Update()
    {
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
    }
}
