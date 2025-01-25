using System;
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
            anger.Update();
            fear.Update();
            envy.Update();
        }
    }

    [System.Serializable]
    public class Emotion
    {
        [Header("Editable")]
        [Range(0,1)]
        [SerializeField] float startingLevel = .5f;
        [Range(0,1)]
        [SerializeField] float maxLevel = 1f;
        [Range(0,1)]
        [SerializeField] float rateOfIncrease = .01f;
        [SerializeField] private EmotionType emotionType;

        [Header("View")]
        [SerializeField] float currentLevel;
        
        public float  StartingLevel { get; set; }
        public float  MaxLevel { get; set; }
        public float  RateOfIncrease { get; set; }
        public void Start() => currentLevel = startingLevel;
        
        public void RemoveBubbles(float reduction)
        {
            currentLevel -= reduction;
            if (currentLevel < 0) currentLevel = 0;
        }

        public void AddBubbles(float addition)
        {
            currentLevel += addition;
            if (currentLevel >= 1.0)
            {
                
            }
        }

        public void Update() => AddBubbles(rateOfIncrease * Time.deltaTime);
    }
}
