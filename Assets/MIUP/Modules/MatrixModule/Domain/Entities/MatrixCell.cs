using System.Collections.Generic;

namespace MIUP.GameName.MatrixModule.Domain 
{
    /// <summary>
    /// Matrix cell.
    /// </summary>
    public class MatrixCell<T>
	{
        #region INSTANCE VARIABLES
        #endregion

        #region PROPERTIES
		/// <summary>
		/// Gets a value indicating whether this instance is blocked.
		/// </summary>
		/// <value><c>true</c> if this instance is blocked; otherwise, <c>false</c>.</value>
		public bool IsBlocked {get; private set;}

		/// <summary>
		/// Gets the content.
		/// </summary>
		/// <value>The content.</value>
		public T Content {get; private set;}
        #endregion

		#region PUBLIC METHODS
		/// <summary>
		/// Initializes a new instance of the <see cref="MIUP.GameName.MatrixModule.Domain.MatrixCell`1"/> class.
		/// </summary>
		/// <param name="content">Content.</param>
		public MatrixCell(T content)
		{
			//Lets start unblocked.
			this.IsBlocked = false;
			this.Content = content;
		}

		/// <summary>
		/// Sets at blocked.
		/// </summary>
		/// <param name="isBlocked">If blocked.</param>
		public void SetBlocked(bool isBlocked)
		{
			this.IsBlocked = isBlocked;
		}
        #endregion
	}
}