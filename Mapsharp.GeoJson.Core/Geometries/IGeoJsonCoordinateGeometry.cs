using System.Collections;

namespace Mapsharp.GeoJson.Core.Geometries
{
    public interface IGeoJsonCoordinateGeometry<T> : IGeometry
        where T : IEnumerable
    {
        T Coordinates { get; set; }
    }
}