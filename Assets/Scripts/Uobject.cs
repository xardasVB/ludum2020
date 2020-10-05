using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Uobject : MonoBehaviour {
  public string InteractText;
  public UnityEvent InteractAction;
  public bool isBusy = false;

  public void SayPhrase(string phrase) {
    isBusy = true;
    CanvasScript.Instance.dialog.WriteText(new Dictionary<string, float>() {
      { phrase, 0.05f }
    }, "...", null, () => isBusy = false);
  }

  public void GoToSleep() {
    CanvasScript.Instance.date.text.text = "";
    CanvasScript.Instance.date.GetComponent<CanvasGroup>().DOFade(1f, 1f).OnComplete(() => {
      SceneManager.LoadScene("SampleScene");
    });
  }

}
