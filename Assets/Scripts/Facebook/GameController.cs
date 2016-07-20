using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using utils;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject pic;
	public Button button;
	void Start () {
		if (User.pic != null) {
			Sprite sprite = Sprite.Create (User.pic, new Rect (0, 0, 400, 400), new Vector2 (0.5f, 0.5f));
			pic.GetComponent<Image> ().sprite = sprite;	
		}
		button.onClick.AddListener (() => shareLink ());

	}

	void shareLink(){
		FacebookController.instance.shareLink ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
