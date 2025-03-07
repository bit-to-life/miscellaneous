using NetTopologySuite.Features;
using NetTopologySuite.IO.Esri;
using NetTopologySuite.IO.Esri.Shapefiles.Readers;

namespace BitToLife.Miscellaneous.Spatial;

/// <summary>
/// Shapefile을 읽어들입니다.
/// </summary>
public sealed class ShpReader
{
    /// <summary>
    /// Shapefile을 읽어들입니다. 모든 Feature를 한번에 로드합니다. 
    /// </summary>
    /// <param name="shpPath">Shp파일경로</param>
    /// <param name="dbfPath">Dbf파일경로</param>
    /// <param name="options">옵션</param>
    /// <returns></returns>
    public static Feature[] Read(string shpPath, string dbfPath, ShapefileReaderOptions options)
    {
        using (FileStream shp = new(shpPath, FileMode.Open, FileAccess.Read))
        using (FileStream dbf = new(dbfPath, FileMode.Open, FileAccess.Read))
        {
            return Shapefile.ReadAllFeatures(shp, dbf, options);
        }
    }

    /// <summary>
    /// Shapefile을 읽어들입니다. 모든 Feature를 한번에 로드합니다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="shpPath">Shp파일경로</param>
    /// <param name="dbfPath">Dbf파일경로</param>
    /// <param name="options">옵션</param>
    /// <param name="onLoaded">모든 Feature를 로드 후 실행할 Func</param>
    /// <returns></returns>
    public static T Read<T>(string shpPath, string dbfPath, ShapefileReaderOptions options, Func<Feature[], T> onLoaded)
    {
        Feature[] features = Read(shpPath, dbfPath, options);

        return onLoaded(features);
    }

    /// <summary>
    /// Shapefile을 읽어들입니다. Feature를 순차적으로 로드합니다.
    /// </summary>
    /// <param name="shpPath">Shp파일경로</param>
    /// <param name="dbfPath">Dbf파일경로</param>
    /// <param name="options">옵션</param>
    /// <param name="onRead">Feature를 순차적으로 읽으면서 실행할 Action</param>
    public static void Read(string shpPath, string dbfPath, ShapefileReaderOptions options, Action<Feature> onRead)
    {
        using (FileStream shp = new(shpPath, FileMode.Open, FileAccess.Read))
        using (FileStream dbf = new(dbfPath, FileMode.Open, FileAccess.Read))
        using (ShapefileReader reader = Shapefile.OpenRead(shp, dbf, options))
        {
            while (reader.Read(out bool deleted, out Feature feature))
            {
                if (!deleted)
                {
                    onRead(feature);
                }
            }
        }
    }
}