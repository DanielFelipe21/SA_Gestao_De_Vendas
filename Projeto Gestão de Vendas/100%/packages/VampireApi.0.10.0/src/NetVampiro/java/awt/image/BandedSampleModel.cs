using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.awt.image
{
    public sealed class BandedSampleModel : ComponentSampleModel
    {

        private static int[] createIndices(int numBands)
        {
            int[] indices = new int[numBands];
            for (int i = 0; i < numBands; i++)
            {
                indices[i] = i;
            }
            return indices;
        }

        private static int[] createOffsets(int numBands)
        {
            int[] offsets = new int[numBands];
            for (int i = 0; i < numBands; i++)
            {
                offsets[i] = 0;
            }
            return offsets;
        }

        public BandedSampleModel(int dataType, int w, int h, int numBands) :
            this(dataType, w, h, w, BandedSampleModel.createIndices(numBands),
                    BandedSampleModel.createOffsets(numBands))
        {
        }

        public BandedSampleModel(int dataType, int w, int h, int scanlineStride,
                int[] bankIndices, int[] bandOffsets) :
            base(dataType, w, h, 1, scanlineStride, bankIndices, bandOffsets)
        {
        }


        public override SampleModel createCompatibleSampleModel(int w, int h)
        {
            return new BandedSampleModel(dataType, w, h, w, bankIndices,
                    bandOffsets);
        }


        public override DataBuffer createDataBuffer()
        {
            DataBuffer data = null;
            int size = scanlineStride * height;

            switch (dataType)
            {
                case DataBuffer.TYPE_BYTE:
                    data = new DataBufferByte(size, numBanks);
                    break;
                case DataBuffer.TYPE_SHORT:
                case DataBuffer.TYPE_USHORT:
                    data = new DataBufferShort(size, numBanks);
                    break;
                case DataBuffer.TYPE_INT:
                    data = new DataBufferInt(size, numBanks);
                    break;
                case DataBuffer.TYPE_FLOAT:
                    data = new DataBufferFloat(size, numBanks);
                    break;
                case DataBuffer.TYPE_DOUBLE:
                    data = new DataBufferDouble(size, numBanks);
                    break;
            }

            return data;

        }


        public override SampleModel createSubsetSampleModel(int[] bands)
        {
            if (bands.Length > numBands)
            {
                // awt.64=The number of the bands in the subset is greater than the number of bands in the sample model
                throw new RasterFormatException("The number of the bands in the subset is greater than the number of bands in the sample model"); //$NON-NLS-1$
            }

            int[] indices = new int[bands.Length];
            int[] offsets = new int[bands.Length];

            for (int i = 0; i < bands.Length; i++)
            {
                indices[i] = bankIndices[bands[i]];
                offsets[i] = bandOffsets[bands[i]];
            }

            return new BandedSampleModel(dataType, width, height, scanlineStride,
                    indices, offsets);
        }


        public override Object getDataElements(int x, int y, Object obj, DataBuffer data)
        {
            switch (dataType)
            {
                case DataBuffer.TYPE_BYTE:
                    {
                        byte[] bdata;

                        if (obj == null)
                        {
                            bdata = new byte[numBands];
                        }
                        else
                        {
                            bdata = (byte[])obj;
                        }

                        for (int i = 0; i < numBands; i++)
                        {
                            bdata[i] = (byte)getSample(x, y, i, data);
                        }

                        obj = bdata;
                        break;
                    }
                case DataBuffer.TYPE_SHORT:
                case DataBuffer.TYPE_USHORT:
                    {
                        short[] sdata;

                        if (obj == null)
                        {
                            sdata = new short[numBands];
                        }
                        else
                        {
                            sdata = (short[])obj;
                        }

                        for (int i = 0; i < numBands; i++)
                        {
                            sdata[i] = (short)getSample(x, y, i, data);
                        }

                        obj = sdata;
                        break;
                    }
                case DataBuffer.TYPE_INT:
                    {
                        int[] idata;

                        if (obj == null)
                        {
                            idata = new int[numBands];
                        }
                        else
                        {
                            idata = (int[])obj;
                        }

                        for (int i = 0; i < numBands; i++)
                        {
                            idata[i] = getSample(x, y, i, data);
                        }

                        obj = idata;
                        break;
                    }
                case DataBuffer.TYPE_FLOAT:
                    {
                        float[] fdata;

                        if (obj == null)
                        {
                            fdata = new float[numBands];
                        }
                        else
                        {
                            fdata = (float[])obj;
                        }

                        for (int i = 0; i < numBands; i++)
                        {
                            fdata[i] = getSampleFloat(x, y, i, data);
                        }

                        obj = fdata;
                        break;
                    }
                case DataBuffer.TYPE_DOUBLE:
                    {
                        double[] ddata;

                        if (obj == null)
                        {
                            ddata = new double[numBands];
                        }
                        else
                        {
                            ddata = (double[])obj;
                        }

                        for (int i = 0; i < numBands; i++)
                        {
                            ddata[i] = getSampleDouble(x, y, i, data);
                        }

                        obj = ddata;
                        break;
                    }
            }

            return obj;
        }


        public override int[] getPixel(int x, int y, int[] iArray, DataBuffer data)
        {
            int[] pixel;
            if (iArray == null)
            {
                pixel = new int[numBands];
            }
            else
            {
                pixel = iArray;
            }

            for (int i = 0; i < numBands; i++)
            {
                pixel[i] = getSample(x, y, i, data);
            }

            return pixel;
        }


        public override int getSample(int x, int y, int b, DataBuffer data)
        {
            if (x < 0 || y < 0 || x >= this.width || y >= this.height)
            {
                // awt.63=Coordinates are not in bounds
                throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
            }

            return data.getElem(bankIndices[b], y * scanlineStride + x +
                   bandOffsets[b]);
        }


        public override double getSampleDouble(int x, int y, int b, DataBuffer data)
        {
            if (x < 0 || y < 0 || x >= this.width || y >= this.height)
            {
                // awt.63=Coordinates are not in bounds
                throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
            }

            return data.getElemDouble(bankIndices[b], y * scanlineStride + x +
                   bandOffsets[b]);
        }


        public override float getSampleFloat(int x, int y, int b, DataBuffer data)
        {
            if (x < 0 || y < 0 || x >= this.width || y >= this.height)
            {
                // awt.63=Coordinates are not in bounds
                throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
            }

            return data.getElemFloat(bankIndices[b], y * scanlineStride + x +
                   bandOffsets[b]);
        }


        public override int[] getSamples(int x, int y, int w, int h, int b, int[] iArray,
                DataBuffer data)
        {
            int[] samples;
            int idx = 0;

            if (iArray == null)
            {
                samples = new int[w * h];
            }
            else
            {
                samples = iArray;
            }

            for (int i = y; i < y + h; i++)
            {
                for (int j = x; j < x + w; j++)
                {
                    samples[idx++] = getSample(j, i, b, data);
                }
            }

            return samples;
        }


        public override int GetHashCode()
        {
            int hash = base.hashCode();
            int tmp = java.dotnet.lang.Operator.shiftRightUnsignet(hash, 8);
            hash <<= 8;
            hash |= tmp;

            return hash ^ 0x55;
        }


        public override void setDataElements(int x, int y, Object obj, DataBuffer data)
        {
            switch (dataType)
            {
                case DataBuffer.TYPE_BYTE:
                    byte[] bdata = (byte[])obj;
                    for (int i = 0; i < numBands; i++)
                    {
                        setSample(x, y, i, bdata[i] & 0xff, data);
                    }
                    break;

                case DataBuffer.TYPE_SHORT:
                case DataBuffer.TYPE_USHORT:
                    short[] sdata = (short[])obj;
                    for (int i = 0; i < numBands; i++)
                    {
                        setSample(x, y, i, sdata[i] & 0xffff, data);
                    }
                    break;

                case DataBuffer.TYPE_INT:
                    int[] idata = (int[])obj;
                    for (int i = 0; i < numBands; i++)
                    {
                        setSample(x, y, i, idata[i], data);
                    }
                    break;

                case DataBuffer.TYPE_FLOAT:
                    float[] fdata = (float[])obj;
                    for (int i = 0; i < numBands; i++)
                    {
                        setSample(x, y, i, fdata[i], data);
                    }
                    break;

                case DataBuffer.TYPE_DOUBLE:
                    double[] ddata = (double[])obj;
                    for (int i = 0; i < numBands; i++)
                    {
                        setSample(x, y, i, ddata[i], data);
                    }
                    break;
            }
        }


        public override void setPixel(int x, int y, int[] iArray, DataBuffer data)
        {
            for (int i = 0; i < numBands; i++)
            {
                setSample(x, y, i, iArray[i], data);
            }
        }


        public override void setPixels(int x, int y, int w, int h, int[] iArray,
                DataBuffer data)
        {
            int idx = 0;

            for (int i = y; i < y + h; i++)
            {
                for (int j = x; j < x + w; j++)
                {
                    for (int n = 0; n < numBands; n++)
                    {
                        setSample(j, i, n, iArray[idx++], data);
                    }
                }
            }
        }


        public override void setSample(int x, int y, int b, double s, DataBuffer data)
        {
            if (x < 0 || y < 0 || x >= this.width || y >= this.height)
            {
                // awt.63=Coordinates are not in bounds
                throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
            }

            data.setElemDouble(bankIndices[b], y * scanlineStride + x +
                   bandOffsets[b], s);
        }


        public override void setSample(int x, int y, int b, float s, DataBuffer data)
        {
            if (x < 0 || y < 0 || x >= this.width || y >= this.height)
            {
                // awt.63=Coordinates are not in bounds
                throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
            }

            data.setElemFloat(bankIndices[b], y * scanlineStride + x +
                   bandOffsets[b], s);
        }


        public override void setSample(int x, int y, int b, int s, DataBuffer data)
        {
            if (x < 0 || y < 0 || x >= this.width || y >= this.height)
            {
                // awt.63=Coordinates are not in bounds
                throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
            }

            data.setElem(bankIndices[b], y * scanlineStride + x +
                           bandOffsets[b], s);
        }


        public override void setSamples(int x, int y, int w, int h, int b, int[] iArray,
                DataBuffer data)
        {
            int idx = 0;

            for (int i = y; i < y + h; i++)
            {
                for (int j = x; j < x + w; j++)
                {
                    setSample(j, i, b, iArray[idx++], data);
                }
            }

        }
    }
}