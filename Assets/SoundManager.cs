using _Project.Code.Data.Static.Sound;
using _Project.Code.Services.SoundPlayer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance;
    [HideInInspector] public bool IsSoundEnabled = true;
    private bool _isMusicEnabled = true;
    private AudioSource _audioSource;
    [HideInInspector] public bool IsMusicEnabled {
        get { return _isMusicEnabled; }
        set {
            if (value == _isMusicEnabled) return;
            _isMusicEnabled = value;
            if (_isMusicEnabled) {
                OnMusicEnabled();
            } else {
                OnMusicDisabled();
            }
        }
    }



    [Inject] private SoundPlayer _soundPlayer;

    private void Awake() {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        if (IsMusicEnabled) {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }

    public void PlaySound(SoundId id) {
        if (IsSoundEnabled)
            _soundPlayer.PlaySound(id);
    }

    private void OnMusicDisabled() {
        _audioSource.Pause();
    }

    private void OnMusicEnabled() {
        _audioSource.UnPause();
    }
}
