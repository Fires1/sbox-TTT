using System.Collections.Generic;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using TTT.Items;
using TTT.Player;

namespace TTT.UI
{
	public class PerkDisplay : Panel
	{
		private readonly Dictionary<Perk, PerkSlot> _entries = new();

		public PerkDisplay()
		{
			StyleSheet.Load( "/ui/generalhud/inventorywrapper/perkdisplay/PerkDisplay.scss" );
		}

		public override void Tick()
		{
			base.Tick();

			if ( Local.Pawn is not TTTPlayer player )
			{
				return;
			}

			for ( int i = 0; i < player.Perks.Count; ++i )
			{
				var perk = player.Perks.Get( i );
				if ( !_entries.ContainsKey( perk ) )
				{
					_entries[perk] = AddPerkSlot( perk );
				}
			}

			foreach ( var keyValue in _entries )
			{
				var perk = keyValue.Key;
				var slot = keyValue.Value;

				if ( !player.Perks.Contains( perk ) )
				{
					_entries.Remove( perk );
					slot?.Delete();
				}
			}
		}

		public PerkSlot AddPerkSlot( Perk perk )
		{
			var perkSlot = new PerkSlot( perk );
			AddChild( perkSlot );
			return perkSlot;
		}

		public class PerkSlot : Panel
		{
			private readonly Perk _perk;
			private readonly Label _name;
			private readonly Image _image;
			private readonly Label _activeLabel;

			public PerkSlot( Perk perk )
			{
				_perk = perk;
				var item = perk as IItem;

				var panel = Add.Panel( "icon-panel" );
				_image = panel.Add.Image();
				_image.SetImage( $"/ui/icons/{item.GetItemData().Library.Name}.png" );

				_activeLabel = panel.Add.Label( "", "active" );
				_activeLabel.AddClass( "text-shadow" );
				_activeLabel.AddClass( "centered" );

				_name = Add.Label( item.GetItemData().Title, "name" );
				_name.AddClass( "text-shadow" );
			}

			public override void Tick()
			{
				base.Tick();
				_activeLabel.Text = _perk.ActiveText();
			}
		}
	}
}
