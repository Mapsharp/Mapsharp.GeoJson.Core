using Mapsharp.GeoJson.Core.Validators;
using System.ComponentModel.DataAnnotations;

namespace Mapsharp.GeoJson.Core.Geometries
{
    /// <summary>
    ///  A GeoJson LineString.
    /// </summary>
    public class LineString : GeometryBase, IGeoJsonCoordinateGeometry<IEnumerable<Position>>, IValidatableObject
    {
        protected override GeometryType GeometryType => GeometryType.LineString;
        /// <summary>
        /// An IEnumerable of coordinates that should contain at least two members.
        /// </summary>
        public IEnumerable<Position> Coordinates { get; set; }

        public LineString()
        {
            Coordinates = new List<Position>();
        }

        public LineString(IEnumerable<Position> positions)
        {
            Coordinates = positions.ToList();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return GeometryValidator.ValidateLineStringCoordinates(Coordinates, nameof(Coordinates));
        }

        public override int GetCardinality()
        {
            return Coordinates?.FirstOrDefault().Cardinality ?? default(Position).Cardinality;
        }
    }
}
