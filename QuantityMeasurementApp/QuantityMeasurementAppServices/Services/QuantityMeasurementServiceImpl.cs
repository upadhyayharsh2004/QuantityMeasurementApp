using System;
using QuantityMeasurementAppServices.Interfaces;
using QuantityMeasurementAppModels.DTOs;
using QuantityMeasurementAppModels.Entities;
using QuantityMeasurementAppBusiness.Exceptions;
using QuantityMeasurementAppRepositories.Interfaces;
using QuantityMeasurementAppBusiness.Interfaces;
using QuantityMeasurementAppBusiness;
using QuantityMeasurementAppBusiness.Implementations;
using QuantityMeasurementAppModels.Enums;

namespace QuantityMeasurementApp.Services
{
    /*
     * ============================================================================================
     * CLASS: QuantityMeasurementServiceImpl
     * 
     * This class represents the SERVICE LAYER of the application.
     * It acts as a bridge between:
     * 
     * - Presentation Layer (UI / Controller)
     * - Business Logic Layer (Quantity, IMeasurable implementations)
     * - Data Access Layer (Repository)
     * 
     * Responsibilities:
     * --------------------------------------------------------------------------------------------
     * - Perform operations like Add, Subtract, Divide, Compare, Convert
     * - Convert input DTOs into business objects (Quantity<IMeasurable>)
     * - Resolve units dynamically based on input strings
     * - Handle exceptions and log them into the repository
     * - Return results back as DTOs
     * 
     * Design Principles Used:
     * --------------------------------------------------------------------------------------------
     * - Dependency Injection (repository injected via constructor)
     * - Separation of Concerns
     * - Abstraction via interfaces (IMeasurable, Repository)
     * ============================================================================================
     */
    public class QuantityMeasurementServiceImpl : IQuantityMeasurementService
    {
        // Repository used to persist operation results
        private readonly IQuantityMeasurementRepository repository;

        /*
         * ========================================================================================
         * CONSTRUCTOR
         * 
         * Injects repository dependency.
         * This allows flexibility (cache or database implementation).
         * ========================================================================================
         */
        public QuantityMeasurementServiceImpl(IQuantityMeasurementRepository repository)
        {
            this.repository = repository;
        }

        /*
         * ========================================================================================
         * METHOD: ResolveUnit (CORE HELPER METHOD)
         * 
         * Converts a string-based unit into a concrete IMeasurable implementation.
         * 
         * Example:
         * - ("length", "Feet") → LengthMeasurementImpl(LengthUnit.Feet)
         * 
         * Why this is important:
         * ----------------------------------------------------------------------------------------
         * - Decouples UI input from business logic
         * - Dynamically supports multiple measurement types
         * - Centralizes unit resolution logic
         * ========================================================================================
         */
        private IMeasurable ResolveUnit(string measurementType, string unitName)
        {
            switch (measurementType.ToLower())
            {
                case "length":
                    LengthUnit lu = (LengthUnit)Enum.Parse(
                        typeof(LengthUnit), unitName, true);
                    return new LengthMeasurementImpl(lu);

                case "weight":
                    WeightUnit wu = (WeightUnit)Enum.Parse(
                        typeof(WeightUnit), unitName, true);
                    return new WeightMeasurementImpl(wu);

                case "volume":
                    VolumeUnit vu = (VolumeUnit)Enum.Parse(
                        typeof(VolumeUnit), unitName, true);
                    return new VolumeMeasurementImpl(vu);

                case "temperature":
                    TemperatureUnit tu = (TemperatureUnit)Enum.Parse(
                        typeof(TemperatureUnit), unitName, true);
                    return new TemperatureMeasurementImpl(tu);

                default:
                    throw new ArgumentException(
                        "Unknown measurement type: " + measurementType);
            }
        }

        /*
         * ========================================================================================
         * METHOD: Add
         * 
         * Adds two quantities and converts the result into a target unit.
         * ========================================================================================
         */
        public QuantityDTO Add(QuantityDTO first, QuantityDTO second,
            string targetUnit)
        {
            try
            {
                /*
                 * Resolve units into business logic implementations
                 */
                IMeasurable unit1 = ResolveUnit(first.MeasurementType, first.UnitName);
                IMeasurable unit2 = ResolveUnit(second.MeasurementType, second.UnitName);
                IMeasurable target = ResolveUnit(first.MeasurementType, targetUnit);

                /*
                 * Create Quantity objects and perform addition
                 */
                Quantity<IMeasurable> q1 = new Quantity<IMeasurable>(first.Value, unit1);
                Quantity<IMeasurable> q2 = new Quantity<IMeasurable>(second.Value, unit2);
                Quantity<IMeasurable> result = q1.Add(q2, target);

                /*
                 * Save successful operation in repository
                 */
                repository.Save(new QuantityMeasurementEntity(
                    "Add",
                    first.Value, first.UnitName,
                    second.Value, second.UnitName,
                    result.GetValue(),
                    first.MeasurementType));

                /*
                 * Return result as DTO
                 */
                return new QuantityDTO(result.GetValue(), targetUnit, first.MeasurementType);
            }
            catch (Exception ex)
            {
                // Log error and rethrow custom exception
                repository.Save(new QuantityMeasurementEntity("Add", ex.Message));
                throw new QuantityMeasurementException(
                    "Add operation failed: " + ex.Message, ex);
            }
        }

