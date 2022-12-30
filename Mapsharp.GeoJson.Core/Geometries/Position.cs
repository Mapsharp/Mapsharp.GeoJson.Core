using System.Collections;

namespace Mapsharp.GeoJson.Core.Geometries
{
    public readonly struct Position : IEnumerable<double>
    {
        private readonly Dimension _cardinality;
        public int Cardinality => (int)_cardinality + 2;
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public double this[int index]
        {
            get
            {
                if (index == 0) return X;
                if (index == 1) return Y;
                if (index == 2 && _cardinality == Dimension.D3) return Z;
                throw new IndexOutOfRangeException();
            }
        }

        public Position(double x, double y)
        {
            _cardinality = Dimension.D2;
            X = x;
            Y = y;
            Z = double.NaN;
        }

        public Position(double x, double y, double z)
        {
            _cardinality = Dimension.D3;
            X = x;
            Y = y;
            Z = z;
        }

        public static Position FromEnumerable(IEnumerable<double> enumerable)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));

            var enumerator = enumerable.GetEnumerator();

            if (!enumerator.MoveNext())
                throw new ArgumentException("An empty enumerable cannot be converted to a Coordinate.");
            double x = enumerator.Current;

            if (!enumerator.MoveNext())
                throw new ArgumentException("A coordinate requires at least two values, but only one was provided.");
            double y = enumerator.Current;

            if (!enumerator.MoveNext())
            {
                return new Position(x, y);
            }

            double z = enumerator.Current;

            if (!enumerator.MoveNext())
            {
                return new Position(x, y, z);
            }

            throw new ArgumentException("A coordinate can only contain two or three values.");
        }

        public IEnumerator<double> GetEnumerator()
        {
            yield return X;
            yield return Y;
            if (_cardinality == Dimension.D3)
                yield return Z;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private enum Dimension
        {
            D2 = 0,
            D3 = 1
        }

    }




}
