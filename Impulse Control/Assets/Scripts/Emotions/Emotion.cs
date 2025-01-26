using ImpulseControl.Modifiers;
using ImpulseControl.Timers;
using Unity.VisualScripting;
using UnityEngine;
using Timer = System.Timers.Timer;


namespace ImpulseControl
{
    
    public enum EmotionStates
    {
        Exhausted,
        ExhaustedFear,
        CrashingOut,
        Paused,
        Normal
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
        [SerializeField] private EmotionType emotionType;

        [Header("View")]
        [SerializeField] private float currentLevel;
        [SerializeField] private float rateOfIncrease;
        [SerializeField] private LiveModifiers liveModifiers;
        [SerializeField] private EmotionStates state = EmotionStates.Normal;
        [SerializeField] private float crashOutDuration;

        public EmotionType EmotionType => emotionType;
        public EmotionStates EmotionState => state;

        public void Start(LiveModifiers mods)
        {
            state = EmotionStates.Normal;
            liveModifiers = mods;
            currentLevel = startingLevel;
        }
        
        /// <summary>
        /// Adds to the emotion based on its rate of increase each frame
        /// If crash out return true
        /// </summary>
        /// <returns>bool crash out</returns>
        public bool Update()
        {
            //update normal rate
            rateOfIncrease = emotionType switch
            {
                EmotionType.Anger => liveModifiers.Anger.angerFillPerSecond,
                EmotionType.Fear => liveModifiers.Fear.fearFillPerSecond,
                EmotionType.Envy => liveModifiers.Envy.envyFillPerSecond,
                _ => liveModifiers.Anger.angerFillPerSecond
            };

            switch(state)
            {
                case EmotionStates.Exhausted:
                    break;
                case EmotionStates.Normal:
                    AddOrRemoveBubblesRate(rateOfIncrease * Time.deltaTime);
                    if (currentLevel >= maxLevel) return true;
                    break;
                case EmotionStates.Paused:
                    break;
                case EmotionStates.CrashingOut:
                    AddOrRemoveBubblesRate(-1/crashOutDuration * Time.deltaTime);
                    break;
                case EmotionStates.ExhaustedFear:
                    AddOrRemoveBubblesRate(rateOfIncrease * liveModifiers.Fear.exhaustionEmotionFillPercentage * Time.deltaTime);
                    if (currentLevel >= maxLevel) return true;
                    break;
                
            }
            return false;
        }

        #region ModifyStates
        /// <summary>
        /// Called when another emotion crashes out
        /// </summary>
        public void Exhausted()
        {
            Debug.Log(emotionType + " Exhausted");
            currentLevel = 0;
            state = EmotionStates.Exhausted;
        }
        
        public void Pause()
        {
            Debug.Log(emotionType + " Paused");
            currentLevel = 0;
            state = EmotionStates.Paused;
        }
        
        //Called to exhaust fear, caches the fear rate
        public void ExhaustedFear()
        {
            Debug.Log(emotionType + " Exhausted fear");
            currentLevel = 0;
            state = EmotionStates.ExhaustedFear;
        }
        
        //called to crasj out, decreases the rate.
        public void CrashOut(float duration)
        {
            Debug.Log(emotionType + " Crash Out");
            crashOutDuration = duration;
            state = EmotionStates.CrashingOut;
        }

        public void ResetToNormal()
        {
            state = EmotionStates.Normal;
            Debug.Log(emotionType + "Normal");
        }

        #endregion
       
        #region Modify Amount
        /// <summary>
        /// Adds or removes to the emotion. Input has to have - to subtrace
        /// </summary>
        /// <param name="addition"></param>
        public void AddOrRemoveBubblesRate(float change)
        {
            currentLevel += change;
            if (currentLevel < 0) currentLevel = 0;
        }
        
        public void RemoveBubbles(float change)
        {
            currentLevel -= change;
            if (currentLevel < 0) currentLevel = 0;
        }
        #endregion
    }
}
