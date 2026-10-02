using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _orientationTransform;
    [SerializeField] private Transform _playerVisualTransform;

    [Header("Settings")]
    [SerializeField] private float _rotationSpeed = 7f;

    private void Update()
    {
        // Kameranın baxış istiqamətini hesablayır (hündürlük fərqini nəzərə almadan)
        Vector3 viewDirection = _playerTransform.position - new Vector3(transform.position.x, _playerTransform.position.y, transform.position.z);
        _orientationTransform.forward = viewDirection.normalized;

        // Düymə girişlərini alır
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Hərəkət istiqamətini hesablayır
        Vector3 inputDirection = _orientationTransform.forward * verticalInput + _orientationTransform.right * horizontalInput;

        // Düyməyə basıldıqda xarakterin görsel modelini (Visual) hərəkət istiqamətinə rəvan döndərir
        if (inputDirection != Vector3.zero)
        {
            _playerVisualTransform.forward = Vector3.Slerp(_playerVisualTransform.forward, inputDirection.normalized, Time.deltaTime * _rotationSpeed);
        }
    }
}