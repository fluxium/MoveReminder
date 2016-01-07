using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace MoveReminder
{
    class Program
    {
        static void Main(string[] args)
        {
            //SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
            
            //while (true)
            //{
            //    // Hold the application open
            //}

            System.Timers.Timer moveTimer = new System.Timers.Timer();
            moveTimer.Elapsed += new ElapsedEventHandler(OnMoveElapsed);
            moveTimer.Interval = 3600000;
            //move.Enabled = true;

            SystemEvents.SessionSwitch += (sender, e) => HandleSessionSwitch(moveTimer, sender, e);

            // Prevents Windows Application from closing, keeps 
            new System.Threading.AutoResetEvent(false).WaitOne();
        }

        static void HandleSessionSwitch(System.Timers.Timer timer, object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                //I left my desk
                timer.Stop();
            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                //I returned to my desk
                timer.Start();
            }
        }

        //static void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        //{

        //    if (e.Reason == SessionSwitchReason.SessionLock)
        //    {
        //        //I left my desk
        //        timer.Stop();
        //    }
        //    else if (e.Reason == SessionSwitchReason.SessionUnlock)
        //    {
        //        //I returned to my desk
        //        timer.Start();
        //    }
        //}

        static void OnMoveElapsed(object source, ElapsedEventArgs e)
        {
            MessageBox.Show("Time to change your position!", "Move Reminder");
        }
    }
}
