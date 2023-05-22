namespace AYAResourceExtractor
{
    internal class AyaFileUncompressor
    {
        public static byte[]? Uncompress(string filename)
        {
            // Aya files are in general compressed with zlib. However is seems BEA could only handle 1mb of uncompressed
            // data at a time, so additional to this there are possible multiple compressed parts to each aya file.
            // So for each compreseed part it starts with a 4 byte header containing the byte length of the following compressed
            // part.
            // Most aya files are < 1mb when uncompressed, so typically there is only 1 header and 1 compressed part.  Larger files
            // contain multiple e.g.

            // 4 byte header with length of compressed part
            // zlib compressed part
            // 4 byte header with length of compressed part
            // zlib compressed part
            // etc..
            // This class handles all of this. As memory is not a problem now, we simply want to uncompress an aya file into
            // one returned byte array.

            int error = 0;
            UnCompressFile uncompressor = new();

            byte[] compressedAya;

            try
            {
                compressedAya = File.ReadAllBytes(filename);
            }
            catch
            {
                Log.Error("Unable to find/read file '"+filename+"'");
                return null;
            }
            byte[] unCompressedAya = new byte[1024 * 1024 * 4];

            bool lastPart = false;
            int index = 0;
            int uncompressedIndex = 0;

            while (lastPart == false && error ==0)
            {
                if (compressedAya.Length < index+4)
                {
                    Log.Error("Uncompressing file '" + filename + "' corrupt file?");
                    break;
                }
                byte[] block = new byte[4];
                Array.Copy(compressedAya, index, block, 0, 4);
                uint size = BitConverter.ToUInt32(block);
                index += 4;
                if (compressedAya.Length == size + index)
                {
                    lastPart = true;
                }
                else if (compressedAya.Length < size + index)
                {
                    Log.Error("Uncompressing file '" + filename + "' header inconsistent with length. Corrupt file?");
                    break;
                }

                byte[] compressedPArt = new byte[size];
                byte[] unCompressPart = new byte[1024 * 1024];
                Array.Copy(compressedAya, index, compressedPArt, 0, size);
                index += (int)size;

                int uncompressLength = 0;
                error = uncompressor.Uncompress(compressedPArt, unCompressPart, ref uncompressLength);

                if (error == 0)
                {
                    Array.Copy(unCompressPart, 0, unCompressedAya, uncompressedIndex, uncompressLength);
                    uncompressedIndex += uncompressLength;
                }
                else
                {
                    Log.Error("Uncompressing file '" + filename + "' caused zlib error :" + error);
                }
            }

            byte[]? outUncompressedData = null;

            if (error ==0)
            {
                outUncompressedData = new byte[uncompressedIndex];
                Array.Copy(unCompressedAya, 0, outUncompressedData, 0, uncompressedIndex);  
            }

            return outUncompressedData;
        }
    }
}
