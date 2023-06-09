/*
 * DDS reader
 *
 * author: Juho Peltonen
 * license: GPL3, see license.txt
 */

#include <vector>
 /**
  * Structure representing image data
  *
  * data - raw byte data of the image
  * width and height - image dimensions
  * bpp - bits per pixel
  */
struct Image {
	std::vector<uint8_t> data;
	int width, height, bpp;
};


Image* read_dds(const std::vector<uint8_t>& data);

