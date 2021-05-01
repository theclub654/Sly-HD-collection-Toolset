using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sly_WAL_Editor
{
    public struct WACFileEntry
    {
        public string filename;
        public uint size;
        public char type;
        public uint offset;
        public byte[] fileData;
    }
}
