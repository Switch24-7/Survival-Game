using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour {

	public Texture2D fadedOutTexture; //texture that will overlay the scsreen
	public float fadeSpeed = 0.8f; // the fading speed

	private int drawDepth = -1000; //texture's order in draw hierarchy: very low means it will be on top of everything else 
	private float alpha = 1.0f; // the texture's alpha value between 0 and 1
	private int fadeDir = -1; //fade direction: fade in = 01 and fade out = 1

	void OnGUI()
	{
		//fade out/in the alpha value using a direction, a speed and Time.deltaTime to convert the operation to seconds
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		// force (clamp) the number between 0 and 1 because GUI.color uses alpha values between 0 and 1
		alpha = Mathf.Clamp01(alpha);

		// set color of the GUI (texture). All color values remain the same and the Alpha is set to the alpha variable
		GUI.color = new Color (GUI.color.r,GUI.color.g,GUI.color.b, alpha);				 // set the alpha value
		GUI.depth = drawDepth;															 // make the black texture render on top
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadedOutTexture); // draw the texture to fit the entire screen
	}

	// sets fadeDir to the direction parameter making the scene fade in if -1 or out if 1
	public float BeginFade (int direction)
	{
		fadeDir = direction;
		return (fadeSpeed); // return the fadeSpeed variable so it's easy to time the Application.LoadLevel();
	}

	// OnLevelWasLoaded is called when a level is loaded. It takes loaded level index (int) as a paremeter so you can limit the fade in to certain scenes
	void OnLevelFinishedLoading()
	{
		// alpha = 1; // use this if the alpha is not set to 1 by default
		BeginFade(-1); //calls the fade in function
	}
}
