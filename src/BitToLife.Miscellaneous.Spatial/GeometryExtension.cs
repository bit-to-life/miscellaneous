using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Utilities;
using NetTopologySuite.Simplify;

namespace BitToLife.Miscellaneous.Spatial;
public static class GeometryExtension
{
    /// <summary>
    /// Geometry를 유효하게 고칩니다.
    /// </summary>
    /// <param name="geometry"></param>
    /// <param name="isKeepMulti"></param>
    /// <returns></returns>
    public static Geometry Fix(this Geometry geometry, bool isKeepMulti = true)
    {
        if (!geometry.IsValid)
        {
            Geometry fixedGeometry = GeometryFixer.Fix(geometry, isKeepMulti);

            return fixedGeometry;
        }

        return geometry;
    }

    /// <summary>
    /// Geometry를 단순화합니다.
    /// </summary>
    /// <param name="geometry"></param>
    /// <param name="distanceTolerance">거리 공차</param>
    /// <returns></returns>
    public static Geometry Simplify(this Geometry geometry, double distanceTolerance)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(distanceTolerance);

        Geometry simplifiedGeometry = DouglasPeuckerSimplifier.Simplify(geometry, distanceTolerance);

        return simplifiedGeometry;
    }

    /// <summary>
    /// Geometry를 GeoJson으로 변환합니다.
    /// </summary>
    /// <param name="geometry"></param>
    /// <returns></returns>
    public static string? AsGeoJson(this Geometry geometry)
    {
        string geometryText = geometry.ToString();
        string type = geometry.OgcGeometryType.ToString();
        string upperType = type.ToUpper();

        return geometry.OgcGeometryType switch
        {
            OgcGeometryType.Point => $"{{\"type\": \"{type}\",\"coordinates\":{geometryText.Replace($"{upperType} ", "").Replace("(", "[").Replace(")", "]").Replace(" ", ",")}}}",
            OgcGeometryType.Polygon or
            OgcGeometryType.MultiPolygon or
            OgcGeometryType.MultiPoint or
            OgcGeometryType.LineString => $"{{\"type\": \"{type}\",\"coordinates\":[{geometryText.Replace($"{upperType} ", "").Replace("(", "[").Replace(")", "]").Replace("], ", "]],[").Replace(", ", "],[").Replace(" ", ",")}]}}",
            _ => throw new NotSupportedException("Not supported geometry type."),
        };
    }
}
