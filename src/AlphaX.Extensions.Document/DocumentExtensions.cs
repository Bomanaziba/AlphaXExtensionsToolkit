using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AlphaX.Extensions.Dictionary;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace AlphaX.Extensions.Document
{
    public static class DocumentExtensions
    {

        /// <summary>
        /// Extracts data from excel.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <typeparam name="T"></typeparam>
        public static IList<T> ExtractDataFromExcel<T>(this Microsoft.AspNetCore.Http.IFormFile file)
        {
            var dataList = new List<T>();

            try
            {
                var propertyInfo = DictionaryExtensions.GetObjectProp<T>();
                var fileExtension = Path.GetExtension(file.FileName).ToLower();

                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    stream.Position = 0;

                    if (fileExtension == ".csv")
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            var headers = reader.ReadLine()?.Split(',');

                            while (!reader.EndOfStream)
                            {
                                var line = reader.ReadLine();
                                if (string.IsNullOrWhiteSpace(line)) continue;

                                var values = line.Split(',');
                                var dictionary = new Dictionary<string, object>();

                                for (int i = 0; i < headers?.Length; i++)
                                {
                                    if (i < values.Length)
                                    {
                                        var headerValue = Regex.Replace(headers[i], @"\s+", "");
                                        dictionary[headerValue] = values[i];
                                    }
                                }

                                T data = dictionary.DictionaryToObjectFormatter<T>();
                                dataList.Add(data);
                            }
                        }
                    }
                    else if (fileExtension == ".xls" || fileExtension == ".xlsx")
                    {
                        ISheet sheet;

                        if (fileExtension == ".xls")
                        {
                            var workbook = new HSSFWorkbook(stream);
                            sheet = workbook.GetSheetAt(0);
                        }
                        else
                        {
                            var workbook = new XSSFWorkbook(stream);
                            sheet = workbook.GetSheetAt(0);
                        }

                        IRow headerRow = sheet.GetRow(0);

                        for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                        {
                            var row = sheet.GetRow(i);
                            if (row == null || row.Cells.All(d => d.CellType == CellType.Blank)) continue;

                            var dictionary = new Dictionary<string, object>();

                            foreach (var prop in propertyInfo)
                            {
                                for (int j = 0; j < headerRow.LastCellNum; j++)
                                {
                                    var header = headerRow.GetCell(j);
                                    if (header == null || header.StringCellValue != prop.Name) continue;

                                    var cell = row.GetCell(j);
                                    var headerValue = Regex.Replace(prop.Name, @"\s+", "");
                                    dictionary[headerValue] = cell?.ToString() ?? string.Empty;
                                    break;
                                }
                            }

                            T data = dictionary.DictionaryToObjectFormatter<T>();
                            dataList.Add(data);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return dataList;
        }
    }
}

