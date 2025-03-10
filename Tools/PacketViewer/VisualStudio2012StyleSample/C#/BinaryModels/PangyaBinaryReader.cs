using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketViewer.BinaryModels
{
    public class PangyaBinaryReader : BinaryReader
    {
        #region Construtores

        public PangyaBinaryReader(Stream baseStream)
             : base(baseStream) { }

        #endregion

        #region Methods

        public byte[] GetBytes()
        {
            using (var memoryStream = new MemoryStream())
            {
                memoryStream.GetBuffer();
                BaseStream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Pula bytes a partir da posição no fluxo atual
        /// </summary>
        /// <param name="jump"></param>
        public void Skip(int jump)
        {
            BaseStream.Seek(jump, SeekOrigin.Current);
        }

        #endregion

        #region Leituras de String 

        public string ReadPStr(int count)
        {
            var data = ReadBytes(count);

            var result = BaseStream.Read(data, 0, count);

            ASCIIEncoding enc = new ASCIIEncoding();
            return enc.GetString(data);
        }

        public string ReadPStr()
        {
            return ReadPStr(new ASCIIEncoding());
        }
        public string ReadPStr(Encoding encoding)
        {
            int size = ReadInt16();

            if (Int16.MaxValue < size)
                return String.Empty;

            var result = ReadBytes(size);

            return encoding.GetString(result);
        }


        //Lê sem encoding
        //public string ReadPStr()
        //{
        //    int size = ReadInt16();

        //    if (Int16.MaxValue < size)
        //        return String.Empty;

        //    var result = ReadBytes(size);

        //    return GetString(result);
        //}

        //public static string GetString(byte[] bytes)
        //{
        //    if (bytes.Length % 2 != 0)
        //    {
        //        byte[] newArray = new byte[bytes.Length + 1];
        //        bytes.CopyTo(newArray, 0);
        //        newArray[newArray.Length-1] = byte.Parse("0");
        //        bytes = newArray;
        //    }

        //    char[] chars = new char[bytes.Length / sizeof(char)];
        //    System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
        //    return new string(chars);
        //}

        //public static byte[] GetBytes(string str)
        //{
        //    byte[] bytes = new byte[str.Length * sizeof(char)];
        //    System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
        //    return bytes;
        //}

        #endregion

        #region Leituras de Bytes

        //Lê PangyaString e retorna em um array de Bytes
        public byte[] ReadPStrBytes()
        {
            int size = ReadInt16();

            if (Int16.MaxValue < size)
                return null;

            return ReadBytes(size);
        }

        #endregion

        #region Leituras Numéricas

        public IEnumerable<uint> Read(uint[] model)
        {
            for (int i = 0; i < model.Length; i++)
            {
                yield return ReadUInt32();
            }
        }
        public void Read(object obj)
        {
            foreach (var property in obj.GetType().GetProperties())
            {

                Type type = property.PropertyType;

                TypeCode typeCode = Type.GetTypeCode(type);

                switch (typeCode)
                {

                    case TypeCode.Empty:
                        break;
                    case TypeCode.Object:
                        break;
                    case TypeCode.DBNull:
                        break;
                    case TypeCode.Boolean:
                        {
                            property.SetValue(obj, ReadBoolean());
                        }
                        break;
                    case TypeCode.Char:
                        {
                            property.SetValue(obj, ReadChar());
                        }
                        break;
                    case TypeCode.SByte:
                        {
                            property.SetValue(obj, ReadSByte());
                        }
                        break;

                    case TypeCode.Byte:
                        {
                            property.SetValue(obj, ReadByte());
                        }
                        break;
                    case TypeCode.Int16:
                        {
                            property.SetValue(obj, ReadInt16());
                        }
                        break;
                    case TypeCode.UInt16:
                        {
                            property.SetValue(obj, ReadUInt16());
                        }
                        break;
                    case TypeCode.Int32:
                        {
                            property.SetValue(obj, ReadInt32());
                        }
                        break;
                    case TypeCode.UInt32:
                        property.SetValue(obj, ReadUInt32());
                        break;
                    case TypeCode.Int64:
                        {
                            property.SetValue(obj, ReadInt64());
                        }
                        break;
                    case TypeCode.UInt64:
                        {
                            property.SetValue(obj, ReadUInt64());
                        }
                        break;
                    case TypeCode.Single:
                        {
                            property.SetValue(obj, ReadSingle());
                        }
                        break;
                    case TypeCode.Double:
                        {
                            property.SetValue(obj, ReadDouble());
                        }
                        break;
                    case TypeCode.Decimal:
                        {
                            property.SetValue(obj, ReadDecimal());
                        }
                        break;
                    case TypeCode.DateTime:
                        {
                            //property.SetValue(obj, ReadDateTime());
                        }
                        break;
                    case TypeCode.String:
                        {
                            property.SetValue(obj, ReadPStr());
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("Code_UN: " + typeCode);
                        }
                        break;
                }
            }

        }
        #endregion

    }

}
