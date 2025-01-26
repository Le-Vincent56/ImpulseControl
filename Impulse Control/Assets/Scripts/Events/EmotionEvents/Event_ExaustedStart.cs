using ImpulseControl.Events;

namespace ImpulseControl
{
    public struct Event_ExaustedEnd : IEvent
    {
        public EmotionType emotionType;
    }
}
