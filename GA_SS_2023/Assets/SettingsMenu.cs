using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SettingsMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer audiomixer;
    public AudioMixer audiomixerfx;
    public void SetVolume(float volume){
        audiomixer.SetFloat("volume",volume);
    }
    public void SetVolumefx(float fxvolume){
        audiomixerfx.SetFloat("FxVolume",fxvolume);
    }
}
