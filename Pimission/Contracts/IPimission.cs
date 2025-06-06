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
        void RenderDatas(List<PiModel> piModels);
    }

    public interface IPiMissionPresenter
    {
        void StartMission();
        void FetchMissionDatas();
        void SendMissionRequest(long sampleSize);
    }
}
