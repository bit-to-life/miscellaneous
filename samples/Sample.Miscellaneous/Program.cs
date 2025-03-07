using BitToLife.Miscellaneous;

// CompressionUtil

byte[] source = [
1,0,0,0,1,0,1,0,1,1,0,1,0,1,0,1,0,1,0,0,0,1,1,1,0,0,1,1,1,0,1,0,
0,0,1,0,1,0,1,1,1,0,0,0,0,1,1,1,1,0,1,0,0,0,0,1,0,1,0,1,1,0,1,1,
1,1,0,1,0,0,1,1,1,1,1,0,1,1,1,1,0,1,1,0,0,0,0,0,0,1,0,0,1,0,0,1,
1,1,1,0,0,0,0,1,1,0,1,0,0,1,0,0,0,1,0,1,1,0,1,1,1,1,0,0,0,0,0,1,
1,0,1,1,0,1,1,1,1,0,0,1,0,0,1,0,1,1,0,1,0,0,1,0,1,0,1,0,1,1,0,0,
1,1,1,1,1,1,1,1,0,1,0,1,0,1,1,0,0,0,1,1,0,0,1,1,0,0,1,1,0,0,0,1,
0,0,0,0,1,0,1,1,0,0,1,0,1,0,0,1,0,0,1,1,1,0,0,0,1,1,0,1,0,1,0,0,
0,0,0,0,1,0,0,0,0,1,0,1,1,1,0,1,1,1,0,0,0,0,1,1,0,1,0,0,0,0,0,0,
1,0,1,1,0,0,0,0,0,1,0,1,1,1,0,1,1,1,0,1,1,0,1,1,1,1,0,0,1,1,1,1,
1,0,0,1,0,1,0,1,1,1,0,1,0,0,0,0,1,0,1,0,0,0,1,0,1,0,1,0,1,1,1,0
];
byte[] compressed = await CompressionUtil.CompressAsync(source);
byte[] decompressed = await CompressionUtil.DecompressAsync(compressed);

string text = """
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus gravida lectus enim, eu sagittis libero lacinia nec. Sed nec viverra magna. Sed eu est ex. Curabitur quam orci, bibendum non odio sit amet, vehicula congue nibh. Nam rhoncus arcu eu velit placerat, et bibendum massa tristique. Pellentesque nisl nisi, laoreet non velit a, eleifend commodo lectus. Nullam elementum, arcu id commodo aliquet, ante arcu egestas erat, nec cursus justo risus eu mi. Vestibulum ullamcorper, erat in pulvinar ultrices, lacus elit rhoncus ipsum, a aliquam ex nisl eu risus. Nulla quis ultrices sem, vitae dapibus turpis. Curabitur faucibus tincidunt enim ac sollicitudin. Aliquam ornare ante elit, vel commodo dolor maximus a. Duis tempus in quam et condimentum.

Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed quis pulvinar nulla. Nullam tincidunt malesuada dui, id condimentum lorem venenatis eget. Mauris quis augue vitae lacus facilisis accumsan vitae ut massa. Nulla mattis elementum tincidunt. Duis a facilisis eros. Mauris vitae lorem porttitor ligula scelerisque cursus. In tempor porttitor blandit. Praesent tincidunt elit sit amet turpis hendrerit faucibus. In quis dictum magna. Vivamus volutpat est vel mi fermentum aliquet.

Nullam dignissim nibh sapien, non ultricies eros egestas ut. Praesent iaculis aliquet lectus, et suscipit velit elementum sed. Fusce tempus aliquet leo in molestie. Quisque viverra massa pulvinar turpis malesuada porta. Praesent rhoncus orci nulla, non vehicula urna aliquet at. Quisque consequat orci quis sollicitudin lobortis. Maecenas dapibus dignissim sem ut volutpat. Nunc eu lacus ipsum. Nulla sed nisi bibendum, tempor ligula id, molestie ligula. Vivamus arcu lacus, aliquet ut mattis vel, elementum aliquam quam. Aenean molestie efficitur sapien sed fringilla. Proin felis tortor, commodo ac egestas eget, aliquet sed velit. Mauris id nunc at risus mollis vulputate.

Integer vel auctor elit, id feugiat nibh. Nullam venenatis purus ut enim tristique cursus. Mauris quis eros velit. Donec rhoncus mi pharetra, mollis ipsum in, imperdiet mi. Vestibulum non varius augue. Vestibulum interdum sapien tortor, nec facilisis est blandit quis. Etiam ex felis, vestibulum eget scelerisque in, egestas non mi.

Vivamus quis fermentum purus, vel hendrerit risus. Duis id lobortis risus, nec elementum felis. Fusce aliquam urna viverra arcu elementum ornare. Integer nisi velit, hendrerit porttitor urna et, hendrerit convallis turpis. Curabitur nec risus a est aliquam accumsan. Curabitur velit nunc, posuere sit amet dolor ac, placerat sagittis erat. Maecenas tempus, velit fermentum pharetra feugiat, felis purus facilisis nulla, in varius nisl velit non metus.
""";
byte[] compressedText = await CompressionUtil.CompressTextAsync(text);
string decompressedText = await CompressionUtil.DecompressTextAsync(compressedText);

