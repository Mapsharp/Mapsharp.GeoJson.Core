using Mapsharp.GeoJson.Core.Validators;
using System.ComponentModel.DataAnnotations;

namespace Mapsharp.GeoJson.Core.Geometries
{
    /// <summary>
    /// A geoJson Polygon
    /// </summary>
    public class Polygon : GeometryBase, IGeoJsonCoordinateGeometry<IEnumerable<IEnumerable<Position>>>, IValidatableObject
    {
        protected override GeometryType GeometryType => GeometryType.Point;
        public IEnumerable<IEnumerable<Position>> Coordinates { get; set; }

        public Polygon()
        {
            Coordinates = new List<IEnumerable<Position>>();
        }

        public Polygon(IEnumerable<IEnumerable<Position>> coordinates)
        {
            Coordinates = coordinates.Select(lr => lr.ToList()).ToList();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return GeometryValidator.ValidatePolygonCoordinates(Coordinates, nameof(Coordinates));
        }

        public override int GetCardinality()
        {
            return Coordinates?.FirstOrDefault()?.FirstOrDefault().Cardinality ?? default(Position).Cardinality;
        }
    }
}
