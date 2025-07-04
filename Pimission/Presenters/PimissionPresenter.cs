﻿using Pimission.Contracts;
using Pimission.Models;
using Pimission.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pimission.Presenters
{
    internal class PimissionPresenter : IPiMissionPresenter
    {
        IPiMissionWindow piMissionWindow;
        IPIMissionService piMissionService;
        CancellationTokenSource cts = new CancellationTokenSource();

        public PimissionPresenter(IPiMissionWindow piMissionWindow, IPIMissionService piMissionService)
        {
            this.piMissionWindow = piMissionWindow;
            this.piMissionService = piMissionService;
        }

        public PiModel SendMissionRequest(long sampleSize)
        {
            if (cts.IsCancellationRequested)
            {
                return null;
            }
            PiModel piModel = new PiModel(sampleSize);
            piModel = piMissionService.Request(piModel);
            return piModel;
        }

        public void StartMission()
        {
            cts = new CancellationTokenSource();
            piMissionService.Start(cts.Token);
        }

        public void StopMission()
        {
            cts.Cancel();
        }

        public bool MissionSwitch()
        {
            if (!cts.IsCancellationRequested)
            {
                StopMission();
                return false;
            }
            else
            {
                StartMission();
                return true;
            }
        }
    }
}
