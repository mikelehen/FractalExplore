
#include "stdafx.h"
#include <iostream.h>
#include <math.h>
#include <string.h>
#include <stdlib.h>
#include <stdio.h>

extern "C" {
#include "jpeglib.h"
}

unsigned char *Buffer;
const double PI = 3.1415926535897932;

const long ITERATIONS = 5000000;
const double a=-2.511160, b=-1.0181, c=1.100450, d=1.448990;
//const double a=-1.46722, b=-2.37496, c=1.009920,d=0.995120;
//const double a=-1.695357,b=-2.30932, c=.80836, d=.88782;
//const double a=-2.26115, b=-2.92089, c=1.404, d=1.44902;
//const double a=-1.82466, b=2.99543, c=.61728, d=.97884;

void CalcFractal(int nSrcX, int nSrcY, long iterations, double a, double b, double c, double d);
void WriteFractal(char *strFileName, int nSrcX, int nSrcY, int nDstX, int nDstY);
void MakeFractal(char *szOutputFile, int srcX, int srcY, int dstX, int dstY, long iterations, double a, double b, double c, double d);

int main(void)
{
	int dstX = 640, dstY = 480;
	int srcX = 3560, srcY = 1920;
	MakeFractal("image.jpg", srcX, srcY, dstX, dstY, ITERATIONS, a, b, c, d);
	return 0;
}

void MakeFractal(char *strOutputFile, int srcX, int srcY, int dstX, int dstY, long iterations, double a, double b, double c, double d)
{
	Buffer = (unsigned char*)malloc(srcX * srcY * sizeof(unsigned char));
	memset(Buffer,0,srcX * srcY & sizeof(unsigned char));
	CalcFractal(srcX, srcY, iterations, a, b, c, d);
	WriteFractal(strOutputFile, srcX, srcY, dstX, dstY);
}


void CalcFractal(int nSrcX, int nSrcY, long iterations, double a, double b, double c, double d)
{
	double x=.1, y=.1;
	double newx,newy;
	long index;
	for(index=0;index<iterations;index++)
	{
		newx = sin(y*b) + c * sin(x*b);
		newy = sin(x*a) + d * sin(y*a);
		x = newx;
		y = newy;
		newx = (int)(nSrcX/2 + newx * (nSrcX/(2*c+2)));
		newy = (int)(nSrcY/2 + newy * (nSrcY/(2*d+2)));
		Buffer[(int)newy*nSrcX+(int)newx] = 1;
	}
}

void WriteFractal(char *strFileName, int nSrcX, int nSrcY, int nDstX, int nDstY)
{
	struct jpeg_compress_struct cinfo;
	struct jpeg_error_mgr jerr;
	FILE *outfile;
	JSAMPROW row_pointer[1];	/* pointer to a single row */
	JSAMPLE *scanline;

	scanline = (JSAMPLE *)malloc(nDstX * 3 * sizeof(JSAMPLE));
	
	cinfo.err = jpeg_std_error(&jerr);
	jpeg_create_compress(&cinfo);
	if ((outfile = fopen(strFileName, "wb")) == NULL)
	{
		fprintf(stderr,"Error: could not open file %s\n", strFileName);
		exit(1);
	}
	jpeg_stdio_dest(&cinfo, outfile);
	cinfo.image_width = nDstX;
	cinfo.image_height = nDstY;
	cinfo.input_components = 3;
	cinfo.in_color_space = JCS_RGB;
	jpeg_set_defaults(&cinfo);
	jpeg_start_compress(&cinfo, TRUE);

	int dstx,dsty=0,x,y;
	double deltax = ((double)nSrcX / nDstX);
	double deltay = ((double)nSrcY / nDstY);
	int hits, hitsposs;
	while (cinfo.next_scanline < cinfo.image_height)
	{
		memset(scanline, 0, nDstX * 3 * sizeof(JSAMPLE));
		for(dstx=0;dstx<nDstX;dstx++)
		{
			hits = hitsposs = 0;
			for(x=(int)(dstx*deltax);x<(int)((dstx+1)*deltax);x++)
			{
				for(y=(int)(dsty*deltay);y<(int)((dsty+1)*deltay);y++)
				{
					hitsposs++;
					if (Buffer[y*nSrcX+x] == 1)
					{
						hits++;
					}
				}
			}
			scanline[dstx*3 + 0] = (255 * hits)/hitsposs;
			scanline[dstx*3 + 1] = (255 * hits)/hitsposs;
			scanline[dstx*3 + 2] = (255 * hits)/hitsposs;
		}
		row_pointer[0] = scanline;
		jpeg_write_scanlines(&cinfo, row_pointer, 1);
		dsty++;
	}
	jpeg_finish_compress(&cinfo);
	jpeg_destroy_compress(&cinfo);							    	    					
	free(scanline);
}
