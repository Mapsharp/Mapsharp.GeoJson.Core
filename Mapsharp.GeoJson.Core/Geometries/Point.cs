namespace Mapsharp.GeoJson.Core.Geometries
{
    /// <summary>
    /// A geoJson Point
    /// </summary>
    public class Point : GeometryBase, IGeoJsonCoordinateGeometry<Position>
    {
        protected override GeometryType GeometryType => GeometryType.Point;
        public Position Coordinates { get; set; }

        public Point() { }

        public Point(double x, double y)
        {
            Coordinates = new Position(x, y);
        }

        public Point(double x, double y, double z)
        {
            Coordinates = new Position(x, y, z);
        }

        public Point(Position coordinates)
        {
            Coordinates = coordinates;
        }

        public override int GetCardinality()
        {
            return Coordinates.Cardinality;
        }
    }
}
