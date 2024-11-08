using UnityEngine;
using UnityEngine.InputSystem;

public class HandleTouchInput : MonoBehaviour
{
    private GameModeScript gameModeScript;
    private TouchInput touchInput;
    private Camera mainCamera;
    [SerializeField] private GameManager gameManager;

    void Awake()
    {
        touchInput = new TouchInput();
        gameModeScript = GameObject.Find("GameModeManager").GetComponent<GameModeScript>();
    }

    void OnEnable()
    {
        touchInput.Touch.TouchPosition.Enable();
        touchInput.Touch.LeftClickPosition.Enable();
    }

    void OnDisable()
    {
        touchInput.Touch.TouchPosition.Disable();
        touchInput.Touch.LeftClickPosition.Disable();
    }

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Handle touch input only when a new touch begins
        if (gameManager.isGameOver || gameManager.isGamePaused) return;

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Vector2 touchPosition = touchInput.Touch.TouchPosition.ReadValue<Vector2>();
            HandleTap(touchPosition);
        }
        // For mouse click on PC, detect the start of the click
        else if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 clickPosition = Mouse.current.position.ReadValue();
            HandleTap(clickPosition);
        }
    }

    private void HandleTap(Vector2 position)
    {
        if (gameModeScript.currentMode != GameModeScript.GameMode.ColorFrenzy)
        {
            int excludedLayerMask = ~LayerMask.GetMask("Static", "Bomb");
            Ray ray = mainCamera.ScreenPointToRay(position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, excludedLayerMask))
            {
                GeneralGameObject currentGeneralGameObject = hit.transform.GetComponent<GeneralGameObject>();
                currentGeneralGameObject?.GameObjectTouched();
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Bomb")))
            {
                BombPrefab bombPrefabScript = hit.transform.GetComponent<BombPrefab>();
                bombPrefabScript?.GameObjectTouched();
            }
        }
        else
        {
            int excludedLayerMask = ~LayerMask.GetMask("Static");
            Ray ray = mainCamera.ScreenPointToRay(position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, excludedLayerMask))
            {
                ColorObject currentColorObject = hit.transform.GetComponent<ColorObject>();
                currentColorObject?.GameObjectTouched();
            }
        }
    }
}