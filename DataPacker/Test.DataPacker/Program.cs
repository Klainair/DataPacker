using DataPacker;
using DataPacker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DataPacker
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new DtPack();

            data.Pack(new Catalog()
            {
                TxtFile = "ETOT TEKST TOJE ZAPAKOVAN",
                Picture = new byte[] { 0x01, 0x02, 0x03 }, //Передать массив байтов картинки
                SoftwareName = new string[] { "pervi", "vtoroi", "treti" } 
            }, "res.ovd");

            var res = data.Unpack("res.ovd");

            Console.WriteLine(res.TxtFile);

            foreach (var v in res.Picture)
                Console.WriteLine(v);

            foreach (var v in res.SoftwareName)
                Console.WriteLine(v);


            Console.WriteLine("Готово");

            Console.ReadLine();
        }
    }
}
