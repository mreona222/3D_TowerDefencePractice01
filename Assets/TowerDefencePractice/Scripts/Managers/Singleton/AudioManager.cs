using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace TowerDefencePractice.Managers
{
    public class AudioManager : ManagerBase<AudioManager>
    {
        [SerializeField]
        AudioMixer _mixer;

        public enum TitleBGM
        {
            ビッグモンキー,

        }

        public enum BattleBGM
        {
            el_matador,
        }

        public enum ResultBGM
        {
            ファンファーレ4,
            ごきげんバブリン,
            やってしもた,
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

        private void Start()
        {
            _mixer.SetFloat("Master", ConvertFloat2DB(PlayerPrefs.GetFloat("MasterVolume", 0.5f)));
            _mixer.SetFloat("BGM", ConvertFloat2DB(PlayerPrefs.GetFloat("BGMVolume", 0.5f)));
            _mixer.SetFloat("SE", ConvertFloat2DB(PlayerPrefs.GetFloat("SEVolume", 0.5f)));
        }

        private void OnDestroy()
        {
            GameManager.OnGameStateChanged -= ChangeBGM;
        }

        // float => DB
        public static float ConvertFloat2DB(float volume) =>
            Mathf.Clamp(20.0f * Mathf.Log10(Mathf.Clamp(volume, 0, 1.0f)), -80.0f, 0);

        private void ChangeBGM(GameManager.GameState state)
        {
            switch (state)
            {
                case GameManager.GameState.Title:
                    if (source.clip != bgmTitle[(int)TitleBGM.ビッグモンキー])
                    {
                        source.clip = bgmTitle[(int)TitleBGM.ビッグモンキー];
                        source.Play();
                    }
                    break;

                case GameManager.GameState.Menu:
                    if (source.clip != bgmTitle[(int)TitleBGM.ビッグモンキー])
                    {
                        source.clip = bgmTitle[(int)TitleBGM.ビッグモンキー];
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

                case GameManager.GameState.Option:
                    if (source.clip != bgmTitle[(int)TitleBGM.ビッグモンキー])
                    {
                        source.clip = bgmTitle[(int)TitleBGM.ビッグモンキー];
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
                source.clip = bgmResult[(int)ResultBGM.ファンファーレ4];
                source.loop = false;
                source.Play();
                yield return new WaitUntil(() => !source.isPlaying || source.clip != bgmResult[(int)ResultBGM.ファンファーレ4]);
                if(source.clip != bgmResult[(int)ResultBGM.ファンファーレ4])
                {
                    source.loop = true;
                    yield break;
                }
                source.clip = bgmResult[(int)ResultBGM.ごきげんバブリン];
                source.loop = true;
                source.Play();
            }
        }

        public void GameFailed()
        {
            source.clip = bgmResult[(int)ResultBGM.やってしもた];
            source.Play();
        }
    }
}