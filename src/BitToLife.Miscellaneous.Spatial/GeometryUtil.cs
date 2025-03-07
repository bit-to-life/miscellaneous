using BitToLife.Miscellaneous;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using NetTopologySuite.Utilities;

namespace BitToLife.Miscellaneous.Spatial;

/// <summary>
/// Geometry 관련 유틸리티
/// </summary>
public static class GeometryUtil
{
    // SRID: 4326 (WGS 84)
    private const int SRID = 4326;

    private static readonly NtsGeometryServices geometryServices = NtsGeometryServices.Instance;

    private static readonly GeometryFactory geometryFactory = geometryServices.CreateGeometryFactory(SRID);

    // 지구의 반지름 (미터)
    private const double EARTH_RADIUS_METER = 6_378_000;

    // 지구의 둘레 (미터)
    private const double EARTH_CIRCUMFERENCE_METER = EARTH_RADIUS_METER * 2 * Math.PI;// 40_074_155;

    // 지구 둘레 1도의 거리 (미터)
    private const double ONE_DEGREE_OF_LATITUDE_METER = EARTH_CIRCUMFERENCE_METER / 360;// 111_317;

    /// <summary>
    /// Square Geometry를 생성합니다.
    /// </summary>
    /// <param name="southWestCoordinate">남서 좌표</param>
    /// <param name="northEastCoordinate">북동 좌표</param>
    /// <returns><see cref="Polygon"/></returns>
    public static Polygon CreateGeometryRectangle(double[] southWestCoordinate, double[] northEastCoordinate)
    {
        ArgumentOutOfRangeException.ThrowIfNotEqual(southWestCoordinate.Length, 2);
        ArgumentOutOfRangeException.ThrowIfNotEqual(northEastCoordinate.Length, 2);

        double swLng = southWestCoordinate[0];
        double swLat = southWestCoordinate[1];
        double neLng = northEastCoordinate[0];
        double neLat = northEastCoordinate[1];

        return CreateGeometryPolygon(
            [swLng, swLat],
            [swLng, neLat],
            [neLng, neLat],
            [neLng, swLat],
            [swLng, swLat]
        );
    }

    /// <summary>
    /// Polygon Geometry를 생성합니다.
    /// </summary>
    /// <param name="coordinates"></param>
    /// <returns></returns>
    public static Polygon CreateGeometryPolygon(params double[][] coordinates)
    {
        ArgumentNullException.ThrowIfNull(coordinates);
        ArgumentOutOfRangeException.ThrowIfLessThan(coordinates.Length, 3);

        Coordinate[] coordinateArray = (
            from c in coordinates
            select new Coordinate(c[0], c[1])
        ).ToArray();

        Coordinate first = coordinateArray.First();
        Coordinate last = coordinateArray.Last();

        bool isClosed = first[0] == last[0] && first[1] == last[1];

        ArgumentOutOfRangeException.ThrowIfLessThan(coordinates.Length, isClosed ? 4 : 3);

        Polygon polygon = geometryFactory.CreatePolygon(isClosed ? coordinateArray : [.. coordinateArray, first]);

        return polygon;
    }

    /// <summary>
    /// Point Geometry를 생성합니다.
    /// </summary>
    /// <param name="longitude"></param>
    /// <param name="latitude"></param>
    /// <returns><see cref="Point"/></returns>
    public static Point CreateGeometryPoint(double longitude, double latitude)
    {
        Point point = geometryFactory.CreatePoint(new Coordinate(longitude, latitude));

        return point;
    }

    /// <summary>
    /// 지정된 크기의 Circle Geometry를 생성합니다.
    /// </summary>
    /// <param name="x">X 좌표 (Longitude)</param>
    /// <param name="y">Y 좌표 (Latitude)</param>
    /// <param name="diameter">지름 (Meter)</param>
    /// <param name="numberOfPoints">포인트 개수</param>
    /// <returns></returns>
    public static Polygon CreateCircle(double x, double y, double diameter, int numberOfPoints = 100)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(diameter);

        // https://gis.stackexchange.com/questions/377438/buffer-line-between-two-coordinates-with-meters
        GeometricShapeFactory shapeFactory = new()
        {
            Centre = new Coordinate(x, y),
            Width = diameter / (EARTH_CIRCUMFERENCE_METER * Math.Cos(MathUtil.DegreeToRadian(y)) / 360),
            Height = diameter / ONE_DEGREE_OF_LATITUDE_METER,
            NumPoints = numberOfPoints,
        };

        Polygon circle = shapeFactory.CreateEllipse();

