# ILGPU Functional Base

Somebody asked me for a no-nonsense example of how to use ILGPU for the purpose of parralel processing - this is it.
This is thus a lowly console application that does not touch graphics at all, just uses the GPU to parrallel perform a bunch of multiplications

[Model]
A, B, C = integers.
UseData... {C = A * B}

Two constants at the top
UseCpu (you will need this to be true if you want to debug 'inside the GPU')
Threads (this is the number of the parrallel processes that you will run - GPU dependant of course)

1) We create the Accelerator in two lines
2) We create the data in two line, (CPU and GPU versions)
3) We define our Kernal in 1 line
4) We have a Main method that
- a) Calls [5] SetupData (Setup the CPU data and thow it into the GPU for use)
- b) Calls [6] UseTheGpu (Tells the Kernal to get going)
- c) Calls [7] UseTheData (Gets the data out of the gpu, and in this case prints it to the console)
5) SetupData method
6) WhatTheGpuDoes method
7) UseTheData method
