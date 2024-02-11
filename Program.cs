using ILGPU;
using ILGPU.Runtime;
using ILGPU_Functional_Base.Models;

namespace ILGPU_Functional_Base
{
    /// <summary>
    /// Just to be clear here this is not written very well; I have tried to keep everything in here in order so that you can see how it works.
    /// If you are interested in this at all, I am sure you are competent enough to put it into a decent architecture yourself for your own use.
    /// </summary>
    public static class Program
    {

        // You will need to set me to true if you want to debug stuff inside the GPU usage
        private const bool UseCpu = false;
        // The number of simultaneous operations that will be sent to the GPU
        private const int Threads = 10000;
        

        // This is where we set up the accelerator - with the UseCpu default set as the const above.
        private static readonly Context Context = Context.CreateDefault();
        private static readonly Accelerator Accelerator = Context.GetPreferredDevice(preferCPU: UseCpu).CreateAccelerator(Context);


        // Check out MyData struct... for example just integers (A, B, C) and a UseData method that performs C = A * B
        // This is the CPU's version of the data (we will need thread copies of it)
        private static readonly MyData[] CpuData= new MyData[Threads];
        // This is the GPU version of the same data
        private static readonly MemoryBuffer1D<MyData, Stride1D.Dense> GpuData = Accelerator.Allocate1D(CpuData);


        // Set up the kernel to take in the right inputs
        // Index 1D is just an integer of how many threads
        // ArrayView1d is just the GPU version of the data.
        // 'Kernel' is the method that we are going to run when we use this action.
        private static readonly Action<Index1D, ArrayView1D<MyData, Stride1D.Dense>> UseTheGpu 
            = Accelerator.LoadAutoGroupedStreamKernel<Index1D, ArrayView1D<MyData, Stride1D.Dense>>(WhatTheGpuDoes);


        static void Main() // usually - string[] args)
        {
            SetupData();
            UseTheGpu(Threads, GpuData.View);
            UseTheData();
        }


        /// <summary>
        /// We are going to set up the data in here - in the case of this example we are just going to set up the (threads) number of MyData with random data
        /// </summary>
        private static void SetupData()
        {
            var rnd = new Random();
            for (var i = 0; i < Threads; i++)
            {
                // Notice we are setting up the CPU data here
                CpuData[i].A = rnd.Next(1000);
                CpuData[i].B = rnd.Next(1000);
            }
            // Once it is set up, we can pass it into the gpu
            GpuData.CopyFromCPU(CpuData);
        }

        /// <summary>
        /// This is what the GPU does
        /// </summary>
        /// <param name="index"> Just like we were inside a for loop</param>
        /// <param name="data"></param>
        private static void WhatTheGpuDoes(Index1D index, ArrayView1D<MyData, Stride1D.Dense> data)
        {
            // All we are doing here is multiplying A * B as randomly setup.
            data[index].UseData();
        }

        // This is where we are using our data
        private static void UseTheData()
        {
            // Firstly move the data into the CPU, so we can read it.
            GpuData.CopyToCPU(CpuData);
            // For the sake of example we are just going to write the answers to the console.
            foreach (var thingWeSentToTheGpu in CpuData)
            {
                Console.Write($"{thingWeSentToTheGpu.C}, ");
            }
        }

    }
}
