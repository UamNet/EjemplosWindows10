
/**
 * Module dependencies.
 */

var express = require('express');
var routes = require('./routes');
var user = require('./routes/user');
var http = require('http');
var path = require('path');

var app = express();

// all environments
app.set('port', process.env.PORT || 3000);
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'jade');
app.use(express.favicon());
app.use(express.logger('dev'));
app.use(express.json());
app.use(express.urlencoded());
app.use(express.methodOverride());
app.use(app.router);
app.use(require('stylus').middleware(path.join(__dirname, 'public')));
app.use(express.static(path.join(__dirname, 'public')));

// development only
if ('development' == app.get('env')) {
    app.use(express.errorHandler());
}

app.get('/', routes.index);
app.get('/users', user.list);

//REST API

var quotes = [
    { Id: 1, Text: "No sabeis hacer makefiles" },
    { Id: 2, Text: "Se masca la tragedia" },
];

app.get('/api/quotes', function (req, res) {
    res.setHeader('Cache-Control','no-cache');

    res.json(quotes);
});

app.get('/api/quotes/:id', function (req, res) {
    var q = quotes.filter(function (x) { return x.Id = req.params.id });
    if (q.length == 0) {
        res.statusCode = 404;
        return res.send('Error 404: No quote found');
    }
    res.json(q[0]);
});

app.post('/api/quotes/new/:text', function (req, res) {
    var q = { Text: req.params.text };
    q.Id = 1+quotes.
                map(function (x) { return x.Id }).
                reduce(function (x, y) { return x>y?x:y});
    quotes.push(q);
    res.json(q);
});

//REST API END

http.createServer(app).listen(app.get('port'), function () {
    console.log('Express server listening on port ' + app.get('port'));
});

