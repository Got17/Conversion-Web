namespace MyConverter

open WebSharper
open WebSharper.UI
open WebSharper.UI.Templating

// Define a type named LengthTemplate, which represents a template loaded from the file "src/index.html"
// ClientLoad.FromDocument specifies that the template will be loaded from the document object model (DOM) of the client-side web page
type LengthTemplate = Template<"src/index.html", ClientLoad.FromDocument>

[<JavaScript>]
module LengthPage =
    
    // Define a function to render the length converter page with Centimeter input
    let CentimeterPage() =
        // Create a variable to store the input value
        let input = Var.Create "0"

        // Create a view for the input value
        let viewInput = input.View

        // Define a function to convert Centimeter to Meter and Kilometer
        let centimeterToMeterAndKilometer(num: float) =
            View.Map (fun (value:string) ->
                try
                    let result = float value / num 
                    
                    $"{result}"
                with _ ->
                    "Error"
            ) viewInput
        
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
            .convertToResult1(centimeterToMeterAndKilometer 100.0)
            .convertToResult2(centimeterToMeterAndKilometer 100000.0)
            .changeTo1("/lengthMeter")
            .changeToButton1("Meters")
            .changeTo2("/lengthKilometer")
            .changeToButton2("Kilometers")
            .Doc()

    // Define a function to render the length converter page with Meter input
    let MeterPage() =
        let input = Var.Create "0"

        let viewInput = input.View

        let meterToCentimeter() =
            View.Map (fun (value:string) ->
                try
                    let result = float value * 100.0
                    
                    $"{result}"
                with _ ->
                    "Error"
            ) viewInput

        let meterToKilometer() = 
            View.Map ( fun (value:string) ->
                try     
                    let result = (float value / 1000.0) 
                    $"{result}"
                with _ ->
                    "Error" 
            ) viewInput
        
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
            .convertToResult1(meterToCentimeter())
            .convertToResult2(meterToKilometer())
            .changeTo1("/lengthCentimeter")
            .changeToButton1("Centimeters")
            .changeTo2("/lengthKilometer")
            .changeToButton2("Kilometers")
            .Doc()

    // Define a function to render the length converter page with Kilometer input
    let KilometerPage() =
        let input = Var.Create "0"

        let viewInput = input.View

        let kilometerToMeterAndCentimeter (num: float) =
            View.Map (fun (value:string) ->
                try
                    let result = float value * num // 1000.0
                    
                    $"{result}"
                with _ ->
                    "Error"
            ) viewInput
        
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
            .convertToResult1(kilometerToMeterAndCentimeter 1000.0)
            .convertToResult2(kilometerToMeterAndCentimeter 100000.0)
            .changeTo1("/lengthMeter")
            .changeToButton1("Meters")
            .changeTo2("/lengthCentimeter")
            .changeToButton2("Centimeters")
            .Doc()
