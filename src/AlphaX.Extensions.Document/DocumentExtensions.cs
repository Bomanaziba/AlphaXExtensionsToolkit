using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AlphaX.Extensions.Dictionary;
using AlphaX.Extensions.Generics;
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
            if (file == null || file.Length == 0)
                throw new ArgumentException("File cannot be null or empty.", nameof(file));

            var dataList = new List<T>();
            var propertyInfo = typeof(T)?.GetObjectProp();
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
                    ISheet sheet = fileExtension == ".xls"
                        ? new HSSFWorkbook(stream).GetSheetAt(0)
                        : new XSSFWorkbook(stream).GetSheetAt(0);

                    var headerRow = sheet.GetRow(sheet.FirstRowNum);
                    var headerMap = new Dictionary<int, string>();

                    for (int j = 0; j < headerRow.LastCellNum; j++)
                    {
                        var cell = headerRow.GetCell(j);
                        if (cell != null)
                        {
                            var headerName = Regex.Replace(cell.StringCellValue, @"\s+", "");
                            headerMap[j] = headerName;
                        }
                    }

                    for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                    {
                        var row = sheet.GetRow(i);
                        if (row == null || row.Cells.All(d => d.CellType == CellType.Blank)) continue;

                        var dictionary = new Dictionary<string, object>();

                        foreach (var kvp in headerMap)
                        {
                            var cell = row.GetCell(kvp.Key);
                            var value = cell != null ? GetCellValue(cell) : null;
                            dictionary[kvp.Value] = value;
                        }

                        T data = dictionary.DictionaryToObjectFormatter<T>();
                        dataList.Add(data);
                    }
                }
            }
            return dataList;
        }

        private static object GetCellValue(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Numeric:
                    return DateUtil.IsCellDateFormatted(cell) ? cell.DateCellValue : (object)cell.NumericCellValue;
                case CellType.Boolean:
                    return cell.BooleanCellValue;
                case CellType.Formula:
                    // For formula cells, we can return the evaluated value
                    return cell.CellFormula; // or use cell.NumericCellValue for numeric results
                default:
                    return null;
            }
        }

    }
}

