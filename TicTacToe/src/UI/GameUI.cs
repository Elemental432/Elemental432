namespace TicTacToe.UI;

using System.Text;
using TicTacToe.Enums;
using TicTacToe.GameCore;

public sealed class GameUI {
	private readonly Game _game;
	
	public GameUI(Game game) {
		_game = game;
	}
	
	public void NewGame() {
		Console.Clear();
		this.ShowBoard();
	}
	
	public void EntryChoice()
		=> Console.Write("Choose a number between 1 and 9: ");
	
	public void InvalidMove()
		=> Console.WriteLine("Invalid move. Try again.");
	
	public string? GetInput()
		=> Console.ReadLine();
	
	public async Task Delay(int timer)
		=> await Task.Delay(timer);
	
	public void ShowResults() {
		if (_game.CheckWinner(out Symbol? result)) {
			if (result?.Type is SymbolType.X)
				Console.WriteLine("You win!");
			else
				Console.WriteLine("You lose!");
		} else if (_game.Board.AllSquaresAreFilled) {
			Console.WriteLine("It's a tie!");
		} else {
			Console.WriteLine("The game is not over yet.");
		}
	}
	
	public void ShowBoard() {
		StringBuilder sb = new StringBuilder();
		sbyte i = 1;
		
		foreach (var row in _game.Board.Matrix) {
			sb.Append(string.Join("|", row.Select(square => square is null ? new string(' ', 3) : square.ToString())));
			sb.Append('\n');
			
			if (i != GameBoard.BOARD_SIZE) {
				string text = new string('-', (GameBoard.BOARD_SIZE * 3) + (GameBoard.BOARD_SIZE - 1));
				sb.Append(text);
				sb.Append('\n');
			}
			++i;
		}
		
		Console.WriteLine(string.Join("", sb));
	}
}