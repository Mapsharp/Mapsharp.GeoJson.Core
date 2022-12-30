using System.Collections;

namespace Mapsharp.GeoJson.Core.Geometries
{
    public interface IGeometry
    {
        string Type { get; set; }
        int GetCardinality();
    }
}