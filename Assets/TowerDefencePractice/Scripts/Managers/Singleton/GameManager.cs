using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Managers
{
    public class GameManager : ManagerBase<GameManager>
    {
        public enum GameState
        {
            Title = 1,
            Menu,
            Battle
        }

        public GameState state;

        public static event Action<GameState> OnGameStateChanged;

        private void OnEnterGameState(GameState newState)
        {
            switch (newState)
            {
                case GameState.Title:
                    break;
                case GameState.Menu:
                    break;
                case GameState.Battle:
                    break;
            }
        }

        private void OnExitGameState(GameState prevState)
        {
            switch (prevState)
            {
                case GameState.Title:
                    break;
                case GameState.Menu:
                    break;
                case GameState.Battle:
                    break;
            }
        }


        public void UpdateGameState(GameState newState)
        {
            OnExitGameState(state);
            state = newState;
            OnEnterGameState(state);

            OnGameStateChanged?.Invoke(newState);
        }
    }
}