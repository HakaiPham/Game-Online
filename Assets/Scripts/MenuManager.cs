using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;
    public Button volumeButton;
    public Slider volumeSlider;
    public Animator sliderAnim;

    private bool sliderVisible = false;

    void Start()
    {
        startButton.onClick.AddListener(() => SceneManager.LoadScene("Selection"));
        quitButton.onClick.AddListener(() => Application.Quit());
        volumeButton.onClick.AddListener(OnVolumeClick);

        volumeSlider.onValueChanged.AddListener(OnSliderChanged);

        volumeSlider.gameObject.SetActive(true);

        // Load volume
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        volumeSlider.value = savedVolume;
        AudioListener.volume = savedVolume;
    }

    void OnVolumeClick()
    {
        if (!sliderVisible)
        {
            volumeSlider.gameObject.SetActive(true);
            sliderAnim.SetTrigger("SlideIn");
            sliderVisible = true;
        }
        else
        {
            sliderAnim.SetTrigger("SlideOut");
            sliderVisible = false;
        }
    }

    void OnSliderChanged(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("MasterVolume", value);
    }
}
