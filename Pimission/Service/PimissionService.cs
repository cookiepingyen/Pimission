using Pimission.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pimission.Service
{
    internal class PimissionService : IPIMissionService
    {
        ConcurrentDictionary<long, PiModel> keyValuePairs = new ConcurrentDictionary<long, PiModel>();
        ConcurrentQueue<PiModel> sampleSizeQueue = new ConcurrentQueue<PiModel>();

        public PimissionService()
        {

        }

        public void Request(PiModel piModel)
        {
            if (!keyValuePairs.ContainsKey(piModel.SampleSize))
            {
                keyValuePairs.TryAdd(piModel.SampleSize, piModel);
                sampleSizeQueue.Enqueue(piModel);
            }
        }

        public void Start(CancellationToken token)
        {
            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    if (sampleSizeQueue.Count > 0 && sampleSizeQueue.TryDequeue(out PiModel pimodel))
                    {
                        PIMission pIMission = new PIMission(pimodel.SampleSize);
                        double value = await pIMission.Calculate();
                        pimodel.Value = value;
                        keyValuePairs[pimodel.SampleSize] = pimodel;
                    }
                }
            });
        }
    }
}
