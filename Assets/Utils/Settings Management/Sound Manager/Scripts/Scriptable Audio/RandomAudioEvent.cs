﻿using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName="Audio Events/Random")]
public class RandomAudioEvent : AudioEvent
{
	public AudioClip[] clips;

	[Range(0.1f, 1f)] public float volumeMin = 0.45f;
	[Range(0.1f, 1f)] public float volumeMax = 0.55f;
	
	[Range(0.1f, 2f)] public float pitchMin = 0.9f;
	[Range(0.1f, 2f)] public float pitchMax = 1.1f;

	public override void Play(AudioSource source)
	{
		if (clips.Length == 0) return;

		source.clip = clips[Random.Range(0, clips.Length)];
		source.volume = Random.Range(volumeMin, volumeMax) * AdjustVolume(audioType);
		source.pitch = Random.Range(pitchMin, pitchMax);
		source.Play();
	}
}