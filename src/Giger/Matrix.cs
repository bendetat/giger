using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giger
{
    public class Matrix
    {
        readonly char[] _delimiters =
        {
                ' ', ',', '\t', '\r', '\n'
            };
        private readonly double[] _values;

        public Matrix(params double[] values)
        {
            _values = values;
        }

        public Matrix(string values)
        {
            _values = values.Split(_delimiters)
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(double.Parse)
                .ToArray();
        }

        public void Validate(int rows, int columns)
        {
            if (_values.Count() != rows*columns)
            {
                throw new InvalidOperationException($"Matrix requires {rows}*{columns}={rows*columns} values but there are {_values.Count()} values present");
            }
        }

        public override string ToString()
        {
            return string.Join(" ", _values.Select(x => x.ToString()));
        }
    }

    public static class MatrixExtensions
    {
        public static Matrix ToMatrix(this string input, int rows, int columns)
        {
            var matrix = new Matrix(input);

            matrix.Validate(rows, columns);

            return matrix;
        }

        public static Matrix ToMatrix(this double[] input, int rows, int columns)
        {
            var matrix = new Matrix(input);

            matrix.Validate(rows, columns);

            return matrix;
        }
    }
}
