using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AlphaX.Extensions.Document;
using AlphaX.Extensions.Generics.Tests.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using NPOI.XSSF.UserModel;
using Xunit;

namespace AlphaX.Extensions.Document.Tests
{
    public class DocumentExtensionsTest
    {


        [Fact]
        public void ExtractDataFromExcel_ShouldThrowArgumentException_WhenFileIsNull()
        {
            IFormFile file = null;
            Assert.Throws<ArgumentException>(() => DocumentExtensions.ExtractDataFromExcel<SampleModel0>(file));
        }

        [Fact]
        public void ExtractDataFromExcel_ShouldThrowArgumentException_WhenFileIsEmpty()
        {
            var file = SampleModel0.CreateCsvFormFile("", "empty.csv");
            Assert.Throws<ArgumentException>(() => DocumentExtensions.ExtractDataFromExcel<SampleModel0>(file));
        }

        [Fact]
        public void ExtractDataFromExcel_ShouldExtractData_FromCsvFile()
        {
            var csvContent = "Name,Age\nJohn,30\nJane,25";
            var file = SampleModel0.CreateCsvFormFile(csvContent, "data.csv");

            var result = DocumentExtensions.ExtractDataFromExcel<SampleModel0>(file);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("John", result[0].Name);
            Assert.Equal("30", result[0].Age);
            Assert.Equal("Jane", result[1].Name);
            Assert.Equal("25", result[1].Age);
        }

        // Note: For .xls/.xlsx tests, you would need to generate a valid Excel file stream.
        // This is a simplified placeholder test.
        [Fact]
        public void ExtractDataFromExcel_ShouldExtractData_FromXlsxFile()
        {
            // Arrange: Create a simple .xlsx file in memory using NPOI
            var workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet();
            var header = sheet.CreateRow(0);
            header.CreateCell(0).SetCellValue("Name");
            header.CreateCell(1).SetCellValue("Age");

            var row1 = sheet.CreateRow(1);
            row1.CreateCell(0).SetCellValue("Alice");
            row1.CreateCell(1).SetCellValue("22");

            var row2 = sheet.CreateRow(2);
            row2.CreateCell(0).SetCellValue("Bob");
            row2.CreateCell(1).SetCellValue("28");

            // Use a temp stream for writing
            byte[] excelBytes;
            using (var tempStream = new MemoryStream())
            {
                workbook.Write(tempStream);
                excelBytes = tempStream.ToArray(); // copy into byte array
            }

            // Use a new stream from bytes
            var stream = new MemoryStream(excelBytes);
            var file = new FormFile(stream, 0, stream.Length, "file", "data.xlsx");

            // Act
            var result = DocumentExtensions.ExtractDataFromExcel<SampleModel0>(file);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Alice", result[0].Name);
            Assert.Equal("22", result[0].Age);
            Assert.Equal("Bob", result[1].Name);
            Assert.Equal("28", result[1].Age);
        }

    }
}