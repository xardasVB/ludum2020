using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class WorkingSpace : MonoBehaviour {

  public float fakeChance = 25f;

  public TextMeshProUGUI Name;
  public TextMeshProUGUI Email;
  public TextMeshProUGUI PasspordId;
  public TextMeshProUGUI ExpireDate;
  public TextMeshProUGUI Country;
  public TextMeshProUGUI City;
  public TextMeshProUGUI CreditCard;
  public GameObject Error;
  public int mistakeCount = 5;
  public int successRequired = 40;

  private HumanDataContainer data;
  private HumanData curData;
  private int successCount = 0;

  void Start() {
    //if (!Application.dataPath.Contains("http")) {
    //  data = JsonUtility.FromJson<HumanDataContainer>(File.ReadAllText(Application.dataPath + "/Data.json"));
    //  SetRandomData();
    //}
    //else 
    StartCoroutine(GetRequest(Application.dataPath + "/Data.json"));
  }

  IEnumerator GetRequest(string uri) {
    using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)) {
      // Request and wait for the desired page.
      yield return webRequest.SendWebRequest();

      string[] pages = uri.Split('/');
      int page = pages.Length - 1;

      if (webRequest.isNetworkError) {
        Debug.LogError(pages[page] + ": Error: " + webRequest.error);
      }
      else {
        data = JsonUtility.FromJson<HumanDataContainer>(webRequest.downloadHandler.text);
        SetRandomData();
        SetActive(false);
      }
    }
  }

  private bool isFired = false;
  void Update() {
    if (isFired) return;
    if (Input.GetKeyDown(KeyCode.E)) {
      if (Error.activeSelf) {
        Error.SetActive(false);
        if (mistakeCount <= 0) {
          CanvasScript.Instance.Computer.GetFired();
          isFired = true;
        }
        successCount--;
        SetRandomData();
        return;
      }
      Approve();
    }
    if (Input.GetKeyDown(KeyCode.Q)) {
      if (Error.activeSelf) return;
      Disapprove();
    }
  }

  public void Approve() {
    if (curData.faked) {
      ShowError(curData.fakedReason);
    }
    else {
      SetRandomData();
    }
  }
  public void Disapprove() {
    if (!curData.faked) {
      ShowError("Data is correct");
    }
    else {
      SetRandomData();
    }
  }

  void ShowError(string text) {
    mistakeCount--;
    Error.SetActive(true);
    Error.GetComponentInChildren<TextMeshProUGUI>().text = "ERROR:\n" + text + ".\nYou have " + mistakeCount + " mistakes left.";
  }


  public void SetActive(bool state) {
    Error.SetActive(false);
    if (state)
      gameObject.SetActive(true);
    transform.localScale = state ? new Vector3(0, 0, 0) : new Vector3(1, 1, 1);
    transform.DOScale(state ? 1 : 0, 0.7f).OnComplete(() => {
      if (!state)
        gameObject.SetActive(false);
    });
  }

  void SetRandomData() {
    successCount++;
    if (successCount >= successRequired) {
      if (GameController.daysCount == 1 || GameController.isFired) 
        CanvasScript.Instance.Computer.FinishDay();
      else
        CanvasScript.Instance.Computer.FinishDay2();
    }
    curData = data.data.OrderBy(a => Random.value).FirstOrDefault();

    if (Random.Range(0, 100) < fakeChance) {
      curData.faked = true;
      switch (Random.Range(0, 4)) {
        case 0:
          curData.Name = Guid.NewGuid().ToString();
          curData.fakedReason = "Name is not real";
          break;
        case 1:
          if (Random.value > 0.2f)
            curData.Email = TruncatePercents(curData.Email);
          else
            curData.Email = Guid.NewGuid().ToString();
          curData.fakedReason = "Email is incorrect";
          break;
        case 2:
          curData.ExpireDate = curData.ExpireDate.Remove(curData.ExpireDate.Length - 1, 1) + Random.Range(0, 4);
          curData.fakedReason = "Passport already expired";
          break;
        case 3:
          if (Random.value > 0.5f)
            curData.CreditCard = curData.CreditCard.Remove(0, 5);
          else
            curData.CreditCard = curData.CreditCard.Replace(curData.CreditCard[Random.Range(0, curData.CreditCard.Length)], 'i');
          curData.fakedReason = "Credit card is fake";
          break;
        default:
          break;
      }
    }

    Name.text = "Name: " + curData.Name;
    Email.text = "Email: " + curData.Email;
    PasspordId.text = "Passpord ID: " + curData.PasspordId;
    ExpireDate.text = "Expire Date: " + curData.ExpireDate;
    Country.text = "Country: " + curData.Country;
    City.text = "City: " + curData.City;
    CreditCard.text = "Credit Card: " + curData.CreditCard;
  }

  private string TruncatePercents(string input) {
    return Regex.Replace(input, @"@+", "");
  }
}

[Serializable]
public class HumanDataContainer {
  public List<HumanData> data = new List<HumanData>();
}

[Serializable]
public class HumanData {
  public string Name;
  public string Email;
  public string PasspordId;
  public string ExpireDate;
  public string Country;
  public string City;
  public string CreditCard;
  public bool faked = false;
  public string fakedReason = "";
}
