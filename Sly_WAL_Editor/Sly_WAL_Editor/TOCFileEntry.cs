using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sly_WAL_Editor
{
    public struct TOCFileEntry
    {
        public uint offset;
        public uint size;
        public byte[] fileData;
    }
}
