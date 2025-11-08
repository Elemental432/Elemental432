namespace TicTacToe.Players;

using TicTacToe.Enums;
using TicTacToe.GameCore;

public sealed class HumanPlayer {
	private readonly Game _game;
	public readonly Symbol Symbol = new Symbol(SymbolType.X);
	
	public HumanPlayer(Game game) {
		_game = game;
	}
	
	public void Play(sbyte i) {
		if (_game.TryAccessMatrix(i, out (sbyte row, sbyte col) move)) {
			if (_game.Board.CanPlayAt(move.row, move.col))
				_game.Board.PlayAt(move.row, move.col, Symbol);
			else
				throw new Exception($"Cannot play at row {move.row}, col {move.col}.");
		} else {
			throw new Exception($"Could not find the index '{i}' in the matrix.");
		}
	}
}