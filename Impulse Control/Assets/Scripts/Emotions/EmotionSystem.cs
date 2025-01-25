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

        private int priorityA = 1;
        private int priorityF = 2;
        private int priorityE = 3;

        public Emotion Anger => anger;
        public Emotion Fear => fear;
        public Emotion Envy => envy;

        private void Start()
        {
            anger.Start();
            fear.Start();
            envy.Start();
        }

        private void Update()
        {
            if (anger.Update())
            {
                EventBus<Event_CrashOut>.Raise(new Event_CrashOut()
                {
                    emotionType = anger.EmotionType
                });
                envy.CrashOut();
                fear.CrashOut();
                return;
            };
            if (fear.Update())
            {
                EventBus<Event_CrashOut>.Raise(new Event_CrashOut()
                {
                    emotionType = fear.EmotionType
                });
                envy.CrashOut();
                anger.CrashOut();
                return;
            };
            if(envy.Update())
            {
                EventBus<Event_CrashOut>.Raise(new Event_CrashOut()
                {
                    emotionType = fear.EmotionType
                });
                fear.CrashOut();
                anger.CrashOut();
            };
        }
    }
}
