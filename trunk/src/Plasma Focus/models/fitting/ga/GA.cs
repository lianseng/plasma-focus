/*  ----------------------------------------------------------------------------
 *  National Institute of Education, NTU, Singapore
 *  ----------------------------------------------------------------------------
 *  Lee Model Plasma Focus
 *  ----------------------------------------------------------------------------
 *  File:       GAFit.cs
 *  Author:     Loh Lian Seng
 *
 * 
 * The MIT License
 * 
 * Copyright (c) 2009 Loh Lian Seng
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:

 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.

 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 *
 *  ----------------------------------------------------------------------------
 */
using System;
using System.Collections;
using System.IO;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;

namespace Plasma_Focus.models.fitting.ga
{

    public delegate void ReportProgress(int i, string s);
    public delegate double GAFunction(double[] values);


    public class GA
    {
        int MutationFactor = 50;
        #region debug
        static StreamWriter sw = null;

        [Conditional("QA")]
        static void writeQuality(double massf, double currf, double massfr, double currfr, double fitness, double totalFitness, string stage)
        {
            debugLine(massf + ", " + currf + ", " + massfr + ", " + currfr + ", " + fitness + ", " + totalFitness + "," + stage);

        }


        [Conditional("QA")]
        public static void debugOpen()
        {
            // startQuality();

            FileStream file;
            string name = "fitness.csv";
            try
            {
                if (sw != null) return;

                if (System.IO.File.Exists(name))
                {
                    long size = new FileInfo(name).Length;
                    if (size > 1e6)
                        file = new FileStream(name, FileMode.CreateNew, FileAccess.Write);
                    else
                        file = new FileStream(name, FileMode.Append, FileAccess.Write);
                }
                else
                    file = new FileStream(name, FileMode.CreateNew, FileAccess.Write);
                sw = new StreamWriter(file);
            }
            catch (Exception e)
            {
                report(-1, "Cannot open log file\n " + e.Message);
                Console.WriteLine(e.Message);
            }
        }

        [Conditional("QA")]
        public static void debugLine(string str)
        {
            sw.WriteLine(str);
        }


        [Conditional("QA")]
        public void debug(string str)
        {
            sw.Write(str);
        }
        [Conditional("QA")]

        public static void debugClose()
        {
            sw.Close();
            sw = null;
        }

        #endregion debug

        public static ReportProgress report { get; set; }
        public string stage { get; set; }
        public BackgroundWorker worker { get; set; }
        public bool final { get; set; }   // set for final fine tuning...

        double prevFitness;
        int stagnant;

        double fittestFitness; 
 
        public GA(double crossoverRate, double mutationRate, int populationSize, int generationSize, bool elitism, int length)
        {
            initialize();
            this.mutationRate = mutationRate;
            this.crossoverRate = crossoverRate;
            this.populationSize = populationSize;
            generations = generationSize;
            chromosomeLen = length;
            this.elitism = elitism;
        }

 
        public void initialize()
        { 
            fittestFitness = 0;
            prevFitness = 0; stagnant = 0;
        }

 
        public ArrayList Go(double[] start, bool final)
        {
            this.final = final;

            if (getFitness == null)
                throw new ArgumentNullException("Need to supply fitness function");
            if (chromosomeLen == 0)
                throw new IndexOutOfRangeException("Chromosome size not set");

            //  Create the fitness table.
            fitnessTable = new ArrayList();

            nextGeneration = new ArrayList(generations);
            Chromosome.MutationRate = mutationRate;

            thisGeneration = new ArrayList(generations);
            report(0, "Creating initial population for " + stage);
        
            CreateChromosomes(start);
   
            debug(stage + ", ");
            RankPopulation();
   
            for (int i = 0; i < generations; i++)
            {
                if (worker.CancellationPending == true) return thisGeneration;

                report((i+1) * 100 / generations, stage + " generation " + i + ", fit = " + String.Format("{0:F4}", fittestFitness));

                CreateNextGeneration();

                debug(stage + ", ");
                RankPopulation();

                Console.WriteLine(stage + " generation " + i + ", fitness " + totalFitness);
            }

            return thisGeneration;
        }

        /// 
        /// After ranking all the genomes by fitness, use a 'roulette wheel' selection
        /// method.  This allocates a large probability of selection to those with the 
        /// highest fitness.
        /// 
        /// <returns>Random individual biased towards highest fitness</returns>
        private int RouletteSelection()
        {
            double randomFitness = seed.NextDouble() * totalFitness;
            int idx = -1;
            int mid;
            int first = 0;

            // ignore the 0 fitness genes
            while (first < fitnessTable.Count && (double)fitnessTable[first++] == 0) ;
            //first -= 10;  // keep some for mutation
            //if (first < 0) first = 0;

            int last = populationSize - 1;
            mid = (last - first) / 2;

            //  ArrayList's BinarySearch is for exact values only
            //  so do this by hand.
            while (idx == -1 && first <= last)
            {
                if (randomFitness <= (double)fitnessTable[mid])
                {
                    last = mid;
                }
                else if (randomFitness > (double)fitnessTable[mid])
                {
                    first = mid;
                }
                mid = (first + last) / 2;
                //  lies between i and i+1
                if ((last - first) == 1)
                    idx = last;
            }

            if (idx == -1) idx = 0;

            return idx;
        }

