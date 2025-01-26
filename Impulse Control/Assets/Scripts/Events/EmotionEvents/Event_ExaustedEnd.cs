using ImpulseControl.Events;

namespace ImpulseControl
{
    public struct Event_ExaustedStart : IEvent
    {
        public EmotionType emotionType;
    }
}
