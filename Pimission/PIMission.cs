using Pimission.Enums;
using Pimission.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pimission
{
    class PIMission
    {
        public readonly PiModel piModel;
        public PIMission(PiModel piModel)
        {
            this.piModel = piModel;
        }

        public async Task<double> Calculate()
        {
            long insideCircle = 0;
            await Parallel.ForAsync(0, piModel.SampleSize, piModel.CancellationTokenSource.Token, async (index, token) =>
            {
                if (piModel.CancellationTokenSource.Token.IsCancellationRequested)
                {
                    return;
                }
                Random rand = new Random();

                double x = rand.NextDouble();
                double y = rand.NextDouble();

                if (x * x + y * y <= 1.0)
                {
                    Interlocked.Increment(ref insideCircle);
                }

            });
            return 4.0 * insideCircle / piModel.SampleSize;
        }

    }
}
