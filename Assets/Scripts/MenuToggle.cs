using UnityEngine;
using UnityEngine.InputSystem;

public class MenuToggle : MonoBehaviour
{
    [SerializeField] private RectTransform uiPanel;

    private RectTransform rectTransform;
    private Vector2 initialPosition;
    private Vector2 hiddenPosition;
    private bool isVisible = false;
    private Vector2 hiddenPositionOffset = new Vector2(15, 15);

    void Start()
    {
        if (uiPanel == null)
        {
            Debug.LogError("Menu Controls is not found");
            return;
        }

        initialPosition = uiPanel.anchoredPosition;
        hiddenPosition = hiddenPositionOffset;

        uiPanel.anchoredPosition = hiddenPosition;
    }

    public void OnShowControls()
    {
        if ( uiPanel == null) return;

        Debug.Log("Show Controls triggered");
        isVisible = !isVisible;
        uiPanel.anchoredPosition = isVisible ? initialPosition : hiddenPosition;
    }
}
