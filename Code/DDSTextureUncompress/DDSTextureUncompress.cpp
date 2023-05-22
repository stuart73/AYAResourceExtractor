#using <mscorlib.dll>

#include "DDSreader.h"

using namespace System;

//
// CLR C++ wrapper class to uncompress a DDS texture
//
public ref class DDSTextureUncompress
{
private:

public:

    int Uncompress(array<Byte>^ input, int length, array<Byte>^ output, int% width, int% height)
    {

        std::vector<uint8_t> newdata;

        for (int i = 0; i < length; i++)
        {
            newdata.push_back((uint8_t) input[i]);
        }
       
        Image* image = read_dds(newdata);

        for (int i = 0; i < image->height * image->height * 4; i++)
        {
            output[i] = image->data[i];
        }

        width = image->width;
        height = image->height;

        return 1;
    }

};
