using Sandbox;
using Sandbox.UI;
using TTT.Player;

namespace TTT.UI.Menu
{
	[UseTemplate]
	public partial class HomePage : Panel
	{
		private Button ShopEditorButton { get; set; }
		private Button ForceSpectatorButton { get; set; }

		public void GoToSettingsPage()
		{
			TTTMenu.Instance.AddPage( new SettingsPage() );
		}

		public void GoToKeyBindingsPage()
		{
			TTTMenu.Instance.AddPage( new KeyBindingsPage() );
		}


		public void GoToShopEditor()
		{
			// Call to server which sends down server data and then adds the ShopEditorPage.
			ShopEditorPage.ServerRequestShopEditorAccess();
		}

		public void GoToComponentTesting()
		{
			TTTMenu.Instance.AddPage( new ComponentTestingPage() );
		}

		public override void Tick()
		{
			if ( Local.Pawn is not TTTPlayer player )
			{
				return;
			}

			ShopEditorButton.SetClass( "inactive", !Local.Client.HasPermission( "shopeditor" ) );
			ForceSpectatorButton.Text = $"Force Spectator Mode ({(player.IsForcedSpectator ? "Enabled" : "Disabled")})";
		}

		public void ToggleForceSpectator()
		{
			TTTPlayer.ToggleForceSpectator();
		}
	}
}
