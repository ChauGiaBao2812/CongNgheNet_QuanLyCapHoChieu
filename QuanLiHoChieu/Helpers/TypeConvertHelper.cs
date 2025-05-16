namespace QuanLiHoChieu.Helpers
{
    public class TypeConvertHelper
    {
        public static byte[] HexStringToByteArray(string hex)
        {
            if (string.IsNullOrEmpty(hex))
                return Array.Empty<byte>();

            if (hex.Length % 2 != 0)
                throw new ArgumentException("Invalid length of the hex string.");

            var bytes = new byte[hex.Length / 2];

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }

            return bytes;
        }
    }
}
