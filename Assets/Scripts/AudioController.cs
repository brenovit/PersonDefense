using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AudioController : MonoBehaviour {

	public	Slider volumeSlider;								//controlador de audio(não funcionando)
	public Text volumeIndicator;
	private float volume = 0f;
	void Start(){
		volumeSlider.value = AudioListener.volume;
		volume = volumeSlider.value * 100;
		volumeIndicator.text = "Volume\n"+volume.ToString ("#")+"%";
	}

	public void VolumeMusica(){		
		AudioListener.volume = volumeSlider.value;
		volume = volumeSlider.value * 100;
		volumeIndicator.text = "Volume\n"+volume.ToString ("#")+"%";
    }
}
