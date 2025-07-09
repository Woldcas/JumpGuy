using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float mixerMultiplier = 25;

    [Header("SFX Settings")]
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TextMeshProUGUI sfxValueText;
    [SerializeField] private string sfxParametr;

    [Header("BGM Settings")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private TextMeshProUGUI bgmValueText;
    [SerializeField] private string bgmParametr;

    public void SFXSliderValue(float value)
    {
        sfxValueText.text = Mathf.RoundToInt(value * 100) + "%"; // Display percentage value
        float newValue = Mathf.Log10(value) * mixerMultiplier; // Convert linear value to logarithmic scale for audio mixer
        audioMixer.SetFloat(sfxParametr, newValue);
    }   

    public void BGMSliderValue(float value)
    {
        bgmValueText.text = Mathf.RoundToInt(value * 100) + "%";
        float newValue = Mathf.Log10(value) * mixerMultiplier;
        audioMixer.SetFloat(bgmParametr, newValue);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(sfxParametr, sfxSlider.value);
        PlayerPrefs.SetFloat(bgmParametr, bgmSlider.value);
    }

    private void OnEnable()
    {
        sfxSlider.value = PlayerPrefs.GetFloat(sfxParametr, .7f);
        bgmSlider.value = PlayerPrefs.GetFloat(bgmParametr, .7f);
    }
}
