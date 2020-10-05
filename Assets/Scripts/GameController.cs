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

  public Material skybox;
  public Light light;
  public Color skyboxColorNight;
  public Color skyboxColorDay;
  public Color LightColorNight;
  public Color lightColorDay;

  void Awake() {
    _instance = this;
  }

  public FirstPersonMovement player;

  public string GetNextDate() {
    return "April 28\n10:32 AM";
  }

  public void SetNight() {
    skybox.SetColor(297, skyboxColorNight);
    light.color = LightColorNight;
  }

  public void SetDay() {
    skybox.SetColor(297, skyboxColorDay);
    light.color = lightColorDay;
  }
}
