// <copyright file="BoardPlayerType.cs" company="Harris Hill Products Inc.">
//     Copyright (c) Harris Hill Products Inc. All rights reserved.
// </copyright>

namespace Board.Core
{
    /// <summary>
    /// Specifies the type of a player on Board.
    /// </summary>
    public enum BoardPlayerType
    {
        /// <summary>
        /// A human player that has a profile on this device.
        /// </summary>
        Profile,
        
        /// <summary>
        /// A human player that does not have a profile on this device.
        /// </summary>
        /// <remarks>
        /// Guest players are ephemeral. They do not persist beyond a single game session
        /// nor are they accessible outside of the app they were created in.
        /// </remarks>
        Guest,
    }
}