using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour {
  public GameObject toYouPrefab;
  public GameObject fromYouPrefab;
  public ContentSizeFitter messages;

  public Image SendButton;
  public TextMeshProUGUI typeField;
  public TextMeshProUGUI isTyping;
  public AudioSource typeSound;
  public Color btnColor1;
  public Color btnColor2;

  private bool canSend = false;
  private bool btnColor = false;
  private int dotCount = 0;
  private float curCd = 0;
  private float cd = 0.5f;

  private float _speed = 0.07f;
  private bool startPrinting = false;
  private bool fakePrinting = false;
  private bool startDiscarding = false;
  private string _text = "";
  private float printCd = 0;
  private float discardCd = 0;
  private float sendBtnCd = 0;
  private Action _action;

  void Update() {
    SendButton.gameObject.SetActive(canSend);
    if (canSend) {
      if (Input.GetKeyDown(KeyCode.E)) {
        canSend = false;
        SendChatMessage(typeField.text, fromYouPrefab);
        typeField.text = "";
        _action?.Invoke();
      }

      sendBtnCd += Time.deltaTime;
      if (sendBtnCd >= cd) {
        sendBtnCd = 0;
        SendButton.color = btnColor ? btnColor1 : btnColor2;
        btnColor = !btnColor;
      }
    }

    if (startPrinting) {
      printCd += Time.deltaTime;
      if (printCd >= _speed) {
        printCd = 0;
        if (_text == "") {
          startPrinting = false;
          if (!fakePrinting)
            canSend = true;
          return;
        }

        typeField.text += _text[0];
        _text = _text.Remove(0, 1);
      }
    }

    if (startDiscarding) {
      discardCd += Time.deltaTime;
      if (discardCd >= _speed) {
        discardCd = 0;
        if (typeField.text == "") {
          startDiscarding = false;
          return;
        }

        typeField.text = typeField.text.Remove(typeField.text.Length - 1, 1);
      }
    }

    if (isTyping.enabled) {
      isTyping.text = "is typing";
      for (int i = 0; i < dotCount; i++)
        isTyping.text += ".";
      curCd += Time.deltaTime;
      if (curCd >= cd) {
        curCd = 0;
        dotCount++;
        if (dotCount >= 4)
          dotCount = 0;
      }
    }
  }

  public void TypeToYouMessage(string message, float time = 3f) {
    Sequence seq = DOTween.Sequence();
    seq.AppendCallback(() => isTyping.enabled = true);
    seq.AppendInterval(time);
    seq.AppendCallback(() => isTyping.enabled = false);
    seq.AppendCallback(() => {
      SendChatMessage(message, toYouPrefab);
    });
    seq.Play();
  }
  public void TypeFromYouMessage(string message, Action action = null) {
    _text = message;
    _action = action;
    startPrinting = true;
  }
  public void TypeFakeMessage(string message, float time = 3f) {
    _text = message;
    startPrinting = true;
    fakePrinting = true;
    Sequence seq = DOTween.Sequence();
    seq.AppendInterval(time);
    seq.AppendCallback(() => {
      startDiscarding = true;
      fakePrinting = false;
    });
    seq.Play();
  }

  void SendChatMessage(string message, GameObject prefab) {
    if (message == "")
      return;
    var pref = Instantiate(prefab, messages.transform);
    pref.transform.SetAsFirstSibling();
    pref.GetComponentInChildren<TextMeshProUGUI>().text = message;
    ResetLayouts();
  }

  public void SetActive(bool state) {
    if (state)
      gameObject.SetActive(true);
    transform.localScale = state ? new Vector3(0, 0, 0) : new Vector3(1, 1, 1);
    transform.DOScale(state ? 1 : 0, 0.7f).OnComplete(() => {
      if (!state)
        gameObject.SetActive(false);
    });
  }

  public void ResetLayouts() {
    foreach (var csf in messages.GetComponentsInChildren<ContentSizeFitter>()) {
      csf.enabled = true;
      csf.SetLayoutVertical();
      LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)csf.transform);
      csf.enabled = false;
    }
  }
}
