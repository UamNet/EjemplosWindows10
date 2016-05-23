(function() {
  'use strict';
  var app = WinJS.Application;
  var activation = Windows.ApplicationModel.Activation;
  app.onactivated = function (args) {
    if (args.detail.kind === activation.ActivationKind.launch) {
      if (args.detail.previousExecutionState !== activation.ApplicationExecutionState.terminated) {
          // TODO: This application has been newly launched. Initialize your application here.
          document.getElementById("send").addEventListener("click", sendQuote);
          document.getElementById("refresh").addEventListener("click", refreshList);
          WinJS.Namespace.define("Sample.ListView", {
              data: quotesData
          });
          WinJS.UI.processAll();
          refreshList();
      } else {
        // TODO: This application has been reactivated from suspension.
        // Restore application state here.
      }
    }
  };
  app.oncheckpoint = function (args) {
    // TODO: This application is about to be suspended. Save any state that needs to persist across suspensions here.
    // You might use the WinJS.Application.sessionState object, which is automatically saved and restored across suspension.
    // If you need to complete an asynchronous operation before your application is suspended, call args.setPromise().
  };
  app.start();
}());

var quotesData = new WinJS.Binding.List([]);

function refreshList() {
    var http_request = new XMLHttpRequest();
    http_request.onreadystatechange = function () {
        if (http_request.readyState == 4) {
            if (http_request.status == 200) {
                while (quotesData.length > 0) {
                    quotesData.pop();
                }
                JSON.parse(http_request.responseText).forEach(function (x) {
                    quotesData.push(x);
                });
            } else {
                document.getElementById("status").innerHTML = "Se ha producido un error en el servidor";
            }
        }
    };
    http_request.open('GET', "http://localhost:1337/api/quotes/", true);
    http_request.send();
}

function sendQuote() {
    var http_request = new XMLHttpRequest();
    http_request.onreadystatechange = function () {
        if (http_request.readyState == 4) {
            if (http_request.status == 200) {
                document.getElementById("status").innerHTML = "Cita añadida con éxito";
                refreshList();
            } else {
                document.getElementById("status").innerHTML = "Se ha producido un error en el servidor";
            }
        }
    };
    var value=document.getElementById("quoteText").value;
    http_request.open('POST', "http://localhost:1337/api/quotes/new/"+value, true);
    http_request.send("");
}



