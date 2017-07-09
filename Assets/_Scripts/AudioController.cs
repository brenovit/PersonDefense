using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class PlayAudio {
	public string audioName;
	public AudioClip audioClip;
}

public class AudioController : MonoBehaviour {
	
	public AudioSource musicAudioSource;
	public AudioSource effectAudioSource;

	[SerializeField]
	public List<PlayAudio> playAudioList;

	private float musicVolume = 1;
	private float effectVolume = 1;

	void OnEnable(){
		EventManager.CriarEvento ("MuteMusic", MuteMusic);
		EventManager.CriarEvento ("MuteEffect", MuteEffect);
		EventManager.CriarEvento ("PlaySound", PlaySound);
	}

	void OnDisable(){
		EventManager.RemoverEvento ("MuteMusic", MuteMusic);
		EventManager.RemoverEvento ("MuteEffect", MuteEffect);
		EventManager.RemoverEvento ("PlaySound", PlaySound);
	}

	/// <summary>
	/// Plaies the sound.
	/// </summary>
	/// <param name="audio">Audio clip object</param>
	/// <param name="param">The volume of the audio will play. (0-1)f</param>
	public void PlaySound(GameObject obj, string audioName){
		foreach (PlayAudio pa in playAudioList) {
			if(audioName.Equals(pa.audioName)){
				effectAudioSource.PlayOneShot (pa.audioClip);
			}
		}
		//float volume = string.IsNullOrEmpty (param) ? 0f : float.Parse
		//if ( && effectMute == false) {
		//}
	}

	public void MuteMusic(GameObject obj, string param){
		//deixar todas as musicas mudas, salvar em memoria
		musicVolume = musicVolume == 1f ? 0f : 1f;
		musicAudioSource.volume = musicVolume;
	}

	public void MuteEffect(GameObject obj, string param){
		//deixar todos os efeitos mudos. salvar em memoria
		effectVolume = effectVolume == 1f ? 0f : 1f;
		effectAudioSource.volume = effectVolume;
	}

	public void PlayAudio(AudioClip audio){
		effectAudioSource.PlayOneShot (audio);
	}
}
