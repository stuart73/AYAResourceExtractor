using Fbx;

namespace AYAResourceExtractor
{
    // Exports a AyaModel as a FBX file
    internal class FbxModelExporter
    {
        public void Export(AyaModel model, string outputFolder, string modelFilename, string resourcesPath, bool binaryFbx, bool asciiFbx)
        {
            // The fbx library just allows you to read/modify and then save.  So we need a template fbx model to start with.
            FbxDocument documentNode = FbxIO.ReadBinary(@"BoxWithTextures.fbx");

            // For help with debug
            //FbxIO.WriteAscii(documentNode, @"..\..\..\..\..\TemplateFBXFiles\BoxWithTextures_accii.fbx");

            FbxNode objectsNode = documentNode["Objects"];
            FbxNode geometryNode = objectsNode["Geometry"];

            List<AyaIndexType> newIndices = GenerateTriListIndicesFromTriStrip(model);

            double[] vertices = new double[model.AyaVertices.Count * 3];
            List<float> UV = new();
            List<int> UVIndex = new();

            int vv = 0;
            foreach (AyaVertex ver in model.AyaVertices)
            {
                vertices[vv++] = ver.position.x;
                vertices[vv++] = ver.position.y;
                vertices[vv++] = -ver.position.z;  
            }

            geometryNode["Vertices"].Value = vertices;

            List<int> outputIndices = new();
            foreach (AyaIndexType indi in newIndices)
            {
                outputIndices.Add(indi.index);
            }

            geometryNode["PolygonVertexIndex"].Value = outputIndices.ToArray();
            List<int> matrialList = new();

            List<double> normals = new();
            foreach (AyaIndexType indi in newIndices)
            {
                AyaIndexType touse = indi;
                if (indi.index < 0)     // e.g. -4 means end of polygon with index 3
                {
                    touse.index = -(indi.index + 1);
                    matrialList.Add(touse.texture);
                }
                AyaVertex vertex = model.AyaVertices[touse.index];

                UV.Add(vertex.U);
                UV.Add(-vertex.V);

                UVIndex.Add(UVIndex.Count);

                normals.Add(vertex.normal.x);
                normals.Add(vertex.normal.y);
                normals.Add(-vertex.normal.z);
            }

            geometryNode["LayerElementNormal"]["Normals"].Value = normals.ToArray();
            geometryNode["LayerElementUV"]["UV"].Value = UV.ToArray();
            geometryNode["LayerElementUV"]["UVIndex"].Value = UVIndex.ToArray();

            geometryNode["LayerElementMaterial"]["Materials"].Value = matrialList.ToArray();


            int maxTextures = 24; // NOTE: this needs to match with number of materials set in BoxWithTextures.fbx template
            int texNumber = 0;
            foreach (String texture in model.Textures)
            {
                if (texNumber >= maxTextures)
                {
                    Log.Error("material with more than 24 textures");
                    continue;
                }
                string path = outputFolder + @"\MeshTextures\";
                string replace = texture.Replace(".tga", ".png");

                replace = replace.Replace("meshtex\\", "");
                replace = replace.Replace(" ", "");

                FbxNode TextureNode = objectsNode.Nodes[2+ maxTextures + texNumber];
                FbxNode VideoNode = objectsNode.Nodes[2+ maxTextures+ maxTextures + texNumber];

                TextureNode["Media"].Value = "Video::" + replace;
                TextureNode["FileName"].Value = path + replace;
                TextureNode["RelativeFilename"].Value = path + replace;


                VideoNode.Properties[1] = "Video::" + replace;
                VideoNode["Filename"].Value = path + replace;
                VideoNode["RelativeFilename"].Value = path + replace;
                VideoNode["Properties70"]["P"].Properties[4] = path + replace;
                texNumber++;

                AyaTextureExtractor.ExtractorTexture(texture, resourcesPath, path);
            }

            if (asciiFbx == true)
            {
                FbxIO.WriteAscii(documentNode, outputFolder + @"\" + modelFilename + "_ascii.fbx");
            }
            if (binaryFbx == true)
            {
                FbxIO.WriteBinary(documentNode, outputFolder + @"\" + modelFilename + "_binary.fbx");
            }
        }

        // Converts a tri strip into a tri list
        private List<AyaIndexType> GenerateTriListIndicesFromTriStrip(AyaModel model)
        {
            List<AyaIndexType> newIndices = new();

            foreach (List<AyaIndexType> indylist in model.AyaIndices)
            {
                int i = 0;
                while (i < indylist.Count)
                {
                    AyaIndexType a, b, c;

                    if (i == 0)
                    {
                        a = indylist[0];
                        b = indylist[1];
                        c = indylist[2];
                        i += 3;
                    }
                    else
                    {
                        a = indylist[i - 1];
                        b = indylist[i - 2];
                        c = indylist[i];

                        if (i % 2 == 1)  // swap vertices order in every new point in strip ( i.e. to make them all clockwise or anti-clockwise)
                        {
                            (b, a) = (a, b);
                        }
                        i += 1;
                    }

                    // if 3 different indices then make a triangle out of it
                    if (a.index != b.index && a.index != c.index && b.index != c.index)
                    {
                        newIndices.Add(a);
                        newIndices.Add(b);
                        c.index = -(c.index + 1);
                        newIndices.Add(c);
                    }
                }
            }
            return newIndices;
        }
    }
}