        return circle;
    }

    /// <summary>
    /// 두 좌표의 거리를 m로 반환
    /// </summary>
    /// <param name="point1">좌표1</param>
    /// <param name="point2">좌표2</param>
    /// <returns></returns>
    public static double GetDistance(double[] coordinate1, double[] coordinate2)
    {
        ArgumentOutOfRangeException.ThrowIfNotEqual(coordinate1.Length, 2);
        ArgumentOutOfRangeException.ThrowIfNotEqual(coordinate2.Length, 2);

        // Haversine formula

        // longitde (경도)
        double lambda1 = coordinate1[0];
        double lambda2 = coordinate2[0];

        // latitude (위도)
        double phi1 = coordinate1[1];
        double phi2 = coordinate2[1];

        double phi1R = MathUtil.DegreeToRadian(phi1);
        double phi2R = MathUtil.DegreeToRadian(phi2);
        double deltaPhi = MathUtil.DegreeToRadian(phi2 - phi1);
        double deltaLambda = MathUtil.DegreeToRadian(lambda2 - lambda1);

        double a = Math.Sin(deltaPhi / 2) * Math.Sin(deltaPhi / 2) + Math.Cos(phi1R) * Math.Cos(phi2R) * Math.Sin(deltaLambda / 2) * Math.Sin(deltaLambda / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        double d = EARTH_RADIUS_METER * c;

        return d;
    }

    /// <summary>
    /// 폴리곤 기준으로 지정된 좌표의 반대편 좌표를 반환합니다.
    /// </summary>
    /// <param name="polygon">폴리곤</param>
    /// <param name="basePoint">좌표</param>
    /// <returns></returns>
    public static Coordinate GetReversePoint(Polygon polygon, double[] coordinate)
    {
        ArgumentOutOfRangeException.ThrowIfNotEqual(coordinate.Length, 2);

        Coordinate baseCoordinate = new(coordinate[0], coordinate[1]);
        IOrderedEnumerable<Coordinate> coordinates = (
            from c in polygon.Coordinates
            orderby Math.Abs(c.Distance(baseCoordinate))
            select c
        );

        Coordinate nearCoordinate = coordinates.First();
        Coordinate farCoordinate = coordinates.Last();

        double nearDiffX = baseCoordinate.X - nearCoordinate.X;
        double nearDiffY = baseCoordinate.Y - nearCoordinate.Y;

        Coordinate newCoordinate = new(farCoordinate.X - nearDiffX, farCoordinate.Y - nearDiffY);

        return newCoordinate;
    }

    /// <summary>
    /// 지정된 좌표에서 랜덤한 좌표를 반환합니다.
    /// </summary>
    /// <param name="coordinate"></param>
    /// <param name="minDistance"></param>
    /// <param name="maxDistance"></param>
    /// <returns></returns>
    public static double[] GetRandomPoint(double[] coordinate, int minDistance, int maxDistance)
    {
        ArgumentOutOfRangeException.ThrowIfNotEqual(coordinate.Length, 2);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(minDistance);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxDistance);
        ArgumentOutOfRangeException.ThrowIfEqual(minDistance, maxDistance);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(minDistance, maxDistance);

        double longitude = coordinate[0];
        double latitude = coordinate[1];

        double xDistance = RandomUtil.Next(minDistance, maxDistance) / (EARTH_CIRCUMFERENCE_METER * Math.Cos(MathUtil.DegreeToRadian(latitude)) / 360) / 2;
        double yDistance = RandomUtil.Next(minDistance, maxDistance) / ONE_DEGREE_OF_LATITUDE_METER / 2;

        double radian = MathUtil.DegreeToRadian(RandomUtil.Next(0, 360));
        double x = xDistance * Math.Cos(radian);
        double y = yDistance * Math.Sin(radian);

        return [longitude + x, latitude + y];
    }

    /// <summary>
    /// 지정된 좌표 범위의 그리드를 생성합니다.
    /// </summary>
    /// <param name="swLng"></param>
    /// <param name="swLat"></param>
    /// <param name="neLng"></param>
    /// <param name="neLat"></param>
    /// <param name="tileWidth"></param>
    /// <param name="tileHeight"></param>
    /// <returns></returns>
    public static IEnumerable<SpatialGrid> CreateRegularGrids(double[] southWestCoordinate, double[] northEastCoordinate, double tileWidth, double tileHeight)
    {
        ArgumentOutOfRangeException.ThrowIfNotEqual(southWestCoordinate.Length, 2);
        ArgumentOutOfRangeException.ThrowIfNotEqual(northEastCoordinate.Length, 2);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(tileWidth);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(tileHeight);

        double swLng = southWestCoordinate[0];
        double swLat = southWestCoordinate[1];
        double neLng = northEastCoordinate[0];
        double neLat = northEastCoordinate[1];

        double minColumnIndex = Math.Floor(swLng / tileWidth);
        double maxColumnIndex = Math.Ceiling(neLng / tileWidth) - 1;
        double minRowIndex = Math.Floor(swLat / tileHeight);
        double maxRowIndex = Math.Ceiling(neLat / tileHeight) - 1;
        double columnIndex = minColumnIndex;

        List<SpatialGrid> list = [];
        while (columnIndex <= maxColumnIndex)
        {
            double rowIndex = minRowIndex;

            while (rowIndex <= maxRowIndex)
            {
                list.Add(
                    new SpatialGrid(
                        columnIndex,
                        rowIndex,
                        [columnIndex * tileWidth, rowIndex * tileHeight],
                        [(columnIndex * tileWidth) + tileWidth, (rowIndex * tileHeight) + tileHeight]
                    )
                );
                rowIndex++;
            }

            columnIndex++;
        }

        return list;
    }
}