        /*
         * ========================================================================================
         * METHOD: Subtract
         * ========================================================================================
         */
        public QuantityDTO Subtract(QuantityDTO first, QuantityDTO second,
            string targetUnit)
        {
            try
            {
                IMeasurable unit1 = ResolveUnit(first.MeasurementType, first.UnitName);
                IMeasurable unit2 = ResolveUnit(second.MeasurementType, second.UnitName);
                IMeasurable target = ResolveUnit(first.MeasurementType, targetUnit);

                Quantity<IMeasurable> q1 = new Quantity<IMeasurable>(first.Value, unit1);
                Quantity<IMeasurable> q2 = new Quantity<IMeasurable>(second.Value, unit2);
                Quantity<IMeasurable> result = q1.Subtract(q2, target);

                repository.Save(new QuantityMeasurementEntity(
                    "Subtract",
                    first.Value, first.UnitName,
                    second.Value, second.UnitName,
                    result.GetValue(),
                    first.MeasurementType));

                return new QuantityDTO(
                    result.GetValue(), targetUnit, first.MeasurementType);
            }
            catch (Exception ex)
            {
                repository.Save(new QuantityMeasurementEntity("Subtract", ex.Message));
                throw new QuantityMeasurementException("Subtract operation failed: " + ex.Message, ex);
            }
        }

        /*
         * ========================================================================================
         * METHOD: Divide
         * 
         * Divides two quantities and returns a numeric result.
         * ========================================================================================
         */
        public double Divide(QuantityDTO first, QuantityDTO second)
        {
            try
            {
                IMeasurable unit1 = ResolveUnit(first.MeasurementType, first.UnitName);
                IMeasurable unit2 = ResolveUnit(second.MeasurementType, second.UnitName);

                Quantity<IMeasurable> q1 = new Quantity<IMeasurable>(first.Value, unit1);
                Quantity<IMeasurable> q2 = new Quantity<IMeasurable>(second.Value, unit2);
                double result = q1.Divide(q2);

                repository.Save(new QuantityMeasurementEntity(
                    "Divide",
                    first.Value, first.UnitName,
                    second.Value, second.UnitName,
                    result,
                    first.MeasurementType));

                return result;
            }
            catch (Exception ex)
            {
                repository.Save(new QuantityMeasurementEntity("Divide", ex.Message));
                throw new QuantityMeasurementException("Divide operation failed: " + ex.Message, ex);
            }
        }

        /*
         * ========================================================================================
         * METHOD: Compare
         * 
         * Compares two quantities and returns true/false.
         * ========================================================================================
         */
        public bool Compare(QuantityDTO first, QuantityDTO second)
        {
            try
            {
                IMeasurable unit1 = ResolveUnit(first.MeasurementType, first.UnitName);
                IMeasurable unit2 = ResolveUnit(second.MeasurementType, second.UnitName);

                Quantity<IMeasurable> q1 = new Quantity<IMeasurable>(first.Value, unit1);
                Quantity<IMeasurable> q2 = new Quantity<IMeasurable>(second.Value, unit2);
                bool result = q1.Equals(q2);

                repository.Save(new QuantityMeasurementEntity(
                    "Compare",
                    first.Value, first.UnitName,
                    second.Value, second.UnitName,
                    result ? 1 : 0,
                    first.MeasurementType));

                return result;
            }
            catch (Exception ex)
            {
                repository.Save(new QuantityMeasurementEntity("Compare", ex.Message));
                throw new QuantityMeasurementException("Compare operation failed: " + ex.Message, ex);
            }
        }

        /*
         * ========================================================================================
         * METHOD: Convert
         * 
         * Converts a quantity from one unit to another.
         * ========================================================================================
         */
        public QuantityDTO Convert(QuantityDTO quantity, string targetUnit)
        {
            try
            {
                IMeasurable unit = ResolveUnit(
                    quantity.MeasurementType, quantity.UnitName);
                IMeasurable target = ResolveUnit(
                    quantity.MeasurementType, targetUnit);

                Quantity<IMeasurable> q = new Quantity<IMeasurable>(
                    quantity.Value, unit);
                Quantity<IMeasurable> result = q.ConvertTo(target);

                repository.Save(new QuantityMeasurementEntity(
                    "Convert",
                    quantity.Value, quantity.UnitName,
                    result.GetValue(),
                    quantity.MeasurementType));

                return new QuantityDTO(
                    result.GetValue(), targetUnit, quantity.MeasurementType);
            }
            catch (Exception ex)
            {
                repository.Save(new QuantityMeasurementEntity("Convert", ex.Message));
                throw new QuantityMeasurementException("Convert operation failed: " + ex.Message, ex);
            } 
        }
    }
}