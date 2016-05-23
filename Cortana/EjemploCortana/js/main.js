if (typeof Windows !== 'undefined' &&
    typeof Windows.UI !== 'undefined' &&
    typeof Windows.ApplicationModel !== 'undefined') {
    // Subscribe to the Windows Activation Event
    Windows.UI.WebUI.WebUIApplication.addEventListener("activated", function (args) {
        var activation = Windows.ApplicationModel.Activation;
        // Check to see if the app was activated by a voice command
        if (args.kind === activation.ActivationKind.voiceCommand) {
            // Get the speech reco
            var speechRecognitionResult = args.result;
            var textSpoken = speechRecognitionResult.text;
            // Determine the command type {search} defined in vcd
            if (speechRecognitionResult.rulePath[0] === "Play") {
                window.location = "https://www.youtube.com/embed/TrcT7sseLZI?autoplay=1";
            }
            else {
                console.log("No valid command specified");
            }
        }
    });

} else {
    console.log("Windows namespace is unavaiable");
}

/*Para c#
añadir codigo al metodo OnActivated(IActivatedEventArgs e)
https://msdn.microsoft.com/en-us/library/windows/apps/xaml/dn630430(v=win.10).aspx
*/
