using UnityEngine;

public class FirstPersonLook : MonoBehaviour {
  [SerializeField]
  FirstPersonMovement character;
  Vector2 currentMouseLook;
  Vector2 appliedMouseDelta;
  public float sensitivity = 1;
  public float smoothing = 2;


  void Reset() {
    character = GetComponentInParent<FirstPersonMovement>();
  }

  void Start() {
    character = GetComponentInParent<FirstPersonMovement>();
    Cursor.lockState = CursorLockMode.Locked;
  }

  void Update() {
    if (!character.CanMove) return;
    // Get smooth mouse look.
    Vector2 smoothMouseDelta = Vector2.Scale(new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")), Vector2.one * sensitivity * smoothing);
    appliedMouseDelta = Vector2.Lerp(appliedMouseDelta, smoothMouseDelta, 1 / smoothing);
    currentMouseLook += appliedMouseDelta;
    currentMouseLook.y = Mathf.Clamp(currentMouseLook.y, -90, 90);

    // Rotate camera and controller.
    transform.localRotation = Quaternion.AngleAxis(-currentMouseLook.y, Vector3.right);
    character.transform.localRotation = Quaternion.AngleAxis(currentMouseLook.x, Vector3.up);
  }
}
