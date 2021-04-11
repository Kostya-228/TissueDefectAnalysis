﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Histogram<T>
    {
        public SortedDictionary<T, int> histogram { get; private set; }

        public Histogram(List<T> variants)
        {
            histogram = new SortedDictionary<T, int>();
            foreach (var item in variants)
                histogram[item] = 0;
        }


        /// <summary>
        /// Метод для добавления ключа к гистограмме
        /// </summary>
        /// <param name="key"></param>
        public void Add(T key)
        {
            if (histogram.ContainsKey(key))
                histogram[key]++;
            else
                histogram[key] = 1;
        }

        /// <summary>
        /// Печать гистограммы в консоли
        /// </summary>
        public void Print()
        {
            foreach (KeyValuePair<T, int> pair in histogram)
            {
                Console.Write("{1, 2} ", pair.Key, pair.Value);
            }
            Console.WriteLine();
        }
    }
}
