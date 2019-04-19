using UnityEngine;
using UnityEngine.Experimental.Input;

public class PlayerControl : MonoBehaviour {

  public InputMaster controls;

  private void Awake () {
    controls.Player.Shoot.performed += ctx => Shoot ();
  }

  void Move () {
    print ("Player wants to move!");
  }

  void Shoot () {
    print ("We shot the sherif!");
  }

  private void OnEnable () {
    controls.Enable ();
  }
  private void OnDisable () {
    controls.Disable ();
  }
}