// Copyright (c) Damien Guard.  All rights reserved.
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Originally published at http://damieng.com/blog/2007/11/19/calculating-crc-64-in-c-and-net

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Framework.Infrastructure.Utils
{
    /// <summary>
    /// Implements a 64-bit CRC hash algorithm for a given polynomial.
    /// </summary>
    /// <remarks>
    /// For ISO 3309 compliant 64-bit CRC's use Crc64Iso.
    /// </remarks>
    public class Crc64Util : HashAlgorithm
    {
        public const ulong DefaultSeed = 0x0;

        private readonly ulong[] table;

        private readonly ulong seed;
        private ulong hash;

        public Crc64Util(ulong polynomial)
            : this(polynomial, DefaultSeed)
        {
        }

        public Crc64Util(ulong polynomial, ulong seed)
        {
            if (!BitConverter.IsLittleEndian)
                throw new PlatformNotSupportedException("Not supported on Big Endian processors");

            table = InitializeTable(polynomial);
            this.seed = hash = seed;
        }

        public override int HashSize
        {
            get { return 64; }
        }

        public override void Initialize()
        {
            hash = seed;
        }

        protected static ulong CalculateHash(ulong seed, ulong[] table, IList<byte> buffer, int start, int size)
        {
            var hashVal = seed;
            for (var i = start; i < start + size; i++)
                unchecked
                {
                    hashVal = (hashVal >> 8) ^ table[(buffer[i] ^ hashVal) & 0xff];
                }

            return hashVal;
        }

        protected static ulong[] CreateTable(ulong polynomial)
        {
            var createTable = new ulong[256];
            for (var i = 0; i < 256; ++i)
            {
                var entry = (ulong)i;
                for (var j = 0; j < 8; ++j)
                    if ((entry & 1) == 1)
                        entry = (entry >> 1) ^ polynomial;
                    else
                        entry >>= 1;
                createTable[i] = entry;
            }

            return createTable;
        }

        protected override void HashCore(byte[] array, int ibStart, int cbSize)
        {
            hash = CalculateHash(hash, table, array, ibStart, cbSize);
        }

        protected override byte[] HashFinal()
        {
            var hashBuffer = UInt64ToBigEndianBytes(hash);
            HashValue = hashBuffer;
            return hashBuffer;
        }

        private static byte[] UInt64ToBigEndianBytes(ulong value)
        {
            var result = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(result);

            return result;
        }

        private static ulong[] InitializeTable(ulong polynomial)
        {
            if (polynomial == Crc64Iso.Iso3309Polynomial && Crc64Iso.Table != null)
                return Crc64Iso.Table;

            var createTable = CreateTable(polynomial);

            if (polynomial == Crc64Iso.Iso3309Polynomial)
                Crc64Iso.Table = createTable;

            return createTable;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Thirdparty Implementation")]
    public class Crc64Iso : Crc64Util
    {
        public const ulong Iso3309Polynomial = 0xD800000000000000;

#pragma warning disable S2223 // Non-constant static fields should not be visible
#pragma warning disable S1104 // Fields should not have public accessibility
        public static ulong[] Table;
#pragma warning restore S1104 // Fields should not have public accessibility
#pragma warning restore S2223 // Non-constant static fields should not be visible

        public Crc64Iso()
            : base(Iso3309Polynomial)
        {
        }

        public Crc64Iso(ulong seed)
            : base(Iso3309Polynomial, seed)
        {
        }

        public static ulong Compute(byte[] buffer)
        {
            return Compute(DefaultSeed, buffer);
        }

        public static ulong Compute(string text)
        {
            if (text == null)
                return Iso3309Polynomial;

            return Compute(DefaultSeed, Encoding.ASCII.GetBytes(text));
        }

        public static ulong Compute(ulong seed, byte[] buffer)
        {
            if (Table == null)
                Table = CreateTable(Iso3309Polynomial);

            return CalculateHash(seed, Table, buffer, 0, buffer.Length);
        }
    }
}