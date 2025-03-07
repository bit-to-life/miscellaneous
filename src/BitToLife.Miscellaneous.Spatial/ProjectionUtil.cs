using DotSpatial.Projections;
using NetTopologySuite.Geometries;

namespace BitToLife.Miscellaneous.Spatial;

/// <summary>
/// 좌표를 대상 좌표계로 재투영하는 유틸리티
/// </summary>
/// <param name="sourceSRID">원본 좌표계 SRID</param>
/// <param name="destSRID">대상 좌표계 SRID</param>
public class ProjectionUtil(int sourceSRID, int destSRID)
{
    private readonly ProjectionInfo _source = ProjectionInfo.FromEpsgCode(sourceSRID);
    private readonly ProjectionInfo _dest = ProjectionInfo.FromEpsgCode(destSRID);

    /// <summary>
    /// Korean Geodetic Datum 2002 / Unified CS <see href="https://epsg.io/5179">EPSG:5179</see>
    /// </summary>
    /// 
    public const int KGD2002 = 5179;

    /// <summary>
    /// World Geodetic System 1984 - WGS84 <see href="https://epsg.io/4326">EPSG:4326</see>
    /// </summary>
    public const int WGS84 = 4326;

    /// <summary>
    /// 좌표를 대상 좌표계로 재투영합니다.
    /// </summary>
    /// <param name="xy">Point</param>
    /// <returns><see cref="double[]"/></returns>
    public double[] Reproject(double[] xy)
    {
        ArgumentOutOfRangeException.ThrowIfNotEqual(xy.Length, 2);

        DotSpatial.Projections.Reproject.ReprojectPoints(xy, [0.0], _source, _dest, 0, xy.Length - 1);

        return xy;
    }

    /// <summary>
    /// Point를 대상 좌표계로 재투영합니다.
    /// </summary>
    /// <param name="point"></param>
    /// <returns><see cref="LinearRing"/></returns>
    public Point Reproject(Point point)
    {
        double[] xy = Reproject([point.X, point.Y]);

        return new Point(xy[0], xy[1]);
    }

    private T Reproject<T>(T line, Func<Coordinate[], T> createNewInstance) where T : LineString
    {
        Coordinate[] coordinates = line.Coordinates;
        for (int i = 0; i < coordinates.Length; i++)
        {
            double[] xy = Reproject([coordinates[i].X, coordinates[i].Y]);
            coordinates[i] = new Coordinate(xy[0], xy[1]);
        }

        return createNewInstance(coordinates);
    }

    /// <summary>
    /// LineString을 대상 좌표계로 재투영합니다.
    /// </summary>
    /// <param name="lineString"></param>
    /// <returns><see cref="LinearRing" /></returns>
    public LineString Reproject(LineString lineString)
    {
        return Reproject(lineString, (coordinates) => new LineString(coordinates));
    }

    /// <summary>
    /// LinearRing을 대상 좌표계로 재투영합니다.
    /// </summary>
    /// <param name="ring"></param>
    /// <returns></returns>
    public LinearRing Reproject(LinearRing ring)
    {
        return Reproject(ring, (coordinates) => new LinearRing(coordinates));
    }

    /// <summary>
    /// Polygon을 대상 좌표계로 재투영합니다.
    /// </summary>
    /// <param name="polygon"></param>
    /// <returns><see cref="Polygon"/></returns>
    public Polygon Reproject(Polygon polygon)
    {
        LinearRing shell = Reproject((LinearRing)polygon.ExteriorRing);
        LinearRing[] holes = new LinearRing[polygon.NumInteriorRings];
        for (int i = 0; i < polygon.NumInteriorRings; i++)
        {
            holes[i] = Reproject((LinearRing)polygon.GetInteriorRingN(i));
        }

        return new Polygon(shell, holes);
    }

    private static TCollection Reproject<TCollection, TGeometry>(TCollection multiPoint, Func<Geometry, TGeometry> reprojectChild, Func<TGeometry[], TCollection> createNewCollection)
        where TCollection : GeometryCollection
        where TGeometry : Geometry
    {
        TGeometry[] points = new TGeometry[multiPoint.NumGeometries];
        for (int i = 0; i < multiPoint.NumGeometries; i++)
        {
            points[i] = reprojectChild(multiPoint.GetGeometryN(i));
        }

        return createNewCollection(points);
    }

    /// <summary>
    /// MultiPoint를 대상 좌표계로 재투영합니다.
    /// </summary>
    /// <param name="multiPoint"></param>
    /// <returns><see cref="MultiPoint"/></returns>
    public MultiPoint Reproject(MultiPoint multiPoint)
    {
        return Reproject(multiPoint, (g) => Reproject((Point)g), (points) => new MultiPoint(points));
    }

    /// <summary>
    /// MultiLineString을 대상 좌표계로 재투영합니다.
    /// </summary>
    /// <param name="multiLineString"></param>
    /// <returns></returns>
    public MultiLineString Reproject(MultiLineString multiLineString)
    {
        return Reproject(multiLineString, (g) => Reproject((LineString)g), (lineStrings) => new MultiLineString(lineStrings));
    }

    /// <summary>
    /// MultiPolygon을 대상 좌표계로 재투영합니다.
    /// </summary>
    /// <param name="multiPolygon"></param>
    /// <returns><see cref="MultiPolygon"/></returns>
    public MultiPolygon Reproject(MultiPolygon multiPolygon)
    {
        return Reproject(multiPolygon, (g) => Reproject((Polygon)g), (polygons) => new MultiPolygon(polygons));
    }
}
