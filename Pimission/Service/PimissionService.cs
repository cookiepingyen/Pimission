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
        ConcurrentQueue<long> sampleSizeQueue = new ConcurrentQueue<long>();
        ConcurrentBag<PiModel> cache = new ConcurrentBag<PiModel>();

        //internal async Task<PiModel> Request(int sampleSize)
        //{
        //    if (keyValuePairs.ContainsKey(sampleSize))
        //    {
        //        return null;
        //    }
        //    PiModel piModel = new PiModel(sampleSize);
        //    keyValuePairs.TryAdd(sampleSize, piModel);
        //    PIMission pIMission = new PIMission(sampleSize);
        //    double value = await pIMission.Calculate();
        //    piModel.Value = value;

        //    return piModel;
        //}

        public void Request(long sampleSize)
        {
            if (!keyValuePairs.ContainsKey(sampleSize))
            {
                keyValuePairs.TryAdd(sampleSize, new PiModel(sampleSize));
                sampleSizeQueue.Enqueue(sampleSize);
            }
        }

        public List<PiModel> Response()
        {
            List<PiModel> piModels = cache.ToList();
            cache.Clear();
            return piModels;
        }

        public void Start()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    if (sampleSizeQueue.Count > 0 && sampleSizeQueue.TryDequeue(out long sampleSize))
                    {
                        PIMission pIMission = new PIMission(sampleSize);
                        double value = await pIMission.Calculate();
                        var model = new PiModel(sampleSize, value);
                        keyValuePairs[sampleSize] = model;
                        cache.Add(model);
                    }
                }
            });
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
