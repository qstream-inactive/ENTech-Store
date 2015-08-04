using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTech.Store.Services.SharedModule.Dtos
{
    public class FileBaseDto
    {
        public string Type;

        public FileBaseDto(string type)
        {
            this.Type = type;
        }
    }

    public class UploadReferenceDto : FileBaseDto
    {
        public int Id;
        public int UploadedBytes;
        public int TotalBytes;

        public UploadReferenceDto(int id, int uploadedBytes, int totalBytes)
            : base("Upload")
        {
            this.Id = id;
            this.UploadedBytes = uploadedBytes;
            this.TotalBytes = totalBytes;
        }
    }

    public class UrlDto : FileBaseDto
    {
        public string Url;

        public UrlDto(string url)
            : base("Url")
        {
            this.Url = url;
        }
    }

}
