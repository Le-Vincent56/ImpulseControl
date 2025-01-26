using ImpulseControl.Extensions.GameObjects;
using System.Collections;
using UnityEngine;

namespace ImpulseControl.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEmitter : MonoBehaviour
    {
        private SFXManager sfxManager;
        private AudioSource audioSource;
        private Coroutine playingCoroutine;

        public SoundData Data { get; private set; }

        private void Awake()
        {
            // Get or add an AudioSource component
            audioSource = gameObject.GetOrAdd<AudioSource>();
        }

        /// <summary>
        /// Initialize the AudioSource according to a SoundData
        /// </summary>
        public void Initialize(SoundData data, SFXManager sfxManager)
        {
            // Get the Sound Manager
            this.sfxManager = sfxManager;

            // Set data
            audioSource.clip = data.Clip;
            audioSource.outputAudioMixerGroup = data.MixerGroup;
            audioSource.loop = data.Loop;
            audioSource.playOnAwake = data.PlayOnAwake;
            Data = data;
        }

        /// <summary>
        /// Play a sound through the AudioSource
        /// </summary>
        public void Play()
        {
            // Check if the coroutine is active
            if (playingCoroutine != null)
                // Stop the coroutine
                StopCoroutine(playingCoroutine);

            // Play the audio source and start the coroutine
            audioSource.Play();
            playingCoroutine = StartCoroutine(WaitForSoundToEnd());
        }

        /// <summary>
        /// Stop playing a sound through the Audio Source
        /// </summary>
        public void Stop()
        {
            // Check if the coroutine is active
            if (playingCoroutine != null)
            {
                // Stop the coroutine
                StopCoroutine(playingCoroutine);

                // Nullify the coroutine
                playingCoroutine = null;
            }

            // Stop the AudioSource and return the Sound Emitter to the pool
            audioSource.Stop();
            sfxManager.ReturnToPool(this);
        }

        /// <summary>
        /// Coroutine that yields until the AudioSource has finished playing a sound
        /// </summary>
        private IEnumerator WaitForSoundToEnd()
        {
            // Yield until the AudioSource has finished playing
            yield return new WaitWhile(() => audioSource.isPlaying);

            // Return the Sound Emitter to the pool
            sfxManager.ReturnToPool(this);
        }

        /// <summary>
        /// Randomize the pitch of the Sound
        /// </summary>
        public void WithRandomPitch(float min = -0.05f, float max = 0.05f)
        {
            audioSource.pitch += Random.Range(min, max);
        }
    }
}
