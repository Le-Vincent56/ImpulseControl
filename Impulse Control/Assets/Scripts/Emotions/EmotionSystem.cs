using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ImpulseControl.Events;
using ImpulseControl.Modifiers;
using ImpulseControl.Timers;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Timer = System.Timers.Timer;

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

        [SerializeField] private int angerPriority = 3;
        [SerializeField] private int fearPriority = 1;
        [SerializeField] private int envyPriority = 2;

        [Header("References")] 
        [SerializeField] private LiveModifiers liveModifiers;
        
        private Dictionary<Emotion, int> emotionPriority;
        private List<(Emotion emotion, System.Action crashAction)> emotions;
        
        private CountdownTimer timer;
        private CountdownTimer timerExhausted;
        private EmotionType currentCrashOut;
        private EmotionType currentExhausted;
        
        public Emotion Anger => anger;
        public Emotion Fear => fear;
        public Emotion Envy => envy;

        private void Start()
        {
            liveModifiers = GetComponent<LiveModifiers>();
            timer = new CountdownTimer(1f);
            timerExhausted = new CountdownTimer(1f);


            anger.Start(liveModifiers);
            fear.Start(liveModifiers);
            envy.Start(liveModifiers);

            timer.OnTimerStop += () =>
            {
                switch (currentCrashOut)
                {
                    case EmotionType.Anger:
                        EventBus<Event_CrashOutEnd>.Raise(new Event_CrashOutEnd(
                        {
                            emotionType = EmotionType.Anger
                        });
                        currentExhausted = EmotionType.Anger;
                        anger.Exhausted();
                        envy.ResetToNormal();
                        fear.ResetToNormal();
                        timerExhausted.Reset(liveModifiers.Anger.exhaustionDuration);
                        timerExhausted.Start();
                        break;
                    case EmotionType.Envy:
                        EventBus<Event_CrashOutEnd>.Raise(new Event_CrashOutEnd(
                        {
                            emotionType = EmotionType.Envy
                        });
                        currentExhausted = EmotionType.Envy;
                        envy.Exhausted();
                        anger.ResetToNormal();
                        fear.ResetToNormal();
                        timerExhausted.Reset(liveModifiers.Envy.exhaustionDuration);
                        timerExhausted.Start();
                        break;
                    case EmotionType.Fear:
                        EventBus<Event_CrashOutEnd>.Raise(new Event_CrashOutEnd(
                        {
                            emotionType = EmotionType.Fear
                        });
                        currentExhausted = EmotionType.Fear;
                        fear.ExhaustedFear();
                        envy.ExhaustedFear();
                        anger.ExhaustedFear();
                        timerExhausted.Reset(liveModifiers.Fear.exhaustionDuration);
                        timerExhausted.Start();
                        break;
                }

                ;
            };

            timerExhausted.OnTimerStop += () =>
            {
                switch (currentExhausted)
                {
                    case EmotionType.Anger:
                        anger.ResetToNormal();
                        break;
                    case EmotionType.Envy:
                        envy.ResetToNormal();
                        EventBus<Event_EnvyExhaustedFinished>.Raise(new Event_EnvyExhaustedFinished());
                        break;
                    case EmotionType.Fear:
                        fear.ResetToNormal();
                        envy.ResetToNormal();
                        anger.ResetToNormal();
                        break;
                }
            };
        

        //list of emotions and their corresponding crash actions
            emotions = new List<(Emotion emotion, System.Action crashAction)>()
            {
                (anger, () =>
                {
                    EventBus<Event_CrashOut>.Raise(new Event_CrashOut()
                    {
                        emotionType = anger.EmotionType
                    });
                    anger.CrashOut(liveModifiers.Anger.crashOutDuration);
                    envy.Pause();
                    fear.Pause();
                    timerExhausted.Pause(true);
                    currentCrashOut = EmotionType.Anger;
                    timer.Reset(liveModifiers.Fear.crashOutDuration);
                    timer.Start();
                }),
                (fear, () =>
                {
                    EventBus<Event_CrashOut>.Raise(new Event_CrashOut()
                    {
                        emotionType = fear.EmotionType
                    });
                    fear.CrashOut(liveModifiers.Fear.crashOutDuration);
                    envy.Pause();
                    anger.Pause();
                    timerExhausted.Pause(true);
                    currentCrashOut = EmotionType.Fear;
                    timer.Reset(liveModifiers.Fear.crashOutDuration);
                    timer.Start();
                }),
                (envy, () =>
                {
                    EventBus<Event_CrashOut>.Raise(new Event_CrashOut()
                    {
                        emotionType = envy.EmotionType
                    });
                    Envy.CrashOut(liveModifiers.Envy.crashOutDuration);
                    fear.Pause();
                    anger.Pause();
                    timerExhausted.Pause(true);
                    currentCrashOut = EmotionType.Envy;
                    timer.Reset(liveModifiers.Envy.crashOutDuration);
                    timer.Start();
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
                if (emotion.Update())
                {
                    crashAction();
                    break;
                }
            }
            
            
            
        }

        /// <summary>
        /// Get an Emotion by an Emotion Type
        /// </summary>
        public Emotion GetEmotionByType(EmotionType emotionType)
        {
            return emotionType switch
            {
                EmotionType.Anger => anger,
                EmotionType.Fear => fear,
                EmotionType.Envy => envy,
                _ => null
            };
        }
        void OnDestroy()
        {
            timer.Dispose();
            timerExhausted.Dispose();
        }
    }
}
