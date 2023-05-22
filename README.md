# AYAResourceExtractor
Battle Engine Aquila resource extractor tool

This Windows app can be used to extract game models from the PC version of Battle Engine Aquila and will convert them into .fbx files that can be read in Windows default ‘3D viewer’ or apps like Blender.

To use.  Run and select the ‘Battle engine Aquila’ game folder and optionally change the output folder.  Then either extract an individual .aya file (found in data\resources\meshes)  OR select ‘Extract all’.  

Output fbx files can be ascii and/or binary.  Ascii files work in Windows default ‘3D Viewer’.  Binary ones work with Blender.

All the model textures can be found in the output folder under the folder name ‘MeshTextures’.

The app was coded with Visual Studio community 2022 and the solution file (.sln) can be found in Code\AyaResourceExtractor\AYAResourceExtractor.sln

The PC version of Battle Engine Aquila I own (and tested with) I believe came out in 2003 and came as bundled software with a Nvidia graphics card.  I’ve not tested this with the Steam version of BEA.  It is assumed the .Aya files will be the same!.   This app won’t work with Xbox/PS2 versions.

The converter was written in C# with Windows forms, however this app also contains C,C++,C# projects from:

-Zlib from Mark Adler (this is needed as .aya files were compressed with this)

-DSSReader from Juho Peltonen (This is needed as the textures were stored as compressed DXT2)

-FBX from hamish-milne. (This is uses to read/modify/write) fbx files.

Currently this extractor tool only extracts models from the .aya files and does not work 100% on every model.

Limitations include:-

-Some normals on model primitives seem to be wrong (point in wrong direction).  However most are correct.

-No multitexture blending for single primitive (Maybe needed in a few rare places).

-A model is extracted as a ‘static’ single fbx object.  Not as different object parts.

-Animations (including bones) do not get extracted.



