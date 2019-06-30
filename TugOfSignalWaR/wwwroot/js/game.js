﻿"use strict";
var tugOfSingalWaR = {};
(function () {
    var connection = null;
    var team = null;

    function bootstrapHub(connectedCallback) {
        connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();
        connection.on("GameUpdated", onGameUpdated);

        connection.start()
            .then(function () {
                if (!connectedCallback) return;
                connectedCallback();
            })
            .catch(function (err) {
                return console.error(err.toString());
            });
    }

    function onGameJoined(joinGameResponse) {
        team = joinGameResponse.team;
        renderGame(team, joinGameResponse.gameState);
    }

    function onGameUpdated(gameState) {
        if (!team) return;

        renderGame(team, gameState);
    }

    function renderGame() {

    }

    function joinGame() {
        connection.invoke("JoinGame")
            .then(onGameJoined)
            .catch(function (err) {
                return console.error(err.toString());
            });
    }

    tugOfSingalWaR.pull = function () {
        connection.invoke("pull")
            .catch(function (err) {
                return console.error(err.toString());
            });
    }

    bootstrapHub(joinGame);
})();