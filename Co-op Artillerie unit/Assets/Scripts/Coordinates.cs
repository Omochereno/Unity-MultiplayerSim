using System;

namespace Models
{
	[Serializable]
	public class Coordinates
	{
		public int id;

		public int x;

		public int y;

		public override string ToString(){
			return UnityEngine.JsonUtility.ToJson (this, true);
		}
	}
}

