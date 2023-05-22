#using <mscorlib.dll>
#include <msclr/marshal.h>
#include "../ZLib/zlib.h"
#include <stdio.h>

using namespace System;
using namespace msclr::interop;

//
// C# to C wrapper class the uncompresses a file with zlib
//
public ref class UnCompressFile 
{
public:

    //
    // C++ CLI Wrapper for zlib uncompress method  
    //
    int Uncompress(array<System::Byte>^ compressedData, array<System::Byte>^ UncompressedData, int% uncompressLength)
    {
        unsigned char* compressedLocal = new unsigned char[1024 * 1024];
        unsigned char* unCompressedLocal = new unsigned char[1024 * 1024];

        // copy 
        for (int i = 0; i < compressedData->Length; i++)
        {
            compressedLocal[i] = compressedData[i];
        }

        uLongf uncomprLen = 1024 * 1024;
        int err = uncompress(unCompressedLocal, &uncomprLen, compressedLocal, compressedData->Length);

        if (err == 0)
        {
            for (uLongf i = 0; i < uncomprLen; i++)
            {
                UncompressedData[i] = unCompressedLocal[i];
            }
            uncompressLength = uncomprLen;
        }

        return err;
    }
};
