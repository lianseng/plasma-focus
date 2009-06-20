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


namespace Plasma_Focus.models.fitting.ga
{
	 
	public class Chromosome
	{
		public Chromosome(int length)
		{
			m_length = length;
			m_genes = new double[ length ];
			CreateGenes();
		}


		public Chromosome(int length, bool createGenes)
		{
			m_length = length;
			m_genes = new double[ length ];
			if (createGenes)
				CreateGenes();
		}

        // create genes using template passed in
		public Chromosome(ref double[] genes)
		{
			m_length = genes.GetLength(0);
			m_genes = new double[ m_length ];
			for (int i = 0 ; i < m_length ; i++)
				m_genes[i] = genes[i];
		}
		 

		private void CreateGenes()
		{
			// DateTime d = DateTime.UtcNow;
			for (int i = 0 ; i < m_length ; i++)
				m_genes[i] = m_random.NextDouble();
		}

		public void Crossover(ref Chromosome genome2, out Chromosome child1, out Chromosome child2)
		{
			int pos = (int)(m_random.NextDouble() * (double)m_length);
			child1 = new Chromosome(m_length, false);
			child2 = new Chromosome(m_length, false);
			for(int i = 0 ; i < m_length ; i++)
			{
				if (i < pos)
				{
					child1.m_genes[i] = m_genes[i];
					child2.m_genes[i] = genome2.m_genes[i];
				}
				else
				{
					child1.m_genes[i] = genome2.m_genes[i];
					child2.m_genes[i] = m_genes[i];
				}
			}
		}


		public void Mutate()
		{
			for (int pos = 0 ; pos < m_length; pos++)
			{
				if (m_random.NextDouble() < m_mutationRate)
					m_genes[pos] = (m_genes[pos]+m_random.NextDouble())/2.0;
			}
		}

        // limits the mutation to (0,1/f)
        public void Mutate(double f)
        {

            for (int pos = 0; pos < m_length; pos++)
            {
                if (m_random.NextDouble() < m_mutationRate)
                {
                    double factor = m_random.NextDouble();
                    m_genes[pos] = (m_genes[pos] + factor / f)%1;
                }
            }

          //  double seed = m_random.NextDouble();
          ////  if ( seed < m_mutationRate) return;
          //  int pos = (int)Math.Floor((m_random.NextDouble() * 4) % 4);
              
          //  double factor =   seed/f;      // < 0.2
          //  m_genes[pos] = m_genes[pos] + factor;

        }

		public double[] Genes()
		{
			return m_genes;
		}

		public void Output()
		{
			for (int i = 0 ; i < m_length ; i++)
			{
				System.Console.WriteLine("{0:F4}", m_genes[i]);
			}
			System.Console.Write("\n");
		}

		public void GetValues(ref double[] values)
		{
			for (int i = 0 ; i < m_length ; i++)
				values[i] = m_genes[i];
		}


		public double[] m_genes;
		private int m_length;
		private double m_fitness;
		static Random m_random = new Random();

		private static double m_mutationRate;

		public double Fitness
		{
			get
			{
				return m_fitness;
			}
			set
			{
				m_fitness = value;
			}
		}

		public static double MutationRate
		{
			get
			{
				return m_mutationRate;
			}
			set
			{
				m_mutationRate = value;
			}
		}

		public int Length
		{
			get
			{
				return m_length;
			}
		}
	}

    // for Sort()
    public sealed class ChromosomeComparer : IComparer
    {
        public ChromosomeComparer()
        {
        }
        public int Compare(object x, object y)
        {
            if (x == null || y == null)
                throw new ArgumentException("Not of type Chromosome");

            if (!(x is Chromosome) || !(y is Chromosome))
                throw new ArgumentException("Not of type Chromosome");


            if (((Chromosome)x).Fitness > ((Chromosome)y).Fitness)
                return 1;
            else if (((Chromosome)x).Fitness == ((Chromosome)y).Fitness)
                return 0;
            else
                return -1;

        }
    }
}
