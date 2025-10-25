using System;

namespace Script
{
    public static class GameEvents
    {
        public static event Action OnPauseRequested;
        public static event Action OnResumeRequested;

        public static void RequestPause()
        {
            OnPauseRequested?.Invoke();
        }

        public static void RequestResume()
        {
            OnResumeRequested?.Invoke();
        }
    }
}