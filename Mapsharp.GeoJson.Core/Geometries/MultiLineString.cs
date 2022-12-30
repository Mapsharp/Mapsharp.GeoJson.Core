using Mapsharp.GeoJson.Core.Validators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mapsharp.GeoJson.Core.Geometries
{
    /// <summary>
    ///  A GeoJson MultiLineString.
    /// </summary>
    public class MultiLineString : GeometryBase, IGeoJsonCoordinateGeometry<IEnumerable<IEnumerable<Position>>>, IValidatableObject
    {
        protected override GeometryType GeometryType => GeometryType.MultiLineString;
        /// <summary>
        /// An IEnumerable LineStriong Coordinates.
        /// </summary>
        public IEnumerable<IEnumerable<Position>> Coordinates { get; set; }

        public MultiLineString()
        {
            Coordinates = new List<IEnumerable<Position>>();
        }

        public MultiLineString(IEnumerable<IEnumerable<Position>> lineStrings)
        {
            Coordinates = lineStrings.Select(p => p.ToList()).ToList();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return GeometryValidator.ValidateMultiLineStringCoordinates(Coordinates, nameof(Coordinates));
        }

        public override int GetCardinality()
        {
            return Coordinates?.FirstOrDefault()?.FirstOrDefault().Cardinality ?? default(Position).Cardinality;
        }
    }
}
