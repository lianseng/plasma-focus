/*  ----------------------------------------------------------------------------
 *  National Institute of Education, NTU, Singapore
 *  ----------------------------------------------------------------------------
 *  Lee Model Plasma Focus
 *  ----------------------------------------------------------------------------
 *  File:       Constants.cs
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

namespace Plasma_Focus.models
{
	/// <summary>
	/// Description of static constants.
	/// </summary>
	public static class Constants
	{
            // Input some static constants in SI units
			
        public const double  MU = 1.257E-6;    //* Math.Pow(10 , -6); 
		public const double	Pi = 3.142;
		public const double	bc = 1.38E-23;
		public const double	mi = 1.67E-27;
		public const double	MUK = MU / (8 * Pi * Pi * bc);
		public const double	CON11 = 1.6E-20;
		public const double	CON12 = 9.999999E-21;
		public const double	CON2 = 4.6E-31;
		public const double	UGCONS = 8.310001E3;
		public const double	FRF = 0.3;
		public const double	FE = 1 / 3.0;
		public const double	FLAG = 0;
	    

        
	}
}
