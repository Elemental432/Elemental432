namespace TicTacToe.GameCore;

using TicTacToe.Enums;

public sealed class GameBoard {
	// Keep the 'BOARD_SIZE' at 3
	// Otherwise MiniMax will calculate infinite possibilities (Using the maximum of the machine's hardware)
	public const sbyte BOARD_SIZE = 3;
	
	public readonly ushort TotalSquares;
	
	public readonly List<List<Symbol?>> Matrix = new List<List<Symbol?>>();
	public bool AllSquaresAreFilled => Matrix.All(rows => rows.All(col => col is not null));
	
	public GameBoard() {
		TotalSquares = BOARD_SIZE * BOARD_SIZE;
		
		for (sbyte row = 0; row < BOARD_SIZE; row++) {
			Matrix.Add(new List<Symbol?>());
			for (sbyte i = 0; i < BOARD_SIZE; i++)
				Matrix[row].Add(null);
		}
	}
	
	public bool CanPlayAt(sbyte row, sbyte col)
		=> Matrix[row][col] is null;
	
	public void PlayAt(sbyte row, sbyte col, Symbol symbol)
		=> Matrix[row][col] = symbol;
	
	public void UndoMoveAt(sbyte row, sbyte col)
		=> Matrix[row][col] = null;
	
	public bool CheckRows(out Symbol? symbol) {
		foreach (var row in Matrix) {
			var x = row.OfType<Symbol>().Where(s => s.Type is SymbolType.X).ToArray();
			var o = row.OfType<Symbol>().Where(s => s.Type is SymbolType.O).ToArray();
			
			if (x.Length == BOARD_SIZE) {
				symbol = x.FirstOrDefault();
				return true;
			} else if (o.Length == BOARD_SIZE) {
				symbol = o.FirstOrDefault();
				return true;
			}
		}
		
		symbol = null;
		return false;
	}
	
	public bool CheckCols(out Symbol? symbol) {
		for (sbyte col = 0; col < GameBoard.BOARD_SIZE; col++) {
			var column = new List<Symbol?>();
			
			for (sbyte row = 0; row < GameBoard.BOARD_SIZE; row++)
				column.Add(Matrix[row][col]);
			
			var x = column.OfType<Symbol>().Where(s => s.Type is SymbolType.X).ToArray();
			var o = column.OfType<Symbol>().Where(s => s.Type is SymbolType.O).ToArray();
			
			if (x.Length == GameBoard.BOARD_SIZE) {
				symbol = x.FirstOrDefault();
				return true;
			} else if (o.Length == GameBoard.BOARD_SIZE) {
				symbol = o.FirstOrDefault();
				return true;
			}
		}
		
		symbol = null;
		return false;
	}
	
	public bool CheckLeftToRightDiag(out Symbol? symbol) {
		var diag = new List<Symbol?>();
		
		for (sbyte i = 0; i < GameBoard.BOARD_SIZE; i++)
			diag.Add(Matrix[i][i]);
		
		var x = diag.OfType<Symbol>().Where(s => s.Type is SymbolType.X).ToArray();
		var o = diag.OfType<Symbol>().Where(s => s.Type is SymbolType.O).ToArray();
		
		if (x.Length == GameBoard.BOARD_SIZE) {
			symbol = x.FirstOrDefault();
			return true;
		} else if (o.Length == GameBoard.BOARD_SIZE) {
			symbol = o.FirstOrDefault();
			return true;
		}
		
		symbol = null;
		return false;
	}
	
	public bool CheckRightToLeftDiag(out Symbol? symbol) {
		var diag = new List<Symbol?>();
		sbyte row = 0;
		
		for (sbyte col = (GameBoard.BOARD_SIZE - 1); col >= 0; col--)
			diag.Add(Matrix[row++][col]);
		
		var x = diag.OfType<Symbol>().Where(s => s.Type is SymbolType.X).ToArray();
		var o = diag.OfType<Symbol>().Where(s => s.Type is SymbolType.O).ToArray();
		
		if (x.Length == GameBoard.BOARD_SIZE) {
			symbol = x.FirstOrDefault();
			return true;
		} else if (o.Length == GameBoard.BOARD_SIZE) {
			symbol = o.FirstOrDefault();
			return true;
		}
		
		symbol = null;
		return false;
	}
}