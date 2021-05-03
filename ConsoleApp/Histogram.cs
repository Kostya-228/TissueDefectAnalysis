using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Histogram<T>
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

        public int Distanse(Histogram<T> other)
        {
            int diff = 0;
            foreach(var key in histogram.Keys)
            {
                var i = (histogram[key] + other.histogram[key]);
                if (i != 0)
                    diff += (int)Math.Pow(histogram[key] - other.histogram[key], 2) / i;
            }
            return diff;
        }

        /// <summary>
        /// Печать гистограммы в консоли
        /// </summary>
        public void Print()
        {
            foreach (KeyValuePair<T, int> pair in histogram)
            {
                Console.Write("{1, 4} ", pair.Key, pair.Value);
            }
            Console.WriteLine();
        }
    }

    public class RGBHistogramm<T>
    {
        public Histogram<T> R;
        public Histogram<T> G;
        public Histogram<T> B;

        public RGBHistogramm(List<T> variants)
        {
            R = new Histogram<T>(variants);
            G = new Histogram<T>(variants);
            B = new Histogram<T>(variants);
        }
    }
}
