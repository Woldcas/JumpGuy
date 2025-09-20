using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LocalizationManager : MonoBehaviour
{
    [SerializeField] private Button englishButton;
    [SerializeField] private Button turkishButton;
    [SerializeField] private Button russianButton;

    private void Awake()
    {
        englishButton.onClick.AddListener(() => SetLanguage(LanguageType.English));
        turkishButton.onClick.AddListener(() => SetLanguage(LanguageType.Turkish));
        russianButton.onClick.AddListener(() => SetLanguage(LanguageType.Russian));
    }

    public void SetLanguage(LanguageType languageType)
    {
        foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
        {
            if(locale.LocaleName.Equals(languageType.ToString()))
            {
                LocalizationSettings.SelectedLocale = locale;
                return;
            }
        }
    }
}
