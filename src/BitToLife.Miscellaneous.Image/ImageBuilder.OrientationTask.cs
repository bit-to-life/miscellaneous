using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using SixLabors.ImageSharp.Processing;

namespace BitToLife.Miscellaneous.Image;

public sealed partial record ImageBuilder
{
    public sealed record OrientationTask : IImageBuilderTask
    {
        public bool AutoFix { get; init; }

        internal static RotateMode GetRotateMode(ushort orientationValue)
        {
            return orientationValue switch
            {
                // rotated 90 right
                8 => RotateMode.Rotate270,
                // bottoms up
                3 => RotateMode.Rotate180,
                // rotated 90 left
                6 => RotateMode.Rotate90,
                // landscape, do nothing
                _ => 0,
            };
        }

        public void Execute(SixLabors.ImageSharp.Image image)
        {
            if (AutoFix)
            {
                if (
                    image.Metadata.ExifProfile?.TryGetValue(
                        ExifTag.Orientation,
                        out IExifValue<ushort>? orientation
                    ) ?? false
                )
                {
                    if (ushort.TryParse(orientation!.Value.ToString(), out ushort orientationValue))
                    {
                        RotateMode rotateMode = GetRotateMode(orientationValue);
                        if (rotateMode != RotateMode.None)
                        {
                            image.Mutate(x => x.Rotate(rotateMode));
                            image.Metadata.ExifProfile?.RemoveValue(ExifTag.Orientation);
                        }
                    }
                }
            }
        }
    }
}
