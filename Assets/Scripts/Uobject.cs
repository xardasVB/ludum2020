using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Uobject : MonoBehaviour {
  public string InteractText;
  public UnityEvent InteractAction;
  public bool isBusy = false;

  public void SayPhrase(string phrase)
  {
    isBusy = true;
    CanvasScript.Instance.dialog.WriteText(new Dictionary<string, float>() {
      { phrase, 0.05f }
    }, "...", null, () => isBusy = false);
  }

}
