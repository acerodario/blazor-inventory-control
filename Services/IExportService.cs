namespace PruebaFinal.Services
{
    public interface IExportService
    {
        byte[] ExportToExcel<T>(List<T> data, string sheetName);
        byte[] ExportToCsv<T>(List<T> data);
        byte[] ExportToPdf<T>(List<T> data, string title);

    }
}