using UnityEngine;
using System.Collections;

public class MainUI : MonoBehaviour {

	private bool initialized = false;
	private bool loggedIn = false;
	private string scope = "publish_actions";
	private Rect screenArea = new Rect();

	// Use this for initialization
	void Start () {
		screenArea.width = Screen.width;
		screenArea.height = Screen.height;
		FB.Init (OnInitComplete, OnUnityWillHide);
	}

	void OnInitComplete() {
		initialized = true;
	}

	void OnUnityWillHide(bool gameShown) {
		Time.timeScale = gameShown ? 1f : 0f;
	}

	void OnGUI () {
		GUILayout.BeginArea (screenArea);
		GUILayout.BeginVertical ();
		GUILayout.FlexibleSpace ();

		if(initialized) {
			GUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace ();
			string buttonLabel = FB.IsLoggedIn ? "Log Out" : "Log In";
			if(GUILayout.Button(buttonLabel, GUILayout.Width(350), GUILayout.Height(100))) {
				if(!FB.IsLoggedIn) {
					FB.Login (scope, OnLoginComplete);
				}else{
					FB.Logout ();
					loggedIn = false;
				}
			}
			GUILayout.FlexibleSpace ();
			GUILayout.EndHorizontal ();

			/////////////////////////////
			GUILayout.Space (100);
			/////////////////////////////


			GUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace ();
			if(loggedIn) {
				if(GUILayout.Button("Share on Facebook", GUILayout.Width(350), GUILayout.Height(100))) {
					OpenPostFeedDialog ();
				}
			}
			GUILayout.FlexibleSpace ();
			GUILayout.EndHorizontal ();
		}else{
			GUILayout.Label("Initializing...");
		}

		GUILayout.FlexibleSpace ();
		GUILayout.EndVertical ();
		GUILayout.EndArea ();
	}

	void OnLoginComplete(FBResult result) {
		loggedIn = FB.IsLoggedIn;
	}

	void OpenPostFeedDialog() {
		string toId = "";
		string link = "http://klab.com";
		string linkName = "SpellChain";
		string linkCaption = "Card Game";
		string linkDescription = "Let's battle!";
		string picture = "";
		string mediaSource = "";
		string actionName = "";
		string actionLink = "";
		string reference = "";
		FB.Feed (toId, link, linkName, linkCaption, linkDescription, picture, mediaSource, actionName, actionLink, reference);
	}
}
