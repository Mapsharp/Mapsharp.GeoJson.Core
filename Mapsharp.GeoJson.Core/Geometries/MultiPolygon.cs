using Mapsharp.GeoJson.Core.Validators;
using System.ComponentModel.DataAnnotations;

namespace Mapsharp.GeoJson.Core.Geometries
{
    /// <summary>
    /// A geoJson MultiPolygon
    /// </summary>
    public class MultiPolygon : GeometryBase, IGeoJsonCoordinateGeometry<IEnumerable<IEnumerable<IEnumerable<Position>>>>, IValidatableObject
    {
        protected override GeometryType GeometryType => GeometryType.Point;
        public IEnumerable<IEnumerable<IEnumerable<Position>>> Coordinates { get; set; }

        public MultiPolygon()
        {
            Coordinates = new List<IEnumerable<IEnumerable<Position>>>();
        }

        public MultiPolygon(IEnumerable<IEnumerable<IEnumerable<Position>>> coordinates)
        {
            Coordinates = coordinates.Select(p => p.Select(lr => lr.ToList()).ToList()).ToList();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return GeometryValidator.ValidateMultiPolygonCoordinates(Coordinates, nameof(Coordinates));
        }

        public override int GetCardinality()
        {
            return Coordinates?.FirstOrDefault()?.FirstOrDefault()?.FirstOrDefault().Cardinality ?? default(Position).Cardinality;
        }
    }
}
