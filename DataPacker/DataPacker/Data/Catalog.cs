using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataPacker.Data
{
    public class Catalog
    {
        public string TxtFile { get; set; }


        public byte[] Picture { get; set; }


        public string[] SoftwareName { get; set; }


        public Catalog()
        { }


        public Catalog(string txtFile, byte[] picture, string[] software)
        {
            this.TxtFile = txtFile;
            this.Picture = picture;
            this.SoftwareName = software;
        }
    }
}
