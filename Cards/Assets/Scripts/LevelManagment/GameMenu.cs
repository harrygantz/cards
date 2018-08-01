using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DEAL.LevelManagement
{
    public class GameMenu : Menu<GameMenu>
    {
        public void OnPausePressed()
        {
            Time.timeScale = 0;

            PauseMenu.Open();
        }
    } 
}

