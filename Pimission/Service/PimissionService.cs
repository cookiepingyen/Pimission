using Pimission.Enums;
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

        public PiModel Request(PiModel piModel)
        {
            if (!keyValuePairs.ContainsKey(piModel.SampleSize))
            {
                keyValuePairs.TryAdd(piModel.SampleSize, piModel);
                sampleSizeQueue.Enqueue(piModel);
                return piModel;
            }
            return null;
        }

        public void Start(CancellationToken token)
        {
            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    if (sampleSizeQueue.Count > 0 && sampleSizeQueue.TryDequeue(out PiModel pimodel))
                    {
                        await Task.Run(async () =>
                        {
                            try
                            {
                                pimodel.State = Enums.MissionState.running;
                                PIMission pIMission = new PIMission(pimodel);
                                double value = await pIMission.Calculate();
                                pimodel.Value = value;
                                pimodel.State = Enums.MissionState.completed;
                                keyValuePairs[pimodel.SampleSize] = pimodel;
                            }
                            catch (TaskCanceledException)
                            {
                                pimodel.State = MissionState.cancelled;

                            }


                        });

                        //PIMission pIMission = new PIMission(pimodel);
                        //double value = await pIMission.Calculate();
                        //pimodel.Value = value;
                        //keyValuePairs[pimodel.SampleSize] = pimodel;

                    }
                }
            });
        }
    }
}
