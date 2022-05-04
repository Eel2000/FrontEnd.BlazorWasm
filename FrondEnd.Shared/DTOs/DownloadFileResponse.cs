using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrondEnd.Shared.DTOs
{
#nullable disable
    public class DownloadFileResponse
    {
        public Info Info { get; set; }
        public object ReturnData { get; set; }
        public object PfService { get; set; }
        public ReturnFileBinary ReturnFileBinary { get; set; }
    }
    public class Info
    {
        public object Link { get; set; }
        public string Message { get; set; }
        public object Error { get; set; }
        public object MessageID { get; set; }
        public string Status { get; set; }
        public int Count { get; set; }
    }

    public class ReturnFileBinary
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string Base64Binary { get; set; }
    }
}
