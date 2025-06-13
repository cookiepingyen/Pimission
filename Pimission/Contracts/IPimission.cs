using Pimission.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pimission.Contracts
{
    interface IPiMissionWindow
    {

    }

    public interface IPiMissionPresenter
    {
        void StartMission();
        PiModel SendMissionRequest(long sampleSize);
        void StopMission();
        bool MissionSwitch();
    }
}
