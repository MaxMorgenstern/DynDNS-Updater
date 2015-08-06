using DynDNS_Updater.Settings;
using System;
using System.Drawing;

namespace DynDNS_Updater.Entities
{
    class PauseObject
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

        public static void SystemPauseUpdate()
        {
            if (paused)
                return;

            if (!AppSettings.HasUserameAndToken)
                AppSettings.Reference.MainFormReference.AddToLogBoxHandler("Provide username and password", Color.Red);

            paused = true;
            timestamp = DateTime.Now;
//            pauseStartUpdateButton.Text = "Start";
//            StatusStripStatusLabel.Text = "Paused";
            delay = 1;
            AppSettings.Reference.MainFormReference.AddToLogBoxHandler("Update paused");
//            periodic_log_update(null, null);
        }

        public static void SystemContinueUpdate()
        {
            if (!paused)
                return;

            paused = false;
            timestamp = DateTime.MinValue;
//            pauseStartUpdateButton.Text = "Pause";
//            StatusStripStatusLabel.Text = "Ready";

            AppSettings.Reference.MainFormReference.AddToLogBoxHandler("Update continued");
//            periodic_log_update(null, null);
        }

        public static bool CheckContinue()
        {
            if (paused && timestamp.AddHours(1) < DateTime.Now)
                return true;
            return false;
        }
    }
}
