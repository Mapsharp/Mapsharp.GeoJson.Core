using Mapsharp.GeoJson.Core.Geometries;

namespace Mapsharp.GeoJson.Core.Tests
{
    public class PositionTests
    {
        [Fact]
        public void Has2DCardinalityByDefault()
        {
            Position c = new Position();
            Assert.Equal(2, c.Cardinality);
        }

        [Fact]
        public void Has2DCardinality()
        {
            Position c2 = new Position(2, 3);
            Assert.Equal(2, c2.Cardinality);
        }

        [Fact]
        public void Has3DCardinality()
        {
            Position c = new Position(1, 2, 3);
            Assert.Equal(3, c.Cardinality);
        }

        [Fact]
        public void CanCreateFromTwoItemEnumerable()
        {
            double[] xy = { 0.987, 34.2 };
            Position c = Position.FromEnumerable(xy);

            Assert.Equal(2, c.Cardinality);
            Assert.Equal(xy[0], c.X);
            Assert.Equal(xy[1], c.Y);
        }

        [Fact]
        public void CanCreateFromThreeItemEnumerable()
        {
            double[] xy = { 0.987, 34.2, -11.2 };
            Position c = Position.FromEnumerable(xy);

            Assert.Equal(3, c.Cardinality);
            Assert.Equal(xy[0], c.X);
            Assert.Equal(xy[1], c.Y);
            Assert.Equal(xy[2], c.Z);
        }

        [Fact]
        public void FromEnumerableThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                Position c = Position.FromEnumerable(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            });
        }

        [Fact]
        public void FromEnumerableThrowsArgumentExceptionForEmptyEnumerable()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                double[] arr = Array.Empty<double>();
                Position c = Position.FromEnumerable(arr);
            });
        }

        [Fact]
        public void FromEnumerableThrowsArgumentExceptionForOneItemEnumerable()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                double[] arr = { 0 };
                Position c = Position.FromEnumerable(arr);
            });
        }

        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void FromEnumerableThrowsArgumentExceptionForMoreThanThreeItems(int size)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                double[] arr = new double[size];
                Position c = Position.FromEnumerable(arr);
            });
        }
    }
}