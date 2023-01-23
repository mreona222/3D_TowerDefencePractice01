using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace TowerDefencePractice.Managers
{
    public class OptionInstanceManager : MonoBehaviour
    {
        [SerializeField]
        AudioMixer _mixer;

        [SerializeField]
        Slider masterSlider;
        [SerializeField]
        Slider bgmSlider;
        [SerializeField]
        Slider seSlider;

        private void Start()
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Option);

            masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
            bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
            seSlider.value = PlayerPrefs.GetFloat("SEVolume", 0.5f);
        }

        public void OnMasterValueChanged()
        {
            _mixer.SetFloat("Master", AudioManager.ConvertFloat2DB(masterSlider.value));
            PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);
        }

        public void OnBGMValueChanged()
        {
            _mixer.SetFloat("BGM", AudioManager.ConvertFloat2DB(bgmSlider.value));
            PlayerPrefs.SetFloat("BGMVolume", bgmSlider.value);
        }

        public void OnSEValueChanged()
        {
            _mixer.SetFloat("SE", AudioManager.ConvertFloat2DB(seSlider.value));
            PlayerPrefs.SetFloat("SEVolume", seSlider.value);
        }

        public void OnClickDefaultSettings()
        {
            PlayerPrefs.DeleteKey("MasterVolume");
            PlayerPrefs.DeleteKey("BGMVolume");
            PlayerPrefs.DeleteKey("SEVolume");
            masterSlider.value = 0.5f;
            bgmSlider.value = 0.5f;
            seSlider.value = 0.5f;
        }

        public void OnClickReturn2Title()
        {
            SceneTransitionManager.Instance.SceneTrnasitionNormal("Title");
        }
    }
}