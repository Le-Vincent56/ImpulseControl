using ImpulseControl.Singletons;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace ImpulseControl.Audio
{

    public class SFXManager : PersistentSingleton<SFXManager>
    {
        private IObjectPool<SoundEmitter> soundEmitterPool;
        private readonly List<SoundEmitter> activeSoundEmitters = new List<SoundEmitter>();
        public readonly Queue<SoundEmitter> FrequentSoundEmitters = new Queue<SoundEmitter>();

        [SerializeField] private SoundEmitter soundEmitterPrefab;
        [SerializeField] private bool collectionCheck = true;
        [SerializeField] private int defaultCapacity = 10;
        [SerializeField] private int maxPoolSize = 100;
        [SerializeField] private int maxSoundInstances = 30;

        protected override void Awake()
        {
            base.Awake();

            // Initialize the Object Pool
            InitializePool();
        }

        /// <summary>
        /// Create a SoundBuilder to build sounds
        /// </summary>
        public SoundBuilder CreateSound() => new SoundBuilder(this);

        /// <summary>
        /// Create a de-activated Sound Emitter
        /// </summary>
        private SoundEmitter CreateSoundEmitter()
        {
            // Instantiate the Sound Emitter
            SoundEmitter soundEmitter = Instantiate(soundEmitterPrefab);

            // De-activate the Sound Emitter
            soundEmitter.gameObject.SetActive(false);

            return soundEmitter;
        }

        /// <summary>
        /// Take a Sound Emitter from the pool
        /// </summary>
        private void OnTakeFromPool(SoundEmitter soundEmitter)
        {
            if (soundEmitter == null) return;

            // Set the Sound Emitter as active
            soundEmitter.gameObject.SetActive(true);

            // Add it to the active list
            activeSoundEmitters.Add(soundEmitter);
        }

        /// <summary>
        /// Return a Sound Emitter to the pool
        /// </summary>
        private void OnReturnedToPool(SoundEmitter soundEmitter)
        {
            // Set the Sound Emitter as in-active
            soundEmitter.gameObject.SetActive(false);

            // Remove it from the active list
            activeSoundEmitters.Remove(soundEmitter);
        }

        /// <summary>
        /// Destroy a Sound Emitter
        /// </summary>
        private void OnDestroyPoolObject(SoundEmitter soundEmitter)
        {
            // Destroy the Sound Emitter object
            Destroy(soundEmitter.gameObject);
        }

        /// <summary>
        /// Initialize the Sound Emitter pool
        /// </summary>
        private void InitializePool()
        {
            soundEmitterPool = new ObjectPool<SoundEmitter>(
                CreateSoundEmitter,
                OnTakeFromPool,
                OnReturnedToPool,
                OnDestroyPoolObject,
                collectionCheck,
                defaultCapacity,
                maxPoolSize
            );
        }

        /// <summary>
        /// Get a Sound Emitter from the pool
        /// </summary>
        public SoundEmitter Get() => soundEmitterPool.Get();

        /// <summary>
        /// Return a Sound Emitter to the pool
        /// </summary>
        public void ReturnToPool(SoundEmitter soundEmitter) => soundEmitterPool.Release(soundEmitter);

        /// <summary>
        /// Check if a Sound can be played
        /// </summary>
        public bool CanPlaySound(SoundData data)
        {
            // Exit case - if the sound is not a frequent sound; assume that it can always be played
            if (!data.FrequentSound) return true;

            // Check if the Frequent Sound Emitters queue exceeds the max amount of sound instances and 
            // if a SoundEmitter can be dequeued from the queue
            if (FrequentSoundEmitters.Count >= maxSoundInstances && FrequentSoundEmitters.TryDequeue(out SoundEmitter soundEmitter))
            {
                // Try/catch
                try
                {
                    // Stop the Sound Emitter
                    soundEmitter.Stop();
                    return true;
                }
                catch
                {
                    // Debug.Log("SoundEmitter is already released");
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// Stop an active sound from playing
        /// </summary>
        public void StopSound(SoundData data)
        {
            // Create a list to store indexes
            List<int> soundsToStopIndexes = new List<int>();

            for (int i = 0; i < activeSoundEmitters.Count; i++)
            {
                // Skip if the data does not match
                if (activeSoundEmitters[i].Data != data) continue;

                // Add the index
                soundsToStopIndexes.Add(i);
            }

            // Iterate through each index
            foreach (int index in soundsToStopIndexes)
            {
                // Stop the Sound Emitter at that index
                activeSoundEmitters[index].Stop();
            }
        }
    }
}
