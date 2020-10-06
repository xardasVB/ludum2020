using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Computer : MonoBehaviour {
  public Chat Chat;
  public WorkingSpace WorkingSpace;
  public Uobject computer;
  public bool canAltTab = true;
  public AudioSource ambient;

  void Start() {
    gameObject.SetActive(true);
    GetComponent<CanvasGroup>().alpha = 0;
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

    if (Input.GetKeyDown(KeyCode.Z)) {
      if (GetComponent<CanvasGroup>().alpha == 1) {
        CanvasScript.Instance.TurnOffComputer();
      }
    }
  }

  public void SetActive(bool state) {
    computer.isBusy = state;
    if (state)
      ambient.Play();
    else
      ambient.Stop();
    GetComponent<CanvasGroup>().alpha = state ? 1 : 0;
    //gameObject.SetActive(state);
    if (state)
      Chat.ResetLayouts();
    CanvasScript.Instance.mainCamera.GetComponent<BlitTest>().enabled = state;
    GameController.Instance.player.CanMove = !state;
    switch (GameController.daysCount) {
      case 1:
        Scenario1();
        break;
      default:
        Scenario2();
        break;
    }
  }

  public static bool scenario1Started = false;
  public void Scenario1() {
    if (scenario1Started) return;
    scenario1Started = true;
    canAltTab = false;
    WorkingSpace.SetActive(false);
    Chat.SetActive(true);

    Sequence seq = DOTween.Sequence();
    seq.AppendInterval(1.5f);
    seq.AppendCallback(() => Chat.TypeToYouMessage("Despite the outbreak, we have lots of tourists coming through the border", 3f));
    seq.AppendInterval(4f);
    seq.AppendCallback(() => Chat.TypeToYouMessage("And you need to approve their passport data", 3f));
    seq.AppendInterval(4f);
    seq.AppendCallback(() => Chat.TypeToYouMessage("INSTEAD OF SLEEPING!!!!!", 3f));
    seq.AppendInterval(4f);
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
      seq.AppendCallback(() => Chat.TypeToYouMessage("Also check the <color=#FC9E4F>expire date</color> and more importantly their <color=#FC9E4F>16 credit card numbers</color>. They are trying to get through without paying!", 2f));
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


  public static bool scenario2Started = false;
  public void Scenario2() {
    if (scenario2Started) return;
    scenario2Started = true;
    canAltTab = false;
    WorkingSpace.SetActive(false);
    Chat.SetActive(true);

    Sequence seq = DOTween.Sequence();
    seq.AppendInterval(0.5f);
    seq.AppendCallback(() => Chat.TypeToYouMessage("Despite the outbreak, we have lots of tourists coming through the border", 1f));
    seq.AppendInterval(2f);
    seq.AppendCallback(() => Chat.TypeToYouMessage("And you need to approve their passport data", 1f));
    seq.AppendInterval(2f);
    seq.AppendCallback(() => Chat.TypeToYouMessage("INSTEAD OF SLEEPING!!!!!", 1f));
    seq.AppendInterval(2f);
    seq.AppendCallback(() => Chat.TypeFakeMessage("Can you f*ck off man", 2f));
    seq.AppendInterval(4f);
    seq.AppendCallback(() => Chat.TypeFromYouMessage("You said the exact SAME thing yesterday!", () => {
      seq = DOTween.Sequence();
      seq.AppendInterval(0.5f);
      seq.AppendCallback(() => Chat.TypeToYouMessage("Are you sick? I already told you yesterday was day off", 2f));
      seq.AppendInterval(3f);
      seq.AppendCallback(() => Chat.TypeToYouMessage("Anyway, we have a lot of clients, and remember to check all their data", 1f));
      seq.AppendInterval(2f);
      seq.AppendCallback(() => Chat.TypeToYouMessage("If you see invalid <color=#FC9E4F>names or emails</color> disapprove it immediately", 1f));
      seq.AppendInterval(2f);
      seq.AppendCallback(() => Chat.TypeToYouMessage("Also check the <color=#FC9E4F>expire date</color> and more importantly their <color=#FC9E4F>16 credit card numbers</color>. They are trying to get through without paying!", 1f));
      seq.AppendInterval(2f);
      seq.AppendCallback(() => Chat.TypeToYouMessage("Now get back to work! Alt <color=#FC9E4F>Tab</color> to your working space!", 1f));
      seq.AppendInterval(2f);
      seq.AppendCallback(() => Chat.TypeFakeMessage("wtf??? is this some sort of a prank???", 3f));
      seq.AppendInterval(7f);
      seq.AppendCallback(() => Chat.TypeFromYouMessage("Aight", () => canAltTab = true));
      seq.Play();
    }));
    seq.Play();
  }


  public void GetFired() {
    canAltTab = false;
    Chat.SetActive(true);
    WorkingSpace.SetActive(false);

    GameController.isFired = true;
    Sequence seq = DOTween.Sequence();
    seq.AppendInterval(1.5f);
    seq.AppendCallback(() => Chat.TypeToYouMessage("I'm losing clients because of you!", 1f));
    seq.AppendInterval(2.5f);
    seq.AppendCallback(() => Chat.TypeToYouMessage("I should have done it long ago", 1f));
    seq.AppendInterval(2f);
    seq.AppendCallback(() => Chat.TypeToYouMessage("YOU'RE FIRED!!!!!", 1f));
    seq.AppendInterval(2f);
    seq.AppendCallback(() => Chat.TypeFakeMessage("Please, I need this job", 2f));
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

    GameController.isFired = false;
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

  public void FinishDay2() {
    canAltTab = false;
    Chat.SetActive(true);
    WorkingSpace.SetActive(false);

    GameController.isFired = false;
    Sequence seq = DOTween.Sequence();
    seq.AppendInterval(1.5f);
    seq.AppendCallback(() => Chat.TypeToYouMessage("Nice job, I'll send you your salary tomorrow, April 29th", 1f));
    seq.AppendInterval(2.5f);
    seq.AppendCallback(() => Chat.TypeFromYouMessage("But... I can swear you said that yesterday...", () => {
      seq = DOTween.Sequence();
      seq.AppendInterval(1f);
      seq.AppendCallback(() => Chat.TypeToYouMessage("Kid, have a rest. You deserved it.", 2f));
      seq.AppendInterval(4f);
      seq.AppendCallback(() => Chat.TypeFakeMessage("Where is my money Lebowski???", 3f));
      seq.AppendInterval(7f);
      seq.AppendCallback(() => Chat.TypeFromYouMessage("Ok maybe I do need a rest", () => {
        GameController.Instance.SetNight();
        seq = DOTween.Sequence();
        seq.AppendInterval(1f);
        seq.AppendCallback(() => Chat.SetActive(false));
        seq.AppendInterval(0.7f);
        seq.AppendCallback(() => {
          SetActive(false);
          computer.isBusy = true;
          CanvasScript.Instance.dialog.WriteText(new Dictionary<string, float> {
          { "Man I can swear he promised me my salary yesterday. Maybe it was a dream...", 0.05f},
          { "Anyway, I want to sleep.", 0.05f}
        });
        });
        seq.Play();
      }));
      seq.Play();
    }));
    seq.Play();
  }
}
