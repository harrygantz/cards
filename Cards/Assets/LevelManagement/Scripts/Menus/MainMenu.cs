using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DEAL;

namespace LevelManagement
{
	public class MainMenu : Menu<MainMenu>
	{
		public void OnPlayPressed()
		{
			if (GameManager.Instance != null)
			{
				GameManager.Instance.LoadNextLevel();
			}

			GameMenu.Open();
		}

		public void OnSettingsPressed()
		{
			SettingsMenu.Open();
		}

		public void OnLeaderboardPressed()
		{ 
			LeaderboardsMenu.Open();
		}

		public override void OnBackPressed()
		{
			//base.OnBackPress();
			Application.Quit();
		}
	}


}
