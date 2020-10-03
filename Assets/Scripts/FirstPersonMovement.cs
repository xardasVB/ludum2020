using UnityEngine;

public class FirstPersonMovement : MonoBehaviour {
  public float speed = 700;
  public float sprintSpeed = 1200;
  Vector2 velocity;
  private Rigidbody rg;

  void Start() {
    rg = GetComponent<Rigidbody>();
  }

  void LateUpdate() {
    velocity.y = Input.GetAxis("Vertical");
    velocity.x = Input.GetAxis("Horizontal");
    velocity = velocity.normalized * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : speed) * Time.deltaTime;

    var newVelo = transform.forward * velocity.y + transform.right * velocity.x;
    rg.velocity = new Vector3(newVelo.x, rg.velocity.y, newVelo.z);
    //transform.Translate(velocity.x, 0, velocity.y);
  }
}
