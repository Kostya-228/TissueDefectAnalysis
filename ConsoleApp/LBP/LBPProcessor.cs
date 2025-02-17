﻿using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    public class LBPProcessor
    {
        //Mat.Indexer<Vec3b> indexer;
        uint all_possible_variants;
        List<uint> uniform_patterns;
        List<uint> uniform_patterns_extend;
        int[,] lbp_positions;

        int A;
        int B;

        public LBPProcessor(int A = 2, int B = 1, int count = 6)
        {
            this.A = A;
            this.B = B;
            this.all_possible_variants = (uint)Math.Pow(2, count);
            // варианты uniform_patterns
            this.uniform_patterns = CalcUniformPatterns(all_possible_variants);
            // варианты uniform_patterns + один столбик на не uniform_patterns
            uniform_patterns_extend = uniform_patterns.Select(item => item).ToList();
            uniform_patterns_extend.Add(all_possible_variants + 1);

            // позиции точек
            lbp_positions = CalcPositions(A, B, count);
        }

        //public void Test(Mat src)
        //{
        //    // для более быстрого лоступа к пикселям используем индексатор
        //    var indexer = src.GetGenericIndexer<Vec3b>();


        //    int offset_h = B;
        //    int offset_w = A;

        //    for (int i = offset_h; i < src.Size().Height - 1 - offset_h; i+=15)
        //    {
        //        for (int j = offset_w; j < src.Size().Width - 1 - offset_w; j+=15)
        //        {
        //            var histogram = CalcHistogramForArea(i, j, 15, 15, indexer);
        //            histogram.Print();
        //        }
        //        Console.WriteLine();
        //    }
        //    Console.WriteLine("ready");
        //}

        /// <summary>
        /// Подсчет гистограммы област
        /// </summary>
        /// <param name="x">начальная точка x</param>
        /// <param name="y">начальная точка y</param>
        /// <param name="h">высота зоны</param>
        /// <param name="w">ширина зоны</param>
        /// <returns></returns>
        public RGBHistogramm<uint> CalcHistogramForArea(int x, int y, int h, int w, Mat.Indexer<Vec3b> indexer)
        {
            var rgbHistogramm = new RGBHistogramm<uint>(uniform_patterns_extend);
            for (int i = x; i < x+h; i++)
            {
                if (i < A)
                    continue;
                if (i >= x + h - A)
                    continue;
                for (int j = y; j < y+w; j++)
                {
                    if (j < B)
                        continue;
                    if (j >= y + w - B)
                        continue;
                    rgbHistogramm.R.Add(BinaryCodeToValue(CalcPixelLBPCode(i, j, indexer, 'R')));
                    rgbHistogramm.G.Add(BinaryCodeToValue(CalcPixelLBPCode(i, j, indexer, 'G')));
                    rgbHistogramm.B.Add(BinaryCodeToValue(CalcPixelLBPCode(i, j, indexer, 'B')));
                }
            }
            return rgbHistogramm;
        }

        private uint BinaryCodeToValue(string binary_code)
        {
            uint decimal_code = (uint)Convert.ToInt32(binary_code, 2);
            if (!uniform_patterns.Contains(decimal_code))
                return all_possible_variants + 1;
            else
                return decimal_code;
        }

        /// <summary>
        /// Вычисление LBP кода для пикселя
        /// </summary>
        /// <param name="indexer">объект доступа к пикселям</param>
        /// <param name="x">позиция x</param>
        /// <param name="y">позиция y</param>
        /// <param name="positions">позиции точек, по которым считать LBP код</param>
        /// <returns></returns>
        private string CalcPixelLBPCode(int x, int y, Mat.Indexer<Vec3b> indexer, char color)
        {
            string result = "";
            byte threshold;
            for(int i=0; i < lbp_positions.GetLength(0); i++)
            {
                int a = x + lbp_positions[i, 0], b = y + lbp_positions[i, 1];
                var c = indexer[b, a];
                byte value;
                switch(color)
                {
                    case 'R': value = c.Item0; threshold = indexer[y, x].Item0; break;
                    case 'G': value = c.Item1; threshold = indexer[y, x].Item1; break;
                    case 'B': value = c.Item2; threshold = indexer[y, x].Item2; break;
                    default: throw new Exception($"a valid not color ({color})");
                }
                result += (value >= threshold) ? "1" : "0";
            }
            return result;
        }

        /// <summary>
        /// Вычисление яркости пикеля
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static  float CalcLight(Vec3b vector)
        {
            return vector.Item0 + vector.Item1 + vector.Item2;
        }

        /// <summary>
        /// Вычисление 
        /// </summary>
        /// <param name="A">Радиус 1</param>
        /// <param name="B">Радиус 2</param>
        /// <param name="count">Кол-во точек</param>
        /// <returns></returns>
        public static int[,] CalcPositions(int A, int B, int count) 
        {
            int[,] result = new int[count, 2];
            for (int i =0; i < count; i++)
            {
                var theta = 360 / count * i;
                var radians = Math.PI * theta / 180.0;
                var sin = Math.Sin(radians);
                var cos = Math.Cos(radians);
                var R = Math.Sqrt((A * A * B * B)/ (A*A*sin*sin + B*B*cos*cos));
                int g_x = (int)Math.Round(R * cos);
                int g_y = (int)Math.Round(R * sin);
                result[i, 0] = g_x;
                result[i, 1] = g_y;
            }
            return result;
        }

        /// <summary>
        /// Вычисление всех uniform patterns LBP кодов в пределах max_value
        /// </summary>
        /// <param name="max_value">верхняя планка, до куда считать uniform patterns LBP-коды</param>
        /// <returns></returns>
        public static List<uint> CalcUniformPatterns(uint max_value)
        {
            List<uint> uniform_patterns = new List<uint>();
            for(uint i =0; i<max_value; i++)
            {
                string binary_digit = Convert.ToString(i, 2);
                int transition_count = 0;
                for (int j=0; j < binary_digit.Length - 1; j++)
                {
                    if (binary_digit[j] != binary_digit[j + 1])
                        transition_count += 1;
                }
                if (transition_count <= 2)
                    uniform_patterns.Add(i);
            }
            return uniform_patterns;
        }
    }
}
