namespace MyConverter

open WebSharper
open WebSharper.UI
open WebSharper.UI.Templating

// Define a type named TemperatureTemplate, which represents a template loaded from the file "src/index.html"
// ClientLoad.FromDocument specifies that the template will be loaded from the document object model (DOM) of the client-side web page

type TemperatureTemplate = Template<"src/index.html", ClientLoad.FromDocument>

[<JavaScript>]
module TemperaturePage =

    let convertTemperature degree (fromUnit:string) (toUnit:string) = 
        let mutable result = 0.0
        if (fromUnit = "c" && toUnit = "f") then
            View.Map (fun (value:string) ->
                result <- (float value * 1.8) + 32.0
                sprintf "%.2f" result
            ) degree

        elif (fromUnit = "c" && toUnit = "k") then
            View.Map (fun (value:string) ->
                result <- (float value + 273.15)
                sprintf "%.2f" result
            ) degree

        elif (fromUnit = "k" && toUnit = "c") then
            View.Map (fun (value:string) ->
                result <- float value - 273.15
                sprintf "%.2f" result
            ) degree

        elif (fromUnit = "k" && toUnit = "f") then
            View.Map (fun (value:string) ->
                result <- 1.8 * (float value - 273.15) + 32.0
                sprintf "%.2f" result
            ) degree

        elif (fromUnit = "f" && toUnit = "k") then
            View.Map (fun (value:string) ->
                result <- (float value - 32.0) * 5.0/9.0 + 273.15
                sprintf "%.2f" result
            ) degree

        elif (fromUnit = "f" && toUnit = "c") then
            View.Map (fun (value:string) ->
                result <- (float value - 32.0) * (5.0 / 9.0)
                sprintf "%.2f" result
            ) degree

        else degree

    // Create a variable to store the input value
    let input = Var.Create "0"

    // Create a view for the input value
    let viewInput = input.View
        
    // Define a function to render the temperature converter page with Celsius input
    let CelsiusPage() =

        // Render the conversion page using the TemperatureTemplate
        TemperatureTemplate.conversionPage()
            // Set link to homepage
            .ToHomepage("/")
            // Set the header of the converter page
            .converterHeader("Temperature")
            // Set link to the next page
            .ToNextPage("/massKilogram")
            // Set text for the link to the next page
            .toNextPageText("Mass")
            // Set the unit to convert from
            .convertFrom("Celsius")
            // Set the unit symbol for convert from
            .convertFromUnit("°C")
            // Set the unit to convert to (1st unit)
            .convertTo1("Fahrenheit")
            // Set the unit symbol for convert to (1st unit)
            .convertToUnit1("°F")
            // Set the unit to convert to (2nd unit)
            .convertTo2("Kelvin")
            // Set the unit symbol for convert to (2nd unit)
            .convertToUnit2("°K")
            // Set placeholder for input field
            .Placeholder("celsius")
            // Bind input variable to the input field
            .input(input)
            // Bind viewInput to show the input value
            .convertFromResult(viewInput)
            // Bind the result of Celsius to Fahrenheit conversion
            .convertToResult1(convertTemperature viewInput "c" "f")
            // Bind the result of Celsius to Kelvin conversion
            .convertToResult2(convertTemperature viewInput "c" "k")
            // Set link to change to Fahrenheit page
            .changeTo1("/temperatureFahrenheit")
            // Set button text for changing to Fahrenheit page
            .changeToButton1("Fahrenheit")
            // Set link to change to Kelvin page
            .changeTo2("/temperatureKelvin")
            // Set button text for changing to Kelvin page
            .changeToButton2("Kelvin")
            .Doc()

    // Define a function to render the temperature converter page with Kelvin input
    let KelvinPage() =
        
        TemperatureTemplate.conversionPage()
            .ToHomepage("/")
            .converterHeader("Temperature")
            .ToNextPage("/massKilogram")
            .toNextPageText("Mass")
            .convertFrom("Kelvin")
            .convertFromUnit("°K")
            .convertTo1("Celsius")
            .convertToUnit1("°C")
            .convertTo2("Fahrenheit")
            .convertToUnit2("°F")
            .Placeholder("kelvin")
            .input(input)
            .convertFromResult(viewInput)
            .convertToResult1(convertTemperature viewInput "k" "c")
            .convertToResult2(convertTemperature viewInput "k" "f")
            .changeTo1("/temperatureFahrenheit")
            .changeToButton1("Fahrenheit")
            .changeTo2("/temperatureCelsius")
            .changeToButton2("Celsius")
            .Doc()

    // Define a function to render the temperature converter page with Fahrenheit input
    let FahrenheitPage() =
        
        TemperatureTemplate.conversionPage()
            .ToHomepage("/")
            .converterHeader("Temperature")
            .ToNextPage("/massKilogram")
            .toNextPageText("Mass")
            .convertFrom("Fahrenheit")
            .convertFromUnit("°F")
            .convertTo1("Celsius")
            .convertToUnit1("°C")
            .convertTo2("Kelvin")
            .convertToUnit2("°K")
            .Placeholder("fahrenheit")
            .input(input)
            .convertFromResult(viewInput)
            .convertToResult1(convertTemperature viewInput "f" "c")
            .convertToResult2(convertTemperature viewInput "f" "k")
            .changeTo1("/temperatureCelsius")
            .changeToButton1("Celsius")
            .changeTo2("/temperatureKelvin")
            .changeToButton2("Kelvin")
            .Doc()