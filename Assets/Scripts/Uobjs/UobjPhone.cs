using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UobjPhone : Uobject {
  public Sprite bossImg;
  public Uobject computer;
  public Uobject doors;
  public List<GameObject> phone = new List<GameObject>();

  void Start() {
    computer.isBusy = true;
    doors.isBusy = true;
  }

  public void Interact() {
    GetComponent<AudioSource>().Stop();
    isBusy = true;
    foreach (var part in phone)
      part.SetActive(false);
    if (GameController.isFired) {
      ScenarioFired();
      return;
    }

    switch (GameController.daysCount) {
      case 1:
        Scenario1();
        break;
      case 2:
        Scenario2();
        break;
      default:
        Scenario3();
        break;
    }
  }

  void Scenario1() {
    CanvasScript.Instance.dialog.WriteText(new Dictionary<string, float>() {
      { "Hello?", 0.05f }
    }, "...", null, () => {
      CanvasScript.Instance.dialog.WriteText(new Dictionary<string, float>() {
        { "ARE YOU OUT OF YOUR MIND???", 0.15f },
        { "Do you know what time it is? We have a lot of work to do.", 0.05f },
        { "Even though you're quarantined doesn't mean you can do nothing.", 0.05f }
      }, "BOSS", bossImg, () => {
        CanvasScript.Instance.dialog.WriteText(new Dictionary<string, float>() {
          { "Alright, i'm sorry. I was up for too late last night, that won't happen again.", 0.05f },
          { "Just a moment, I`ll turn my computer on.", 0.05f },
        }, "...", null, () => {
          foreach (var part in phone)
            part.SetActive(true);
          computer.isBusy = false;
          //isBusy = false;
        });
      });
    });
  }

  void ScenarioFired() {
    CanvasScript.Instance.dialog.WriteText(new Dictionary<string, float>() {
      { "Uhm, hello?", 0.05f }
    }, "...", null, () => {
      CanvasScript.Instance.dialog.WriteText(new Dictionary<string, float>() {
        { "ARE YOU OUT OF YOUR MIND???", 0.15f },
        { "Do you know what time it is? We have a lot of work to do.", 0.05f },
        { "Even though you're quarantined doesn't mean you can do nothing.", 0.05f }
      }, "BOSS", bossImg, () => {
        CanvasScript.Instance.dialog.WriteText(new Dictionary<string, float>() {
          { "You've already said that", 0.05f },
          { "Didn't you fire me yesterday?", 0.05f }
        }, "...", null, () => {
          CanvasScript.Instance.dialog.WriteText(new Dictionary<string, float>() {
            { "Did you hit your head? Yesterday was day off.", 0.05f },
            { "But if you get late one more time i WILL fire you. Now start working.", 0.05f },
          }, "BOSS", bossImg, () => {
            foreach (var part in phone)
              part.SetActive(true);
            computer.isBusy = false;
            //isBusy = false;
          });
        });
      });
    });
  }

  void Scenario2() {
    CanvasScript.Instance.dialog.WriteText(new Dictionary<string, float>() {
      { "Uhm, hello?", 0.05f }
    }, "...", null, () => {
      CanvasScript.Instance.dialog.WriteText(new Dictionary<string, float>() {
        { "ARE YOU OUT OF YOUR MIND???", 0.15f },
        { "Do you know what time is it? We have a lot of work to do.", 0.05f },
        { "Even though you're quarantined doesn't mean you can do nothing.", 0.05f }
      }, "BOSS", bossImg, () => {
        CanvasScript.Instance.dialog.WriteText(new Dictionary<string, float>() {
          { "You said the exact same thing yesterday.", 0.05f },
          { "And where is my salary? You said I`ll get it today.", 0.05f }
        }, "...", null, () => {
          CanvasScript.Instance.dialog.WriteText(new Dictionary<string, float>() {
            { "Did you hit your head? Yesterday was day off.", 0.05f },
            { "And you can forget about salary if you get late one more time.", 0.05f },
            { "Get to work!", 0.05f },
          }, "BOSS", bossImg, () => {
            foreach (var part in phone)
              part.SetActive(true);
            computer.isBusy = false;
            //isBusy = false;
          });
        });
      });
    });
  }

  void Scenario3() {
    CanvasScript.Instance.dialog.WriteText(new Dictionary<string, float>() {
      { "YES????", 0.05f }
    }, "...", null, () => {
      CanvasScript.Instance.dialog.WriteText(new Dictionary<string, float>() {
        { "ARE YOU OUT OF YOUR MIND???", 0.15f },
        { "Do you know what time is it? We have a lot of work to do.", 0.05f },
        { "Even though you're quarantined doesn't mean you can do nothing.", 0.05f }
      }, "BOSS", bossImg, () => {
        CanvasScript.Instance.dialog.WriteText(new Dictionary<string, float>() {
          { "NO I HAVE DONE ENOUGH", 0.15f },
          { "F*CK THIS SH*T IM GETTING CRAZY IN THIS QUARANTINE", 0.15f }
        }, "...", null, () => {
          CanvasScript.Instance.dialog.WriteText(new Dictionary<string, float>() {
            { "...", 0.2f }
          }, "BOSS", bossImg, () => {
            CanvasScript.Instance.dialog.WriteText(new Dictionary<string, float>() {
              { "Whatever I`ll just go for a walk", 0.15f }
            }, "...", null, () => {
              foreach (var part in phone)
                part.SetActive(true);
              doors.isBusy = false;
              //isBusy = false;
            });
          });
        });
      });
    });
  }


}
