using System.Collections.Generic;
using System.Linq;
using ImpulseControl.Events;
using UnityEngine;

namespace ImpulseControl
{
    public enum EmotionType
    {
        Anger,
        Fear,
        Envy
    }
    public class EmotionSystem : MonoBehaviour
    {
        [Header("Emotions")] 
        [SerializeField] private Emotion anger;
        [SerializeField] private Emotion fear;
        [SerializeField] private Emotion envy;

        private int angerPriority = 3;
        private int fearPriority = 1;
        private int envyPriority = 2;

        private Dictionary<Emotion, int> emotionPriority;
        private List<(Emotion emotion, System.Action crashAction)> emotions;
            
        
        public Emotion Anger => anger;
        public Emotion Fear => fear;
        public Emotion Envy => envy;

        private void Start()
        {
            anger.Start();
            fear.Start();
            envy.Start();
            
            //list of emotions and their corresponding crash actions
            emotions = new List<(Emotion emotion, System.Action crashAction)>()
            {
                (anger, () =>
                {
                    EventBus<Event_CrashOut>.Raise(new Event_CrashOut()
                    {
                        emotionType = anger.EmotionType
                    });
                    envy.CrashOut();
                    fear.CrashOut();
                }),
                (fear, () =>
                {
                    EventBus<Event_CrashOut>.Raise(new Event_CrashOut()
                    {
                        emotionType = fear.EmotionType
                    });
                    envy.CrashOut();
                    anger.CrashOut();
                }),
                (envy, () =>
                {
                    EventBus<Event_CrashOut>.Raise(new Event_CrashOut()
                    {
                        emotionType = envy.EmotionType
                    });
                    fear.CrashOut();
                    anger.CrashOut();
                })
            };
            //dictionary to help with sorting
            emotionPriority = new Dictionary<Emotion, int>
            {
                { anger, angerPriority },
                { fear, fearPriority },
                { envy, envyPriority }
            };
        }

       
        private void Update()
        {
            //sort emotions by corresding priority
            List<(Emotion emotion, System.Action crashAction)> emotionSorted
                = emotions.OrderByDescending((e => emotionPriority[e.emotion])).ToList();

            //check for crash and update each emotion
            foreach ((Emotion emotion, System.Action crashAction) in emotionSorted)
            {
                Debug.Log((emotion.EmotionType));
                if (emotion.Update())
                {
                    crashAction();
                    break;
                }
            }
        }
    }
}
