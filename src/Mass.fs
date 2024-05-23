namespace MyConverter

open WebSharper
open WebSharper.UI
open WebSharper.UI.Templating

// Define a type named MassTemplate, which represents a template loaded from the file "src/index.html"
// ClientLoad.FromDocument specifies that the template will be loaded from the document object model (DOM) of the client-side web page
type MassTemplate = Template<"src/index.html", ClientLoad.FromDocument>

[<JavaScript>]
module MassPage =
    
    // Define a function to render the mass converter page with Gram input
    let GramPage() =
        // Create a variable to store the input value
        let input = Var.Create "0"

        // Create a view for the input value
        let viewInput = input.View

        // Define a function to convert Gram to Milligram
        let gramToMilligram() =
            View.Map (fun (value:string) ->
                try
                    let result = float value * 1000.0
                    
                    $"{result}"
                with _ ->
                    "Error"
            ) viewInput

        // Define a function to convert Gram to Kilogram
        let gramToKilogram() = 
            View.Map ( fun (value:string) ->
                try     
                    let result = float value / 1000.0
                    $"{result}"
                with _ ->
                    "Error" 
            ) viewInput
        
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
            .convertToResult1(gramToMilligram())
            .convertToResult2(gramToKilogram())
            .changeTo1("/massMilligram")
            .changeToButton1("Milligram")
            .changeTo2("/massKilogram")
            .changeToButton2("Kilogram")
            .Doc()

    // Define a function to render the mass converter page with Milligram input
    let MilligramPage() =
        let input = Var.Create "0"

        let viewInput = input.View

        let milligramToGram() =
            View.Map (fun (value:string) ->
                try
                    let result = float value * 1000.0
                    
                    $"{result}"
                with _ ->
                    "Error"
            ) viewInput

        let milligramToKilogram() = 
            View.Map ( fun (value:string) ->
                try     
                    let result = float value / 1000000.0
                    $"{result}"
                with _ ->
                    "Error" 
            ) viewInput
        
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
            .convertToResult1(milligramToGram())
            .convertToResult2(milligramToKilogram())
            .changeTo1("/massGram")
            .changeToButton1("Gram")
            .changeTo2("/massKilogram")
            .changeToButton2("Kilogram")
            .Doc()


    // Define a function to render the mass converter page with Kilogram input
    let KilogramPage() =
        let input = Var.Create "0"

        let viewInput = input.View

        let kilogramToGramAndMilligram (num: float) =
            View.Map (fun (value:string) ->
                try
                    let result = float value * num // 1000.0
                    
                    $"{result}"
                with _ ->
                    "Error"
            ) viewInput
        
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
            .convertToResult1(kilogramToGramAndMilligram 1000.0)
            .convertToResult2(kilogramToGramAndMilligram 1000000.0)
            .changeTo1("/massGram")
            .changeToButton1("Gram")
            .changeTo2("/massMilligram")
            .changeToButton2("Milligram")
            .Doc()