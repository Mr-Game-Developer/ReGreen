using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;  // Reference to the Cinemachine FreeLook camera
    public RectTransform touchPanel;            // The UI panel that will capture touch input
    public float xSensitivity = 0.5f;           // Sensitivity for horizontal rotation
    public float ySensitivity = 0.5f;           // Sensitivity for vertical rotation
    public float dampingFactor = 0.1f;          // Damping for smooth camera movement

    private EventSystem eventSystem;

    private void Start()
    {
        // Initialize the EventSystem to handle UI interactions
        eventSystem = EventSystem.current;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);  // Get the first touch (single finger control)

            // Check if the touch is moving
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchPositionUI;

                // Convert touch position to the local space of the touch panel
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(touchPanel, touch.position, null, out touchPositionUI))
                {
                    // Check if the touch is within the bounds of the touch panel
                    if (touchPanel.rect.Contains(touchPositionUI))
                    {
                        // Get the delta of the touch movement (how much the touch has moved since the last frame)
                        Vector2 delta = touch.deltaPosition;

                        // Calculate the target X and Y axis rotation values based on the touch movement
                        float targetX = freeLookCamera.m_XAxis.Value + delta.x * xSensitivity;
                        float targetY = freeLookCamera.m_YAxis.Value - delta.y * ySensitivity / 100f;

                        // Clamp the vertical rotation (Y-axis) to prevent over-rotation
                        targetY = Mathf.Clamp(targetY, freeLookCamera.m_YAxis.m_MinValue, freeLookCamera.m_YAxis.m_MaxValue);

                        // Smoothly transition to the target rotation using the damping factor
                        freeLookCamera.m_XAxis.Value = Mathf.Lerp(freeLookCamera.m_XAxis.Value, targetX, dampingFactor);
                        freeLookCamera.m_YAxis.Value = Mathf.Lerp(freeLookCamera.m_YAxis.Value, targetY, dampingFactor);
                    }
                }
            }
        }
    }
}
