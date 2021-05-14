using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sly_WAL_Editor
{
    public struct WAL
    {
        public uint unk;
        public uint entryCount;
        public List<TOCEntry> entries;
        public string walDir;
    }
}
