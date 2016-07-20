using UnityEngine;
using System.Collections;
using Facebook.Unity;
using System.Collections.Generic;
using utils;
//using System.Uri;

public class FacebookController : MonoBehaviour {

	public static FacebookController instance;

	void Awake(){
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad (gameObject);
			activateSDK ();
		}
	}

	void activateSDK(){
		if (!FB.IsInitialized) {
			FB.Init (onInitComplete, onHideUnity);
		} else {
			FB.ActivateApp ();
		}
	}

	void onInitComplete(){
		if (FB.IsInitialized) {
			FB.ActivateApp ();
		}
	}

	void onHideUnity(bool isGameShown){
		if (!isGameShown) {
			Time.timeScale = 0f;
		} else {
			Time.timeScale = 1f;
		}
	}

	public void login(){
		var perms = new List<string> ();
		perms.Add ("email");
		perms.Add ("public_profile");
		FB.LogInWithReadPermissions (perms, onCompleteCallback);
	}

	public void onCompleteCallback(ILoginResult result){
		if (FB.IsLoggedIn) {
			var token = Facebook.Unity.AccessToken.CurrentAccessToken;
			FB.API ("/me?fields=first_name,last_name,email,id",
				HttpMethod.GET, onProfileLoad);
			FB.API ("/me/picture?type=square&width=400&height=400", HttpMethod.GET, onPicComplete);
		}
	}

	void onProfileLoad(IResult result){
		User.first_name = result.ResultDictionary ["first_name"].ToString();
		User.last_name = result.ResultDictionary ["last_name"].ToString();

	}

	void onPicComplete(IGraphResult result){
		User.pic = result.Texture;
		MenuController.instance.changeScene ();
	}

	public void shareLink(){
		FB.ShareLink (new System.Uri ("http://www.facebook.com"), callback: shareCallback);
	}

	void shareCallback(IShareResult result){
		if (result.Cancelled || !string.IsNullOrEmpty (result.Error)) {
			print (result.Error);
		} else if (!string.IsNullOrEmpty (result.PostId)) {
			print (result.PostId);
		} else {
			print ("sin postid");
		}
	}


}
