using UnityEngine;
using UnityEngine.InputSystem;

public class HandleTouchInput : MonoBehaviour
{
    // Reference to the generated Input Action class
    private TouchInput touchInput;
    private Camera mainCamera;

    void Awake()
    {
        // Initialize the Input Action instance
        touchInput = new TouchInput();
    }

    void OnEnable()
    {
        // Enable the TouchPosition action
        touchInput.Touch.TouchPosition.Enable();
    }

    void OnDisable()
    {
        // Disable the TouchPosition action
        touchInput.Touch.TouchPosition.Disable();
    }

    void Start()
    {
        // Cache the main camera reference
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Get the touch position from the Input Action
        Vector2 touchPosition = touchInput.Touch.TouchPosition.ReadValue<Vector2>();
        
        int excludedLayerMask = ~LayerMask.GetMask("Static", "Bomb");

        // Check if there is an active touch
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            // Create a ray from the camera through the touch position
            Ray ray = mainCamera.ScreenPointToRay(touchPosition);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, excludedLayerMask))
            {
                GeneralGameObject currentGeneralGameObject = hit.transform.GetComponent<GeneralGameObject>();

                currentGeneralGameObject.GameObjectTouched();
            } else if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Bomb")))
            {
            
                BombPrefab bombPrefabScript = hit.transform.GetComponent<BombPrefab>();

                bombPrefabScript.GameObjectTouched();

            }

        }
    }
}
