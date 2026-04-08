namespace QuantityMeasurementApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            QuantityMeasurementApp quantityApp = QuantityMeasurementApp.GetInstance();

            try
            {
                quantityApp.Start();
                quantityApp.ReportAllRecordsMeasurements();
            }
            catch (Exception exception)
            {
                Console.WriteLine("[Program] Unhandled Exception Error Caught: " + exception.Message);
            }
            finally
            {
                quantityApp.CloseResources();
            }
        }
    }
}