using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {


  public Image icon;
  public TextMeshProUGUI name;
  public TextMeshProUGUI text;
  public GameObject nextgo;

  private float _speed = 0.05f;
  private bool startPrinting = false;
  private Dictionary<string, float> _texts = new Dictionary<string, float>();
  private string _text = "";
  private Action _onFinish;

  void Awake() {
    gameObject.SetActive(false);
  }

  public void WriteText(Dictionary<string, float> curtexts, string author = "...", Sprite image = null, Action onFinish = null) {
    gameObject.SetActive(true);
    nextgo.SetActive(false);
    name.text = author;

    icon.gameObject.SetActive(image != null);
    icon.sprite = image;
    _texts = curtexts;
    _text = _texts.FirstOrDefault().Key;
    _speed = _texts.FirstOrDefault().Value;
    _texts.Remove(_text);
    text.text = "";
    startPrinting = true;
    _onFinish = onFinish;
    GameController.Instance.player.CanMove = false;
  }

  public void Next() {
    if (startPrinting) {
      text.text += _text;
      startPrinting = false;
      nextgo.SetActive(true);
      return;
    }

    if (_texts.Count > 0) {
      nextgo.SetActive(false);
      _text = _texts.FirstOrDefault().Key;
      _speed = _texts.FirstOrDefault().Value;
      _texts.Remove(_text);
      text.text = "";
      startPrinting = true;
    }
    else {
      gameObject.SetActive(false);
      _onFinish?.Invoke();
      GameController.Instance.player.CanMove = true;
    }
  }

  private float _curCd = 0;

  void Update() {
    if (startPrinting) {
      _curCd += Time.deltaTime;
      if (_curCd >= _speed) {
        _curCd = 0;
        if (_text == "") {
          startPrinting = false;
          nextgo.SetActive(true);
          return;
        }

        text.text += _text[0];
        _text = _text.Remove(0, 1);
      }
    }
  }

}
