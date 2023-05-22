
namespace AYAResourceExtractor
{
    // This class imports an AyaModel from raw binary (bytes)
    internal class AyaModelImporter
    {
        public AyaModel Model { get; private set; } = new();

        public uint[] partVBBookmark = new uint[1000];

        public void Import(byte[] fileBytes)
        {
            uint index = 8;  // starts 8 bytes in

            Model = new();

            index += 4;
            uint numTextures = ReadUint(fileBytes, ref index);

            index += 7 * 4;

            string meshName = ReadString(fileBytes, ref index, 300);

            Log.AddMessage("Mesh name =" + meshName);
            Log.AddMessage("Number textures =" + numTextures);

            index += 12;
            uint numParts = ReadUint(fileBytes, ref index);
            Log.AddMessage("Number parts =" + numParts);

            index += 4 * 5;

            ReadTextures(fileBytes, ref index, numTextures);
            
            for (uint i = 0; i < numParts; i++) 
            {
                ReadModelPart(fileBytes, ref index, i);
            }

            Log.AddMessage("Number of vertices :" + Model.AyaVertices.Count);
        }

        private string ReadString(byte[] fileBytes, ref uint index, uint length)
        {
            byte[] name = new byte[length];
            Array.Copy(fileBytes, index, name, 0, length);
            string readString = System.Text.Encoding.UTF8.GetString(name).TrimEnd('\0');
            index += length;
            return readString;
        }

        private uint ReadUint(byte[] fileBytes, ref uint index)
        {
            byte[] block = new byte[4];
            Array.Copy(fileBytes, index, block, 0, 4);
            uint size = BitConverter.ToUInt32(block);
            index += 4;
            return size;
        }

        private ushort ReadUshort(byte[] fileBytes, ref uint index)
        {
            byte[] block = new byte[2];
            Array.Copy(fileBytes, index, block, 0, 2);
            ushort amount = BitConverter.ToUInt16(block);
            index += 2;
            return amount;
        }

        private float ReadFloat(byte[] bytes, ref uint index)
        {
            float amount = BitConverter.ToSingle(bytes, (int)index);
            index += 4;
            return amount;
        }

        private AyaVector ReadVector(byte[] bytes, ref uint index)
        {
            AyaVector vector;
            vector.x = ReadFloat(bytes, ref index);
            vector.y = ReadFloat(bytes, ref index);
            vector.z = ReadFloat(bytes, ref index);
            vector.w = ReadFloat(bytes, ref index);
            return vector;
        }

        private AyaVector ReadVector3(byte[] bytes, ref uint index)
        {
            AyaVector vector;
            vector.x = ReadFloat(bytes, ref index);
            vector.y = ReadFloat(bytes, ref index);
            vector.z = ReadFloat(bytes, ref index);
            vector.w = 0;
            return vector;
        }

        private AyaMatrix ReadMatrix(byte[] fileBytes, ref uint index)
        {
            AyaMatrix orientation = new();
            orientation.xAxis = ReadVector(fileBytes, ref index);
            orientation.yAxis = ReadVector(fileBytes, ref index);
            orientation.zAxis = ReadVector(fileBytes, ref index);
            return orientation;
        }

        private AyaVertex ReadVertex(byte[] bytes, uint vertexchunksize)
        {
            uint index = 0;
            AyaVertex vertex;
            vertex.position = ReadVector3(bytes, ref index);   // 12

            if (vertexchunksize == 48)
            {
                // not sure what this is.  Bone weighting or something maybe....
                float f = ReadFloat(bytes, ref index);
                f = ReadFloat(bytes, ref index);
                f = ReadFloat(bytes, ref index);
            }

            vertex.normal = ReadVector3(bytes, ref index);    //12 
            vertex.colour = ReadUint(bytes, ref index);    //4  
            vertex.U = ReadFloat(bytes, ref index);// 4
            vertex.V = ReadFloat(bytes, ref index);//4 
            return vertex;
        }

        string ReadTag(byte[] fileBytes, ref uint index)
        {
            byte[] tag = new byte[4];
            Array.Copy(fileBytes, index, tag, 0, 4);
            string tagName = System.Text.Encoding.UTF8.GetString(tag).TrimEnd('\0');
            index += 8; // 4 byte tag name  + 4 bytes length
            return tagName;
        }

        private void ReadTextures(byte[] fileBytes, ref uint index, uint numTextures)
        {
            string tagName = ReadTag(fileBytes, ref index);

            if (tagName != "CMST")
            {
                Log.Error("Expecting CMST");
            }

            // Skip
            index += numTextures * 36;

            for (uint i = 0; i < numTextures; i++)
            {
                ReadTexture(fileBytes, ref index);
            }
        }

        private void ReadTexture(byte[] fileBytes, ref uint index)
        {
            string tagName = ReadTag(fileBytes, ref index);

            if (tagName != "MSHT")
            {
                Log.Error("Expecting MSHT");
            }

            tagName = ReadTag(fileBytes, ref index);

            if (tagName != "TEXB")
            {
                Log.Error("Expecting TEXB");
            }

            index += 20;  // read texture used region and scale. Used ever on meshes?

            string textureName = ReadString(fileBytes, ref index, 128);

            Model.Textures.Add(textureName);

            Log.AddMessage("Texture name = " + textureName);

        }

