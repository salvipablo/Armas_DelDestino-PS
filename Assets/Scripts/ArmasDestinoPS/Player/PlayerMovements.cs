using UnityEngine;
using TMPro;
using ArmasDestinoPS.Managers;
using ArmasDestinoPS.Means;
using ArmasDestinoPS.Inventory;

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
      [SerializeField] private float rayDistance = 3f;
      private GameObject _cameraObject;
      [SerializeField] private TextMeshProUGUI objectNameText;
    #endregion
    
    #region Methods
      private void Start()
      {
        _rb = GetComponent<Rigidbody>();
        //Cursor.lockState = CursorLockMode.Locked;
        _camera = transform.Find("Camera");
        itCanJump =  true;
      }

      private void Update()
      {
        Movements(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
      }

      private void Movements(float horizontalTwistMouse, float verticalTwistMouse)
      {
        if (!InventoryManager.Instance.TheInventoryOpen)
        {
          Walk();
          Look(horizontalTwistMouse, verticalTwistMouse);
          Jump();
          CheckInteraction();
        }
      }
      
      private void CheckInteraction()
      {
        InteractableObject interactable = null;
        Ray ray = new Ray(_camera.position, _camera.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
          _cameraObject = hit.collider.gameObject;
          interactable = _cameraObject.GetComponent<InteractableObject>();

          if (interactable != null) objectNameText.text = interactable.ObjectName;
          else objectNameText.text = "";
        }

        if (Input.GetButton("Fire1") && interactable != null)
        {
          string statusOp;
          Item itemToSave = new Item(interactable.ObjectName);
          
          if (interactable.WhichSection == 99) statusOp = 
                  InventoryManager.Instance.AddItemToSlot(itemToSave,interactable.Qty);
          else statusOp = 
                  InventoryManager.Instance.AddItemToSlotDeveloper(itemToSave,interactable.Qty,
                                                            interactable.WhichSection, interactable.WhichSlot);
          
          Debug.Log(statusOp);
          if (statusOp == "Item agregado al inventario") Destroy(interactable.gameObject);
        }

        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
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

