using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

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

  public static int daysCount = 0;
  public static bool isFired = false;

  public Material skybox;
  public Light light;
  public Color skyboxColorNight;
  public Color skyboxColorDay;
  public Color LightColorNight;
  public Color lightColorDay;
  public Uobject bed;

  void Awake() {
    _instance = this;
  }

  public FirstPersonMovement player;

  public string GetNextDate() {
    return "April 28\n10:32 AM";
  }

  public void SetNight() {
    bed.isBusy = false;
    skybox.SetColor("_Tint", skyboxColorNight);
    light.color = LightColorNight;
  }

  public void SetDay() {
    bed.isBusy = true;
    skybox.SetColor("_Tint", skyboxColorDay);
    light.color = lightColorDay;
  }

  public void FinishGame() {
    CanvasScript.Instance.date.text.text = "";
    CanvasScript.Instance.date.GetComponent<CanvasGroup>().DOFade(1f, 1f).OnComplete(() => {
      SceneManager.LoadScene("EndScene");
    });
  }
}
