using System;

namespace JAM.Netduino3.Helpers
{
  public static class Utility
  {
    private static char[] _base64Chars = 
      { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/', '=' };

    private static char[] _emptyCharArray = new char[0];
    private static byte[] _emptyByteArray = new byte[0];

    /// <summary>
    /// Convert a byte array to a Base64 encoded string
    /// </summary>
    /// <param name="bytes">Bytes to convert</param>
    /// <param name="insertLineBreaks">True if line breaks should be inserted every 76 characters.</param>
    /// <returns>String of Base64 encoded bytes</returns>
    public static string ToBase64String(byte[] bytes, bool insertLineBreaks = false)
    {
      char[] chars = ToBase64CharArray(bytes, 0, bytes.Length, insertLineBreaks);
      if (chars == _emptyCharArray) return string.Empty;
      return new string(chars);
    }

    /// <summary>
    /// Converts a byte array to a Base64 encoded array of chars
    /// </summary>
    /// <param name="bytes">Bytes to convert</param>
    /// <param name="insertLineBreaks">True if line breaks should be inserted every 76 characters.</param>
    /// <returns>Char array of Base64 encoded bytes</returns>
    public static char[] ToBase64CharArray(byte[] bytes, bool insertLineBreaks = false)
    {
      return ToBase64CharArray(bytes, 0, bytes.Length, insertLineBreaks);
    }

    /// <summary>
    /// Converts a byte array to a Base64 encoded array of chars
    /// </summary>
    /// <param name="bytes">Bytes to convert</param>
    /// <param name="offset">Offset into the array of bytes</param>
    /// <param name="length">Number of bytes to encode</param>
    /// <param name="insertLineBreaks">True if line breaks should be inserted every 76 characters.</param>
    /// <returns>Char array of Base64 encoded bytes</returns>
    public static char[] ToBase64CharArray(byte[] bytes, int offset, int length, bool insertLineBreaks)
    {
      if (length == 0) return _emptyCharArray;

      if (offset < 0 ) throw new ArgumentOutOfRangeException("offset", "Must be greater than or equal to 0.");
      if (length < 0) throw new ArgumentOutOfRangeException("length", "Must be greater than or equal to 0.");      
      if (offset + length > bytes.Length) throw new ArgumentOutOfRangeException("length", "offset + length is beyound the upper bound of the array.");
      
      int padding = length % 3;            
      int lastCompleteByte = offset + length - padding;

      int encodedLength = (length / 3) * 4;
      if (padding != 0)
      {
        encodedLength += 4;
      }

      if (insertLineBreaks)
      {
        int lines = encodedLength / 76;
        if (lines > 0 && lines % 76 == 0)
        {
          lines--;
        }
        encodedLength += lines * 2;
      }

      char[] base64 = new char[encodedLength];
      int index = 0;
      int byteOffset = 0;
      int charCount = 0;
      for (int i = offset; i < lastCompleteByte; i += 3, index++, byteOffset += 4, charCount += 4)
      {
        if (insertLineBreaks && charCount == 76)
        {
          base64[i + index] = '\r';
          base64[i + index + 1] = '\n';
          charCount = 0;
          index += 2;          
          byteOffset += 2;
        }

        base64[i + index] = _base64Chars[bytes[i] >> 2];
        base64[i + index + 1] = _base64Chars[((bytes[i] & 0x03) << 4) | (bytes[i + 1] >> 4)];
        base64[i + index + 2] = _base64Chars[((bytes[i + 1] & 0x0f) << 2) | (bytes[i + 2] >> 6)];
        base64[i + index + 3] = _base64Chars[bytes[i + 2] & 0x3f];        
      }

      if (insertLineBreaks && padding != 0 && charCount == 76)
      {
        base64[byteOffset] = '\r';
        base64[byteOffset + 1] = '\n';
        byteOffset += 2;
      }

      switch (padding)
      {
        case 1:
          base64[byteOffset] = _base64Chars[bytes[lastCompleteByte] >> 2];
          base64[byteOffset + 1] = _base64Chars[(bytes[lastCompleteByte] & 0x03) << 4];
          base64[byteOffset + 2] = _base64Chars[64];
          base64[byteOffset + 3] = _base64Chars[64];          
          break;

        case 2:
          base64[byteOffset] = _base64Chars[bytes[lastCompleteByte] >> 2];
          base64[byteOffset + 1] = _base64Chars[((bytes[lastCompleteByte] & 0x03) << 4) | (bytes[lastCompleteByte + 1] >> 4)];
          base64[byteOffset + 2] = _base64Chars[(bytes[lastCompleteByte + 1] & 0x0f) << 2];
          base64[byteOffset + 3] = _base64Chars[64];          
          break;
      }

      return base64;
    }

    /// <summary>
    /// Convert a string of Base64 encoded bytes to a byte array
    /// </summary>
    /// <param name="base64String">Base64 encoded data as a string</param>
    /// <returns>Decoded byte array</returns>
    public static byte[] FromBase64String(string base64String)
    {
      return FromBase64CharArray(base64String.ToCharArray());
    }

    /// <summary>
    /// Convert a char array of Base64 encoded bytes to a byte array
    /// </summary>
    /// <param name="base64CharArray">Base64 encoded data as an array of chars</param>
    /// <returns>Decoded byte array</returns>
    public static byte[] FromBase64CharArray(char[] base64CharArray)
    {
      return FromBase64CharArray(base64CharArray, 0, base64CharArray.Length);
    }

    /// <summary>
    /// Convert a char array of Base64 encoded bytes to a byte array
    /// </summary>
    /// <param name="base64CharArray">Base64 encoded data as an array of chars</param>
    /// <param name="offset">Offset into the encoded array</param>
    /// <param name="length">Number of characters to decode</param>
    /// <returns>Decoded byte array</returns>
    public static byte[] FromBase64CharArray(char[] base64CharArray, int offset, int length)
    {
      if (length == 0) return _emptyByteArray;

      if (offset < 0) throw new ArgumentOutOfRangeException("offset", "Must be greater than or equal to 0.");
      if (length < 0) throw new ArgumentOutOfRangeException("length", "Must be greater than or equal to 0.");
      if (offset + length > base64CharArray.Length) throw new ArgumentOutOfRangeException("length", "offset + length is beyound the upper bound of the array.");

      int finalBytes = base64CharArray[offset + length - 2] == _base64Chars[64] ? 1 : base64CharArray[offset + length - 1] == _base64Chars[64] ? 2 : 3;

      int lines = 0;
      if (length >= 78)
      {
        for (int i = 76; i < length - 1; i += 78)
        {
          if (base64CharArray[i] == '\r' && base64CharArray[i + 1] == '\n')
          {
            lines++;
          }
          else
          {
            break;
          }
        }
      }

      int blocks = (length - (lines * 2)) / 4;
      
      int byteLength = blocks * 3;
      byte[] bytes = new byte[(byteLength - 3) + finalBytes];

      int b64Index;
      int binIndex;
      int b1, b2, b3, b4;

      int lineCheck = 0;
      int blockOffset = 0;
      for (int i = 0; i < blocks - 1; i++, lineCheck++)
      {
        b64Index = blockOffset + (i << 2);

        if (lineCheck == 19)
        {
          if (base64CharArray[b64Index] == '\r' && base64CharArray[b64Index + 1] == '\n')
          {
            b64Index += 2;
            blockOffset += 2;
            lineCheck = 0;            
          }
        }        

        b1 = Base64Lookup(base64CharArray[b64Index]);
        b2 = Base64Lookup(base64CharArray[b64Index + 1]);
        b3 = Base64Lookup(base64CharArray[b64Index + 2]);
        b4 = Base64Lookup(base64CharArray[b64Index + 3]);

        binIndex = (i << 1) + i;
        bytes[binIndex] = (byte)((b1 << 2) | ((b2 & 0x30) >> 4));
        bytes[binIndex + 1] = (byte)(((b2 & 0x0f) << 4) | ((b3 & 0x3c) >> 2));
        bytes[binIndex + 2] = (byte)(((b3 & 0x03) << 6) | b4);        
      }

      b64Index = blockOffset + ((blocks - 1) << 2);

      if (base64CharArray[b64Index] == '\r' && base64CharArray[b64Index + 1] == '\n')
      {
        b64Index += 2;      
      }

      b1 = Base64Lookup(base64CharArray[b64Index]);
      b2 = Base64Lookup(base64CharArray[b64Index + 1]);

      binIndex = ((blocks - 1) << 1) + (blocks - 1);
      switch (finalBytes)
      {
        case 1:
          bytes[binIndex] = (byte)((b1 << 2) | ((b2 & 0x30) >> 4));              
          break;

        case 2:
          b3 = Base64Lookup(base64CharArray[b64Index + 2]);
          bytes[binIndex] = (byte)((b1 << 2) | ((b2 & 0x30) >> 4));
          bytes[binIndex + 1] = (byte)(((b2 & 0x0f) << 4) | ((b3 & 0x3c) >> 2));

          break;

        case 3:
          b3 = Base64Lookup(base64CharArray[b64Index + 2]);
          b4 = Base64Lookup(base64CharArray[b64Index + 3]);
          bytes[binIndex] = (byte)((b1 << 2) | ((b2 & 0x30) >> 4));
          bytes[binIndex + 1] = (byte)(((b2 & 0x0f) << 4) | ((b3 & 0x3c) >> 2));
          bytes[binIndex + 2] = (byte)(((b3 & 0x03) << 6) | b4);
          break;
      }

      return bytes;
    }

    private static int Base64Lookup(char c)
    {
      if (c >= 'A' && c <= 'Z') return c - 'A';
      if (c >= 'a' && c <= 'z') return 26 + c - 'a';
      if (c >= '0' && c <= '9') return 52 + c - '0';
      if (c == '+') return 62;
      if (c == '/') return 63;
      throw new ArgumentException("c", "Invalid Base64 character");
    }
  }
}
