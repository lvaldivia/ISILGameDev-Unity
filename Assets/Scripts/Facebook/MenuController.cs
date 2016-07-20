using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public Button startButton;
	public GameObject panel;
	public static MenuController instance;

	void Awake(){
		instance = this;
	}

	void Start () {
		panel.SetActive (false);
		startButton.onClick.AddListener (() => callLogin ());
	}

	void callLogin(){
		panel.SetActive (true);
		FacebookController.instance.login ();
	}

	public void deactivatePopup(){
		panel.SetActive (false);
	}

	public void changeScene(){
		SceneManager.LoadScene ("Game");
	}

	void shareLink(){
		
	}
}
