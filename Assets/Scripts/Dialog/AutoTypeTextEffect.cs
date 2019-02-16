using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Serialization;

public class AutoTypeTextEffect : MonoBehaviour {

	string text;
	string currentText = ""; 
	float delay = .05f;

	AudioSource sound;


	
	bool nextTextSet = false;
	float nextTextDelay = 2f;


	public GameObject myBaloon;
	

	// Use this for initialization
	public void StartText () {
		print("texto startado");
		text = GetComponent<Text> ().text;
		sound = GetComponent<AudioSource> ();

		StartCoroutine (showText ());

	}
	/*
	void Update(){
		//print ("text lengh: " + text.Length+ " Current Text Lengh: "+ currentText.Length);
		if (!startOrEndText) {
			if (currentText.Length == text.Length - 1) {
				nextTextSet = true;
			}

			if (nextTextSet) {
				if (nextTextDelay <= 0) {
					ShowNextText ();

				} else {
					nextTextDelay -= Time.deltaTime;
				}
			}
		}
	}
*/
	IEnumerator showText(){
		print("mostrando texto");
		for (int i = 0; i < text.Length + 1; i++) {
			if (i > 1 && currentText.Length >= text.Length)
			{
				i = currentText.Length;
			}
			else
			{
				if (sound != null)
				{
					sound.Play();
				}
				else
				{
					Debug.Log("no sound reference found");
				}
				currentText = text.Substring (0, i);
				this.GetComponent<Text> ().text = currentText;
			}

			yield return new WaitForSeconds (delay);
		}
	}

	public void EndText()
	{
		currentText = text;
		this.GetComponent<Text> ().text = currentText;
	}
	public void HideBalloon()
	{
		myBaloon.SetActive(false);
	}

	public bool IsTextComplete()
	{
		bool status;
		if (currentText.Length < text.Length)
		{
			status = false;
		}
		else
		{
			status = true;
		}

		return status;
	}
	
	/*
	public void ShowNextText() //set the next text with dialog to display
	{
		if (currentText.Length < text.Length - 1)
		{
			
			return;
		}
		if (!lastText) {
			NextTextObject.SetActive (true);
			gameObject.SetActive (false);
		} else {
            if (interactionObject != null)
            {
                interactionObject.InteractWith();
            }

			NextTextObject.SetActive (false);
			gameObject.SetActive (false);

		}
	}

	public void SetInitialText() //setting text withou any tipe of dialog to act as a placeholder
	{
		initialTextObject.SetActive (true);
		gameObject.SetActive (false);

	}
	*/
}
