using DataPacker.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataPacker
{
    public class DtPack
    {

        public Catalog DCatalog { get; set; }

        public DtPack()
        {
        }

        public Catalog Unpack(string filePath)
        {
            Catalog _cat = new Catalog();

            using (FileStream st = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                st.Position = 8;
                byte[] lengthText = new byte[2];
                st.Read(lengthText, 0, lengthText.Length);
                ushort textLength = BitConverter.ToUInt16(lengthText, 0);

                byte[] text_byte = new byte[textLength];
                st.Read(text_byte, 0, textLength);
                string text = Encoding.Default.GetString(text_byte);

                ///////////(((((
                _cat.TxtFile = text;
                ////////////////////


                byte[] lengthPicture = new byte[4];
                st.Read(lengthPicture, 0, lengthPicture.Length);
                uint pictureLength = BitConverter.ToUInt32(lengthPicture, 0);

                byte[] picture_byte = new byte[pictureLength];
                st.Read(picture_byte, 0, (int)pictureLength);

                ////////////////////////////
                _cat.Picture = picture_byte;
                ////////////////////////////


                byte[] countSoft = new byte[1];
                st.Read(countSoft, 0, countSoft.Length);

                List<string> soft = new List<string>();
                for(int i = 0; i < countSoft[0]; i++)
                {
                    byte[] lengthSoft = new byte[1];
                    st.Read(lengthSoft, 0, lengthSoft.Length);
                    int softLength = lengthSoft[0];

                    byte[] soft_byte = new byte[softLength];
                    st.Read(soft_byte, 0, softLength);

                    soft.Add(Encoding.Default.GetString(soft_byte));
                }

                ///////////////////////////////////
                _cat.SoftwareName = soft.ToArray();
                ///////////////////////////////////

                return _cat;
            }
        }

        

        public void Pack(Catalog catalog, string filePath)
        {
            MemoryStream stream = new MemoryStream();
            byte[] startAndEnd = Encoding.Unicode.GetBytes(".ovd0000");

            stream.Write(startAndEnd, 0, startAndEnd.Length);


            /* Запись текста в массив */
            byte[] textData = DataWrite(Encoding.Unicode.GetBytes(catalog.TxtFile), 2);
            stream.Write(textData, 0, textData.Length);

            /* Запись картинки в массив */
            byte[] imageData = DataWrite(catalog.Picture, 4);
            stream.Write(imageData, 0, imageData.Length);

            /* Запись программ в массив */
            byte[] softData = SoftDataWrite(catalog.SoftwareName);
            stream.Write(softData, 0, softData.Length);


            stream.Write(startAndEnd, 0, startAndEnd.Length);

            using (FileStream output = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                output.Write(stream.ToArray(), 0, (int)stream.Length);
            }
        }

        byte[] SoftDataWrite(string[] data)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(BitConverter.GetBytes(data.Length), 0, 1);
            foreach (string str in data)
            {
                byte by = (byte)str.Length;
                ms.Write(new byte[] { by }, 0, 1);

                byte[] dataString = Encoding.Default.GetBytes(str.ToCharArray());
                ms.Write(dataString, 0, dataString.Length);
            }

            return ms.ToArray();
        }

        byte[] DataWrite(byte[] data, int lgth)
        {
            MemoryStream ms = new MemoryStream();
            byte[] by = new byte[2];
            if (lgth == 2)
            {
                by = new byte[2];
                by = BitConverter.GetBytes((ushort)data.Length);
            }
            else if (lgth == 4)
            {
                by = new byte[4];
                by = BitConverter.GetBytes((int)data.Length);
            }


            ms.Write(by, 0, by.Length);

            ms.Write(data, 0, data.Length);

            return ms.ToArray();
        }
    }
}
