using Pimission.Contracts;
using Pimission.Models;
using Pimission.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pimission.Presenters
{
    internal class PimissionPresenter : IPiMissionPresenter
    {
        IPiMissionWindow piMissionWindow;
        PimissionService pimissionService = new PimissionService();

        public PimissionPresenter(IPiMissionWindow piMissionWindow)
        {
            this.piMissionWindow = piMissionWindow;
        }

        public void SendMissionRequest(long sampleSize)
        {
            pimissionService.Request(sampleSize);
        }

        public void FetchMissionDatas()
        {
            List<PiModel> piModels = pimissionService.Response();
            this.piMissionWindow.RenderDatas(piModels);
        }

        public void StartMission()
        {
            pimissionService.Start();
        }
    }
}