        private void LogVector(string name, AyaVector vector)
        {
            string message = name +":"+ vector.x +"," + vector.y + "," + vector.z;
            Log.AddMessage(message);
        }

        private void ReadModelPart(byte[] fileBytes, ref uint index, uint partNumber)
        {
            string tagName = ReadTag(fileBytes, ref index);

            if (tagName != "MESP")
            {
                Log.Error("Expecting MESP but got " + tagName);
            }

            tagName = ReadTag(fileBytes, ref index);

            if (tagName != "CMSP")
            {
                Log.Error("Expecting CMSP but got " + tagName);
            }

            AyaMatrix currentOrientation = ReadMatrix(fileBytes, ref index);
            AyaMatrix baseOrientation = ReadMatrix(fileBytes, ref index);
            AyaVector offsetPosition = ReadVector(fileBytes, ref index);
            AyaVector basePosition = ReadVector(fileBytes, ref index);

            /*
            Log.AddMessage("Part " + partNumber);
            LogVector("c ori x", currentOrientation.xAxis);
            LogVector("c ori y", currentOrientation.yAxis);
            LogVector("c ori z", currentOrientation.zAxis);
            LogVector("b ori x", baseOrientation.xAxis);
            LogVector("b ori y", baseOrientation.yAxis);
            LogVector("b ori z", baseOrientation.zAxis);
            LogVector("c offset", offsetPosition);
            LogVector("b offset", basePosition);
            */

            index += 8; // pointers

            uint partNum = ReadUint(fileBytes, ref index);
            uint partType = ReadUint(fileBytes, ref index);
            uint numChildren = ReadUint(fileBytes, ref index);
            //Log.AddMessage("part num:" + partNum + ", type:" + partType + ", children:" + numChildren);

            index += 4 * 5;

            uint numDVert = ReadUint(fileBytes, ref index);
            uint numPVert = ReadUint(fileBytes, ref index);
            uint numTris = ReadUint(fileBytes, ref index);
            uint numAFrames = ReadUint(fileBytes, ref index);
            uint numVFrames = ReadUint(fileBytes, ref index);
            uint numHFrames = ReadUint(fileBytes, ref index);
            uint numBones = ReadUint(fileBytes, ref index);

            index += 4 * 6; // pointer stuff

            string partName = ReadString(fileBytes, ref index, 32);

            index += 16 * 4;  // pointers and more

            tagName = ReadTag(fileBytes, ref index);

            if (tagName == "CHLD")
            {
                // read child part numbers
                index += 4 * numChildren;
                tagName = ReadTag(fileBytes, ref index);
            }

            if (tagName == "PRNT")
            {
                // parent part number
                index += 4;
                tagName = ReadTag(fileBytes, ref index);
            }

            if (tagName == "NMIC")
            {
                // next part in chain
                index += 4;
                tagName = ReadTag(fileBytes, ref index);
            }

            if (tagName == "BBOX")
            {
                // bug? BBOX tag stored twice!
                tagName = ReadTag(fileBytes, ref index);
                if (tagName != "BBOX")
                {
                    Log.Error("Expecting BBOX but got " + tagName);
                }

                index += 4 * 4; // vector origin
                index += 4 * 4; // vector axis
                index += 4; // valid
                index += 4; // radius

                tagName = ReadTag(fileBytes, ref index);
            }

            if (tagName == "VHFM")
            {
                index += numVFrames;
                tagName = ReadTag(fileBytes, ref index);

            }

            if (tagName == "HORI")
            {
                index += numHFrames * 4 * 4 * 3;
                tagName = ReadTag(fileBytes, ref index);
            }

            if (tagName == "HPOS")
            {
                index += numHFrames * 4 * 4;
                tagName = ReadTag(fileBytes, ref index);
            }

            if (tagName == "HFOV")
            {
                index += numHFrames * 4;
                tagName = ReadTag(fileBytes, ref index);
            }

            if (tagName == "BONE") 
            {
                index += numBones * 4;
                tagName = ReadTag(fileBytes, ref index);
            }

            if (tagName == "BONW")
            {
                index += numPVert * 4 * numBones;
                tagName = ReadTag(fileBytes, ref index);
            }

            if (tagName == "BONS")
            {
                index += numPVert * 4 * numBones * 3;
                tagName = ReadTag(fileBytes, ref index);
            }

            if (tagName == "PBKT") 
            {
                // we don't need this skip over
                index -= 4;
                uint size = ReadUint(fileBytes, ref index);
                index += size;
                tagName = ReadTag(fileBytes, ref index);
            }

            if (tagName == "CPOS") 
            {
                // we don't need this skip over
                index -= 4;
                uint size = ReadUint(fileBytes, ref index);
                index += size;

                tagName = ReadTag(fileBytes, ref index);
            }

            if (tagName == "CORI") 
            {
                // we don't need this skip over
                index -= 4;
                uint size = ReadUint(fileBytes, ref index);
                index += size;

                tagName = ReadTag(fileBytes, ref index);
            }

            if (tagName == "REFR")
            {       
                // Part referencing
                uint refpart = ReadUint(fileBytes, ref index);
             
                // We need to copy vertices/indices from a different part but using a different pos/ori.
                // This is used, for example, on guncrab mesh with repeating 'same' legs. I assume this was to save memory
                // at the time.
                if (refpart > partNum)
                {
                    Log.Error("Have not imported part " + refpart + " yet, so can't use it as reference");
                }
                else
                {
                    uint tempIndex = partVBBookmark[refpart];
                    ReadVertexBuffer(fileBytes, ref tempIndex, basePosition, baseOrientation, true);
                }
                tagName = ReadTag(fileBytes, ref index);
            }

            bool include = true;
            if (partNum ==4)
            {
                include = true;
            }

            if (tagName == "PMVB")
            {
                // Keep a bookmark of VB part incase we have a reference "REFR" to it later.
                partVBBookmark[partNum] = index;

                ReadVertexBuffer(fileBytes, ref index, basePosition, baseOrientation, include);

                tagName = ReadTag(fileBytes, ref index);

            }

            if (tagName == "MESP" || tagName == "BBOX" || tagName == "CCUS" || tagName == "CAMD")
            {
                index -= 8;
                return;
            }

            Log.Error("Unexected tag " + tagName);
            return ;
        }

      

