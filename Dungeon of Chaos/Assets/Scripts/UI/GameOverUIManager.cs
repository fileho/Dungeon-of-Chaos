using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Currently, we are not using this class, as this way of handling dying slows down the game too much (we are
/// attempting to have a more fast paced game)
/// </summary>
public class GameOverUIManager : MonoBehaviour {
	[Header ("=== Game Won Menu ===")]
	[SerializeField]
	Button mainMenu;
	[SerializeField]
	Button quitGame;

	private void OnEnable ()
	{
		mainMenu.onClick.AddListener (OnMainMenuPressed);
		quitGame.onClick.AddListener (OnQuitGamePressed);
	}

	private void OnDisable ()
	{
		mainMenu.onClick.RemoveListener (OnMainMenuPressed);
		quitGame.onClick.RemoveListener (OnQuitGamePressed);
	}

	void OnMainMenuPressed ()
	{
		print ("Main Menu Called");
		UIEvents.MainMenuPressed?.Invoke ();
	}

	void OnQuitGamePressed ()
	{
		print ("Application Quit Called");
		Application.Quit ();
	}
}
