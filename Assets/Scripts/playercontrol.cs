using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _orientationTransform;

    [Header("Movement Settings")]
    [SerializeField] private float _movementSpeed = 500f;

    [Header("Jump Settings")]
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private float _jumpForce = 7f;

    private Rigidbody _playerRigidbody;
    private float _horizontalInput, _verticalInput;
    private Vector3 _movementDirection;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;
    }

    private void Update()
    {
        SetInputs();
    }

    private void FixedUpdate()
    {
        SetPlayerMovement();
    }

    private void SetInputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(_jumpKey))
        {
            SetPlayerJumping();
        }
    }

    private void SetPlayerMovement()
    {
        _movementDirection = _orientationTransform.forward * _verticalInput 
                           + _orientationTransform.right * _horizontalInput;

        _playerRigidbody.AddForce(_movementDirection * _movementSpeed, ForceMode.Force);
    }

    private void SetPlayerJumping()
    {
        _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
        _playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
}