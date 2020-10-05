using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour {
  public float speed = 700;
  public float sprintSpeed = 1200;
  public bool CanMove = true;
  public Camera mainCamera;

  private Vector2 velocity;
  private CharacterController cc;

  public float movementSmoothing = 0.15f;

  void Start() {
    GameController.Instance.player = this;
    cc = GetComponent<CharacterController>();
    mainCamera = GetComponentInChildren<Camera>();
  }

  private void FixedUpdate() {
    if (!CanMove) return;
    velocity.y = Input.GetAxis("Vertical");
    velocity.x = Input.GetAxis("Horizontal");
    velocity = velocity.normalized * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : speed) * Time.fixedDeltaTime;// * Time.deltaTime;
    var desiredPosition =(transform.forward * velocity.y + transform.right * velocity.x + transform.up * (-9.8f * Time.fixedDeltaTime));
    cc.Move(desiredPosition);

    //rg.velocity = new Vector3(newVelo.x, rg.velocity.y, newVelo.z);
  }

  void Update() {

    Uobject uobj = null;
    Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, 3f, LayerMask.GetMask("Default"))) {
      uobj = hit.transform.GetComponent<Uobject>();
      if (uobj && !uobj.isBusy)
        CanvasScript.Instance.SetInteract(uobj.InteractText);
      else
        CanvasScript.Instance.ResetInteract();
    }
    else {
      CanvasScript.Instance.ResetInteract();
    }

    if (Input.GetKeyDown(KeyCode.E)) {
      if (CanvasScript.Instance.dialog.gameObject.activeSelf) {
        CanvasScript.Instance.dialog.Next();
        return;
      }
      if (uobj && !uobj.isBusy) {
        uobj.InteractAction.Invoke();
      }
    }

  }
}
