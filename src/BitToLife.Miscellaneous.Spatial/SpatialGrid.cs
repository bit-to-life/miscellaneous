using NetTopologySuite.Geometries;

namespace BitToLife.Miscellaneous.Spatial;

public sealed record SpatialGrid
{
    public SpatialGrid(double columnIndex, double rowIndex, double[] southWestCoordinate, double[] northEastCoordinate)
    {
        Code = $"{columnIndex}-{rowIndex}";
        Polygon = GeometryUtil.CreateGeometryRectangle(southWestCoordinate, northEastCoordinate);
    }

    public string Code { get; init; }

    public Polygon Polygon { get; init; }
}