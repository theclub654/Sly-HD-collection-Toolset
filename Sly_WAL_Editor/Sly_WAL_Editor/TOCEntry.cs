using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sly_WAL_Editor
{
    public struct TOCEntry
    {
        public string folderName;
        public string fileNames;
        public List<TOCFileEntry> files;
        public byte[] tocTable;
    }
}
