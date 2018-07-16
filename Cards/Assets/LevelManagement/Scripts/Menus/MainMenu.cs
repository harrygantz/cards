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
		}

		public void OnSettingsPressed()
		{
			if (MenuManager.Instance != null && SettingsMenu.Instance != null)
			{
				MenuManager.Instance.OpenMenu(SettingsMenu.Instance);
			}
		}

		public void OnLeaderboardPressed()
		{ 
			if (MenuManager.Instance != null && LeaderboardsMenu.Instance != null)
			{
				MenuManager.Instance.OpenMenu(LeaderboardsMenu.Instance);
			}
		}

		public override void OnBackPress()
		{
			//base.OnBackPress();
			Application.Quit();
		}
	}


}
