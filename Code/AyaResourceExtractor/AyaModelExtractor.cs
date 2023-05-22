
namespace AYAResourceExtractor
{
    // Extracts a model from an aya file and exports it as a .fbx along with it's textures.
    internal class AyaModelExtractor
    {
        readonly AyaModelImporter importer = new();
        readonly FbxModelExporter exporter = new();
        public void ExtractModel(string resourcesPath, string filename, string outputFolder, bool binaryFbx, bool asciiFbx)
        {
            Log.AddMessage("Extracting file '" + filename + "'");
            string modelFilename = Path.GetFileNameWithoutExtension(filename);

            byte[]? fileBytes = AyaFileUncompressor.Uncompress(filename);

            if (fileBytes == null || fileBytes.Length ==0)
            {
                return;
            }

            // This saves a temp copy of uncompressed binary aya model (useful for debug purposes)
            {
                // using var writer = new BinaryWriter(File.OpenWrite(@"..\..\..\..\..\temp.raw"));
                // writer.Write(fileBytes, 0, fileBytes.Length);
            }
      
            importer.Import(fileBytes);
            exporter.Export(importer.Model, outputFolder, modelFilename, resourcesPath, binaryFbx, asciiFbx);
            Log.AddMessage("Done.");
        }
    }
}
