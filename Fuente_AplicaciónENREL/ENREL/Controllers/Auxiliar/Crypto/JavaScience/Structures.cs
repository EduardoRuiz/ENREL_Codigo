using System;
using System.Runtime.InteropServices;

namespace VER.Crypto.JavaScience
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CRYPT_KEY_PROV_INFO
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public String pwszContainerName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public String pwszProvName;
        public uint dwProvType;
        public uint dwFlags;
        public uint cProvParam;
        public IntPtr rgProvParam;
        public uint dwKeySpec;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CERT_NAME_BLOB
    {
        public int cbData;
        public IntPtr pbData;
    }
}
