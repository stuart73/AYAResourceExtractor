
namespace AYAResourceExtractor
{
    public struct AyaVector
    {
        public float x;
        public float y;
        public float z;
        public float w;
    }

    public struct AyaVertex
    {
        public AyaVector position;
        public AyaVector normal;
        public float U;
        public float V;
        public uint colour;
    }

    public struct AyaIndexType
    {
        public int index;
        public int texture;
    }

    public class AyaMatrix
    {
        public AyaVector xAxis;
        public AyaVector yAxis;
        public AyaVector zAxis;

        public AyaVector MultiplyWithVector(AyaVector vector)
        {
            AyaVector returnVector;
            returnVector.x = (xAxis.x * vector.x) + (xAxis.y * vector.y) + (xAxis.z * vector.z);
            returnVector.y = (yAxis.x * vector.x) + (yAxis.y * vector.y) + (yAxis.z * vector.z);
            returnVector.z = (zAxis.x * vector.x) + (zAxis.y * vector.y) + (zAxis.z * vector.z);
            returnVector.w = 0;

            return returnVector;
        }
    }

    // Really rough representation of an aya model
    internal class AyaModel
    {
        public List<AyaVertex> AyaVertices { get; set; } = new(); 
        
        // Indices treated as a tri strip
        public List<List<AyaIndexType>> AyaIndices { get; set; } = new();
        public List<string> Textures { get; set; } = new();
    }
}
