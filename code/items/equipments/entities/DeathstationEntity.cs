using Sandbox;

using TTT.Player;
using TTT.UI;

namespace TTT.Items
{
	[Library( "ttt_equipment_deathstation_ent", Title = "Death Station" )]
	[Precached( "models/health_station/health_station.vmdl" )]
	public partial class DeathstationEntity : Prop, IEntityHint
	{
		[Net]
		public float StoredDamage { get; set; } = 200f;

		public override string ModelPath => "models/health_station/health_station.vmdl";

		public override void Spawn()
		{
			base.Spawn();

			SetModel( ModelPath );
			SetupPhysicsFromModel( PhysicsMotionType.Dynamic );
		}

		public float HintDistance => TTTPlayer.INTERACT_DISTANCE;

		public string TextOnTick => $"Hold {Input.GetButtonOrigin( InputButton.Use ).ToUpper()} to use the Health Station. ({StoredDamage} charges)";

		public bool CanHint( TTTPlayer client )
		{
			return true;
		}

		public EntityHintPanel DisplayHint( TTTPlayer client )
		{
			return new Hint( TextOnTick );
		}

		public void Tick( TTTPlayer player )
		{
			if ( IsClient )
			{
				return;
			}

			if ( player.LifeState != LifeState.Alive )
			{
				return;
			}

			using ( Prediction.Off() )
			{
				if ( Input.Down( InputButton.Use ) )
				{
					if ( StoredDamage > 0 )
					{
						StoredDamage -= player.Health;
						player.TakeDamage( DamageInfo.Generic( 1000 ) );
					}
				}
			}
		}
	}
}
