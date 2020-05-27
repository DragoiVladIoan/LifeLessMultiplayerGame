var io = require('socket.io')(process.env.PORT || 3000);
var Player = require('./Player.js');

console.log('server started');

var players = [];
var sockets = [];
var randomSeed = 1;

io.on('connection', function (socket) {

    console.log("connection made!");
    var player = new Player();
    var playerId = player.id;

    players[playerId] = player;
    player.count = Object.keys(players).length;

    sockets[playerId] = socket;

    //first player connected generates a random spawn seed
    if (Object.keys(players).length == 1) {
        randomSeed = player.random();
        player.randomSpawnSeed = randomSeed;

        console.log(player.randomSpawnSeed);

    }
    else {
        player.randomSpawnSeed = randomSeed;
        console.log(player.randomSpawnSeed);
    }

    console.log("player registered!");
    socket.emit('register', {id: playerId});

    console.log("spawning!");
    socket.emit('spawn', player); //client spawned
    socket.broadcast.emit('spawn', player); //other clients know I have spawned

    //spawn everybody else for me
    Object.keys(players).forEach(function(id) {
        if (id != playerId) {
            socket.emit('spawn', players[id]);
        }
    });

    socket.on('move', function (data) {
        player.position.x = data.position.x;
        player.position.y = data.position.y;

        socket.broadcast.emit('move', player); 
    });

    socket.on('dash', function (data) {
        player.position.x = data.position.x;
        player.position.y = data.position.y;
        player.scale.x = data.scale.x;
        player.scale.y = data.scale.y;

        socket.broadcast.emit('dash', player); 
    });

    socket.on('disconnect', function () {
        console.log('client disconnected');
        delete players[playerId];
        delete sockets[socket];
        socket.broadcast.emit('disconnected', player);
    });
})