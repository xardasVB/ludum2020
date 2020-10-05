using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UobjPhone : Uobject {
  public Sprite bossImg;
  public Uobject computer;

  void Start() {
    computer.isBusy = true;
  }

  public void Interact() {
    computer.isBusy = false;
    GetComponent<AudioSource>().Stop();
    isBusy = true;
    CanvasScript.Instance.dialog.WriteText(new Dictionary<string, float>() {
      { "Hello?", 0.05f }
    }, "...", null, () =>
    {
      CanvasScript.Instance.dialog.WriteText(new Dictionary<string, float>() {
        { "ARE YOU OUT OF YOUR MIND???", 0.15f },
        { "Do you know what time is it? We have a lot of work to do.", 0.05f },
        { "Even though you're quarantined doesn't mean you can do nothing.", 0.05f }
      }, "BOSS", bossImg, () => {
        CanvasScript.Instance.dialog.WriteText(new Dictionary<string, float>() {
          { "Alright, i'm sorry. I was up for too late last night, that won't happen again.", 0.05f },
          { "Just a moment, I`ll turn my computer on.", 0.05f },
        }, "...", null, null);//() => isBusy = false);
      });
    });
  }


}
