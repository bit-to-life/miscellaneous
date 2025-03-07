using BitToLife.Miscellaneous.Spatial;
using NetTopologySuite.Geometries;

double[] point1 = [126.984914, 37.551595];
double[] point2 = [127.101720, 37.551595];

double distance = GeometryUtil.GetDistance(point1, point2); // 10.3km

Polygon square = GeometryUtil.CreateGeometryRectangle([126.96475695456189, 37.548532857369125], [126.98014630281568, 37.560774184480806]);
string? squareGeojson = square.AsGeoJson();

Polygon circle = GeometryUtil.CreateCircle(point1[0], point1[1], 100);
string? geojson = circle.AsGeoJson();

Geometry simplifiedPolygon = circle.Simplify(0.00001);
string? simplifiedGeojson = simplifiedPolygon.AsGeoJson();

Coordinate reversePoint = GeometryUtil.GetReversePoint(square, [126.96778745261423, 37.5507370518463]);

Polygon p = GeometryUtil.CreateGeometryPolygon(
    [126.96831775002056, 37.56468272225577],
    [126.95769146451181, 37.55844410939372],
    [126.95755863594269, 37.552125994407206]
);
string? j = p.AsGeoJson();

Console.ReadLine();
