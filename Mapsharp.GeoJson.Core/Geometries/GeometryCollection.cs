using Mapsharp.GeoJson.Core.Validators;
using System.ComponentModel.DataAnnotations;

namespace Mapsharp.GeoJson.Core.Geometries
{
    /// <summary>
    /// A GeoJson GeometryCollection.
    /// </summary>
    public class GeometryCollection : GeometryBase, IValidatableObject
    {
        protected override GeometryType GeometryType => GeometryType.GeometryCollection;

        public IEnumerable<GeometryBase> Geometries { get; set; }

        public GeometryCollection()
        {
            Geometries = new List<GeometryBase>();
        }

        public GeometryCollection(IEnumerable<GeometryBase> geometries)
        {
            Geometries = geometries.ToList();
        }

        public override int GetCardinality()
        {
            return Geometries?.FirstOrDefault()?.GetCardinality() ?? default(Position).Cardinality;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return GeometryValidator.ValidateGeometryCollectionGeometries(Geometries, nameof(Geometries), validationContext);
        }
    }
}
