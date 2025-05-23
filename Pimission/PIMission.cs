using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pimission
{
    class PIMission
    {
        private readonly object _lockObj = new object();
        public readonly long SampleSize;
        public PIMission(long sampleSize)
        {
            this.SampleSize = sampleSize;
        }

        public async Task<double> Calculate()
        {
            long insideCircle = 0;
            await Parallel.ForAsync(0, SampleSize, async (index, token) =>
            {
                Random rand = new Random();

                double x = rand.NextDouble();
                double y = rand.NextDouble();

                if (x * x + y * y <= 1.0)
                {
                    lock (_lockObj)
                    {
                        insideCircle++;
                    }
                }

            });
            return 4.0 * insideCircle / SampleSize;
        }

    }
}
