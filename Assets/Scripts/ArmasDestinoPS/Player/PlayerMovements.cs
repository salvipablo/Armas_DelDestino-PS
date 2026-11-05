using UnityEngine;
using TMPro;
using ArmasDestinoPS.Managers;
using ArmasDestinoPS.Means;
using ArmasDestinoPS.Inventory;
using ArmasDestinoPS.NPCs;

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
        Cursor.lockState = CursorLockMode.Locked;
        _camera = transform.Find("Camera");
        itCanJump =  true;
      }

      private void Update()
      {
        if (!InventoryManager.Instance.TheInventoryOpen && !NPCManager.Instance.IsTalkingNPC())
        {
          Movements(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
          CheckInteraction();
        }
      }

      private void Movements(float horizontalTwistMouse, float verticalTwistMouse)
      {
          Walk();
          Look(horizontalTwistMouse, verticalTwistMouse);
          Jump();
      }
      
      private void CheckInteraction()
      {
        InteractableObject interactableObject = null;
        NPC npc = null;
        
        objectNameText.text = "";
        
        Ray ray = new Ray(_camera.position, _camera.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
          _cameraObject = hit.collider.gameObject;
          
          interactableObject = _cameraObject.GetComponent<InteractableObject>();
          npc = _cameraObject.GetComponent<NPC>();

          if (interactableObject != null) objectNameText.text = interactableObject.ObjectName;
          if (npc != null) objectNameText.text = npc.NPCName;
        }
        
        // Si presiono el boton del mouse izquierdo, y estoy mirando un NPC. Inicio accion de interactuar con el.
        if (Input.GetButton("Fire1") && npc != null) npc.StartConversation();
          
        // Si presiono el boton del mouse izquierdo, y estoy mirando un objeto. Inicio accion de juntarlo.
        if (Input.GetButton("Fire1") && interactableObject != null) GatherResource(interactableObject);

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

      private void GatherResource(InteractableObject interactableObject)
      {
        string statusOp;
        Sprite iconItem = ItemsManager.Instance.GetSprite(interactableObject.ObjectName);
          
        Item itemToSave = new Item(1, interactableObject.ObjectName, true, "Description", iconItem);
          
        if (interactableObject.WhichSection == 99) statusOp = 
          InventoryManager.Instance.AddItemToSlot(itemToSave, interactableObject.Qty);
        else statusOp = 
          InventoryManager.Instance.AddItemToSlotDeveloper(itemToSave,interactableObject.Qty,
            interactableObject.WhichSection - 1, interactableObject.WhichSlot - 1);
          
        if (statusOp == "Item agregado al inventario") Destroy(interactableObject.gameObject);
      }
    #endregion
  }
}

