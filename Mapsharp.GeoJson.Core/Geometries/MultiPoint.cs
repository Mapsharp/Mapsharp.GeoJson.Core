namespace Mapsharp.GeoJson.Core.Geometries
{
    /// <summary>
    ///  A GeoJson MultiPoint
    /// </summary>
    public class MultiPoint : GeometryBase, IGeoJsonCoordinateGeometry<IEnumerable<Position>>
    {
        protected override GeometryType GeometryType => GeometryType.MultiPoint;
        public IEnumerable<Position> Coordinates { get; set; }

        public MultiPoint()
        {
            Coordinates = new List<Position>();
        }

        public MultiPoint(IEnumerable<Position> coordinates)
        {
            Coordinates = coordinates.ToList();
        }

        public override int GetCardinality()
        {
            return Coordinates?.FirstOrDefault().Cardinality ?? default(Position).Cardinality;
        }
    }
}
