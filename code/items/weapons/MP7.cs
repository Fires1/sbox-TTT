using System;
using Sandbox;

using SWB_Base;
using TTT.Player;
using TTT.UI;

namespace TTT.Items
{
	[Library( "ttt_weapon_mp7", Title = "MP7" )]
	[Shop( SlotType.Primary, 100 )]
	[Spawnable]
	[Precached( "models/weapons/v_mp7.vmdl", "models/weapons/w_mp7.vmdl" )]
	[Hammer.EditorModel( "models/weapons/w_mp7.vmdl" )]
	public class MP7 : WeaponBase, ICarriableItem, IEntityHint
	{
		public virtual ItemData GetItemData() { return _data; }
		private readonly ItemData _data = new( typeof( MP7 ) );
		public Type DroppedType => typeof( SMGAmmo );

		public override float TuckRange => -1f;
		public override int Bucket => 1;
		public override HoldType HoldType => HoldType.Rifle;
		public override string HandsModelPath => "models/weapons/v_arms_ter.vmdl";
		public override string ViewModelPath => "models/weapons/v_mp7.vmdl";
		public override AngPos ViewModelOffset => new()
		{
			Angle = new Angles( 0, -5, 0 ),
			Pos = new Vector3( -5, 0, 0 )
		};
		public override string WorldModelPath => "models/weapons/w_mp7.vmdl";
		public override string Icon => "";
		public override int FOV => 75;
		public override int ZoomFOV => 75;

		public MP7()
		{
			General = new WeaponInfo
			{
				DrawTime = 1.4f,
				ReloadTime = 2.6f
			};

			Primary = new ClipInfo
			{
				Ammo = 30,
				AmmoType = AmmoType.SMG,
				ClipSize = 30,

				BulletSize = 4f,
				Damage = 6f,
				Force = 1f,
				Spread = 0.03f,
				Recoil = 1f,
				RPM = 700,
				FiringType = FiringType.auto,
				ScreenShake = new ScreenShake
				{
					Length = 0.4f,
					Speed = 3.0f,
					Size = 0.35f,
					Rotation = 0.35f
				},

				DryFireSound = "dryfire_pistol-1",
				ShootSound = "mp7_fire-1",

				BulletEjectParticle = "particles/pistol_ejectbrass.vpcf",
				MuzzleFlashParticle = "particles/swb/muzzle/flash_medium.vpcf",

				InfiniteAmmo = 0
			};
		}

		public override void Simulate( Client client )
		{
			WeaponGenerics.Simulate( client, Primary, DroppedType );
			base.Simulate( client );
		}

		public float HintDistance => TTTPlayer.INTERACT_DISTANCE;
		public virtual string TextOnTick => WeaponGenerics.PickupText( _data.Library.Title );
		bool ICarriableItem.CanDrop() { return true; }
		public bool CanHint( TTTPlayer player ) { return true; }
		public EntityHintPanel DisplayHint( TTTPlayer player ) { return new Hint( TextOnTick ); }
		public void Tick( TTTPlayer player ) { WeaponGenerics.Tick( player, this ); }
	}
}


