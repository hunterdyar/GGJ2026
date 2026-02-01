// <copyright file="BoardSaveGamePlayer.cs" company="Harris Hill Products Inc.">
//     Copyright (c) Harris Hill Products Inc. All rights reserved.
// </copyright>

namespace Board.Save
{
    using Board.Core;

    /// <summary>
    /// Represents player display information for save games.
    /// </summary>
    /// <seealso cref="BoardSaveGamePlayerData"/>
    /// <seealso cref="BoardSaveGameManager"/>
    public sealed class BoardSaveGamePlayer : BoardPlayer
    {
        private const string kLogTag = nameof(BoardSaveGamePlayer);
        
        /// <inheritdoc/>
        protected override string logTag => kLogTag;
        
        /// <summary>
        /// Gets the player's persistent application-specific identifier.
        /// </summary>
        /// <remarks>
        /// This identifier is consistent for the same profile playing the same application.
        /// </remarks>
        public override string playerId {get; protected set;}

        /// <summary>
        /// Initializes a new instance of the <see cref="BoardSaveGamePlayer"/> class with the specified <see cref="BoardSaveGamePlayerData"/>.
        /// </summary>
        /// <param name="data"><see cref="BoardSaveGamePlayerData"/> supplied by the Board native API.</param>
        internal BoardSaveGamePlayer(ref BoardSaveGamePlayerData data) : base(data.nameString,
            data.avatarIdString,
            data.playerIdString.StartsWith("guest_") ? BoardPlayerType.Guest : BoardPlayerType.Profile)
        {
            this.playerId = data.playerIdString;
        }

        // <inheritdoc/>
        public override string ToString()
        {
            return $"{{playerId={playerId} name={name} avatarId={avatarId}}}";
        }
    }
}
