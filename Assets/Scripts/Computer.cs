using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Computer : MonoBehaviour {
  public Chat Chat;
  public WorkingSpace WorkingSpace;
  public Uobject computer;
  public bool canAltTab = true;

  void Start() {
    gameObject.SetActive(false);
  }

  void Update() {
    if (canAltTab && Input.GetKeyDown(KeyCode.Tab)) {
      if (Chat.gameObject.activeSelf) {
        Chat.SetActive(false);
        WorkingSpace.SetActive(true);
      }
      else {
        Chat.SetActive(true);
        WorkingSpace.SetActive(false);
      }
    }
  }

  public void SetActive(bool state) {
    computer.isBusy = state;
    gameObject.SetActive(state);
    Chat.ResetLayouts();
    CanvasScript.Instance.mainCamera.GetComponent<BlitTest>().enabled = state;
    GameController.Instance.player.CanMove = !state;
    Scenario1();
  }

  private static bool scenario1Started = false;
  public void Scenario1() {
    if (scenario1Started) return;
    scenario1Started = true;
    canAltTab = false;
    Chat.SetActive(true);

    Sequence seq = DOTween.Sequence();
    seq.AppendInterval(1.5f);
    seq.AppendCallback(() => Chat.TypeToYouMessage("Despite the outbreak, we have a lot of tourists coming through the border", 3f));
    seq.AppendInterval(5f);
    seq.AppendCallback(() => Chat.TypeToYouMessage("And you need to approve their passport data", 3f));
    seq.AppendInterval(5f);
    seq.AppendCallback(() => Chat.TypeToYouMessage("INSTEAD OF SLEEPING!!!!!", 3f));
    seq.AppendInterval(5f);
    seq.AppendCallback(() => Chat.TypeFakeMessage("F*ck off", 2f));
    seq.AppendInterval(4f);
    seq.AppendCallback(() => Chat.TypeFromYouMessage("I'm sorry, as I said it won't happen again", () => {
      seq = DOTween.Sequence();
      seq.AppendInterval(1f);
      seq.AppendCallback(() => Chat.TypeToYouMessage("You need to be more responsible", 2f));
      seq.AppendInterval(4f);
      seq.AppendCallback(() => Chat.TypeToYouMessage("We have a lot of clients, and remember to check all their data", 2f));
      seq.AppendInterval(4f);
      seq.AppendCallback(() => Chat.TypeToYouMessage("If you see invalid <color=#FC9E4F>names or emails</color> disapprove it immediately", 2f));
      seq.AppendInterval(4f);
      seq.AppendCallback(() => Chat.TypeToYouMessage("Also check the <color=#FC9E4F>expire date</color> and more importantly their <color=#FC9E4F>credit card</color>. They are trying to get through without paying!", 2f));
      seq.AppendInterval(4f);
      seq.AppendCallback(() => Chat.TypeToYouMessage("Now get back to work! Alt <color=#FC9E4F>Tab</color> to your working space!", 2f));
      seq.AppendInterval(4f);
      seq.AppendCallback(() => Chat.TypeFakeMessage("Whatever old c*nt", 2f));
      seq.AppendInterval(4f);
      seq.AppendCallback(() => Chat.TypeFromYouMessage("Don't worry I won't disappoint you", () => canAltTab = true));
      seq.Play();
    }));
    seq.Play();
  }


  public void GetFired() {
    canAltTab = false;
    Chat.SetActive(true);
    WorkingSpace.SetActive(false);

    Sequence seq = DOTween.Sequence();
    seq.AppendInterval(1.5f);
    seq.AppendCallback(() => Chat.TypeToYouMessage("I'm losing clients because of you!", 1f));
    seq.AppendInterval(2.5f);
    seq.AppendCallback(() => Chat.TypeToYouMessage("I should have done it long ago", 1f));
    seq.AppendInterval(2f);
    seq.AppendCallback(() => Chat.TypeToYouMessage("YOU'RE FIRED!!!!!", 1f));
    seq.AppendInterval(2f);
    seq.AppendCallback(() => Chat.TypeFakeMessage("F*ck off", 2f));
    seq.AppendInterval(4f);
    seq.AppendCallback(() => Chat.TypeFromYouMessage("Go f*ck yourself", () => {
      GameController.Instance.SetNight();
      seq = DOTween.Sequence();
      seq.AppendInterval(1f);
      seq.AppendCallback(() => Chat.SetActive(false));
      seq.AppendInterval(0.7f);
      seq.AppendCallback(() => {
        SetActive(false);
        computer.isBusy = true;
        CanvasScript.Instance.dialog.WriteText(new Dictionary<string, float> {
          {"It's late already, f*ck this guy, I`ll go take a nap", 0.05f}
        });
      });
      seq.Play();
    }));
    seq.Play();
  }

  public void FinishDay() {
    canAltTab = false;
    Chat.SetActive(true);
    WorkingSpace.SetActive(false);

    Sequence seq = DOTween.Sequence();
    seq.AppendInterval(1.5f);
    seq.AppendCallback(() => Chat.TypeToYouMessage("Nice job, I'll send you your salary tomorrow, April 29th", 1f));
    seq.AppendInterval(2.5f);
    seq.AppendCallback(() => Chat.TypeFromYouMessage("Great! Thank you!", () => {
      GameController.Instance.SetNight();
      seq = DOTween.Sequence();
      seq.AppendInterval(1f);
      seq.AppendCallback(() => Chat.SetActive(false));
      seq.AppendInterval(0.7f);
      seq.AppendCallback(() => {
        SetActive(false);
        computer.isBusy = true;
        CanvasScript.Instance.dialog.WriteText(new Dictionary<string, float> {
          { "It's late already, I wanna sleep. Can't wait for my salary tomorrow!", 0.05f}
        });
      });
      seq.Play();
    }));
    seq.Play();
  }
}