        /// 
        /// Rank population and sort in order of fitness.
        /// 
        private void RankPopulation()
        {
            totalFitness = 0;
            for (int i = 0; i < populationSize; i++)
            {
                Chromosome g = ((Chromosome)thisGeneration[i]);

                if (g == null)
                    g = new Chromosome(chromosomeLen);

                g.Fitness = FitnessFunction(g.Genes());

                totalFitness += g.Fitness;

            }
            thisGeneration.Sort(new ChromosomeComparer());

            // process fittest gene
            Chromosome f1 = ((Chromosome)(thisGeneration[populationSize - 1]));
            Chromosome fittest = new Chromosome(ref f1.m_genes);
            fittestFitness = f1.Fitness;

            if (fittestFitness == prevFitness)
            {
                if (++stagnant == 2)
                {
                    Debug.WriteLine(-1, "Stagnant gene pool");
                    //seed = new Random();
                    //Chromosome.MutationRate += 0.1;
                    this.crossoverRate += 0.1;
                    final = true;
                    MutationFactor *= 5;
                    //stagnant = 0;
                }
            } 
            else
            {
                prevFitness = fittestFitness;
                stagnant = 0;
            }

            #region debug
#if DEBUG
            Debug.Write(stage + " max fitness " + f1.Fitness + " Params:     ");

            if (stage.StartsWith("Radial"))
                debug(",,");

            for (int i = 0; i < chromosomeLen; i++)
            {
                Debug.Write("  " + fittest.m_genes[i]);
                debug(fittest.m_genes[i] + ", ");
            }
            if (stage.StartsWith("Axial"))
                debug(",, ");

            debug(fittestFitness + ",");
            Debug.WriteLine("");
#endif
            #endregion
           
            double fitness = 0.0;
            fitnessTable.Clear();

            // build table for use in roulette selection
            for (int i = 0; i < populationSize; i++)
            {
                double f = ((Chromosome)thisGeneration[i]).Fitness;
                if (f < 0) f = 0;          // possible but unlikely

                fitness += f;
                fitnessTable.Add((double)fitness);
            }

            debugLine(fitness + " ");

        }


        /// 
        /// Create the *initial* genomes by repeated calling the supplied fitness function
        /// 
        private void CreateChromosomes(double[] start)
        {
            Chromosome g1 = null;
            if (start != null)
                g1 = new Chromosome(ref start);
            else
                g1 = new Chromosome(chromosomeLen);
            thisGeneration.Add(g1);

            for (int i = 1; i < populationSize; i++)
            {
                Chromosome g = new Chromosome(chromosomeLen);
                thisGeneration.Add(g);
            }
        }

        private void CreateNextGeneration()
        {
            nextGeneration.Clear();

            Chromosome[] fittest = new Chromosome[20];

            if (elitism)
            {
                for (int i = 0; i < 20; i++)
                {
                    Chromosome t = (Chromosome)thisGeneration[populationSize - i - 1];
                    fittest[i] = new Chromosome(ref t.m_genes);
                }
            }
  
            for (int i = 0; i < populationSize; i += 2)
            {
                int pidx1 = RouletteSelection();
                int pidx2 = RouletteSelection();

                if (pidx1 == -1 || pidx2 == -1)
                    Console.WriteLine("problem finding parents " + i);

                Chromosome parent1, parent2, child1, child2;
                parent1 = ((Chromosome)thisGeneration[pidx1]);
                parent2 = ((Chromosome)thisGeneration[pidx2]);

                if (seed.NextDouble() < crossoverRate)
                {
                    parent1.Crossover(ref parent2, out child1, out child2);
                }
                else
                {
                    child1 = parent1;
                    child2 = parent2;
                }

                if (final)
                {
                    child1.Mutate(MutationFactor);   // mutation factor is (0,1/5)
                    child2.Mutate(MutationFactor);
                }
                else
                { 
                    child1.Mutate();
                    child2.Mutate();
                }
                nextGeneration.Add(child1);
                nextGeneration.Add(child2);
            }

            if (elitism && fittest != null)
            {   
                int i;
                for (i = 0; i < fittest.Length; i++)
                {
                    nextGeneration[i] = fittest[i];
                }

                // generate a cluster of points around the best fit gene for hill climbing optimization
                if (fittest.Length < populationSize-fittest.Length)
                {
                    foreach (Chromosome g in generatePeers(fittest[0]))
                        nextGeneration[i++] = g;
                }
            }
 
            thisGeneration.Clear();
            for (int i = 0; i < populationSize; i++)
                thisGeneration.Add(nextGeneration[i]);
        }

