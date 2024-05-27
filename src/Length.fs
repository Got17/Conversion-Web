namespace MyConverter

open WebSharper
open WebSharper.UI
open WebSharper.UI.Templating

// Define a type named LengthTemplate, which represents a template loaded from the file "src/index.html"
// ClientLoad.FromDocument specifies that the template will be loaded from the document object model (DOM) of the client-side web page
type LengthTemplate = Template<"src/index.html", ClientLoad.FromDocument>

[<JavaScript>]
module LengthPage =

    let convertLength length (fromUnit:string) (toUnit:string) = 
        let mutable result = 0.0
        if (fromUnit = "m" && toUnit = "cm") then
            View.Map (fun (value:string) ->
                result <- float value * 100.0
                $"{result}"
            ) length

        elif (fromUnit = "m" && toUnit = "km") then
            View.Map (fun (value:string) ->
                result <- float value / 1000.0
                $"{result}"
            ) length

        elif (fromUnit = "km" && toUnit = "m") then
            View.Map (fun (value:string) ->
                result <- float value * 1000.0
                $"{result}"
            ) length

        elif (fromUnit = "km" && toUnit = "cm") then
            View.Map (fun (value:string) ->
                result <- float value * 100000.0
                $"{result}"
            ) length

        elif (fromUnit = "cm" && toUnit = "m") then
            View.Map (fun (value:string) ->
                result <- float value / 100.0
                $"{result}"
            ) length

        elif (fromUnit = "cm" && toUnit = "km") then
            View.Map (fun (value:string) ->
                result <- float value / 100000.0
                $"{result}"
            ) length

        else length

    // Create a variable to store the input value
    let input = Var.Create "0"

    // Create a view for the input value
    let viewInput = input.View    

    // Define a function to render the length converter page with Centimeter input
    let CentimeterPage() =
        // Render the conversion page using the LengthTemplate
        LengthTemplate.conversionPage()
            .ToHomepage("/")
            .converterHeader("Length")
            .ToNextPage("/temperatureCelsius")
            .toNextPageText("Temperature")
            .convertFrom("Centimeters")
            .convertFromUnit("Cm")
            .convertTo1("Meters")
            .convertToUnit1("m")
            .convertTo2("Kilometers")
            .convertToUnit2("Km")
            .Placeholder("Centimeters")
            .input(input)
            .convertFromResult(viewInput)
            .convertToResult1(convertLength viewInput "cm" "m")
            .convertToResult2(convertLength viewInput "cm" "km")
            .changeTo1("/lengthMeter")
            .changeToButton1("Meters")
            .changeTo2("/lengthKilometer")
            .changeToButton2("Kilometers")
            .Doc()

    // Define a function to render the length converter page with Meter input
    let MeterPage() =
        
        LengthTemplate.conversionPage()
            .ToHomepage("/")
            .converterHeader("Length")
            .ToNextPage("/temperatureCelsius")
            .toNextPageText("Temperature")
            .convertFrom("Meters")
            .convertFromUnit("m")
            .convertTo1("Centimeters")
            .convertToUnit1("Cm")
            .convertTo2("Kilometers")
            .convertToUnit2("Km")
            .Placeholder("Meters")
            .input(input)
            .convertFromResult(viewInput)
            .convertToResult1(convertLength viewInput "m" "cm")
            .convertToResult2(convertLength viewInput "m" "km")
            .changeTo1("/lengthCentimeter")
            .changeToButton1("Centimeters")
            .changeTo2("/lengthKilometer")
            .changeToButton2("Kilometers")
            .Doc()

    // Define a function to render the length converter page with Kilometer input
    let KilometerPage() =
        
        LengthTemplate.conversionPage()
            .ToHomepage("/")
            .converterHeader("Length")
            .ToNextPage("/temperatureCelsius")
            .toNextPageText("Temperature")
            .convertFrom("Kilometers")
            .convertFromUnit("Km")
            .convertTo1("Meters")
            .convertToUnit1("m")
            .convertTo2("Centimeters")
            .convertToUnit2("Cm")
            .Placeholder("Kilometers")
            .input(input)
            .convertFromResult(viewInput)
            .convertToResult1(convertLength viewInput "km" "m")
            .convertToResult2(convertLength viewInput "km" "cm")
            .changeTo1("/lengthMeter")
            .changeToButton1("Meters")
            .changeTo2("/lengthCentimeter")
            .changeToButton2("Centimeters")
            .Doc()
