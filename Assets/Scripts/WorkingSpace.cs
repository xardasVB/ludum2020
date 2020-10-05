using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
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
    data = JsonUtility.FromJson<HumanDataContainer>(File.ReadAllText(Application.dataPath + "/Data.json"));
    SetRandomData();
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
      if (GameController.daysCount == 1) 
        CanvasScript.Instance.Computer.FinishDay();
      else
        CanvasScript.Instance.Computer.FinishDay2();
    }
    curData = data.data.OrderBy(a => Random.value).FirstOrDefault();
    Name.text = "Name: " + curData.Name;
    Email.text = "Email: " + curData.Email;
    PasspordId.text = "Passpord ID: " + curData.PasspordId;
    ExpireDate.text = "Expire Date: " + curData.ExpireDate;
    Country.text = "Country: " + curData.Country;
    City.text = "City: " + curData.City;
    CreditCard.text = "Credit Card: " + curData.CreditCard;

    if (Random.Range(0, 100) < fakeChance) {
      curData.faked = true;
      switch (Random.Range(0, 4)) {
        case 0:
          Name.text = "Name: " + Guid.NewGuid();
          curData.fakedReason = "Name is not real";
          break;
        case 1:
          Email.text = "Email: " + curData.Email.Replace("@", "");
          curData.fakedReason = "Email is incorrect";
          break;
        case 2:
          ExpireDate.text = "Expire Date: " + curData.ExpireDate.Remove(curData.ExpireDate.Length - 1, 1) + Random.Range(0, 4);
          curData.fakedReason = "Passport already expired";
          break;
        case 3:
          if (Random.value > 0.5)
            CreditCard.text = "Credit Card: " + curData.CreditCard.Remove(0, 5);
          else
            CreditCard.text = "Credit Card: " + curData.CreditCard.Replace(curData.CreditCard[Random.Range(0, curData.CreditCard.Length)], 'i');
          curData.fakedReason = "Credit card is fake";
          break;
        default:
          break;
      }
    }

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
