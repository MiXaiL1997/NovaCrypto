using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaCrypto
{
    class RandomGenerator
    {
        public static double congruential(int x, int max) // функция генерации псевдослучайных чисел
        {
             int m = max, // генерация псевдослучайных чисел в диапазоне значений от 0 до 100 (выбирается случайно m > 0)
                      a = 8, // множитель (выбирается случайно 0 <= a <= m)
                    inc = 65; // инкрементирующее значение (выбирается случайно 0 <= inc <= m)
            x = ((a * x) + inc) % m; // формула линейного конгруэнтного метода генерации псевдослучайных чисел  
            //return (x / (double)m);
            return x;
        }
    }
}
