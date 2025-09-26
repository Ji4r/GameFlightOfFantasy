using UnityEngine;

public class WindowSizeController : MonoBehaviour
{
    public static WindowSizeController instance;

    [SerializeField] private int minWidth = 660;
    [SerializeField] private int minHeight = 360;
    [SerializeField] private int maxWidth = 3840;
    [SerializeField] private int maxHeight = 2160;

    private int lastWidth;
    private int lastHeight;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        lastWidth = Screen.width;
        lastHeight = Screen.height;

        // Применяем ограничения при старте
        ApplySizeConstraints();
    }

    void Update()
    {
        // Проверяем изменение размера каждые 5 кадров (оптимизация)
        if (Time.frameCount % 5 == 0 &&
            (Screen.width != lastWidth || Screen.height != lastHeight))
        {
            ApplySizeConstraints();
            lastWidth = Screen.width;
            lastHeight = Screen.height;
        }
    }

    private void ApplySizeConstraints()
    {
        int currentWidth = Screen.width;
        int currentHeight = Screen.height;

        if (currentWidth < minWidth || currentHeight < minHeight ||
            currentWidth > maxWidth || currentHeight > maxHeight)
        {
            int newWidth = Mathf.Clamp(currentWidth, minWidth, maxWidth);
            int newHeight = Mathf.Clamp(currentHeight, minHeight, maxHeight);

            Screen.SetResolution(newWidth, newHeight, FullScreenMode.Windowed);
            Debug.Log($"Window size constrained to: {newWidth}x{newHeight}");
        }
    }
}
