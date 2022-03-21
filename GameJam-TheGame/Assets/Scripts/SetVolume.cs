using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer Mixer;
    float value = 0.5f;

    public void Awake()
    {
        bool result = Mixer.GetFloat("MyMasterVol", out value);
        if(result)
        {
            Debug.Log("Value set!");
            this.GetComponent<Slider>().value = Mathf.Abs(value/20);
        } else
        {
            Debug.Log("Value not set!");
            Debug.Log(value);
            this.GetComponent<Slider>().value = value;
        }
    }
    public void SetLevel(float sliderValue)
    {
        Mixer.SetFloat("MyMasterVol", Mathf.Log10(sliderValue)*20);
    }
}
