using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DEAL.Utilities;
using DEAL.Data;
using UnityEngine.UI;

namespace DEAL.LevelManagement
{
	public class MainMenu : Menu<MainMenu>
	{
		[SerializeField]
		private float _playDelay = 0.5f;
		
		[SerializeField]
		private TransitionFader _startTransitionPrefab;
		
		[SerializeField]
		private InputField _playerNameInputField;

		private DataManager _dataManager;

		protected override void Awake()
		{
			base.Awake();
			_dataManager = FindObjectOfType<DataManager>();
		}

		private void Start()
		{
			LoadPlayerInfo();
		}

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

		public void OnPlayerNameValueChanged(string text)
		{
			if (_dataManager != null)
			{
				_dataManager.PlayerName = text;
			}
		}

		public void OnPlayerNameEndEdit()
		{
			if (_dataManager != null)
			{
				_dataManager.Save();
			}
		}

		public override void OnBackPressed()
		{
			//base.OnBackPress();
			Application.Quit();
		}

		// This is a bullshit and should probably change later on because we really don't want a player to put his/her
		// info in the main menu screen.
		private void LoadPlayerInfo()
		{
			if (_dataManager != null && _playerNameInputField != null)
			{
				_dataManager.Load();
				_playerNameInputField.text = _dataManager.PlayerName;
			}

			
		}
	}


}
