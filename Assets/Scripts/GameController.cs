using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

  private static GameController _instance;

  public static GameController Instance {
    get {
      if (_instance == null)
        _instance = FindObjectOfType<GameController>();
      return _instance;
    }
    set {
      _instance = value;
    }
  }

  void Awake() {
    _instance = this;
  }

  public FirstPersonMovement player;

  void Start() {
    //CanvasScript.Instance.date.ShowDate(GetNextDate());
  }

  void Update() {

  }

  public string GetNextDate()
  {
    return "June 28\n8:00 AM";
  }
}
