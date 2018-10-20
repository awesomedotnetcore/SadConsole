﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Maps;

namespace BasicTutorial.Maps.TileBlueprints
{
    /// <summary>
    /// The blueprint for a door tile.
    /// </summary>
    public class Door : Tile.TileBlueprint
    {
        public bool StartClosed { get; set; } = true;

        public Door() : base("door")
        {
            Appearance = new Cell(Color.SaddleBrown, Color.Black, '-');
            Type = TileTypes.Door;
            Title = "Door";
            Description = "An old wooden door.";

            OnTileStateChanged = (tile, oldState) =>
            {
                if (tile.TileState == (int)TileStates.Door.Opened)
                {
                    tile.ChangeGlyph('\'');
                    tile.Flags = Helpers.UnsetFlag(tile.Flags, (int)(TileFlags.BlockLOS | TileFlags.BlockMove));
                }
                else if (tile.TileState == (int)TileStates.Door.Closed)
                {
                    tile.ChangeGlyph('+');
                    tile.Flags = Helpers.SetFlag(tile.Flags, (int)(TileFlags.BlockLOS | TileFlags.BlockMove));
                }
            };
        }

        public override Tile Create()
        {
            var tile = base.Create();

            if (StartClosed)
                tile.TileState = (int)TileStates.Door.Closed;
            else
                tile.TileState = (int)TileStates.Door.Opened;

            return tile;
        }
    }
}
