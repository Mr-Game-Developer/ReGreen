using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class VirtualTrackpad : MonoBehaviour , IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public float movementSensitivityX = 1.0f; // Adjusts sensitivity of X-axis movement
    public float movementSensitivityY = 1.0f; // Adjusts sensitivity of Y-axis movement
    public RectTransform trackpadArea; // Reference to the UI element representing the trackpad
    private PlayerInput _playerInput;
    private Vector2 startDragPosition;
    private bool isDragging = false;
    private bool mobile = false;
    private CinemachineFreeLook TouchControl;


    private void Start()
    {
        TouchControl = FindObjectOfType<CinemachineFreeLook>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(trackpadArea, eventData.position))
        {
            isDragging = true;
            mobile = true;
            startDragPosition = eventData.position;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging && eventData.pointerDrag.transform.right != Vector3.zero)
        {
            Vector2 delta = eventData.position - startDragPosition;

            // Clamp delta to prevent excessive camera rotation (optional)
            delta.x = Mathf.Clamp(delta.x, -trackpadArea.rect.width / 2, trackpadArea.rect.width / 2);

            // Separate X and Y updates using Cinemachine's damping (consider adjusting damping values)
            TouchControl.m_XAxis.Value = delta.x * movementSensitivityX * Time.deltaTime;
            
            startDragPosition = eventData.position;
        }

        if (isDragging && eventData.pointerDrag.transform.right == Vector3.zero)
        {
            Vector2 delta = eventData.position - startDragPosition;

            delta.y = Mathf.Clamp(delta.y, -trackpadArea.rect.width / 2, trackpadArea.rect.width / 2);
            TouchControl.m_YAxis.Value = -(delta.y * movementSensitivityY * Time.deltaTime)/100;
        }
    }
}