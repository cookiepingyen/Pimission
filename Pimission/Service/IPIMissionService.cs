using Pimission.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pimission.Service
{
    internal interface IPIMissionService
    {
        void Start();
        void Stop();
        void Request(long sampleSize);
        List<PiModel> Response();
    }
}
