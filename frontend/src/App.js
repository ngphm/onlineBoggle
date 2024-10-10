import { useState, useEffect } from "react";

function Square({ value, highlight = false, onSquareClick }) {
    return (
        <button
            className={highlight ? "square-highlight" : "square"}
            onClick={onSquareClick}
        >
            {value}
        </button>
    );
}

function Reset({ enabled, onResetClick }) {
    if (enabled) {
        return (
            <button className={"reset-button"} onClick={onResetClick}>
                Play Again
            </button>
        );
    } else {
        return;
    }
}

const Timer = ({ time }) => {
    if (time > 0) {
        return (
            <div
                className="timer"
                style={{ color: time < 60000 && time > 0 ? "red" : "black" }}
            >
                {Math.floor((time / 1000 / 60) % 60)}:
                {("00" + Math.floor((time / 1000) % 60)).slice(-2)}
            </div>
        );
    } else {
        return <div className="timer">Time's up!</div>;
    }
};

export default function Board() {
    const baseLetters = [
        "g",
        "a",
        "u",
        "t",
        "p",
        "r",
        "m",
        "r",
        "d",
        "o",
        "l",
        "a",
        "e",
        "s",
        "i",
        "c",
    ];
    const [squares, setSquares] = useState(baseLetters);
    const [selected, setSelected] = useState(Array(0));
    const [storedWords, setStoredWords] = useState(Array(0));
    const [score, setScore] = useState(0);
    const [highScore, SetHighScore] = useState(0);
    const [active, setActive] = useState(true);
    const gameTimeMs = 180000;
    const [timeLeft, setTimeLeft] = useState(gameTimeMs);
    const [deadline, setDeadline] = useState(Date.now() + gameTimeMs);
    const wordList = storedWords.map((element, index) => (
        <li key={element + index.toString()}>{element}</li>
    ));

    function getTime() {
        if (deadline - Date.now() < 0) {
            setActive(false);
            endGame();
        }
        if (active) {
            setTimeLeft(deadline - Date.now());
        }
    }

    useEffect(() => {
        const interval = setInterval(() => getTime(deadline), 1000);

        return () => clearInterval(interval);
    }, [timeLeft, active]);

    useEffect(() => {
        var requestOptions = {
            method: "GET",
            redirect: "follow",
        };

        fetch("http://localhost:5217/board", requestOptions)
            .then((response) => response.json())
            .then((result) => setSquares(result["state"]))
            .catch((error) => console.log("error", error));
    }, []);

    function isIndexSelected(i) {
        return selected.findIndex((n) => n == i) != -1;
    }

    function endGame() {
        setActive(false);
        if (score > highScore) {
            SetHighScore(score);
        }
        setSelected([]);
        setTimeLeft(0);
        setDeadline(Date.now() + gameTimeMs);
    }

    function resetGame() {
        var requestOptions = {
            method: "GET",
            redirect: "follow",
        };

        fetch("http://localhost:5217/board", requestOptions)
            .then((response) => response.json())
            .then((result) => setSquares(result["state"]))
            .catch((error) => console.log("error", error));
        setActive(true);
        setStoredWords([]);
        setSelected([]);
        setScore(0);
        setTimeLeft(gameTimeMs);
        setDeadline(Date.now() + gameTimeMs);
    }

    function handleSquareClick(i) {
        if (!active) {
            return;
        }

        const nextSquares = squares.slice();
        if (i == selected[selected.length - 1]) {
            //prevents short or duplicate submissions
            console.log();
            if (
                selected.length > 2 &&
                storedWords.findIndex((n) => n == indexToString(selected)) == -1
            ) {
                setStoredWords([...storedWords, indexToString(selected)]);
                var myHeaders = new Headers();
                myHeaders.append("Content-Type", "application/json");

                var raw = JSON.stringify({
                    Letters: indexToString(selected),
                });

                var requestOptions = {
                    method: "POST",
                    headers: myHeaders,
                    body: raw,
                    redirect: "follow",
                };

                fetch("http://localhost:5217/board", requestOptions)
                    .then((response) => response.json())
                    .then((result) => setScore(score + result["score"]))
                    .catch((error) => console.log("error", error));
            }
            setSelected([]);
            return;
        } else if (isIndexSelected(i)) {
            setSelected(selected.slice(0, selected.findIndex((n) => n == i) + 1));
            return;
        } else if (isAdjacentToNext(i)) {
            setSelected([...selected, i]);
            setSquares(nextSquares);
        }
    }

    function isAdjacentToNext(i) {
        if (selected.length == 0) {
            return true;
        }
        //oh dios mio
        const lastCell = selected[selected.length - 1];
        const column = lastCell % 4; //assumes a 4x4 grid
        const row = Math.floor(lastCell / 4);
        let check = false;
        if (column != 0) {
            if (i == lastCell - 1) {
                check = true;
            } //left
            if (row != 0 && i == lastCell - 5) {
                check = true;
            } //top-left
            if (row != 3 && i == lastCell + 3) {
                check = true;
            } //lower-left
        }
        if (column != 3) {
            if (i == lastCell + 1) {
                check = true;
            } //right
            if (row != 0 && i == lastCell - 3) {
                check = true;
            } //top-right
            if (row != 3 && i == lastCell + 5) {
                check = true;
            } //lower-right
        }
        if (row != 0 && i == lastCell - 4) {
            check = true;
            //above
        }
        if (row != 3 && i == lastCell + 4) {
            check = true;
            //below
        }

        return check != 0;
    }

    function indexToString(array) {
        return array
            .map((tile) => squares[tile])
            .toString()
            .replaceAll(",", "")
            .replaceAll("q", "qu");
    }

    return (
        <>
            <Timer time={timeLeft} />
            <div className="column">
                <div className="board-row">
                    <Square
                        highlight={isIndexSelected(0)}
                        value={squares[0]}
                        onSquareClick={() => handleSquareClick(0)}
                    />
                    <Square
                        highlight={isIndexSelected(1)}
                        value={squares[1]}
                        onSquareClick={() => handleSquareClick(1)}
                    />
                    <Square
                        highlight={isIndexSelected(2)}
                        value={squares[2]}
                        onSquareClick={() => handleSquareClick(2)}
                    />
                    <Square
                        highlight={isIndexSelected(3)}
                        value={squares[3]}
                        onSquareClick={() => handleSquareClick(3)}
                    />
                </div>
                <div className="board-row">
                    <Square
                        highlight={isIndexSelected(4)}
                        value={squares[4]}
                        onSquareClick={() => handleSquareClick(4)}
                    />
                    <Square
                        highlight={isIndexSelected(5)}
                        value={squares[5]}
                        onSquareClick={() => handleSquareClick(5)}
                    />
                    <Square
                        highlight={isIndexSelected(6)}
                        value={squares[6]}
                        onSquareClick={() => handleSquareClick(6)}
                    />
                    <Square
                        highlight={isIndexSelected(7)}
                        value={squares[7]}
                        onSquareClick={() => handleSquareClick(7)}
                    />
                </div>
                <div className="board-row">
                    <Square
                        highlight={isIndexSelected(8)}
                        value={squares[8]}
                        onSquareClick={() => handleSquareClick(8)}
                    />
                    <Square
                        highlight={isIndexSelected(9)}
                        value={squares[9]}
                        onSquareClick={() => handleSquareClick(9)}
                    />
                    <Square
                        highlight={isIndexSelected(10)}
                        value={squares[10]}
                        onSquareClick={() => handleSquareClick(10)}
                    />
                    <Square
                        highlight={isIndexSelected(11)}
                        value={squares[11]}
                        onSquareClick={() => handleSquareClick(11)}
                    />
                </div>
                <div className="board-row">
                    <Square
                        highlight={isIndexSelected(12)}
                        value={squares[12]}
                        onSquareClick={() => handleSquareClick(12)}
                    />
                    <Square
                        highlight={isIndexSelected(13)}
                        value={squares[13]}
                        onSquareClick={() => handleSquareClick(13)}
                    />
                    <Square
                        highlight={isIndexSelected(14)}
                        value={squares[14]}
                        onSquareClick={() => handleSquareClick(14)}
                    />
                    <Square
                        highlight={isIndexSelected(15)}
                        value={squares[15]}
                        onSquareClick={() => handleSquareClick(15)}
                    />
                </div>
                <h2>
                    <div className="wordOut">{indexToString(selected)}</div>
                </h2>
                <div
                    style={{
                        display: "flex",
                        justifyContent: "center",
                        alignItems: "top",
                    }}
                >
                    <Reset enabled={!active} onResetClick={() => resetGame()} />
                </div>
                <div
                    style={{
                        display: "flex",
                        justifyContent: "center",
                        alignItems: "top",
                        color: highScore == score ? "gold" : "black",
                    }}
                >
                    {!active && highScore > 0
                        ? "Best Score: " + highScore.toString()
                        : ""}
                </div>
                <div
                    style={{
                        display: "flex",
                        justifyContent: "center",
                        alignItems: "top",
                    }}
                >
                    <b>
                        {!active && highScore == score && highScore > 0 ? "New Best!" : ""}
                    </b>
                </div>
            </div>
            <div className="column">
                <h1>Submitted:</h1>
                <h3>Score: {score}</h3>
                <ul>{wordList}</ul>
            </div>
        </>
    );
}
