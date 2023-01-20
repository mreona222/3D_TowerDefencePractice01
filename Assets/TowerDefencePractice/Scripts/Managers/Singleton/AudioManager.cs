using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Managers
{
    public class AudioManager : ManagerBase<AudioManager>
    {
        public enum TitleBGM
        {
            �r�b�O�����L�[,

        }

        public enum BattleBGM
        {
            el_matador,
        }

        public enum ResultBGM
        {
            �t�@���t�@�[��4,
            ��������o�u����,
            ����Ă�����,
        }

        [SerializeField]
        AudioClip[] bgmTitle;

        [SerializeField]
        AudioClip[] bgmBattle;

        [SerializeField]
        AudioClip[] bgmResult;

        [SerializeField]
        AudioSource source;

        protected override void Init()
        {
            base.Init();
            GameManager.OnGameStateChanged += ChangeBGM;
        }

        private void OnDestroy()
        {
            GameManager.OnGameStateChanged -= ChangeBGM;
        }

        private void ChangeBGM(GameManager.GameState state)
        {
            switch (state)
            {
                case GameManager.GameState.Title:
                    if (source.clip != bgmTitle[(int)TitleBGM.�r�b�O�����L�[])
                    {
                        source.clip = bgmTitle[(int)TitleBGM.�r�b�O�����L�[];
                        source.Play();
                    }
                    break;

                case GameManager.GameState.Menu:
                    if (source.clip != bgmTitle[(int)TitleBGM.�r�b�O�����L�[])
                    {
                        source.clip = bgmTitle[(int)TitleBGM.�r�b�O�����L�[];
                        source.Play();
                    }
                    break;

                case GameManager.GameState.Battle:
                    if (source.clip != bgmBattle[(int)BattleBGM.el_matador])
                    {
                        source.clip = bgmBattle[(int)BattleBGM.el_matador];
                        source.Play();
                    }
                    break;

            }
        }

        public void GameClear()
        {
            StartCoroutine(Clear());

            IEnumerator Clear()
            {
                source.clip = bgmResult[(int)ResultBGM.�t�@���t�@�[��4];
                source.loop = false;
                source.Play();
                yield return new WaitUntil(() => !source.isPlaying);
                source.clip = bgmResult[(int)ResultBGM.��������o�u����];
                source.loop = true;
                source.Play();
            }
        }

        public void GameFailed()
        {
            source.clip = bgmResult[(int)ResultBGM.����Ă�����];
            source.Play();
        }
    }
}