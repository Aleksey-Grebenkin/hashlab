using System;
using System.Linq;
using System.Security.Cryptography;

namespace HashTable
{
    public class HashTable
    {
        // Количество коллизий
        public int Count { get; private set; }

        private const int _arrayCount = 1000;

        private const int c1 = 5;

        private const int c2 = 7;

        // Массив
        private readonly int?[] _array = new int?[_arrayCount];

        public int Add(int item)
        {
            var hash = SimpleHash(item);
            var i = 0;

            if (_array[hash] == item)
            {
                throw new ArgumentException("the value is already there");
            }

            while (_array[hash] != null)
            {
                hash = QuadraticSounding(item, i);
                i++;
            }

            Count += i;

            _array[hash] = item;

            return i;
        }

        public int AddMD5(int item)
        {
            var hash = HashMD5(item);
            var i = 0;

            if (_array[hash] == item)
            {
                throw new ArgumentException("the value is already there");
            }

            while (_array[hash] != null)
            {
                hash = QuadraticSoundingMD5(item, i);
                i++;
            }

            Count += i;

            _array[hash] = item;

            return i;
        }

        public int AddAnother(int item)
        {
            var hash = Hash(item);
            var i = 0;

            if (_array[hash] == item)
            {
                throw new ArgumentException("the value is already there");
            }

            while (_array[hash] != null)
            {
                hash = QuadraticSoundingAnother(item, i);
                i++;
            }

            Count += i;

            _array[hash] = item;

            return i;
        }

        private long SimpleHash(int item)
        {
            return item % _arrayCount;
        }

        private long HashMD5(int item)
        {
            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(BitConverter.GetBytes(item));

            var hashStr = string.Join("", hash.Select(b => b.ToString()));
            var endStr = hashStr.Substring(hashStr.Length - _arrayCount.ToString().Length);

            return Convert.ToInt64(endStr) % _arrayCount;
        }

        private long Hash(int item)
        {
            var a = item * 0.56849876742;
            var partS = Math.Round((a - Math.Round(a, 0)), 6).ToString();
            var part = partS.Split(',')[1];

            var endStr = part.Substring(part.Length - _arrayCount.ToString().Length);

            return Convert.ToInt64(endStr) % _arrayCount;
        }

        private long QuadraticSounding(int item, int i)
        {
            return (SimpleHash(item) + c1 * i + c2 * (long)Math.Pow(i, 2)) % _arrayCount;
        }

        private long QuadraticSoundingMD5(int item, int i)
        {
            return (HashMD5(item) + c1 * i + c2 * (long)Math.Pow(i, 2)) % _arrayCount;
        }

        private long QuadraticSoundingAnother(int item, int i)
        {
            return (Hash(item) + c1 * i + c2 * (long)Math.Pow(i, 2)) % _arrayCount;
        }
    }
}
