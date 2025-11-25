using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PruebaFinal.Services
{
    public class ExportService : IExportService
    {
        public byte[] ExportToExcel<T>(List<T> data, string sheetName)
        {
            try
            {
                Console.WriteLine($"[ExportToExcel] Iniciando con {data?.Count ?? 0} registros");

                if (data == null || data.Count == 0)
                {
                    Console.WriteLine("[ExportToExcel] ❌ Sin datos");
                    return new byte[0];
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(sheetName ?? "Datos");
                    var properties = typeof(T).GetProperties();

                    Console.WriteLine($"[ExportToExcel] Propiedades encontradas: {properties.Length}");

                    // Agregar encabezados
                    for (int i = 0; i < properties.Length; i++)
                    {
                        worksheet.Cell(1, i + 1).Value = properties[i].Name;
                        worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                        worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.LightGray;
                    }

                    // Agregar datos
                    for (int row = 0; row < data.Count; row++)
                    {
                        for (int col = 0; col < properties.Length; col++)
                        {
                            var value = properties[col].GetValue(data[row]);
                            worksheet.Cell(row + 2, col + 1).Value = value?.ToString() ?? "";
                        }
                    }

                    // Ajustar columnas automáticamente
                    worksheet.Columns().AdjustToContents();

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var result = stream.ToArray();
                        Console.WriteLine($"[ExportToExcel] ✅ Archivo generado: {result.Length} bytes");
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ExportToExcel] ❌ Error: {ex.Message}");
                Console.WriteLine($"[ExportToExcel] Stack: {ex.StackTrace}");
                return new byte[0];
            }
        }

        public byte[] ExportToCsv<T>(List<T> data)
        {
            try
            {
                Console.WriteLine($"[ExportToCsv] Iniciando con {data?.Count ?? 0} registros");

                if (data == null || data.Count == 0)
                {
                    Console.WriteLine("[ExportToCsv] ❌ Sin datos");
                    return new byte[0];
                }

                var sb = new StringBuilder();
                var properties = typeof(T).GetProperties();

                Console.WriteLine($"[ExportToCsv] Propiedades encontradas: {properties.Length}");

                // Encabezados
                sb.AppendLine(string.Join(",", properties.Select(p => $"\"{p.Name}\"")));

                // Datos
                foreach (var item in data)
                {
                    var values = properties.Select(p =>
                    {
                        var value = p.GetValue(item)?.ToString() ?? "";

                        // Escapar valores CSV
                        if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
                        {
                            value = "\"" + value.Replace("\"", "\"\"") + "\"";
                        }

                        return value;
                    });

                    sb.AppendLine(string.Join(",", values));
                }

                var result = Encoding.UTF8.GetBytes(sb.ToString());
                Console.WriteLine($"[ExportToCsv] ✅ Archivo generado: {result.Length} bytes");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ExportToCsv] ❌ Error: {ex.Message}");
                Console.WriteLine($"[ExportToCsv] Stack: {ex.StackTrace}");
                return new byte[0];
            }
        }

        public byte[] ExportToPdf<T>(List<T> data, string title)
        {
            try
            {
                Console.WriteLine($"[ExportToPdf] Iniciando con {data?.Count ?? 0} registros");

                if (data == null || data.Count == 0)
                {
                    Console.WriteLine("[ExportToPdf] ❌ Sin datos");
                    return new byte[0];
                }

                using (var stream = new MemoryStream())
                {
                    var document = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
                    PdfWriter.GetInstance(document, stream);
                    document.Open();

                    // Título
                    var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                    var heading = new Paragraph(title ?? "Reporte", titleFont)
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingAfter = 20
                    };
                    document.Add(heading);

                    // Tabla
                    var properties = typeof(T).GetProperties();

                    Console.WriteLine($"[ExportToPdf] Propiedades encontradas: {properties.Length}");

                    if (properties.Length == 0)
                    {
                        document.Add(new Paragraph("No hay datos para mostrar"));
                        document.Close();
                        return stream.ToArray();
                    }

                    var table = new PdfPTable(properties.Length)
                    {
                        WidthPercentage = 100
                    };

                    var headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);

                    // Encabezados
                    foreach (var prop in properties)
                    {
                        var cell = new PdfPCell(new Phrase(prop.Name, headerFont))
                        {
                            BackgroundColor = BaseColor.LIGHT_GRAY,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            Padding = 5
                        };
                        table.AddCell(cell);
                    }

                    // Datos
                    var dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 8);

                    foreach (var item in data)
                    {
                        foreach (var prop in properties)
                        {
                            var value = prop.GetValue(item)?.ToString() ?? "";
                            var cell = new PdfPCell(new Phrase(value, dataFont))
                            {
                                Padding = 3
                            };
                            table.AddCell(cell);
                        }
                    }

                    document.Add(table);
                    document.Close();

                    var result = stream.ToArray();
                    Console.WriteLine($"[ExportToPdf] ✅ Archivo generado: {result.Length} bytes");
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ExportToPdf] ❌ Error: {ex.Message}");
                Console.WriteLine($"[ExportToPdf] Stack: {ex.StackTrace}");
                return new byte[0];
            }
        }
    }
}