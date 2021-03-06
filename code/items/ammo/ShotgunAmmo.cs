using Sandbox;

namespace TTT.Items
{
	[Library( "ttt_ammo_shotgun", Title = "Shotgun Ammo" )]
	[Spawnable]
	[Hammer.EditorModel( "models/ammo/ammo_shotgun/ammo_shotgun.vmdl" )]
	partial class ShotgunAmmo : TTTAmmo
	{
		public override SWB_Base.AmmoType AmmoType => SWB_Base.AmmoType.Shotgun;
		public override int Amount => 8;
		public override int Max => 36;
		public override string ModelPath => "models/ammo/ammo_shotgun/ammo_shotgun.vmdl";
	}
}
