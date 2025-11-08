namespace TicTacToe.GameCore;

using TicTacToe.Enums;
using TicTacToe.Players;
using TicTacToe.UI;

public sealed class Game {
	public readonly GameBoard Board;
	public readonly GameUI UI;
	public readonly HumanPlayer Player;
	public readonly AIPlayer Bot;
	
	private readonly Dictionary<sbyte, (sbyte, sbyte)> _matrixIndexers = new Dictionary<sbyte, (sbyte, sbyte)>();
	
	public Game() {
		Board = new GameBoard();
		UI = new GameUI(this);
		Player = new HumanPlayer(this);
		Bot = new AIPlayer(this);
		
		sbyte i = 1;
		for (sbyte row = 0; row < GameBoard.BOARD_SIZE; row++) {
			for (sbyte col = 0; col < GameBoard.BOARD_SIZE; col++) {
				_matrixIndexers[i++] = (row, col);
			}
		}
	}
	
	public bool IsValidIndex(sbyte index) {
		if (index > 0 && index < (Board.TotalSquares + 1)) {
			if (TryAccessMatrix(index, out (sbyte row, sbyte col) move))
				return Board.CanPlayAt(move.row, move.col);
		}
		
		return false;
	}
	
	public bool TryAccessMatrix(sbyte index, out (sbyte, sbyte) tuple)
		=> _matrixIndexers.TryGetValue(index, out tuple);
	
	public bool CheckWinner(out Symbol? symbol) {
		if (Board.CheckRows(out symbol))
			return true;
		
		if (Board.CheckCols(out symbol))
			return true;
		
		if (Board.CheckLeftToRightDiag(out symbol))
			return true;
		
		if (Board.CheckRightToLeftDiag(out symbol))
			return true;
		
		symbol = null;
		return false;
	}
	
	public bool GamedEnded() {
		if (CheckWinner(out _))
			return true;
		
		return Board.AllSquaresAreFilled;
	}
}