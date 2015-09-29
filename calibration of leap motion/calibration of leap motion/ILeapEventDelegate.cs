using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace calibration_of_leap_motion
{
    //this allows any class implementing this interface to act as a delegate for the Leap Motion events.
    public interface ILeapEventDelegate
    {
        void LeapEventNotification(string EventName);
    }
}
