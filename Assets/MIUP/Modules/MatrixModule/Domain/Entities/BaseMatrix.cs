using System.Collections.Generic;

namespace MIUP.GameName.MatrixModule.Domain 
{
    /// <summary>
    /// Matrix.
    /// </summary>
    public class BaseMatrix<T>
	{
        #region INSTANCE VARIABLES
        /// <summary>
		/// The matrix. Each array member represents a row in the matrix.
		/// Each row has a dictionary of cells, where the key is the stringified pair (col, z), and the value is the cell.
        /// </summary>
		private Dictionary<string, MatrixCell<T>>[] matrix;

		/// <summary>
		/// Flag to check if the matrix is using gravity.
		/// </summary>
		private bool isUsingGravity;
        #endregion

        #region PROPERTIES
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public int Height { get; private set; }

        /// <summary>
        /// Gets or sets the cols.
        /// </summary>
        /// <value>The cols.</value>
        public int Width { get; private set; }

        /// <summary>
        /// Gets or sets the depth.
        /// </summary>
        /// <value>The depth.</value>
        public int Depth { get; private set; }

		/// <summary>
		/// Gets the blocked directions.
		/// </summary>
		/// <value>The blocked directions.</value>
		public MatrixDefinitions.CellDirections[] BlockedDirections { get; private set; }

		/// <summary>
		/// Gets the unavailable directions.
		/// </summary>
		/// <value>The unavailable directions.</value>
		protected MatrixDefinitions.CellDirections[] UnavailableDirections { get; private set; }
		#endregion

        #region PUBLIC METHODS
		//TODO: ADD MORE CONSTRUCTORS.
        /// <summary>
        /// Initializes a new instance of the <see cref="MIUP.GameName.MatrixModule.Domain.BaseMatrix`1"/> class.
        /// </summary>
        /// <param name="height">Height.</param>
        /// <param name="width">Width.</param>
        /// <param name="depth">Depth.</param>
		public BaseMatrix(int height, int width, int depth, bool isUsingGravity, MatrixDefinitions.CellDirections[] blockedDirections)
        {
			this.BlockedDirections = blockedDirections;
			this.UnavailableDirections = new MatrixDefinitions.CellDirections[] {MatrixDefinitions.CellDirections.Same};

			this.isUsingGravity = isUsingGravity;

            this.Height = height;
            this.Width = width;
            this.Depth = depth;

            matrix = new Dictionary<string, MatrixCell<T>>[this.Height];

            for(int i = 0; i < this.Height; i++)
            {
				matrix[i] = new Dictionary<string, MatrixCell<T>>();
            }
        }

        /// <summary>
        /// Adds the cell at row, col and z.
        /// </summary>
        /// <param name="cell">Cell.</param>
        /// <param name="row">Row.</param>
        /// <param name="col">Col.</param>
        /// <param name="z">Z.</param>
        public void AddCellAt(T cell, int row, int col, int z = 0)
        {
            this.CheckDimension(row, col, z);
            if(this.CanAddCellAt(row, col, z))
            {
				matrix[row][MatrixDefinitions.GetKeyForIndex(col, z)] = new MatrixCell<T>(cell);

				//Check in all adjasent cells if we have to block something.
				this.ApplyOnAdjasentCells(row, col, z, this.SetCellBlockedState);
            }
        }

        /// <summary>
        /// Removes the cell at row, col and z.
        /// </summary>
        /// <returns>The removed cell.</returns>
        /// <param name="row">Row.</param>
        /// <param name="col">Col.</param>
        /// <param name="z">The z coordinate.</param>
        public T RemoveCellAt(int row, int col, int z = 0)
        {
            this.CheckDimension(row, col, z);
            string key = MatrixDefinitions.GetKeyForIndex(col, z);

			MatrixCell<T> removedCell = matrix[row][key];
            matrix[row].Remove(key);

			if(this.isUsingGravity)
			{
				//This method should check if we have to unblock something.
				this.CheckGravityAt(row, col, z);
			}
			else
			{
				//Check in all adjasent cells if we have to unblock something.
				this.ApplyOnAdjasentCells(row, col, z, this.SetCellBlockedState);
			}

			return removedCell.Content;
        }

        /// <summary>
        /// Determines whether this instance has cell at the specified row col z.
        /// </summary>
        /// <returns><c>true</c> if this instance has cell at the specified row col z; otherwise, <c>false</c>.</returns>
        /// <param name="row">Row.</param>
        /// <param name="col">Col.</param>
        /// <param name="z">The z coordinate.</param>
        public bool HasCellAt(int row, int col, int z = 0)
        {
			if(!this.IsValidIndex(row, col, z))
			{
				return false;
			}
            string key = MatrixDefinitions.GetKeyForIndex(col, z);
            return matrix[row].ContainsKey(key);
        }

