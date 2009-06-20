/*  ----------------------------------------------------------------------------
 *  National Institute of Education, NTU, Singapore
 *  ----------------------------------------------------------------------------
 *  Lee Model Plasma Focus
 *  ----------------------------------------------------------------------------
 *  File:       IniSection.cs
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
using System.Collections.Generic;

namespace Plasma_Focus.models
{
    public class IniSection : Dictionary<string, string>
    {
        public void Add(string line)
        {
            if (line.Length != 0)
            {
                int index = line.IndexOf('=');
                if (index != -1)
                {
                    string s1 = line.Substring(0, index);
                    string s2 = line.Substring(index + 1, line.Length - index - 1);
                    base.Add(s1.Trim(), s2.Trim());
                }
                else
                    throw new Exception("Keys must have an equal sign.");
            }
        }

        public string ToString(string key)
        {
            return key + "=" + this[key];
        }

        public string[] GetKeys()
        {
            string[] output = new string[this.Count];
            byte i = 0;
            foreach (KeyValuePair<string, string> item in this)
            {
                output[i] = item.Key;
                i++;
            }
            return output;
        }
        public bool HasKey(string key)
        {
            foreach (KeyValuePair<string, string> item in this)
            {
                if (item.Key == key)
                    return true;
            }
            return false;
        }
    }
}
