using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Infrastructure.Utils
{
    public class PositionableStreamReader : StreamReader
    {
        private long _position;

        public PositionableStreamReader(string fileName, Encoding enc)
            : base(fileName, enc)
        {
            if (IsPreamble())
            {
                _position = this.CurrentEncoding.GetPreamble().Length;
            }
        }

        public long Position
        {
            get
            {
                return _position;
            }

            set
            {
                ((Stream)BaseStream).Seek(value, SeekOrigin.Begin);
                this.DiscardBufferedData();
                _position = BaseStream.Position;
            }
        }

        public bool IsPreamble()
        {
            byte[] preamble = this.CurrentEncoding.GetPreamble();
            bool res = true;
            for (int i = 0; i < preamble.Length; i++)
            {
                int dd = BaseStream.ReadByte();
                if (preamble[i] != dd)
                {
                    res = false;
                    break;
                }
            }

            Position = 0;
            return res;
        }

        public override string ReadLine()
        {
            string line = base.ReadLine();
            if (line != null)
            {
                _position += CurrentEncoding.GetByteCount(line);
            }

            _position += CurrentEncoding.GetByteCount(Environment.NewLine);
            return line;
        }
    }
}