// DateTimeUtil

DateTimeOffset kr1 = new(2024, 12, 14, 5, 34, 48, TimeSpan.Zero);
DateTimeOffset kr2 = new(2024, 12, 14, 5, 34, 48, TimeSpan.FromHours(9));
DateTimeOffset utc1 = kr1.KstToUtc();
DateTimeOffset utc2 = kr2.KstToUtc(); // utc1과 utc2는 같은 값을 가집니다.
DateTimeOffset kr3 = utc2.UtcToKst(); // kr2와 kr3은 같은 값을 가집니다.
var months = DateTimeUtil.GetMonths(
    new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
    new DateTimeOffset(2024, 7, 1, 0, 0, 0, TimeSpan.Zero)
); // 6

// MathUtil

double m1 = MathUtil.Median([1, 2, 3]); // 2
double m2 = MathUtil.Median([1, 2]); // 1.5

double r = MathUtil.DegreeToRadian(180); // 3.141592653589793
double d = MathUtil.RadianToDegree(r); // 180

// PaginatedSet

int[] items = Enumerable.Range(1, 100).ToArray();
int pageSize = 10;
int totalItemCount = items.Length;
int totalPageCount = PaginatedSet<int>.CalcTotalPageCount(items.Length, pageSize);
PaginatedSet<int> set0 = PaginatedSet<int>.Create(items.Skip(0 * pageSize).Take(pageSize).ToArray(), totalItemCount, pageSize);
PaginatedSet<int> set1 = PaginatedSet<int>.Create(items.Skip(1 * pageSize).Take(pageSize).ToArray(), totalItemCount, pageSize, 1);
PaginatedSet<int> set2 = PaginatedSet<int>.Create(items.Skip(2 * pageSize).Take(pageSize).ToArray(), totalItemCount, pageSize, 2);
PaginatedSet<int> lastSet = PaginatedSet<int>.Create(items.Skip(set0.LastPageIndex * pageSize).Take(pageSize).ToArray(), totalItemCount, pageSize, set0.LastPageIndex);
PaginatedSet<int> emptySet = PaginatedSet<int>.CreateEmpty();

// PhoneNumberUtil

string phoneNumber = "010-1234-5678";
string p1 = PhoneNumberUtil.Korean.WithoutDash(phoneNumber); // 01012345678
string p2 = PhoneNumberUtil.Korean.WithDash(p1); // 010-1234-5678
string suffix = PhoneNumberUtil.Korean.Suffix(phoneNumber); // 5678

// RandomUtil

int randomNumber = RandomUtil.Next(1, 5); // 1 ~ 4 중 랜덤한 숫자
int[] numbers = Enumerable.Range(1, 10).ToArray();
RandomUtil.Shuffle(numbers); // numbers 배열을 섞습니다.

// SequentialNumberUtil

SequentialNumberUtil sequentialNumberUtil = new();
int seq1 = sequentialNumberUtil.Next(); // 1
int seq2 = sequentialNumberUtil.Next(); // 2
sequentialNumberUtil.Reset();
int seq3 = sequentialNumberUtil.Next(); // 1
int seq4 = sequentialNumberUtil.Next(); // 2
int seq5 = sequentialNumberUtil.Next(); // 3
sequentialNumberUtil.Reset(10);
int seq6 = sequentialNumberUtil.Next(); // 10

// UnitUtil

double py1 = UnitUtil.SquareMeter.ConvertToPyeong(84); // 25.40...
double sq1 = UnitUtil.Pyeong.ConvertToSquareMeter(py1); // 84.00...

Console.ReadLine();
