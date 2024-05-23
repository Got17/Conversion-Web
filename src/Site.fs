namespace MyConverter

open WebSharper
open WebSharper.Sitelets
open WebSharper.UI.Templating
open WebSharper.UI

// creates a type called "MyTemplate", which represents an HTML template loaded from the file "index.html" on the client side.

type MyTemplate = Template<"src/index.html", ClientLoad.FromDocument>

// Define an discriminated union type named EndPoint, which represents different pages of the web application
// Each case of the union corresponds to a specific endpoint and page of the application

type EndPoint =
    | [<EndPoint "/">] Home  // Represents the home page
    | [<EndPoint "/temperatureCelsius">] TemperatureCelsiusPage // Represents the temperature page with Celsius input
    | [<EndPoint "/temperatureKelvin">] TemperatureKelvinPage // Represents the temperature page with Kelvin input
    | [<EndPoint "/temperatureFahrenheit">] TemperatureFahrenheitPage // Represents the temperature page with Fahrenheit input
    | [<EndPoint "/lengthCentimeter">] LengthCentimeterPage // Represents the length page with Centimeter input
    | [<EndPoint "/lengthMeter">] LengthMeterPage // Represents the length page with Meter input
    | [<EndPoint "/lengthKilometer">] LengthKilometerPage // Represents the length page with Kilometer input
    | [<EndPoint "/massGram">] MassGramPage // Represents the mass page with Gram input
    | [<EndPoint "/massMilligram">] MassMilligramPage // Represents the mass page with Milligram input
    | [<EndPoint "/massKilogram">] MassKilogramPage // Represents the mass page with Kilogram input
    
    
// create Homepage of the Website
[<JavaScript>]
module Client =
    // Define a function Homepage, which generates the homepage of the web application
    let Homepage() = 
        // Use MyTemplate to render the homepage template
        MyTemplate.homepage()
            // Add a link to the Temperature Converter web page with Celsius input
            .TemperaturePage("/temperatureCelsius")
            // Add a link to the Mass Converter web page with Gram input
            .MassPage("/massGram") 
            // Add a link to the Length Converter web page with Centimeter input
            .LengthPage("/lengthCentimeter")
            .Doc()
   


module Site =
    open WebSharper.UI.Server

    open type WebSharper.UI.ClientServer


    [<Website>]
    let Main =
        // Use Application.MultiPage to handle multiple pages of the website
        Application.MultiPage (fun ctx -> function
            | EndPoint.Home -> 
                // Render the homepage
                Content.Page ([
                    // Use MyTemplate to render the homepage template
                    MyTemplate()
                        // Set the title of the page
                        .Title("Home Page")
                        // Add the homepage container with client-side content
                        .homepageContainer(client(Client.Homepage()))
                        .Doc()
                ])
            | EndPoint.TemperatureCelsiusPage -> 
                // Render the temperature converter page with Celsius input
                Content.Page ([
                    MyTemplate()
                        .Title("Temperature Converter")
                        .convertContainer1(client(TemperaturePage.CelsiusPage()))
                        .Doc()
                ])
            | EndPoint.TemperatureKelvinPage -> 
                // Render the temperature converter page with Kelvin input
                Content.Page ([
                    MyTemplate()
                        .Title("Temperature Converter")
                        .convertContainer2(client(TemperaturePage.KelvinPage()))
                        .Doc()
                ])
            | EndPoint.TemperatureFahrenheitPage -> 
                // Render the temperature converter page with Fahrenheit input
                Content.Page ([
                    MyTemplate()
                        .Title("Temperature Converter")
                        .convertContainer3(client(TemperaturePage.FahrenheitPage()))
                        .Doc()
                ])
            | EndPoint.LengthCentimeterPage ->
                // Render the length converter page with Centimeter input
                Content.Page ([
                    MyTemplate()
                        .Title("Length Converter")
                        .convertContainer1(client(LengthPage.CentimeterPage()))
                        .Doc()
                ])
            | EndPoint.LengthMeterPage ->
                // Render the length converter page with Meter input
                Content.Page ([
                    MyTemplate()
                        .Title("Length Converter")
                        .convertContainer2(client(LengthPage.MeterPage()))
                        .Doc()
                ])
            | EndPoint.LengthKilometerPage ->
                // Render the length converter page with Kilometer input
                Content.Page ([
                    MyTemplate()
                        .Title("Length Converter")
                        .convertContainer3(client(LengthPage.KilometerPage()))
                        .Doc()
                ])
            | EndPoint.MassGramPage ->
                // Render the mass converter page with Gram input
                Content.Page ([
                    MyTemplate()
                        .Title("Mass Converter")
                        .convertContainer1(client(MassPage.GramPage()))
                        .Doc()
                ])
            | EndPoint.MassMilligramPage ->
                // Render the mass converter page with Milligram input
                Content.Page ([
                    MyTemplate()
                        .Title("Mass Converter")
                        .convertContainer2(client(MassPage.MilligramPage()))
                        .Doc()
                ])
            | EndPoint.MassKilogramPage ->
                // Render the mass converter page with Kilogram input
                Content.Page ([
                    MyTemplate()
                        .Title("Mass Converter")
                        .convertContainer3(client(MassPage.KilogramPage()))
                        .Doc()
                ])
         )

