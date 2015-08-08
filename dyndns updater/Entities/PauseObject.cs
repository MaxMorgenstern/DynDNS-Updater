using DynDNS_Updater.Settings;
using System;
using System.Drawing;

namespace DynDNS_Updater.Entities
{
    static class PauseObject
    {
        private static bool paused;
        private static DateTime timestamp = DateTime.MinValue;
        private static int delay = 1;

        public static bool IsPaused
        {
            get
            {
                return paused;
            }
        }

        public static bool SkipTick
        {
            get
            {
                if (paused)
                {
                    delay++;
                    if (delay % 2 == 0)
                        return true;
                }
                return false;
            }
        }

        public static bool SystemPauseUpdate()
        {
            if (paused)
                return false;

            if (!AppSettings.HasUserameAndToken)
                AppSettings.Reference.MainFormReference.AddToLogBoxHandler("Provide username and password", Color.Red);

            paused = true;
            timestamp = DateTime.Now;

            delay = 1;
            AppSettings.Reference.MainFormReference.AddToLogBoxHandler("Update paused");

            return true;
        }

        public static bool SystemContinueUpdate()
        {
            if (!paused)
                return false;

            paused = false;
            timestamp = DateTime.MinValue;

            AppSettings.Reference.MainFormReference.AddToLogBoxHandler("Update continued");

            return true;
        }

        public static bool CheckContinue()
        {
            if (paused && timestamp.AddHours(1) < DateTime.Now)
                return true;
            return false;
        }
    }
}
