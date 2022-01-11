using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;
    private static InputManager instance;

    public static InputManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return playerControls.Player.Movemnt.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    public bool GetPlayerJumped()
    {
        return playerControls.Player.Jump.triggered;
    }
}
