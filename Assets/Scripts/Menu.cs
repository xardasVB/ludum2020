using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
  public UnityEvent action;
  public CanvasGroup fade;

  // Update is called once per frame
  void Update() {
    if (Input.GetKeyDown(KeyCode.E)) {
      fade.DOFade(1, 1.5f).OnComplete(() => action?.Invoke());
    }
  }

  public void LoadGame() {
    SceneManager.LoadScene("SampleScene");
  }
}
