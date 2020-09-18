using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Vega.Core.Models
{
    public class PhotoSettings
    {
        public int MaxBytes { get; set; }

        public string[] AcceptedFileTypes { get; set; }


        public bool IsSupported(string fileName){
           return AcceptedFileTypes.Any(s =>  Path.GetExtension(fileName).ToLower() == s);
        }
    }
}