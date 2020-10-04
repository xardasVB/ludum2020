using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour {
  public GameObject toYouPrefab;
  public GameObject fromYouPrefab;
  public ContentSizeFitter messages;

  public void TypeToYouMessage(string message) {
    var pref = Instantiate(toYouPrefab, messages.transform);
    pref.transform.SetAsFirstSibling();
    pref.GetComponentInChildren<TextMeshProUGUI>().text = message;
    messages.enabled = true;
    messages.SetLayoutVertical();
    LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)messages.transform);
    messages.enabled = false;
  }
  public void TypeFromYouMessage(string message) {
    var pref = Instantiate(fromYouPrefab, messages.transform);
    pref.transform.SetAsFirstSibling();
    pref.GetComponentInChildren<TextMeshProUGUI>().text = message;
    messages.enabled = true;
    messages.SetLayoutVertical();
    LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)messages.transform);
    messages.enabled = false;
  }

}