        private void ReadVertexBuffer(byte[] fileBytes, ref uint index, AyaVector basePosition, AyaMatrix orientation, bool include)
        {
            string tagName = ReadTag(fileBytes, ref index);
            if (tagName != "CMVB")
            {
                Log.Error("Expecting CMVB");
            }

            index += 8; // pointers

            index += 4 * 64; // pointers

            int numTextures = fileBytes[index];
            index += 4;

            index += 8; //pointers

            uint vertexBufferChunkSize = ReadUint(fileBytes, ref index);
            uint FVFFlags = ReadUint(fileBytes, ref index);  
            uint pritiveType = ReadUint(fileBytes, ref index);

            index += 8; //????

            int ioffset = Model.AyaVertices.Count;

            for (int materialIndex = 0; materialIndex < numTextures; materialIndex++)
            {
                tagName = ReadTag(fileBytes, ref index);
                if (tagName != "MMPT")
                {
                    Log.Error("Expecting MMPT");
                }

                uint vBSize = ReadUint(fileBytes, ref index);
                uint iBSize = ReadUint(fileBytes, ref index);
                uint numIndices = ReadUint(fileBytes, ref index);
                uint numVertices = ReadUint(fileBytes, ref index);
                uint numPrimitives = ReadUint(fileBytes, ref index);
                uint active = ReadUint(fileBytes, ref index);

                tagName = ReadTag(fileBytes, ref index);
                if (tagName != "IBUF")
                {
                    Log.Error("Expecting IBUF");
                }

                List<AyaIndexType> newIndices = new();
                for (int ii = 0; ii < numIndices; ii++)
                {
                    ushort indice = ReadUshort(fileBytes, ref index);
                    AyaIndexType newIndi;
                    newIndi.texture = 0;
                    newIndi.index = (int)indice + ioffset;

                    newIndices.Add(newIndi);
                }

                tagName = ReadTag(fileBytes, ref index);
                if (tagName != "VBUF")
                {
                    Log.Error("Expecting VBUF");
                }

                if (materialIndex == 0)
                {
                    for (int vv = 0; vv < numVertices; vv++)
                    {
                        byte[] vertchunk = new byte[vertexBufferChunkSize];
                        Array.Copy(fileBytes, index, vertchunk, 0, (int)vertexBufferChunkSize);
                        AyaVertex vertex = ReadVertex(vertchunk, vertexBufferChunkSize);
                        vertex.position = orientation.MultiplyWithVector(vertex.position);
                        vertex.normal = orientation.MultiplyWithVector(vertex.normal);
                        vertex.position.x += basePosition.x;
                        vertex.position.y += basePosition.y;
                        vertex.position.z += basePosition.z ;
                        Model.AyaVertices.Add(vertex);
                        index += vertexBufferChunkSize;
                    }
                }
                else
                {
                    index -= 4;
                    uint skip = ReadUint(fileBytes, ref index);
                    index += skip;
                }

                tagName = ReadTag(fileBytes, ref index);
                if (tagName != "TEXR")
                {
                    Log.Error("Expecting TEXR");
                }
                uint[] ttId = new uint[6];

                for (int tt = 0; tt < 6; tt++)
                {
                    ttId[tt] = ReadUint(fileBytes, ref index);
                }

                // Add texture
                for (int ddd = 0; ddd < newIndices.Count; ddd++)
                {
                    AyaIndexType indi = newIndices[ddd]; 
                    indi.texture = (int)ttId[0];    // no multi texture support (is this needed ?)
                    newIndices[ddd] = indi;
                }

                if (include == true)
                {
                    Model.AyaIndices.Add(newIndices);
                }
            }
        }
    }
}
