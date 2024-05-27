namespace MyConverter

open WebSharper
open WebSharper.UI
open WebSharper.UI.Templating

// Define a type named MassTemplate, which represents a template loaded from the file "src/index.html"
// ClientLoad.FromDocument specifies that the template will be loaded from the document object model (DOM) of the client-side web page
type MassTemplate = Template<"src/index.html", ClientLoad.FromDocument>

[<JavaScript>]
module MassPage =

    let convertMass mass (fromUnit:string) (toUnit:string) = 
        let mutable result = 0.0
        if (fromUnit = "g" && toUnit = "mg") then
            View.Map (fun (value:string) ->
                result <- float value * 1000.0
                $"{result}"
            ) mass

        elif (fromUnit = "g" && toUnit = "kg") then
            View.Map (fun (value:string) ->
                result <- float value / 1000.0
                $"{result}"
            ) mass

        elif (fromUnit = "kg" && toUnit = "g") then
            View.Map (fun (value:string) ->
                result <- float value * 1000.0
                $"{result}"
            ) mass

        elif (fromUnit = "kg" && toUnit = "mg") then
            View.Map (fun (value:string) ->
                result <- float value * 1000000.0
                $"{result}"
            ) mass

        elif (fromUnit = "mg" && toUnit = "g") then
            View.Map (fun (value:string) ->
                result <- float value / 1000.0
                $"{result}"
            ) mass

        elif (fromUnit = "mg" && toUnit = "kg") then
            View.Map (fun (value:string) ->
                result <- float value / 1000000.0
                $"{result}"
            ) mass

        else mass

    // Create a variable to store the input value
    let input = Var.Create "0"

    // Create a view for the input value
    let viewInput = input.View
        
    // Define a function to render the mass converter page with Gram input
    let GramPage() =

        // Render the conversion page using the MassTemplate
        MassTemplate.conversionPage()
            .ToHomepage("/")
            .converterHeader("Mass")
            .ToNextPage("/lengthCentimeter")
            .toNextPageText("Length")
            .convertFrom("Gram")
            .convertFromUnit("g")
            .convertTo1("Milligram")
            .convertToUnit1("mg")
            .convertTo2("Kilogram")
            .convertToUnit2("Kg")
            .Placeholder("gram")
            .input(input)
            .convertFromResult(viewInput)
            .convertToResult1(convertMass viewInput "g" "mg")
            .convertToResult2(convertMass viewInput "g" "kg")
            .changeTo1("/massMilligram")
            .changeToButton1("Milligram")
            .changeTo2("/massKilogram")
            .changeToButton2("Kilogram")
            .Doc()

    // Define a function to render the mass converter page with Milligram input
    let MilligramPage() =
        
        MassTemplate.conversionPage()
            .ToHomepage("/")
            .converterHeader("Mass")
            .ToNextPage("/lengthCentimeter")
            .toNextPageText("Length")
            .convertFrom("Milligram")
            .convertFromUnit("mg")
            .convertTo1("Gram")
            .convertToUnit1("g")
            .convertTo2("Kilogram")
            .convertToUnit2("Kg")
            .Placeholder("milligram")
            .input(input)
            .convertFromResult(viewInput)
            .convertToResult1(convertMass viewInput "mg" "g")
            .convertToResult2(convertMass viewInput "mg" "kg")
            .changeTo1("/massGram")
            .changeToButton1("Gram")
            .changeTo2("/massKilogram")
            .changeToButton2("Kilogram")
            .Doc()


    // Define a function to render the mass converter page with Kilogram input
    let KilogramPage() =

        MassTemplate.conversionPage()
            .ToHomepage("/")
            .converterHeader("Mass")
            .ToNextPage("/lengthCentimeter")
            .toNextPageText("Length")
            .convertFrom("Kilogram")
            .convertFromUnit("Kg")
            .convertTo1("Gram")
            .convertToUnit1("g")
            .convertTo2("Milligram")
            .convertToUnit2("mg")
            .Placeholder("Kilogram")
            .input(input)
            .convertFromResult(viewInput)
            .convertToResult1(convertMass viewInput "kg" "g")
            .convertToResult2(convertMass viewInput "kg" "mg")
            .changeTo1("/massGram")
            .changeToButton1("Gram")
            .changeTo2("/massMilligram")
            .changeToButton2("Milligram")
            .Doc()