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
  public Computer Computer;
  public Camera mainCamera;
  public DateController date;

  void Start() {
    GameController.Instance.SetDay();
    date.ShowDate(GameController.Instance.GetNextDate());
  }

  public void TurnOnComputer() {
    Computer.SetActive(true);
  }
  public void TurnOffComputer() {
    Computer.SetActive(false);
  }

  public void SetInteract(string text) {
    Interact.SetActive(true);
    Interact.GetComponentInChildren<TextMeshProUGUI>().text = "[E] " + text;
  }
  public void ResetInteract() {
    Interact.SetActive(false);
  }
}
