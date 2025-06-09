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


        public PimissionService()
        {

        }

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

        public void Start(CancellationToken token)
        {
            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
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
    }
}
