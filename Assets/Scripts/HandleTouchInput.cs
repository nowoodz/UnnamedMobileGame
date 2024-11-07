using UnityEngine;
using UnityEngine.InputSystem;

public class HandleTouchInput : MonoBehaviour
{
    // Reference to the generated Input Action class
    private TouchInput touchInput;
    private Camera mainCamera;
    [SerializeField]GameManager gameManager;

    void Awake()
    {
        // Initialize the Input Action instance
        touchInput = new TouchInput();
    }

    void OnEnable()
    {
        // Enable the TouchPosition action
        touchInput.Touch.TouchPosition.Enable();
        touchInput.Touch.LeftClickPosition.Enable();
    }

    void OnDisable()
    {
        // Disable the TouchPosition action
        touchInput.Touch.TouchPosition.Disable();
        touchInput.Touch.LeftClickPosition.Disable();
    }

    void Start()
    {
        // Cache the main camera reference
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Get touch position if available
        Vector2 touchPosition = Vector2.zero;
        bool inputDetected = false;
        if (gameManager.isGameOver == false)
        {
            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed && gameManager.isGamePaused == false)
            {
                // Mobile touch input
                touchPosition = touchInput.Touch.TouchPosition.ReadValue<Vector2>();
                inputDetected = true;
            }
            else if (Mouse.current != null && Mouse.current.leftButton.isPressed && gameManager.isGamePaused == false)
            {
                // PC mouse input
                touchPosition = Mouse.current.position.ReadValue();
                inputDetected = true;
            }
        }
        

        if (inputDetected)
        {
            // Perform raycasting based on input position
            int excludedLayerMask = ~LayerMask.GetMask("Static", "Bomb");
            Ray ray = mainCamera.ScreenPointToRay(touchPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, excludedLayerMask))
            {
                GeneralGameObject currentGeneralGameObject = hit.transform.GetComponent<GeneralGameObject>();
                currentGeneralGameObject.GameObjectTouched();
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Bomb")))
            {
                BombPrefab bombPrefabScript = hit.transform.GetComponent<BombPrefab>();
                bombPrefabScript.GameObjectTouched();
            }
        }
    }
}
