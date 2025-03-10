using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI.BinaryModels
{
    public class PangyaBinaryReader : BinaryReader
    {
        public PangyaBinaryReader(Stream baseStream)
             : base(baseStream) { }

        /// <summary>
        /// Lê string no formato Pangya sendo os 2 primeiros bytes o tamanho o texto a ser lido e avança a posição atual pelo número de bytes
        /// </summary>
        /// <returns></returns>
        public string ReadPStr()
        {
            int size = ReadInt16();

            if (Int16.MaxValue < size)
                return String.Empty;

            var result = ReadBytes(size);

            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return (enc.GetString(result));
        }

        public string ReadPStr2()
        {
            int size = ReadByte();

            if (Int16.MaxValue < size)
                return String.Empty;

            var result = ReadBytes(size);

            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return (enc.GetString(result));
        }

        public void Skip(int jump)
        {
            BaseStream.Seek(jump, SeekOrigin.Current);
        }

        public IEnumerable<ushort> Read(ushort[] model)
        {
            for (int i = 0; i < model.Length; i++)
            {
                yield return ReadUInt16();
            }
        }

        public IEnumerable<uint> Read(uint[] model)
        {
            for (int i = 0; i < model.Length; i++)
            {
                yield return ReadUInt32();
            }
        }

        public void Read(Object obj)
        {
            foreach (var property in obj.GetType().GetProperties())
            {
                Type type = property.PropertyType;

                System.TypeCode typeCode = System.Type.GetTypeCode(type);

                switch (typeCode)
                {
                    case TypeCode.Empty:
                        break;
                    case TypeCode.Object:
                        break;
                    case TypeCode.DBNull:
                        break;
                    case TypeCode.Boolean:
                        break;
                    case TypeCode.Char:
                        break;
                    case TypeCode.SByte:
                        break;
                    case TypeCode.Byte:
                        property.SetValue(obj, ReadByte());
                        break;
                    case TypeCode.Int16:
                        break;
                    case TypeCode.UInt16:
                        property.SetValue(obj, ReadUInt16());
                        break;
                    case TypeCode.Int32:
                        break;
                    case TypeCode.UInt32:
                        property.SetValue(obj, ReadUInt32());
                        break;
                    case TypeCode.Int64:
                        break;
                    case TypeCode.UInt64:
                        property.SetValue(obj, ReadUInt64());
                        break;
                    case TypeCode.Single:
                        break;
                    case TypeCode.Double:
                        break;
                    case TypeCode.Decimal:
                        break;
                    case TypeCode.DateTime:
                        break;
                    case TypeCode.String:
                        break;
                    default:
                        break;
                }

            }
        }


        public bool HasValueToRead()
        {
            return (BaseStream.Position < BaseStream.Length && (BaseStream.Position + 1 < BaseStream.Length));
        }

    }
}
