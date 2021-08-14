namespace Shubot.Modules
{
	using System;
	using System.Threading.Tasks;

	using Discord.Commands;

	public class MathModule : ModuleBase<SocketCommandContext>
    {
		// .square 20 -> 400
		[Command("square")]
		[Summary("Squares a number.")]
		public async Task SquareAsync(
			[Summary("The number to square.")]
		int num)
		{
			// We can also access the channel from the Command Context.
			await Context.Channel.SendMessageAsync($"{num}^2 = {Math.Pow(num, 2)}");
		}
	}
}