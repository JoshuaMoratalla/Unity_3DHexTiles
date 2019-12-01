# Unity_3DHexTiles
A bunch of files used within Unity to help create a 3D based tile games. Mainly made to develop a 3D game project I am working on.

Currently only has hexagonal type tile-based files as I am going for a hexagonal game design.

Consists of:
  HexTile_Set
  Hex_SelectionManager
  Tile_Values
  Coordinate
  
  HexTile_Set is a file for initializing a hexagonal based tile set aswell as creating shapes and managing the tiles altogether.
  
  Hex Selection Manager is Mainly based on the mouse interacting with the tileset, like selecting clicked tiles based on the raycast of the camera view and etc.
  
  Tile values are a class for the tile that hold its coordinates and other values, such as its default color value should it change material and need to revert. More works needs to be done on this file however it should sufffice for the meantime.
  
  Coordinate is made just to make it easier for the tileset by holding two variables as one instead of isolating and storing both the x and the y as individual componenents.
