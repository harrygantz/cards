using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DEAL;

namespace LevelManagement
{
	public class MainMenu : Menu<MainMenu>
	{
		[SerializeField]
		private float _playDelay = 0.5f;
		
		[SerializeField]
		private TransitionFader _startTransitionPrefab;
		
		public void OnPlayPressed()
		{
			StartCoroutine(OnPlayPressedRoutine());
		}

		private IEnumerator OnPlayPressedRoutine()
		{
			TransitionFader.PlayTransition(_startTransitionPrefab);
			LevelLoader.LoadNextLevel();
			yield return new WaitForSeconds(_playDelay);
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
