/*  ----------------------------------------------------------------------------
 *  National Institute of Education, NTU, Singapore
 *  ----------------------------------------------------------------------------
 *  Lee Model Plasma Focus
 *  ----------------------------------------------------------------------------
 *  File:       IniFile.cs
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
using System.IO;

namespace Plasma_Focus.models
{
    public class IniFile : Dictionary<string, IniSection>
    {
        private string _FileName;
        public string FileName
        {
            get { return _FileName; }
        }

        public IniFile(string file)
        {
            _FileName = file;
        }

        public string Add(string line)
        {
            if (line.StartsWith("["))
                line = line.TrimStart('[');
            if (line.EndsWith("]"))
                line = line.TrimEnd(']');
            this.Add(line.Trim(), new IniSection());

            return line;
        }

        public bool Load()
        {
            // Nothing to load, get out.
            if (!this.Exists())
                return true;

            try
            {
                StreamReader sr = new StreamReader(_FileName);
                string section = "";
                while (sr.Peek() != -1)
                {
                    string read = sr.ReadLine().TrimEnd();
                    if (read.Length ==0 || read.StartsWith("#"))
                        continue;
                    if (read.StartsWith("[") && read.EndsWith("]"))
                        section = this.Add(read);
                    else
                    {
                        if (section.Length != 0)
                            this[section].Add(read);
                        else
                            throw new Exception("Ini file must start with a section.");
                    }
                }
                sr.Close();
 
                return true;
            }
            catch  
            {
                return false;
            }
        }
        public bool Save()
        {
            try
            {
                StreamWriter sw = new StreamWriter(_FileName);
                foreach (string section in this.Keys)
                {
                    sw.WriteLine("[" + section + "]");
                    foreach (string key in this[section].Keys)
                        sw.WriteLine(key + "=" + this[section][key]);
                    sw.WriteLine();
                    sw.Flush();
                }
                sw.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Tells you whether or not this specific INI file exists or not.
        /// </summary>
        /// <returns>True if it is found, false if it is not.</returns>
        public bool Exists()
        {
            if (File.Exists(_FileName))
                return true;
            else
                return false;
        }
        /// <summary>
        /// Deletes the INI file.
        /// </summary>
        /// <returns>True if the file was deleted without error, false if an exception was thrown.</returns>
        public bool Delete()
        {
            try
            {
                File.Delete(_FileName);
                this.Clear();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Moves the INI file to a different location and updates all internal references to it.
        /// </summary>
        /// <param name="path">The place to move it to.</param>
        /// <returns>True if the file was moved without error, false if an exception was thrown.</returns>
        public bool Move(string path)
        {
            try
            {
                File.Move(_FileName, path);
                _FileName = path;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string[] GetSections()
        {
            string[] output = new string[this.Count];
            byte i = 0;
            foreach (KeyValuePair<string, IniSection> item in this)
            {
                output[i] = item.Key;
                i++;
            }
            return output;
        }
        public bool HasSection(string section)
        {
            foreach (KeyValuePair<string, IniSection> item in this)
            {
                if (item.Key == section)
                    return true;
            }
            return false;
        }
    }
}
