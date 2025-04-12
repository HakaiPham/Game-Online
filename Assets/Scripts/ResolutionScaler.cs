using UnityEngine;

public class ResolutionScaler : MonoBehaviour
{
    public int targetWidth = 1280;
    public int targetHeight = 720;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        SetResolution();
    }

    void SetResolution()
    {
        // Tính tỉ lệ chuẩn
        float targetAspect = (float)targetWidth / targetHeight;
        float windowAspect = (float)Screen.width / Screen.height;

        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            // Nếu màn hình hẹp hơn tỉ lệ chuẩn → bo viền trên/dưới
            Rect rect = cam.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            cam.rect = rect;
        }
        else
        {
            // Nếu màn hình rộng hơn tỉ lệ chuẩn → bo viền trái/phải
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = cam.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            cam.rect = rect;
        }
    }
}
