using System.Collections.Generic;

namespace MIUP.GameName.MatrixModule.Domain {
	public static class MatrixDefinitions {
		#region ENUMS
		/// <summary>
		/// Cell directions.
		/// </summary>
		public enum CellDirections
		{
			Same,
			Right,
			Left,
			Top,
			TopRight,
			TopLeft,
			Under,
			UnderRight,
			UnderLeft,
			Behind,
			BehindRight,
			BehindLeft,
			Front,
			FrontRight,
			FrontLeft,
		}
		#endregion

        #region VARIABLES
        private const string KEY_BASE = "{0},{1}";

		private delegate void GetCoordinatesInDirectionDel(int row, int col, int z, out int outRow, out int outCol, out int outZ);

		private static Dictionary<CellDirections, GetCoordinatesInDirectionDel> GetCoordinatesInDirectionDelDict =
            new Dictionary<CellDirections, GetCoordinatesInDirectionDel>()
		{
			{MatrixDefinitions.CellDirections.Same, delegate(int row, int col, int z, out int outRow, out int outCol, out int outZ) 
				{
					outRow = row;
					outCol = col;
					outZ = z;
				} 
			},
			{MatrixDefinitions.CellDirections.Right, delegate(int row, int col, int z, out int outRow, out int outCol, out int outZ)
				{
					outRow = row;
					outZ = z;
					outCol = col + 1; 
				}
			},
			{MatrixDefinitions.CellDirections.Left, delegate(int row, int col, int z, out int outRow, out int outCol, out int outZ)
				{
					outRow = row;
					outZ = z;
					outCol = col - 1; 
				}
			},
			{MatrixDefinitions.CellDirections.Top, delegate(int row, int col, int z, out int outRow, out int outCol, out int outZ) 
				{
					outCol = col;
					outZ = z;
					outRow = row + 1; 
				} 
			},
			{MatrixDefinitions.CellDirections.TopRight, delegate(int row, int col, int z, out int outRow, out int outCol, out int outZ)
				{
					outZ = z;
					outCol = col + 1;
					outRow = row + 1;
				}
			},
			{MatrixDefinitions.CellDirections.TopLeft, delegate(int row, int col, int z, out int outRow, out int outCol, out int outZ) 
				{
					outZ = z;
					outCol = col - 1;
					outRow = row + 1;
				} 
			},
			{MatrixDefinitions.CellDirections.Under, delegate(int row, int col, int z, out int outRow, out int outCol, out int outZ) 
				{
					outCol = col;
					outZ = z;
					outRow = row - 1;
				} 
			},
			{MatrixDefinitions.CellDirections.UnderRight, delegate(int row, int col, int z, out int outRow, out int outCol, out int outZ) 
				{
					outZ = z;
					outCol = col + 1;
					outRow = row - 1;
				} 
			},
			{MatrixDefinitions.CellDirections.UnderLeft, delegate(int row, int col, int z, out int outRow, out int outCol, out int outZ) 
				{
					outZ = z;
					outCol = col - 1;
					outRow = row - 1;
				}
			},
			{MatrixDefinitions.CellDirections.Behind, delegate(int row, int col, int z, out int outRow, out int outCol, out int outZ) 
				{
					outRow = row;
					outCol = col;
					outZ = z + 1;
				}
			},
			{MatrixDefinitions.CellDirections.BehindRight, delegate(int row, int col, int z, out int outRow, out int outCol, out int outZ) 
				{
					outRow = row;
					outCol = col + 1;
					outZ = z + 1;
				} 
			},
			{MatrixDefinitions.CellDirections.BehindLeft, delegate(int row, int col, int z, out int outRow, out int outCol, out int outZ) 
				{
					outRow = row;
					outCol = col - 1;
					outZ = z + 1;
				} 
			},
			{MatrixDefinitions.CellDirections.Front, delegate(int row, int col, int z, out int outRow, out int outCol, out int outZ) 
				{
					outRow = row;
					outCol = col;
					outZ = z - 1;
				}
			},
			{MatrixDefinitions.CellDirections.FrontRight, delegate(int row, int col, int z, out int outRow, out int outCol, out int outZ) 
				{
					outRow = row;
					outCol = col + 1;
					outZ = z - 1;
				}
			},
			{MatrixDefinitions.CellDirections.FrontLeft, delegate(int row, int col, int z, out int outRow, out int outCol, out int outZ) 
				{
					outRow = row;
					outCol = col - 1;
					outZ = z - 1;
				}
			}
		};
        #endregion

        #region METHODS
        /// <summary>
        /// Gets the key for index.
        /// </summary>
        /// <returns>The key for index.</returns>
        /// <param name="col">Col.</param>
        /// <param name="z">Z.</param>
        public static string GetKeyForIndex(int col, int z)
        {
            return string.Format(MatrixDefinitions.KEY_BASE, col, z);
        }

		/// <summary>
		/// Gets the coordinates in direction.
		/// </summary>
		/// <param name="row">Row.</param>
		/// <param name="col">Col.</param>
		/// <param name="z">The z coordinate.</param>
		/// <param name="outRow">Out row.</param>
		/// <param name="outCol">Out col.</param>
		/// <param name="outZ">Out z.</param>
		public static void GetCoordinatesInDirection(this MatrixDefinitions.CellDirections cellDirection, int row, int col, int z, out int outRow, out int outCol, out int outZ)
		{
			MatrixDefinitions.GetCoordinatesInDirectionDelDict[cellDirection](row, col, z, out outRow, out outCol, out outZ);
		}
        #endregion
	}
}
