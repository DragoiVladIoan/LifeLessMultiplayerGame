var shortid = require('shortid');
var Vector2 = require('./Vector2.js');

module.exports = class Player{
    constructor() {
        this.id = shortid.generate();
        this.position = new Vector2();
        this.scale = new Vector2();
        this.count = 0;
        this.randomSpawnSeed = 1;
    }

    
    random() {
        return Math.floor(Math.random() * 100) + 1;
    }
}