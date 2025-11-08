namespace TicTacToe.GameCore;

using TicTacToe.Enums;

public sealed class Symbol {
	public readonly SymbolType Type;
	
	public Symbol(SymbolType symbolType) {
		Type = symbolType;
	}
	
	public override string ToString() => $" {Type} ";
}