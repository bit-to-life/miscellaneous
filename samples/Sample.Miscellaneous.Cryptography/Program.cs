using BitToLife.Miscellaneous.Cryptography;
using System.Security.Cryptography;

byte[] source = RandomNumberGenerator.GetBytes(5120);
string text = """
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus gravida lectus enim, eu sagittis libero lacinia nec. Sed nec viverra magna. Sed eu est ex. Curabitur quam orci, bibendum non odio sit amet, vehicula congue nibh. Nam rhoncus arcu eu velit placerat, et bibendum massa tristique. Pellentesque nisl nisi, laoreet non velit a, eleifend commodo lectus. Nullam elementum, arcu id commodo aliquet, ante arcu egestas erat, nec cursus justo risus eu mi. Vestibulum ullamcorper, erat in pulvinar ultrices, lacus elit rhoncus ipsum, a aliquam ex nisl eu risus. Nulla quis ultrices sem, vitae dapibus turpis. Curabitur faucibus tincidunt enim ac sollicitudin. Aliquam ornare ante elit, vel commodo dolor maximus a. Duis tempus in quam et condimentum.

Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed quis pulvinar nulla. Nullam tincidunt malesuada dui, id condimentum lorem venenatis eget. Mauris quis augue vitae lacus facilisis accumsan vitae ut massa. Nulla mattis elementum tincidunt. Duis a facilisis eros. Mauris vitae lorem porttitor ligula scelerisque cursus. In tempor porttitor blandit. Praesent tincidunt elit sit amet turpis hendrerit faucibus. In quis dictum magna. Vivamus volutpat est vel mi fermentum aliquet.

Nullam dignissim nibh sapien, non ultricies eros egestas ut. Praesent iaculis aliquet lectus, et suscipit velit elementum sed. Fusce tempus aliquet leo in molestie. Quisque viverra massa pulvinar turpis malesuada porta. Praesent rhoncus orci nulla, non vehicula urna aliquet at. Quisque consequat orci quis sollicitudin lobortis. Maecenas dapibus dignissim sem ut volutpat. Nunc eu lacus ipsum. Nulla sed nisi bibendum, tempor ligula id, molestie ligula. Vivamus arcu lacus, aliquet ut mattis vel, elementum aliquam quam. Aenean molestie efficitur sapien sed fringilla. Proin felis tortor, commodo ac egestas eget, aliquet sed velit. Mauris id nunc at risus mollis vulputate.

Integer vel auctor elit, id feugiat nibh. Nullam venenatis purus ut enim tristique cursus. Mauris quis eros velit. Donec rhoncus mi pharetra, mollis ipsum in, imperdiet mi. Vestibulum non varius augue. Vestibulum interdum sapien tortor, nec facilisis est blandit quis. Etiam ex felis, vestibulum eget scelerisque in, egestas non mi.

Vivamus quis fermentum purus, vel hendrerit risus. Duis id lobortis risus, nec elementum felis. Fusce aliquam urna viverra arcu elementum ornare. Integer nisi velit, hendrerit porttitor urna et, hendrerit convallis turpis. Curabitur nec risus a est aliquam accumsan. Curabitur velit nunc, posuere sit amet dolor ac, placerat sagittis erat. Maecenas tempus, velit fermentum pharetra feugiat, felis purus facilisis nulla, in varius nisl velit non metus.
""";

// HashUtil

using (MemoryStream stream = new(source))
{
    byte[] hashedStream = await HashUtil.ComputeHashAsync(stream, HashAlgorithmType.SHA256);
    string hashedStreamString = HashUtil.ToHexString(hashedStream);
}

byte[] hashedSource = HashUtil.ComputeHash(source, HashAlgorithmType.SHA384);
string hashedSourceString = HashUtil.ToHexString(hashedSource);

byte[] hash = HashUtil.ComputeHash(text, HashAlgorithmType.SHA512);
string hashedText = HashUtil.ToHexString(hash);

// AesUtil

byte[] secretKey = RandomNumberGenerator.GetBytes(32);

byte[] encryptedSource = await AESUtil.EncryptAsync(source, secretKey);
byte[] decryptedSource = await AESUtil.DescryptAsync(encryptedSource, secretKey);

byte[] encryptedText = await AESUtil.EncryptTextAsync(text, secretKey);
string decryptedText = await AESUtil.DescryptTextAsync(encryptedText, secretKey);

// RSAUtil

RSAKeyPair rsaKeyPair = RSAUtil.GenerateKeyPair();

using RSAEncryptor rsaEncryptor = RSAUtil.CreateEncryptor(rsaKeyPair.PublicKey);
using RSADescryptor rsaDescryptor = RSAUtil.CreateDescryptor(rsaKeyPair.PrivateKey);

byte[] data = RandomNumberGenerator.GetBytes(245); // 256 바이트(키 사이즈 2048비트) - 11 바이트 (PKCS1 패딩) = 245 바이트
byte[] enc1 = rsaEncryptor.Encrypt(data);
byte[] dec1 = rsaDescryptor.Decrypt(enc1);

Console.ReadLine();