        /// <summary>
        /// Tries to get cell at row, col and z.
        /// </summary>
        /// <returns><c>true</c> if this instance has cell at the specified row col z; otherwise, <c>false</c>.</returns>
        /// <param name="cell">Output Cell.</param>
        /// <param name="row">Row.</param>
        /// <param name="col">Col.</param>
        /// <param name="z">The z coordinate.</param>
        public bool TryGetCellAt(out T cell, int row, int col, int z = 0)
        {
			cell = default(T);
			MatrixCell<T> matrixCell;

			if(this.TryGetMatrixCellAt(out matrixCell, row, col, z))
			{
				cell = matrixCell.Content;
				return true;
			}

			return false;
        }

		/// <summary>
		/// Determines whether the cell at row, col and z, is blocked.
		/// </summary>
		/// <returns><c>true</c> if the cell at row, col and z is blocked; otherwise, <c>false</c>.</returns>
		/// <param name="row">Row.</param>
		/// <param name="col">Col.</param>
		/// <param name="z">Z.</param>
		public bool IsCellBlocked(int row, int col, int z = 0)
		{
			MatrixCell<T> matrixCell;

			if(this.TryGetMatrixCellAt(out matrixCell, row, col, z))
			{
				return matrixCell.IsBlocked;
			}

			return false;
		}
        #endregion

        #region PRIVATE METHODS
		/// <summary>
		/// Tries to get matrx cell at row, col and z.
		/// </summary>
		/// <returns><c>true</c> if this instance has matrix cell at the specified row col z; otherwise, <c>false</c>.</returns>
		/// <param name="matrixCell">Output Matrix Cell.</param>
		/// <param name="row">Row.</param>
		/// <param name="col">Col.</param>
		/// <param name="z">The z coordinate.</param>
		protected bool TryGetMatrixCellAt(out MatrixCell<T> matrixCell, int row, int col, int z = 0)
		{
			matrixCell = null;

			if(!this.IsValidIndex(row, col, z))
			{
				return false;
			}

			string key = MatrixDefinitions.GetKeyForIndex(col, z);
			return matrix[row].TryGetValue(key, out matrixCell);
		}

        /// <summary>
        /// Determines whether the index specified by row col z, is valid.
        /// </summary>
        /// <returns><c>true</c> if the index specified by row col z, is valid; otherwise, <c>false</c>.</returns>
        /// <param name="row">Row.</param>
        /// <param name="col">Col.</param>
        /// <param name="z">The z coordinate.</param>
        protected bool IsValidIndex(int row, int col, int z)
        {
            return (row >= 0 && row < this.Height && col >= 0 && col < this.Width && z >= 0 && z < this.Depth);
        }

        /// <summary>
        /// Checks the dimension.
        /// </summary>
        /// <param name="row">Row.</param>
        /// <param name="col">Col.</param>
        /// <param name="z">Z.</param>
        protected void CheckDimension(int row, int col, int z)
        {
            if(!this.IsValidIndex(row, col, z))
            {
                throw new System.ArgumentOutOfRangeException();
            }
        }

		/// <summary>
		/// Apply an action to the cell at row, col and z, and all adjasent cells.
		/// </summary>
		/// <param name="row">Row.</param>
		/// <param name="col">Col.</param>
		/// <param name="z">The z coordinate.</param>
		/// <param name="action">The Action.</param>
		protected void ApplyOnAdjasentCells(int row, int col, int z, System.Action<int, int, int> action)
		{
			//Check in all directions.
			this.ApplyOnDirectionCell(row, col, z, MatrixDefinitions.CellDirections.Same, action);
			this.ApplyOnDirectionCell(row, col, z, MatrixDefinitions.CellDirections.Right, action);
			this.ApplyOnDirectionCell(row, col, z, MatrixDefinitions.CellDirections.Left, action);
			this.ApplyOnDirectionCell(row, col, z, MatrixDefinitions.CellDirections.Top, action);
			this.ApplyOnDirectionCell(row, col, z, MatrixDefinitions.CellDirections.TopRight, action);
			this.ApplyOnDirectionCell(row, col, z, MatrixDefinitions.CellDirections.TopLeft, action);
			this.ApplyOnDirectionCell(row, col, z, MatrixDefinitions.CellDirections.Under, action);
			this.ApplyOnDirectionCell(row, col, z, MatrixDefinitions.CellDirections.UnderRight, action);
			this.ApplyOnDirectionCell(row, col, z, MatrixDefinitions.CellDirections.UnderLeft, action);
			this.ApplyOnDirectionCell(row, col, z, MatrixDefinitions.CellDirections.Behind, action);
			this.ApplyOnDirectionCell(row, col, z, MatrixDefinitions.CellDirections.BehindRight, action);
			this.ApplyOnDirectionCell(row, col, z, MatrixDefinitions.CellDirections.BehindLeft, action);
			this.ApplyOnDirectionCell(row, col, z, MatrixDefinitions.CellDirections.Front, action);
			this.ApplyOnDirectionCell(row, col, z, MatrixDefinitions.CellDirections.FrontRight, action);
			this.ApplyOnDirectionCell(row, col, z, MatrixDefinitions.CellDirections.FrontLeft, action);
		}

