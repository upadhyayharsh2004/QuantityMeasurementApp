using System;

namespace QuantityMeasurementApp.Interfaces
{
    // This interface defines the contract for any menu system implementation
    // which is responsible for running and controlling the user interaction flow
    public interface IMenu
    {
        // This method is responsible for starting and executing the menu loop
        // including displaying options, taking user input, and handling operations
        void Run();
    }
}