using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AudioController : MonoBehaviour {

	//public	Slider volumeSlider;								//controlador de audio(não funcionando)
	//public Text volumeIndicator;
	//private float volume = 0f;

	private bool musicMute = false;
	private bool effectMute = false;

	void OnEnable(){
		EventManager.CriarEvento ("MuteMusic", MuteMusic);
		EventManager.CriarEvento ("MuteEffect", MuteEffect);
	}

	void OnDisable(){
		EventManager.RemoverEvento ("MuteMusic", MuteMusic);
		EventManager.RemoverEvento ("MuteEffect", MuteEffect);
	}

	/*void Start(){
		volumeSlider.value = AudioListener.volume;
		volume = volumeSlider.value * 100;
		volumeIndicator.text = "Volume\n"+volume.ToString ("#")+"%";
	}

	public void VolumeMusica(){		
		AudioListener.volume = volumeSlider.value;
		volume = volumeSlider.value * 100;
		volumeIndicator.text = "Volume\n"+volume.ToString ("#")+"%";
    }*/

	public void MuteMusic(GameObject obj, string param){
		//deixar todas as musicas mudas, salvar em memoria
	}

	public void MuteEffect(GameObject obj, string param){
		//deixar todos os efeitos mudos. salvar em memoria
	}

}
