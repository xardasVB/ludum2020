using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DateController : MonoBehaviour {
  public TextMeshProUGUI text;
  public float speed = 0.2f;

  private string _text;
  private bool startPrinting;
  private float _curCd = 0;

  public void ShowDate(string date) {
    gameObject.SetActive(true);
    GameController.Instance.player.CanMove = false;
    text.text = "";
    GetComponent<CanvasGroup>().DOFade(1f, 1f).OnComplete(() => {
      _text = date;
      startPrinting = true;
    });
  }

  void Update() {
    if (startPrinting) {
      _curCd += Time.deltaTime;
      if (_curCd >= speed) {
        _curCd = 0;
        if (_text == "") {
          startPrinting = false;
          GetComponent<CanvasGroup>().DOFade(1f, 2f).OnComplete(() => {
            GameController.Instance.player.CanMove = true;
            GetComponent<CanvasGroup>().DOFade(0f, 3f);
          });
          return;
        }

        text.text += _text[0];
        _text = _text.Remove(0, 1);
      }
    }
  }

}
