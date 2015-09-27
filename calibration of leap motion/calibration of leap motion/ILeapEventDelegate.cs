using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace calibration_of_leap_motion
{
    public interface ILeapEventDelegate
    {
        void LeapEventNotification(string EventName);
    }
}
