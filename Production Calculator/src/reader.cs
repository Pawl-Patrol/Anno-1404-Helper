using System;
using System.Diagnostics;


namespace Production_Calculator
{
    public class Reader
    {
        public static int[] ReadPopulation(Process process, long address, long offset, bool x64)
        {

            int[] population = new int[(int)Population.Length];

            long baseAddress;
            if (x64)
            {
                baseAddress = Memory.ReadInt64(process, process.MainModule.BaseAddress.ToInt64() + address);
            }
            else
            {
                baseAddress = Memory.ReadInt32(process, address);
            }
            baseAddress += offset;

            for (int i = 0; i < population.Length; i++)
            {
                population[i] = Memory.ReadInt32(process, baseAddress + Constants.PopulationOffsets[i]);
            }

            return population;
        }
    }
}