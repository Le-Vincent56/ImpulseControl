using UnityEngine;

namespace ImpulseControl
{
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
        public EmotionType EmotionType => emotionType;

        public void Start() => currentLevel = startingLevel;

        /// <summary>
        /// Called when another emotion crashes out
        /// </summary>
        public void CrashOut()
        {
            currentLevel = 0;
            rateOfIncrease = 0;
        }
        /// <summary>
        /// removes from emotion : checks if negative and sets to 0
        /// </summary>
        /// <param name="reduction"></param>
        public void RemoveBubbles(float reduction)
        {
            currentLevel -= reduction;
            if (currentLevel < 0) currentLevel = 0;
        }

        /// <summary>
        /// Adds to the emotion.
        /// </summary>
        /// <param name="addition"></param>
        public void AddBubbles(float addition) => currentLevel += addition;

        /// <summary>
        /// Adds to the emotion based on its rate of increase each frame
        /// If crash out return true
        /// </summary>
        /// <returns>bool crash out</returns>
        public bool Update()
        {
            AddBubbles(rateOfIncrease * Time.deltaTime);
           
            //check for crash out
            if (currentLevel >= maxLevel) return true;

            return false;
        }
    }
}
