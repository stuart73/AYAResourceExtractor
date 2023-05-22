namespace AYAResourceExtractor
{
    internal class AyaTextureExtractor
    { 
        public static void ExtractorTexture(string name, string resourceFolder, string outputFolder)
        {
            byte[] textureDDSUnCompressed = new byte[1024 * 1024 * 4];

            bool folderExists = Directory.Exists(outputFolder);
            if (!folderExists)
            {
                Directory.CreateDirectory(outputFolder);
            }     

            int length = 0;

            int folderIndex = name.IndexOf('\\');
            if (folderIndex >= 0)
            {
                name = name.Substring(folderIndex + 1);
            }

            string filename = resourceFolder+ @"\dxtntextures\meshtex%" + name + "(0)A1R5G5B5.aya";

            if (File.Exists(filename) == false)
            {
                filename = resourceFolder + @"\dxtntextures\meshtex%" + name + "(0)A8R8G8B8.aya";
            }

            string outputName = Path.GetFileNameWithoutExtension(name);
            outputName = outputName.Replace(" ", "");

            string outputPathAndName = outputFolder + outputName + ".png";

            if (File.Exists(outputPathAndName) == true)
            {
                // Log ("Texture name "+ already exists, ignorning
                return;
            }

            // Uncompress as aya zlib
            byte[]? textureZLibUnCompressed = AyaFileUncompressor.Uncompress(filename);  

            if (textureZLibUnCompressed==null)
            {
                return;
            }
            length = textureZLibUnCompressed.Length;

            // Then uncompress as DDS
            DDSTextureUncompress textureUncompressor = new();

            int width = 0;
            int height = 0;
            textureUncompressor.Uncompress(textureZLibUnCompressed, length, textureDDSUnCompressed, ref width, ref height);

            // Generate as .net bitmap
            Bitmap bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            int fbindex = 0;
            for (int v = 0; v < height; v++)
            {
                for (int h = 0; h < width; h++)
                {
                    byte b = textureDDSUnCompressed[fbindex++];
                    byte g = textureDDSUnCompressed[fbindex++];
                    byte r = textureDDSUnCompressed[fbindex++];
                    byte a = textureDDSUnCompressed[fbindex++];

                    Color c = Color.FromArgb(a, r, g, b);
                    bitmap.SetPixel(h, v, c);
                }
            }

            // Now export as png
            bitmap.Save(outputPathAndName, System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
