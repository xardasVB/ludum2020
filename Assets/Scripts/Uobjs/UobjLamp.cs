using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UobjLamp : Uobject {
  public Light light;
  public Material turnOnMat;
  public Material turnOffMat;
  public AudioSource turnOn;
  public AudioSource turnOff;

  public void TurnOn() {
    light.enabled = true;
    InteractText = "Turn off";
    InteractAction.RemoveListener(TurnOn);
    InteractAction.AddListener(TurnOff);
    turnOn?.Play();
    GetComponent<Renderer>().material = turnOnMat;
  }

  public void TurnOff() {
    light.enabled = false;
    InteractText = "Turn on";
    InteractAction.RemoveListener(TurnOff);
    InteractAction.AddListener(TurnOn);
    turnOff?.Play();
    GetComponent<Renderer>().material = turnOffMat;
  }

}
