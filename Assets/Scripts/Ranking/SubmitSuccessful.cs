using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SubmitSuccessful : MonoBehaviour {
	public Text StatusText;
	public Button submit;

	void WasClicked(){
		print ("Inside WasClicked");
		submit.gameObject.SetActive (false);
	}
	public void hideButton(){
		Button btn = submit.GetComponent<Button> ();
		btn.onClick.AddListener (WasClicked);
		WasClicked ();
		StatusText.text = "Submit Successful!";
	}


}
