using System;

namespace Scripts.Squishing
{
    public interface ISquishingService
    {
        public event Action OnBegin;
        public event Action OnEnd;
    }
}
