# Units Usage Guide

This document explains how to use the localized units system in Moondesk for IIoT metrics.

## Overview

The Units system provides localized unit names and symbols for common IIoT metrics in both English and French. All units are accessible through the `Units` resource class.

## Usage

### Basic Usage

```csharp
using Moondesk.Domain.Units;

// Get unit name in current culture
string temperatureUnit = Units.Unit_Temperature_Celsius; // "Celsius" or "Celsius"
string temperatureSymbol = Units.Symbol_Temperature_Celsius; // "°C"

// Get unit name in specific culture
UnitsCulture.SetCulture("fr-FR");
string frenchUnit = Units.Unit_Temperature_Celsius; // "Celsius"
```

### Setting Culture

```csharp
// Set to French
UnitsCulture.SetCulture("fr-FR");

// Set to English (default)
UnitsCulture.SetCulture("en-US");
```

## Available Unit Categories

### Temperature
- **Celsius**: `Unit_Temperature_Celsius` / `Symbol_Temperature_Celsius` (°C)
- **Fahrenheit**: `Unit_Temperature_Fahrenheit` / `Symbol_Temperature_Fahrenheit` (°F)
- **Kelvin**: `Unit_Temperature_Kelvin` / `Symbol_Temperature_Kelvin` (K)

### Pressure
- **Pascal**: `Unit_Pressure_Pascal` / `Symbol_Pressure_Pascal` (Pa)
- **Bar**: `Unit_Pressure_Bar` / `Symbol_Pressure_Bar` (bar)
- **PSI**: `Unit_Pressure_Psi` / `Symbol_Pressure_Psi` (psi)
- **Atmosphere**: `Unit_Pressure_Atm` / `Symbol_Pressure_Atm` (atm)
- **Torr**: `Unit_Pressure_Torr` / `Symbol_Pressure_Torr` (Torr)

### Flow Rate
- **Cubic Meter per Second**: `Unit_FlowRate_CubicMeterPerSecond` / `Symbol_FlowRate_CubicMeterPerSecond` (m³/s)
- **Liter per Second**: `Unit_FlowRate_LiterPerSecond` / `Symbol_FlowRate_LiterPerSecond` (L/s)
- **Liter per Minute**: `Unit_FlowRate_LiterPerMinute` / `Symbol_FlowRate_LiterPerMinute` (L/min)
- **Gallon per Minute**: `Unit_FlowRate_GallonPerMinute` / `Symbol_FlowRate_GallonPerMinute` (gal/min)

### Volume
- **Cubic Meter**: `Unit_Volume_CubicMeter` / `Symbol_Volume_CubicMeter` (m³)
- **Liter**: `Unit_Volume_Liter` / `Symbol_Volume_Liter` (L)
- **Gallon**: `Unit_Volume_Gallon` / `Symbol_Volume_Gallon` (gal)

### Length
- **Meter**: `Unit_Length_Meter` / `Symbol_Length_Meter` (m)
- **Centimeter**: `Unit_Length_Centimeter` / `Symbol_Length_Centimeter` (cm)
- **Millimeter**: `Unit_Length_Millimeter` / `Symbol_Length_Millimeter` (mm)
- **Foot**: `Unit_Length_Foot` / `Symbol_Length_Foot` (ft)
- **Inch**: `Unit_Length_Inch` / `Symbol_Length_Inch` (in)

### Mass
- **Kilogram**: `Unit_Mass_Kilogram` / `Symbol_Mass_Kilogram` (kg)
- **Gram**: `Unit_Mass_Gram` / `Symbol_Mass_Gram` (g)
- **Pound**: `Unit_Mass_Pound` / `Symbol_Mass_Pound` (lb)

### Electrical
- **Volt**: `Unit_Electrical_Volt` / `Symbol_Electrical_Volt` (V)
- **Ampere**: `Unit_Electrical_Ampere` / `Symbol_Electrical_Ampere` (A)
- **Watt**: `Unit_Electrical_Watt` / `Symbol_Electrical_Watt` (W)
- **Kilowatt**: `Unit_Electrical_Kilowatt` / `Symbol_Electrical_Kilowatt` (kW)
- **Kilowatt Hour**: `Unit_Electrical_KilowattHour` / `Symbol_Electrical_KilowattHour` (kWh)
- **Ohm**: `Unit_Electrical_Ohm` / `Symbol_Electrical_Ohm` (Ω)

### Frequency
- **Hertz**: `Unit_Frequency_Hertz` / `Symbol_Frequency_Hertz` (Hz)
- **Kilohertz**: `Unit_Frequency_Kilohertz` / `Symbol_Frequency_Kilohertz` (kHz)
- **RPM**: `Unit_Frequency_Rpm` / `Symbol_Frequency_Rpm` (rpm / tr/min)

### Concentration
- **Parts per Million**: `Unit_Concentration_Ppm` / `Symbol_Concentration_Ppm` (ppm)
- **Parts per Billion**: `Unit_Concentration_Ppb` / `Symbol_Concentration_Ppb` (ppb)
- **Milligrams per Liter**: `Unit_Concentration_MgPerLiter` / `Symbol_Concentration_MgPerLiter` (mg/L)
- **Percent**: `Unit_Concentration_Percent` / `Symbol_Concentration_Percent` (%)

### Chemical
- **pH**: `Unit_pH` / `Symbol_pH` (pH)
- **Conductivity (Siemens)**: `Unit_Conductivity_Siemens` / `Symbol_Conductivity_Siemens` (S/m)
- **Conductivity (Micro)**: `Unit_Conductivity_MicroSiemens` / `Symbol_Conductivity_MicroSiemens` (μS/cm)

### Time
- **Second**: `Unit_Time_Second` / `Symbol_Time_Second` (s)
- **Minute**: `Unit_Time_Minute` / `Symbol_Time_Minute` (min)
- **Hour**: `Unit_Time_Hour` / `Symbol_Time_Hour` (h)

### Velocity
- **Meter per Second**: `Unit_Velocity_MeterPerSecond` / `Symbol_Velocity_MeterPerSecond` (m/s)
- **Kilometer per Hour**: `Unit_Velocity_KilometerPerHour` / `Symbol_Velocity_KilometerPerHour` (km/h)

## Example Implementation

```csharp
public class SensorReading
{
    public double Value { get; set; }
    public string UnitName { get; set; }
    public string UnitSymbol { get; set; }
    
    public static SensorReading CreateTemperatureReading(double value)
    {
        return new SensorReading
        {
            Value = value,
            UnitName = Units.Unit_Temperature_Celsius,
            UnitSymbol = Units.Symbol_Temperature_Celsius
        };
    }
    
    public override string ToString()
    {
        return $"{Value} {UnitSymbol} ({UnitName})";
    }
}
```

## Localization

The system automatically uses the current thread culture. To override:

```csharp
// Temporarily change culture
var originalCulture = Thread.CurrentThread.CurrentUICulture;
UnitsCulture.SetCulture("fr-FR");

string frenchUnit = Units.Unit_Temperature_Celsius; // "Celsius"

// Restore original culture
Thread.CurrentThread.CurrentUICulture = originalCulture;
```

## Adding New Units

To add new units:

1. Add entries to both `Units.resx` and `Units.fr-FR.resx`
2. Follow the naming convention: `Unit_Category_Name` and `Symbol_Category_Name`
3. Rebuild the project to regenerate Designer files
4. Update this documentation

## Best Practices

- Always use symbols for display in charts and compact UIs
- Use full unit names for accessibility and detailed views
- Set culture at application startup for consistent behavior
- Cache unit strings if used frequently to avoid repeated resource lookups
