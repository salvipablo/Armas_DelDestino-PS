using UnityEngine;

namespace ArmasDestinoPS.Player
{
  public class PlayerMovements : MonoBehaviour
  {
    #region Properties
      [SerializeField] private float movSpeed = 5f;
      [SerializeField] public Vector2 turnSensitivity;
      private Transform _camera;
      [SerializeField] public float jumpForce;
      public bool itCanJump;
      private Rigidbody _rb;
    #endregion
    
    #region Methods
      private void Start()
      {
        _rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        _camera = transform.Find("Camera");
        itCanJump =  true;
      }

      private void Update()
      {
        Movements(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
      }

      private void Movements(float horizontalTwistMouse, float verticalTwistMouse)
      {
        Walk();
        Look(horizontalTwistMouse, verticalTwistMouse);
        Jump();
      }
      
      private void Walk()
      {
        if (Input.GetButton("WalkForward")) transform.Translate(0, 0, movSpeed * Time.deltaTime);
        if (Input.GetButton("WalkBackwards")) transform.Translate(0, 0, -movSpeed * Time.deltaTime);
        if (Input.GetButton("WalkRight")) transform.Translate(movSpeed * Time.deltaTime, 0, 0);
        if (Input.GetButton("WalkLeft")) transform.Translate(-movSpeed * Time.deltaTime, 0, 0);
      }
      
      private void Look(float horizontalTwistMouse, float verticalTwistMouse)
      {
        if (horizontalTwistMouse != 0) transform.Rotate(Vector3.up * (horizontalTwistMouse * turnSensitivity.x));

        if (verticalTwistMouse == 0) return;
        
        var angle = (_camera.localEulerAngles.x - verticalTwistMouse * turnSensitivity.y + 360) % 360;
        if (angle > 180) angle -= 360;
        angle = Mathf.Clamp(angle, -80, 80);
        _camera.localEulerAngles = Vector3.right * angle;
      }
      
      private void Jump()
      {
        if (!Input.GetButtonDown("Jump") || !itCanJump) return;
        
        itCanJump = false;
        _rb.AddForce(new Vector3(0, jumpForce, 0));
      }
    #endregion
  }
}

