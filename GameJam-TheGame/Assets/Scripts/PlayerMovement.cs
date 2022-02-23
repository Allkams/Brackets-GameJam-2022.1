using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    private CharacterController PlayerControll;
    private Vector3 playerVelocity;

    [SerializeField, Tooltip("Player speed multiplier.")]
    private float speed = 5f;
    [SerializeField, Tooltip("height for the jump")]
    private float jumpHeight = 2f;

    private float gravity = -9.82f;
    private bool groundedPlayer;
    private float smoothInputSpeed = 0.2f;


    private Vector2 movement = Vector2.zero;
    private bool jumped = false;

    private Vector2 currentInputVec;
    private Vector2 smoothVelocity;

    // Start is called before the first frame update
    void Start()
    {
        PlayerControll = GetComponent<CharacterController>();
    }

    public void onMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void onJump(InputAction.CallbackContext context)
    {
        //jumped = context.ReadValue<bool>(); [Gives error]
        jumped = context.action.triggered;
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = PlayerControll.isGrounded;
        if(groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        currentInputVec = Vector2.SmoothDamp(currentInputVec, movement, ref smoothVelocity, smoothInputSpeed);
        Vector3 move = new Vector3(currentInputVec.x, 0f, 0f);
        PlayerControll.Move(move * Time.deltaTime * speed);

        if(move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        if(jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }

        playerVelocity.y += gravity * Time.deltaTime;
        PlayerControll.Move(playerVelocity * Time.deltaTime);
    }
}
