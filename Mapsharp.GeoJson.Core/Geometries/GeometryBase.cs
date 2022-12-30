using System;

namespace Mapsharp.GeoJson.Core.Geometries
{
    /// <summary>
    /// An abstract GeometryBase class providing a safe implementation of the GeoJson "Type" property.
    /// </summary>
    public abstract class GeometryBase : IGeometry
    {
        protected abstract GeometryType GeometryType { get; }

        public string Type
        {
            get => GeometryType.ToString();
            set
            {
                if (value != GeometryType.ToString())
                    throw new ArgumentException("Type for this instance can only be set to " + GeometryType.ToString());
            }
        }

        public abstract int GetCardinality();
    }
}
