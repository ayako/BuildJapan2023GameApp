// バックエンドから Frame オブジェクトを処理するための SignalR 接続を構成するコードをJavaScriptで生成してください。
// メッセージ名は updateFrame, URL は /gameHub です。 



// バックエンドから取得する Frame オブジェクトをレンダリングする JavaScriptのコードを生成してください。
// すべての小惑星、弾、宇宙船、そしてそれらの爆発を描くために Frame オブジェクトを使用します。 
// 要件は以下の通りです: 
// 1.複数のユーザーが参加できるようにして、それぞれのユーザーがそれぞれ宇宙船を操作します。
// 2.宇宙船の下中央にはユーザー名を白字で表示します。
// 3.ユーザーが死亡判定されたら、ユーザー名を赤字で表示して、宇宙船を透過度50％にします。
// 4.宇宙船は ship.png, 小惑星は asteroid.png, 弾はbullet.png, 爆発は explosion_1.pngを使って描きます。
// 背景はbackground.pngを使います。 
// 5.すべてのユーザーの名前とスコアをcampus の上部、id=scoreboard divに表示します。
// 6.爆発はexplosion_1.pngのスプライトシートで表現します。 
// Frameオブジェクトのサンプルは以下の通りです:
// { "id": 665, "asteroidsCount": 5, "players": [ { "connectionId": "xGrPljK_x0Ga5KjD5Khurg", "name": "Ayako", "x": 813, "y": 658, "angle": 0, "speed": 0, "acceleration": 0, "size": 30, "score": 0, "isDead": false } ], "asteroids": [ { "x": 356.69278, "y": 161.86937, "dx": 0.3062996, "dy": -1.408675, "size": 87 }, { "x": 1699.2211, "y": 437.4298, "dx": 0.4544387, "dy": 1.148027, "size": 70 }, { "x": 1288.5944, "y": 628.1949, "dx": 0.10316945, "dy": 0.57623833, "size": 67 }, { "x": 1338.259, "y": 237.97853, "dx": 0.08912832, "dy": -1.4015363, "size": 68 }, { "x": -27.819336, "y": 301.5507, "dx": 1.0368958, "dy": -0.081866406, "size": 61 } ], "bullets": [ { "x": -27.819336, "y": 301.5507 } ], "explosions": [ { "x": -27.819336, "y": 301.5507 } ], "state": 0 }
















const canvas = document.getElementById("canvas");
const ctx = canvas.getContext("2d");
const scoreboard = document.getElementById("scoreboard");

const images = {};
const imageNames = ["ship", "asteroid", "bullet", "explosion_1", "background"];
imageNames.forEach((name) => {
    const img = new Image();
    img.src = `${name}.png`;
    images[name] = img;
});

function render(frame) {
    // Clear the canvas
    ctx.clearRect(0, 0, canvas.width, canvas.height);

    // Draw the background
    ctx.drawImage(images.background, 0, 0, canvas.width, canvas.height);

    // Draw the asteroids
    frame.asteroids.forEach((asteroid) => {
        ctx.drawImage(images.asteroid, asteroid.x, asteroid.y, asteroid.size, asteroid.size);
    });

    // Draw the bullets
    frame.bullets.forEach((bullet) => {
        ctx.drawImage(images.bullet, bullet.x, bullet.y, 10, 10);
    });

    // Draw the explosions
    frame.explosions.forEach((explosion) => {
        const spriteWidth = 64;
        const spriteHeight = 64;
        const spriteX = Math.floor(explosion.frame / 4) * spriteWidth;
        const spriteY = (explosion.type - 1) * spriteHeight;
        ctx.drawImage(images.explosion_1, spriteX, spriteY, spriteWidth, spriteHeight, explosion.x, explosion.y, spriteWidth, spriteHeight);
    });

    // Draw the spaceships
    frame.players.forEach((player) => {
        const img = images.ship;
        const x = player.x - player.size / 2;
        const y = player.y - player.size / 2;
        const angle = player.angle;
        const alpha = player.isDead ? 0.5 : 1.0;
        ctx.save();
        ctx.translate(x + player.size / 2, y + player.size / 2);
        ctx.rotate(angle);
        ctx.globalAlpha = alpha;
        ctx.drawImage(img, -player.size / 2, -player.size / 2, player.size, player.size);
        ctx.restore();

        // Draw the player name
        ctx.fillStyle = player.isDead ? "red" : "white";
        ctx.font = "16px Arial";
        ctx.textAlign = "center";
        ctx.fillText(player.name, player.x, player.y + player.size / 2 + 20);
    });

    // Update the scoreboard
    scoreboard.innerHTML = "";
    frame.players.forEach((player) => {
        const div = document.createElement("div");
        div.innerHTML = `${player.name}: ${player.score}; `;
        scoreboard.appendChild(div);
    });
}