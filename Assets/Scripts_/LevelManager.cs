using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {
	string name1;
	public void LoadLevel(string name)
	{
		name1 = name;
		StartCoroutine (ChangeLevel2());
	}
	public void QuitGame()
	{
		Application.Quit ();
	}
	public void LoadNextLevel()
	{
		StartCoroutine (ChangeLevel ());
		//print ("NEW LEVEL");
		//UnityEngine.SceneManagement.SceneManager.LoadScene (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex + 1);
	}
	public IEnumerator ChangeLevel()
	{
		float fadeTime = GameObject.Find ("Main Camera").GetComponent<Fading> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}
	public IEnumerator ChangeLevel2()
	{
		float fadeTime = GameObject.Find ("Main Camera").GetComponent<Fading> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
		Application.LoadLevel (name1);
	}

}