		/// <summary>
		/// Apply an action to the cell next to direction.
		/// </summary>
		/// <param name="row">Row.</param>
		/// <param name="col">Col.</param>
		/// <param name="z">The z coordinate.</param>
		/// <param name="cellDirections">Cell directions.</param>
		/// <param name="action">The Action.</param>
		protected void ApplyOnDirectionCell(int row, int col, int z, MatrixDefinitions.CellDirections cellDirection, System.Action<int, int, int> action)
		{
			int outRow, outCol, outZ;
			cellDirection.GetCoordinatesInDirection(row, col, z, out outRow, out outCol, out outZ);
			action(outRow, outCol, outZ);
		}

		/// <summary>
		/// Determines whether this instance has any cell at the specified row col z and cellDirections.
		/// </summary>
		/// <returns><c>true</c> if this instance has any cell at the specified row col z and cellDirections; otherwise, <c>false</c>.</returns>
		/// <param name="row">Row.</param>
		/// <param name="col">Col.</param>
		/// <param name="z">The z coordinate.</param>
		/// <param name="cellDirections">Cell directions.</param>
		protected bool HasCellInDirections(int row, int col, int z, params MatrixDefinitions.CellDirections[] cellDirections)
		{
			int outRow, outCol, outZ;

			for (int i = 0; i < cellDirections.Length; i++)
			{
				cellDirections[i].GetCoordinatesInDirection(row, col, z, out outRow, out outCol, out outZ);
				if(this.HasCellAt(outRow, outCol, outZ))
				{
					return true;	
				}
			}

			return false;
		}

		/// <summary>
		/// Determines whether this instance can add cell at the specified row, col and z.
		/// </summary>
		/// <returns><c>true</c> if this instance can add cell at the specified row, col and z; otherwise, <c>false</c>.</returns>
		/// <param name="row">Row.</param>
		/// <param name="col">Col.</param>
		/// <param name="z">The z coordinate.</param>
		private bool CanAddCellAt(int row, int col, int z = 0)
		{
			return !this.HasCellInDirections(row, col, z, this.UnavailableDirections);
		}

		/// <summary>
		/// Shoulds the cell at row, col and z, be blocked.
		/// </summary>
		/// <returns><c>true</c>, if cell at row, col and z, should be blocked, <c>false</c> otherwise.</returns>
		/// <param name="row">Row.</param>
		/// <param name="col">Col.</param>
		/// <param name="z">The z coordinate.</param>
		private bool ShouldCellBeBlocked(int row, int col, int z = 0)
		{
			return this.HasCellInDirections(row, col, z, this.BlockedDirections);
		}

		/// <summary>
		/// Sets the cell blocked state.
		/// </summary>
		/// <param name="row">Row.</param>
		/// <param name="col">Col.</param>
		/// <param name="z">The z coordinate.</param>
		private void SetCellBlockedState(int row, int col, int z)
		{
			this.SetCellBlockedState(row, col, z, this.ShouldCellBeBlocked(row, col, z));
		}

		/// <summary>
		/// Sets the cell blocked state.
		/// </summary>
		/// <param name="row">Row.</param>
		/// <param name="col">Col.</param>
		/// <param name="z">The z coordinate.</param>
		/// <param name="isBlocked">If cell is blocked.</param>
		private void SetCellBlockedState(int row, int col, int z, bool isBlocked)
		{
			MatrixCell<T> matrixCell;

			if(this.TryGetMatrixCellAt(out matrixCell, row, col, z))
			{
				matrixCell.SetBlocked(isBlocked);
			}
		}
			
		//TODO: IMPLEMENTING GRAVITY
		/// <summary>
		/// Checks the gravity at row, col and z.
		/// </summary>
		/// <param name="row">Row.</param>
		/// <param name="col">Col.</param>
		/// <param name="z">The z coordinate.</param>
		public void CheckGravityAt(int row, int col, int z = 0)
		{
			//Going up checking if we have cells to move down.
			for(int i = row + 1; i < this.Height; i++)
			{
				//If we have a cell in the top, let's move it down.
				if(this.HasCellAt(i, col, z))
				{
					MoveCellDown(i, col, z);
				}

				//If we have some cell in the top left or top right, then just finish. They are blocking all the other cells in the top of them.
				if(col != 0 && this.HasCellAt(i, col - 1, z))
				{
					break;
				}

				if(col != this.Width && this.HasCellAt(i, col + 1, z))
				{
					break;
				}
			}
		}

		/// <summary>
		/// Moves the cell at row, col and z, down.
		/// </summary>
		/// <param name="row">Row.</param>
		/// <param name="col">Col.</param>
		/// <param name="z">The z coordinate.</param>
		private void MoveCellDown(int row, int col, int z)
		{
			if(row > 0)
			{
				//FIXME: Infinite loop
				T cell = this.RemoveCellAt(row, col, z);
				this.AddCellAt(cell, row - 1, col, z);
			}
		}
        #endregion
	}
}