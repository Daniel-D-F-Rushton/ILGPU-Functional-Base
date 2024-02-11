# ILGPU Functional Base

Somebody asked me for a no-nonsense example of how to use ILGPU for the purpose of parralel processing - this is it.
This is thus a lowly console application that does not touch graphics at all, just uses the GPU to parrallel perform a bunch of multiplications

[Model]
A, B, C = integers.
UseData... {C = A * B}

Two constants at the top
UseCpu (you will need this to be true if you want to debug 'inside the GPU')
Threads (this is the number of the parrallel processes that you will run - GPU dependant of course)

-- We create the Accelerator in two lines

-- We create the data in two line, (CPU and GPU versions)

-- We define our Kernal in 1 line

-- We have a Main method that
  -- Calls SetupData (to setup the CPU data and thow it into the GPU for use)
  -- Calls UseTheGpu (Tells the Kernal to get going)
    -- This in turn runs the 'UseData' in the Model to multitple the various A * B on each thread to make a C
  -- Calls UseTheData (gets the data out of the gpu, and in this case prints it to the console

-- SetupData method
-- WhatTheGpuDoes method
-- UseTheData method
