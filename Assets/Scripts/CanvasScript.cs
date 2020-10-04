using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasScript : MonoBehaviour {

  private static CanvasScript _instance;

  public static CanvasScript Instance {
    get {
      if (_instance == null)
        _instance = FindObjectOfType<CanvasScript>();
      return _instance;
    }
    set {
      _instance = value;
    }
  }

  void Awake() {
    _instance = this;
  }

  public Dialog dialog;
  public GameObject Interact;

  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update() {

  }

  public void SetInteract(string text) {
    Interact.SetActive(true);
    Interact.GetComponentInChildren<TextMeshProUGUI>().text = "[E] " + text;
  }
  public void ResetInteract() {
    Interact.SetActive(false);
  }
}
