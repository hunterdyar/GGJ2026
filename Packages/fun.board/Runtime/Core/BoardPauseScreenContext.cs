// <copyright file="BoardPauseScreenContext.cs" company="Harris Hill Products Inc.">
//     Copyright (c) Harris Hill Products Inc. All rights reserved.
// </copyright>

namespace Board.Core
{
    using System;
    using System.Runtime.InteropServices;
    
    using UnityEngine;

    /// <summary>
    /// Encapsulates all the context for Board's pause screen.
    /// </summary>
    [Serializable]
    public struct BoardPauseScreenContext
    {
        // Note: gameId is not exposed - it's automatically set to BoardSettings.appId internally
        
        /// <value>The display name of the application</value>
        public string applicationName;

        /// <value><c>true</c> if the option to save should be shown when exiting the application; otherwise, <c>false</c>.</value>
        public bool showSaveOptionUponExit;

        /// <value>A collection of custom buttons to display.</value>
        public BoardPauseCustomButton[] customButtons;

        /// <value>A collection of audio tracks to display.</value>
        public BoardPauseAudioTrack[] audioTracks;
    }

    /// <summary>
    /// Specifies the icon type for a button in the Board pause screen.
    /// </summary>
    public enum BoardPauseButtonIcon
    {
        None,
        CircularArrow,
        Square,
        LeftArrow,
        DoorWithArrow
    }

    /// <summary>
    /// Represents a custom button for Board's pause screen.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct BoardPauseCustomButton
    {
        /// <value>The unique identifier for the button.</value>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string id;

        /// <value>The button text.</value>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string text;

        /// <value>The button icon's name.</value>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        internal string iconName;

        /// <summary>
        /// Initializes a new instance of <see cref="BoardPauseCustomButton"/> with the specified identifier, title, and icon.
        /// </summary>
        /// <param name="id">A unique identifier (max 64 characters).</param>
        /// <param name="text">The button text (max 128 characters).</param>
        /// <param name="icon">The button icon.</param>
        public BoardPauseCustomButton(string id, string text, BoardPauseButtonIcon icon = BoardPauseButtonIcon.None)
        {
            this.id = id;
            this.text = text;
            this.iconName = icon switch
            {
                BoardPauseButtonIcon.None => null,
                BoardPauseButtonIcon.CircularArrow => "CIRCULAR_ARROW",
                BoardPauseButtonIcon.Square => "SQUARE",
                BoardPauseButtonIcon.LeftArrow => "LEFT_ARROW",
                BoardPauseButtonIcon.DoorWithArrow => "DOOR_WITH_ARROW",
                _ => null
            };
        }
    }

    /// <summary>
    /// Represents an audio track to display in Board's pause screen.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct BoardPauseAudioTrack
    {
        /// <value>The unique identifier for the audio track.</value>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string id;

        /// <value>The display name of the audio track.</value>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string name;

        /// <value>The value of the audio track from 0 to 100.</value>
        public int value; // 0-100
    }

    /// <summary>
    /// Specifies the action type for a button in the Board pause screen.
    /// </summary>
    public enum BoardPauseAction
    {
        None = 0,
        Resume = 1,
        ExitGameSaved = 2,
        ExitGameUnsaved = 3,
        CustomButton = 4
    }

    /// <summary>
    /// Represents the result from a Board pause menu interaction
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct BoardPauseScreenResult
    {
        /// <value>The <see cref="BoardPauseAction">type</see> of action performed by the user.</value>
        public BoardPauseAction actionType;

        /// <value>The identifier for the custom button tapped by the user if any.</value>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string customButtonId;

        /// <value>The new state of all <see cref="BoardPauseAudioTrack">audio tracks</see>.</value>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public BoardPauseAudioTrack[] audioTracks;

        /// <value>The number of audio tracks</value>
        public int audioTrackCount;
    }
}
