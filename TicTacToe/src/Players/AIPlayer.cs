namespace TicTacToe.Players;

using TicTacToe.Enums;
using TicTacToe.GameCore;

public sealed class AIPlayer {
	private readonly Game _game;
	private readonly double _errorChance;
	private readonly Random _random = new Random();
	private readonly Symbol _symbol = new Symbol(SymbolType.O);
	
	public AIPlayer(Game game) {
		_game = game;
		_errorChance = Program.DIFFICULTY_LEVEL switch {
			Difficulty.Easy => 0.75,
			Difficulty.Medium => 0.45,
			Difficulty.Hard => 0.15,
			_ => 0.0,
		};
	}
	
	public void Play() {
		(sbyte row, sbyte col) move;
		
		if (_random.NextDouble() >= _errorChance)
			move = FindBestMovement();
		else
			move = FindRandomMovement();
		
		if (_game.Board.CanPlayAt(move.row, move.col))
			_game.Board.PlayAt(move.row, move.col, _symbol);
	}
	
	private (sbyte, sbyte) FindRandomMovement() {
		List<(sbyte, sbyte)> disponibleMoves = new List<(sbyte, sbyte)>();
		
		for (sbyte i = 1; i < _game.Board.TotalSquares + 1; i++) {
			if (_game.TryAccessMatrix(i, out (sbyte row, sbyte col) move)) {
				if (_game.Board.CanPlayAt(move.row, move.col)) {
					disponibleMoves.Add(move);
				}
			}
		}
		
		int randomIndex = _random.Next(disponibleMoves.Count);
		return disponibleMoves[randomIndex];
	}
	
	private (sbyte, sbyte) FindBestMovement() {
		double bestScore = double.MinValue;
		(sbyte, sbyte) move = default;
		
		for (sbyte i = 0; i < GameBoard.BOARD_SIZE; i++) {
			for (sbyte j = 0; j < GameBoard.BOARD_SIZE; j++) {
				if (_game.Board.CanPlayAt(i, j)) {
					_game.Board.PlayAt(i, j, _symbol);
					double score = MiniMax(1);
					_game.Board.UndoMoveAt(i, j);
					
					if (score > bestScore) {
						bestScore = score;
						move = (i, j);
					}
				}
			}
		}
		
		return move;
	}
	
	private double MiniMax(sbyte depth, bool isMaximizing = false) {
		if (GamedEnded(out double score))
			return score;
		
		double bestScore = isMaximizing ? -1 : 1;
		Symbol symbol = isMaximizing ? _symbol : _game.Player.Symbol;
		for (sbyte i = 0; i < GameBoard.BOARD_SIZE; i++) {
			for (sbyte j = 0; j < GameBoard.BOARD_SIZE; j++) {
				if (_game.Board.CanPlayAt(i, j)) {
					_game.Board.PlayAt(i, j, symbol);
					score = MiniMax(--depth, !isMaximizing);
					_game.Board.UndoMoveAt(i, j);
					
					if (isMaximizing)
						bestScore = Math.Max(score, bestScore);
					else
						bestScore = Math.Min(score, bestScore);
				}
			}
		}
		
		return bestScore;
	}
	
	private bool GamedEnded(out double score) {
		if (_game.CheckWinner(out Symbol? result)) {
			score = result?.Type is SymbolType.X ? -2 : 2;
			return true;
		}
		
		score = 0;
		return _game.Board.AllSquaresAreFilled;
	}
}