        #region generate peers
        //
        // Clustering to help hill climbing optimization
        //
        List<Chromosome> generatePeers(Chromosome g)
        {
            List<Chromosome> s = new List<Chromosome>();
            double factor = 0.05;

            for (int i = 0; i < chromosomeLen; i++)
            {
                Chromosome genome = new Chromosome(ref g.m_genes);
                genome.m_genes[i] += factor;
                genome.m_genes[i] %= 1;

                s.Add(genome);
            }

            for (int i = 0; i < chromosomeLen; i++)
            {

                Chromosome genome = new Chromosome(ref g.m_genes);
                genome.m_genes[i] -= factor;
                genome.m_genes[i] %= 1;
                s.Add(genome);
            }

            for (int i = 0; i < chromosomeLen; i++)
            {

                Chromosome genome = new Chromosome(ref g.m_genes);
                genome.m_genes[i] += factor;
                genome.m_genes[i] %= 1;

                if (++i == chromosomeLen) break;
                genome.m_genes[i] += factor;
                genome.m_genes[i] %= 1;
                s.Add(genome);
            }

            for (int i = 0; i < chromosomeLen; i++)
            {
                Chromosome genome = new Chromosome(ref g.m_genes);
                genome.m_genes[i] -= factor;
                genome.m_genes[i] %= 1;

                if (++i == chromosomeLen) break;
                genome.m_genes[i] -= factor;
                genome.m_genes[i] %= 1;
                s.Add(genome);
            }


            for (int i = 0; i < chromosomeLen; i++)
            {
                Chromosome genome = new Chromosome(ref g.m_genes);
                genome.m_genes[i] -= factor;
                genome.m_genes[i] %= 1;

                if (++i == chromosomeLen) break;
                genome.m_genes[i] += factor;
                genome.m_genes[i] %= 1;
                s.Add(genome);
            }


            for (int i = 0; i < chromosomeLen; i++)
            {
                Chromosome genome = new Chromosome(ref g.m_genes);
                genome.m_genes[i] += factor;
                genome.m_genes[i] %= 1;

                if (++i == chromosomeLen) break;
                genome.m_genes[i] -= factor;
                genome.m_genes[i] %= 1;
                s.Add(genome);
            }
            return s;
        }

         
        #endregion

        #region properties
        private double totalFitness;

        private double mutationRate;
        private double crossoverRate;
        private int populationSize;
        private int generations;
        private int chromosomeLen;
          
        private bool elitism;

        private ArrayList thisGeneration;
        private ArrayList nextGeneration;
        private ArrayList fitnessTable;

        static Random seed = new Random();


        static private GAFunction getFitness;

        public GAFunction FitnessFunction
        {
            get
            {
                return getFitness;
            }
            set
            {
                getFitness = value;
            }
        }


        //  Properties
        public int PopulationSize
        {
            get
            {
                return populationSize;
            }
            set
            {
                populationSize = value;
            }
        }

        public int Generations
        {
            get
            {
                return generations;
            }
            set
            {
                generations = value;
            }
        }

        public int GenomeSize
        {
            get
            {
                return chromosomeLen;
            }
            set
            {
                chromosomeLen = value;
            }
        }

        public double CrossoverRate
        {
            get
            {
                return crossoverRate;
            }
            set
            {
                crossoverRate = value;
            }
        }
        public double MutationRate
        {
            get
            {
                return mutationRate;
            }
            set
            {
                mutationRate = value;
            }
        }
         
        /// <summary>
        /// Keep previous generation's fittest individual in place of worst in current
        /// </summary>
        public bool Elitism
        {
            get
            {
                return elitism;
            }
            set
            {
                elitism = value;
            }
        }
        #endregion

        public void GetBest(out double[] values, out double fitness)
        {
            Chromosome g = ((Chromosome)thisGeneration[populationSize - 1]);
            values = new double[g.Length];
            g.GetValues(ref values);
            fitness = (double)g.Fitness;
        }

        public void GetWorst(out double[] values, out double fitness)
        {
            GetChromosome(0, out values, out fitness);
        }

        public void GetChromosome(int n, out double[] values, out double fitness)
        {
            if (n < 0 || n > populationSize - 1)
                throw new ArgumentOutOfRangeException("n too large, or too small");
            Chromosome g = ((Chromosome)thisGeneration[n]);
            values = new double[g.Length];
            g.GetValues(ref values);
            fitness = (double)g.Fitness;
        }
    }
}
