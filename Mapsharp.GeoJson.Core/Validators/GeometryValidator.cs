using Mapsharp.GeoJson.Core.Geometries;
using System.ComponentModel.DataAnnotations;

namespace Mapsharp.GeoJson.Core.Validators
{
    internal static class GeometryValidator
    {
        internal static IEnumerable<ValidationResult> ValidateLineStringCoordinates(IEnumerable<Position> coordinates, string propertyName)
        {
            if (coordinates == null)
            {
                yield return new ValidationResult($"{propertyName} cannot be null.", new[] { propertyName });
                yield break;
            }

            var enumorator = coordinates.GetEnumerator();
            if (!enumorator.MoveNext())
            {
                yield return new ValidationResult("A line string must contain at least two Positions.", new[] { propertyName });
                yield break;
            }
            var first = enumorator.Current;
            var next = first;

            int capacity = 1;
            while (enumorator.MoveNext())
            {
                capacity++;
                Position previous = next;
                next = enumorator.Current;
                if (previous.Cardinality != next.Cardinality)
                {
                    yield return new ValidationResult("All positions must be of the same cardinality.", new[] { propertyName });
                    yield break;
                }
            }

            if (capacity < 2)
            {
                yield return new ValidationResult("A line string must contain at least two Positions.", new[] { propertyName });
                yield break;
            }
        }

        internal static IEnumerable<ValidationResult> ValidateMultiLineStringCoordinates(IEnumerable<IEnumerable<Position>> coordinates, string propertyName)
        {
            if (coordinates == null)
            {
                yield return new ValidationResult($"{propertyName} cannot be null.", new[] { propertyName });
                yield break;
            }

            foreach (var r in coordinates.SelectMany((lr, i) => ValidateLineStringCoordinates(lr, propertyName + $"[{i}]")))
            {
                yield return r;
            }
        }

        internal static IEnumerable<ValidationResult> ValidateLinearRingCoordinates(IEnumerable<Position> coordinates, string propertyName)
        {
            if (coordinates == null)
            {
                yield return new ValidationResult($"{propertyName} cannot be null.", new[] { propertyName });
                yield break;
            }

            var enumorator = coordinates.GetEnumerator();
            if (!enumorator.MoveNext())
            {
                yield return new ValidationResult("A linear ring must contain at least four Positions.", new[] { propertyName });
                yield break;
            }
            var first = enumorator.Current;
            var next = first;

            int capacity = 1;
            while (enumorator.MoveNext())
            {
                capacity++;
                Position previous = next;
                next = enumorator.Current;
                if (previous.Cardinality != next.Cardinality)
                {
                    yield return new ValidationResult("All positions must be of the same cardinality.", new[] { propertyName });
                    yield break;
                }
            }

            if (capacity < 4)
            {
                yield return new ValidationResult("A linear ring must contain at least four Positions.", new[] { propertyName });
                yield break;
            }

            if (!first.Equals(next))
            {
                yield return new ValidationResult("The first and last position of a linear ring must be equal.", new[] { propertyName });
                yield break;
            }
        }

        internal static IEnumerable<ValidationResult> ValidatePolygonCoordinates(IEnumerable<IEnumerable<Position>> coordinates, string propertyName)
        {
            if (coordinates == null)
            {
                yield return new ValidationResult($"{propertyName} cannot be null.", new[] { propertyName });
                yield break;
            }

            foreach (var r in coordinates.SelectMany((c, i) => ValidateLinearRingCoordinates(c, propertyName + $"[{i}]")))
            {
                yield return r;
                yield break;
            }

            var next = coordinates.First().First();
            foreach (var linearRing in coordinates.Skip(1))
            {
                var previous = next;
                next = linearRing.First();
                if (previous.Cardinality != next.Cardinality)
                {
                    yield return new ValidationResult("All positions must be of the same cardinality.", new[] { propertyName });
                    yield break;
                }
            }
        }

        internal static IEnumerable<ValidationResult> ValidateMultiPolygonCoordinates(IEnumerable<IEnumerable<IEnumerable<Position>>> coordinates, string propertyName)
        {
            if (coordinates == null)
            {
                yield return new ValidationResult($"{propertyName} cannot be null.", new[] { propertyName });
                yield break;
            }

            foreach (var r in coordinates.SelectMany((c, i) => ValidatePolygonCoordinates(c, propertyName + $"[{i}]")))
            {
                yield return r;
                yield break;
            }

            var next = coordinates.First().First().First();
            foreach (var polygonCoordinates in coordinates.Skip(1))
            {
                var previous = next;
                next = polygonCoordinates.First().First();
                if (previous.Cardinality != next.Cardinality)
                {
                    yield return new ValidationResult("All positions must be of the same cardinality.", new[] { propertyName });
                    yield break;
                }
            }
        }

        internal static IEnumerable<ValidationResult> ValidateGeometryCollectionGeometries(IEnumerable<GeometryBase> geometries, string propertyName, ValidationContext context)
        {
            if (geometries == null)
            {
                yield return new ValidationResult($"{propertyName} cannot be null.", new[] { propertyName });
                yield break;
            }

            foreach (var r in geometries.Where(g => g is IValidatableObject vo)
                                        .Select(g => (IValidatableObject)g)
                                        .SelectMany(vo => vo.Validate(context)))
            {
                yield return r;
            }
        }
    }
}
