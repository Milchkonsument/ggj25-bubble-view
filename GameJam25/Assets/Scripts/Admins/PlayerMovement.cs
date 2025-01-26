using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _lookSpeed;
    [SerializeField]
    private float _gravity;
    [SerializeField]
    private Transform _cameraTransform;

    private CharacterController _characterController;
    private Vector3 _velocity;
    private float _xRotation = 0f;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * _lookSpeed;

        transform.Rotate(Vector3.up * mouseX);

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        _cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        _characterController.Move(move * _moveSpeed * Time.deltaTime);

        //Gravity
        if(_characterController.isGrounded && _velocity.y < 0f)
        {
            _velocity.y = -2f;
        }
        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }
}
