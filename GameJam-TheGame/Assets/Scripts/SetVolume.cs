using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    [SerializeField]
    private AudioMixer Mixer;

    [SerializeField]
    private string volumeName;

    [SerializeField]
    private float standardValue = 0.5f;

    public void Start()
    {
        this.GetComponent<Slider>().value = PlayerPrefs.GetFloat(volumeName, standardValue);
        Debug.Log("Level set: " + PlayerPrefs.GetFloat(volumeName));
    }
    public void SetLevel(float sliderValue)
    {
        Mixer.SetFloat(volumeName, Mathf.Log10(sliderValue)*20);
        PlayerPrefs.SetFloat(volumeName, sliderValue);
        Debug.Log("Level Changed:" + " Master: " + (Mathf.Log10(sliderValue) * 20) + " slider: " + sliderValue);
    }
}
